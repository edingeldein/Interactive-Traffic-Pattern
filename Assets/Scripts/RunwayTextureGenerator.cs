using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Renderer))]
public class RunwayTextureGenerator : MonoBehaviour
{
    public int PixelsPerUnit = 100;

    public int dashWidth = 5;
    public int dashNum = 20;

    public int thresholdHeight = 10;

    Texture2D currentTexture;

    void OnValidate()
    {
        var scaleHeight = gameObject.GetComponent<Transform>().localScale.z;
        var scaleWidth = gameObject.GetComponent<Transform>().localScale.x;
        var height = Mathf.FloorToInt(scaleHeight * PixelsPerUnit);
        var width = Mathf.FloorToInt(scaleWidth * PixelsPerUnit);

        currentTexture = new Texture2D(width, height);
        currentTexture.filterMode = FilterMode.Point;
        GetComponent<Renderer>().sharedMaterial.mainTexture = currentTexture;

        DrawTexture();

        currentTexture.Apply();
    }

    private void DrawTexture()
    {
        var textMid = currentTexture.width / 2;
        var lineLeft = textMid - Mathf.FloorToInt(dashWidth / 2f);
        var lineRight = textMid + Mathf.CeilToInt(dashWidth / 2f);

        var dashHeight = (currentTexture.height - (thresholdHeight * 2)) / (dashNum*2);        

        // Color entire texture gray
        for(int y = 0; y < currentTexture.height; y++)
        {
            for (int x = 0; x < currentTexture.width; x++)
                currentTexture.SetPixel(x, y, Color.gray);
        }

        // draw center line
        bool drawLine = false;
        for (int y = thresholdHeight; y < currentTexture.height - thresholdHeight; y++)
        {
            if ((y - thresholdHeight) % dashHeight == 0) drawLine = !drawLine;
            if (!drawLine) continue;

            for (int x = lineLeft; x <= lineRight; x++)
            {
                currentTexture.SetPixel(x, y, Color.white);
            }
        }        
    }

    public void ApplyTexture()
    {
        currentTexture.Apply();
    }
}
