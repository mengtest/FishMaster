using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMaker : MonoBehaviour {

    private Transform fishHolder;
    private List<Transform> genPositions;
    [SerializeField]
    private GameObject[] fishPrefabs;
    [SerializeField]
    private float waveGenWaitTime = 0.3f;//每波生产的时间
    private void Awake()
    {
        fishHolder = GameObject.Find("FishHolder").transform;
        genPositions = new List<Transform>();
        foreach (Transform item in GameObject.Find("GenPos").transform)
        {
            genPositions.Add(item);
        }
    }
    private void Start()
    {
        InvokeRepeating("MakeFishes", 0, waveGenWaitTime);
    }
    private void MakeFishes()
    {
        int genPosIndex = Random.Range(0, genPositions.Count);
        int fishPreIndex = Random.Range(0, fishPrefabs.Length);
        int maxNum = fishPrefabs[fishPreIndex].GetComponent<FishAttr>().maxNum;
        int maxSpeed = fishPrefabs[fishPreIndex].GetComponent<FishAttr>().maxSpeed;
        int fishNum = Random.Range((maxNum / 2) + 1, maxNum);
        int fishSpeed = Random.Range(maxSpeed / 2, maxSpeed);
        int moveType = Random.Range(0, 2);//0表示直走,1表示转弯
        int angOffset;//直走的切斜度
        int angSpeed;////转弯的角速度

        if (moveType==0)
        {
            //TUDO 直走鱼群的生成
            angOffset = Random.Range(-22, 22);
            GenStraightFish(genPosIndex, fishPreIndex, fishNum, fishSpeed, angOffset);
        }
        else
        {
            //TUDO 转弯鱼的生产
        }
    }

    private void GenStraightFish(int genPosIndex,int fishPreIndex,int fishNum,int fishSpeed,int angOffset)
    {
        for (int i = 0; i < fishNum; i++)
        {
            GameObject fish = Instantiate(fishPrefabs[fishPreIndex]);
            fish.transform.SetParent(fishHolder, false);
            fish.transform.localPosition = genPositions[genPosIndex].localPosition;
            fish.transform.localRotation = genPositions[genPosIndex].localRotation;
            fish.transform.Rotate(0, 0, angOffset);
        }
    }
}
