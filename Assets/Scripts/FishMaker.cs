using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMaker : MonoBehaviour {

    private Transform fishHolder;
    private List<Transform> genPositions;
    [SerializeField]
    private GameObject[] fishPrefabs;

    private void Awake()
    {
        fishHolder = GameObject.Find("fishHolder").transform;
        genPositions = new List<Transform>();
        foreach (Transform item in GameObject.Find("GenPos").transform)
        {
            genPositions.Add(item);
        }
    }
}
