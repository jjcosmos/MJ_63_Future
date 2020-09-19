using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeededSpawner : MonoBehaviour
{
    public GradientCreator seededGradient;
    public int currentNodeIndex = 0;
    public int nodeLimit;
    public int recycleThreshold = 5;
    public float spacing = 1;
    public float amplitudeDelta = 2f;
    public float bendFactor = .5f; //used for walking, not for display rot
    public List<LinkedNode> activeNodes;
    public LinkedNode template;
    public List<KitScriptableObject> availibleKits;
    

    private void Awake() 
    {
        GlobalVars.seededSpawner = this;
        seededGradient.GenerateGradients();
        nodeLimit = seededGradient.dimensions;

        if(availibleKits.Count > 0)
        {
            LinkedNode.currentKit = availibleKits[0];
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
        if(currentNodeIndex >= nodeLimit){return;}

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
            Debug.Log("Recycling");
            GameObject toDestroy = activeNodes[0].gameObject;
            activeNodes.RemoveAt(0);
            GameObject.Destroy(toDestroy);
        }
    }

}
