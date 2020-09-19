using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkedNode : MonoBehaviour
{
    public LinkedNode previous {get; set;}
    public LinkedNode next {get; set;}

    [SerializeField] GameObject meshNode;
    [SerializeField] MeshRenderer actualMesh;

    private int indexInChain = 0;
    private bool triggered = false;
    private int childrenDensity = 3;
    private static int maxForwardDelta = 5;
    public static KitScriptableObject currentKit;
    public KitScriptableObject localKit;
    private static float rotationalChaos = 100.35f;
    public int samplRate;
    private int localDifficulty;

    private void Awake() 
    {
        indexInChain = GlobalVars.seededSpawner.currentNodeIndex;
        samplRate = GlobalVars.SampleRate;
        localDifficulty = GlobalVars.difficultyManager.currentDifficultyMod;
        childrenDensity += localDifficulty;
        localKit = currentKit;
        StartCoroutine(SpawnChildrenCoroutine());

        //float mapTest = ReedsUtils.Remap(.3f,0,1,-maxForwardDelta,maxForwardDelta);
        //Debug.Log("Map test says " + mapTest);
    }

    public void SetMaterial(ref Material mat)
    {
        actualMesh.material = mat;
    }

    private IEnumerator SpawnChildrenCoroutine()
    {   
        float sampleGradientPoint = GlobalVars.worldGradient.GetHorizontalValueAtPixel((int)(indexInChain * rotationalChaos));
        ;
        //float sampleGradientPointB = GlobalVars.worldGradient.GetVerticalValueAtPixel(indexInChain);
        int generations = (int)(sampleGradientPoint * childrenDensity);

        System.Random r = new System.Random(indexInChain);
        for(int i = 0; i < generations; i++)
        {   
            
            int offset = i + indexInChain;
            offset = i * (int)transform.position.magnitude;
            
            //Debug.Log(indexInChain);
            //offset = (int)(GlobalVars.worldGradient.GetVerticalValueAtPixel(offset) * 100);

            float forwardPos = 0;
            forwardPos = GlobalVars.worldGradient.GetHorizontalValueAtPixel(offset * samplRate);
            forwardPos = r.Next(100);
            //Debug.Log($"Sample point at {forwardPos} with sample pos at {offset * samplRate}");
            //Debug.Log($"Raw pos is {forwardPos}");
            forwardPos = ReedsUtils.Remap(forwardPos, 0 , 100 , -maxForwardDelta, maxForwardDelta);
            //Debug.Log($"mapped pos is {forwardPos}");

            float zRotation = GlobalVars.worldGradient.GetHorizontalValueAtPixel(offset * samplRate);
            //zRotation = GlobalVars.worldGradient.GetNoiseAtPixel(offset * samplRate * 33333, offset * samplRate * 33333);
            //Debug.Log($"Raw zrot is {zRotation}");
            zRotation = r.Next(100);
            zRotation = ReedsUtils.Remap(zRotation, 0, 100, 0 , 720);
            //Debug.Log($"mapped zrot is {zRotation}");

            //float kitOffset = GlobalVars.worldGradient.GetNoiseAtPixel(offset * samplRate, offset);
            //float kitOffset = GlobalVars.worldGradient.GetHorizontalValueAtPixel(offset * samplRate);
            int kitOffset = r.Next(0, localKit.kitObjects.Count);
            //int kitIndex = (int)ReedsUtils.Remap(kitOffset, 0, 100, 0, currentKit.kitObjects.Count - .1f);
            GameObject kitPiece = Instantiate(localKit.kitObjects[kitOffset], transform.GetChild(0), false);

            kitPiece.transform.localEulerAngles = new Vector3(0,0,zRotation);
            kitPiece.transform.localPosition = new Vector3(0,0,forwardPos);

            //Debug.Log(kitOffset);

            yield return null;
        }
    }

    private IEnumerator TestCoroutine()
    {
        yield return new WaitForSeconds(1f);
        GlobalVars.seededSpawner.SpawnInNextNode();
        yield return null;
    }

    public void SetMeshRot(Quaternion inRot)
    {
        if(meshNode!=null)
        {
            meshNode.transform.rotation = inRot;
        }
    }
    //MESH ROT SHOULD BE PURE LOOKROT

    private void OnTriggerEnter(Collider other) {

        if(other.CompareTag("Player") && !triggered)
        {
            GlobalVars.seededSpawner.SpawnInNextNode();
            GlobalVars.scoreTracker.ModScore(1);
            GlobalVars.playerKitComparator.UpdateCurrentKitProgression(localKit);
            triggered = true;
        }
    }

    

    
}
