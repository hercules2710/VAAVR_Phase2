using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxMoves : MonoBehaviour
{
    public float skyBoxSpeed;
    private void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * skyBoxSpeed);
    }
}
