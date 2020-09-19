using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GradientCreator : MonoBehaviour
{
    [SerializeField] public int dimensions = 512;
    [SerializeField] int previewHeight = 100;
    [SerializeField] public Texture2D horizontalGradient;
    [SerializeField] public Texture2D verticalGradient;
    [SerializeField] public Texture2D noiseGradient;
    [Range(0.01f, 0.2f)] [Header("Don't edit at runtime")]
    [SerializeField] float noiseScale = 33333f;

    public bool generated = false;

    private void Awake() {
        GlobalVars.worldGradient = this;
    }

    public void GenerateGradients()
    {
        int seed = GlobalVars.seed;
        horizontalGradient = CreateGradient(seed, dimensions, previewHeight);
        verticalGradient = CreateGradient(seed * 2, dimensions, previewHeight);
        noiseGradient = GenerateNoise(seed, dimensions);
        generated = true;
    }

    private Texture2D GenerateNoise(int seed, int wH)
    {
        Texture2D noiseTex = new Texture2D(wH, wH, TextureFormat.ARGB32, false);

        for (int w = 0; w < wH; w++)
        {
            for (int h = 0; h < wH; h++)
            {
                noiseTex.SetPixel(w, h, ValueToColor(Mathf.PerlinNoise((float)seed + (float)w * noiseScale, (float)seed + (float)h * noiseScale)));
            }
        }
        noiseTex.Apply();
        return noiseTex;
    }
    
    private Texture2D CreateGradient(int seed, int width, int height)
    {
        Texture2D gradientTex = new Texture2D(width, height, TextureFormat.ARGB32, false);

        for(int w = 0; w < width; w++)
        {
            for (int h = 0; h < height; h++)
            {
                gradientTex.SetPixel(w, h, ValueToColor(Mathf.PerlinNoise((float)seed + (float)w * noiseScale, (float)seed + (float)w * noiseScale)));
            }
        }
        gradientTex.Apply();
        return gradientTex;
    }

    private Color ValueToColor(float value)
    {
        Color toReturn = new Color(value, value, value, 1);
        return toReturn;
    }

    public float GetHorizontalValueAtPixel(int horizontalIndex)
    {
        if(!generated ){return 0;}
        horizontalIndex = horizontalIndex % dimensions;
        float val = horizontalGradient.GetPixel(horizontalIndex, 0).r;
        return val;
    }

    public float GetVerticalValueAtPixel(int verticalIndex)
    {
        if (!generated) { return 0; }
        verticalIndex = verticalIndex % dimensions;
        float val = verticalGradient.GetPixel(verticalIndex, 0).r;
        return val;
    }

    public float GetNoiseAtPixel(int x, int y)
    {
        x = (int)Mathf.Repeat((float)x,dimensions);
        y = (int)Mathf.Repeat((float)y,dimensions);
        return noiseGradient.GetPixel(x,y).r;
    }
}
