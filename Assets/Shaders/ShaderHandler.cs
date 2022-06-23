using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode] //  // Code taken from: https://www.youtube.com/watch?v=bz7MStTq950
public class ShaderHandler : MonoBehaviour
{
    public Material shaderMaterial;

    void OnRenderImage(RenderTexture src, RenderTexture dest) {
        Graphics.Blit(src, dest, shaderMaterial);
    }
}
