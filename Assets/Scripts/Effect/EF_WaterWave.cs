using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EF_WaterWave : MonoBehaviour {

    public Texture[] textures;
    private Material material;
    private int index;

    private void Awake()
    {
        material = GetComponent<MeshRenderer>().material;
        InvokeRepeating("ChangeTexture",0,0.1f);
    }
    private void ChangeTexture()
    {
        material.mainTexture = textures[index];
        index = (index + 1) % textures.Length;
    }

}
