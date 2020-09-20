using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Cinemachine;

public class SeededSpawner : MonoBehaviour
{
    public GradientCreator seededGradient;
    public Transform player;
    public CinemachineVirtualCamera playercam;
    public CinemachineBrain brain;
    public FxContainer FXGroup;
    public int currentNodeIndex = 0;
    public int nodeLimit;
    public int recycleThreshold = 5;
    public float spacing = 1;
    public float amplitudeDelta = 2f;
    public float bendFactor = .5f; //used for walking, not for display rot
    public List<LinkedNode> activeNodes;
    public LinkedNode template;
    public List<KitScriptableObject> availibleKits;
    public int L1Threshold = 20;
    public int L2Threshold = 50;
    public int L3Threshold = 100;

    private int currentKitIndex = 0;
    private int currentLoop = 0;
    private string L1ThresholdString;
    private string L2ThresholdString;
    private string L3ThresholdString;
    private Camera mainCam;

    private void Awake() 
    {
        SetWorldType();
        mainCam = Camera.main;
        GlobalVars.seededSpawner = this;
        seededGradient.GenerateGradients();
        nodeLimit = seededGradient.dimensions;
        currentKitIndex = 0;
        //GlobalVars.playerKitComparator.currentKit = availibleKits[currentKitIndex];
        currentLoop = 0;
        if(availibleKits.Count > currentKitIndex)
        {
            LinkedNode.currentKit = availibleKits[currentKitIndex];
        }
        MakeThresholdSubstrings();
    }

    private void SetWorldType()
    {
        int worldType = PlayerPrefs.GetInt("worldtype",0);
        switch(worldType)
        {
            case(0):
                amplitudeDelta *= 2f;
                break;
            case(1):
                amplitudeDelta = 0f;
                break;
            case(2):
                amplitudeDelta *= 4;
                break;
        }

    }

    private void Start() 
    {   
        activeNodes = new List<LinkedNode>();
        // Pregenerate
        for(int i = 0; i < recycleThreshold - 3; i++)
            SpawnInNextNode(); 
    }

    //called by passing through a node's collider (or automatically in a coroutine to test)
    public void SpawnInNextNode()
    {
        //if(currentNodeIndex >= nodeLimit){return;}

        //Calculate the offset from the last transform
        Transform lastTransform = (currentNodeIndex < 1) ? this.transform : activeNodes[activeNodes.Count-1].transform;

        float spawnOffsetH = seededGradient.GetHorizontalValueAtPixel(currentNodeIndex);
        float spawnOffsetV = seededGradient.GetVerticalValueAtPixel(currentNodeIndex);
        spawnOffsetH = ReedsUtils.Remap(spawnOffsetH, 0,1,-amplitudeDelta, amplitudeDelta);
        spawnOffsetV = ReedsUtils.Remap(spawnOffsetV, 0,1,-amplitudeDelta, amplitudeDelta);

        Vector3 totalOffsetFromLast = lastTransform.position + lastTransform.right * spawnOffsetH + lastTransform.up * spawnOffsetV;
        totalOffsetFromLast += lastTransform.forward * spacing;
        
        //Instantiate new node
        LinkedNode newNode = Instantiate(template, totalOffsetFromLast, Quaternion.identity);
        Quaternion lookatRot = Quaternion.LookRotation(newNode.transform.position - lastTransform.position, Vector3.up);
        newNode.transform.rotation = Quaternion.Slerp(lastTransform.rotation, lookatRot, bendFactor);
        newNode.SetMeshRot(lookatRot);
        newNode.SetMaterial(ref availibleKits[currentKitIndex].material);

        //Append to activenodes, and set links
        if(activeNodes.Count > 0) // else this is the first node
        {
            activeNodes[activeNodes.Count-1].previous = newNode;
            newNode.previous = activeNodes[activeNodes.Count-1];
        }
        activeNodes.Add(newNode);
        currentNodeIndex ++;


        //if trackedNodesList len >= recyclethreshold
        //recycle oldest node(at index 0) 
        if(activeNodes.Count >= recycleThreshold)
        {
            //Debug.Log("Recycling");
            GameObject toDestroy = activeNodes[0].gameObject;
            activeNodes.RemoveAt(0);
            GameObject.Destroy(toDestroy);
        }

        CheckForNewZone();
        CheckPositionOffset(lastTransform);
    }

    private void CheckPositionOffset(Transform lastTransform)
    {
        float threshold = 4000;
        if(Mathf.Abs(lastTransform.position.x) > threshold || Mathf.Abs(lastTransform.position.y) > threshold || Mathf.Abs(lastTransform.position.z) > threshold)
        {
            Debug.Log("Resetting to world orgin");
            foreach(LinkedNode node in activeNodes)
            {
                Vector3 offsetFromPlayer =node.transform.position - player.position ;
                node.transform.position = offsetFromPlayer;
            }
            FXGroup.ClearChildren();
            Vector3 posDelta = player.position * -1;
            Vector3 cameraOffsetFromPlayer = player.position - playercam.transform.position ;
            player.transform.position = Vector3.zero;
            playercam.OnTargetObjectWarped(player, posDelta);
            FXGroup.ClearChildren();
            
        }
    }

    private IEnumerator ReenableAfterAFrame()
    {
        yield return null;
        playercam.enabled = true;
        brain.enabled = true;
    }
    
    private void MakeThresholdSubstrings()
    {
        L1ThresholdString = L1Threshold.ToString();
        L2ThresholdString = L2Threshold.ToString();
        L3ThresholdString = L3Threshold.ToString();
    }

    private void CheckForNewZone()
    {
        //need the last x digits to be == to the threshold
        //NOT used to do achievements or title text. Used for spawning only
        //L1 threshold is the transition between zone 0 and zone 1
        //L2 threshold is the transition between zone 1 and zone 2
        //L3 threshold is the transition between zone 2 and zone 0
        //string myNumString = currentNodeIndex.ToString();
        //int count = myNumString.Length;
        int loopedValue = currentNodeIndex - currentLoop * L3Threshold;

        //Debug.Log("Current node at " + loopedValue);

        if(loopedValue == L1Threshold)
        {
           //Debug.Log("Crossed L1 threshold to zone 1");
           currentKitIndex = 1;
           mainCam.backgroundColor = availibleKits[currentKitIndex].skyboxColor;
           LinkedNode.currentKit = availibleKits[currentKitIndex];
        }
        else if (loopedValue == L2Threshold)
        {
            //Debug.Log("Crossed L2 threshold to zone 2");
            currentKitIndex = 2;
            mainCam.backgroundColor = availibleKits[currentKitIndex].skyboxColor;
            LinkedNode.currentKit = availibleKits[currentKitIndex];
        }
        else if (loopedValue == L3Threshold)
        {
            //Debug.Log("Crossed L3 threshold to zone 0");
            currentKitIndex = 0;
            currentLoop++;
            mainCam.backgroundColor = availibleKits[currentKitIndex].skyboxColor;
            LinkedNode.currentKit = availibleKits[currentKitIndex];
        }
    }

}
