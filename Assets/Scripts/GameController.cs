using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public GameObject[] gunGos;
    private int costIndex = 0;
    [SerializeField]
    private int lv;
    private Text oneShootCostText;
    private Transform bulletHolder;
    private Transform FirePos;
    [SerializeField]
    private GameObject[] bullet1Gos;
    [SerializeField]
    private GameObject[] bullet2Gos;
    [SerializeField]
    private GameObject[] bullet3Gos;
    [SerializeField]
    private GameObject[] bullet4Gos;

    private int[] oneShootCosts = { 40, 50, 60, 70, 80, 90, 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };
	void Start () {
        oneShootCostText = GameObject.Find("oneShootCostText").GetComponent<Text>();
        bulletHolder = GameObject.Find("bulletHolder").transform;
    }
	
    void Fire()
    {
        GameObject[] useBullets = bullet1Gos;
        int bulletIndex;
        if (Input.GetMouseButtonDown(0))
        {
            switch (costIndex / 4)
            {
                case 0:useBullets = bullet1Gos;break;
                case 1: useBullets = bullet2Gos; break; 
                case 2: useBullets = bullet3Gos; break;
                case 3:useBullets = bullet4Gos;break;
            }
     
            bulletIndex = (lv / 10 >= 9) ? 9 : lv / 10; 
            GameObject bullet  = Instantiate(useBullets[bulletIndex]);
            bullet.transform.SetParent(bulletHolder, false);
            bullet.transform.position = gunGos[costIndex / 4].transform.Find("FirePos").transform.position;
            bullet.transform.rotation = gunGos[costIndex / 4].transform.rotation;
            bullet.AddComponent<EF_AutoMove>().dir = Vector3.up;
            bullet.GetComponent<EF_AutoMove>().speed =10f;

        }
    }
	void Update () {
        ChangeBulletCost();
        Fire();
	}

    void ChangeBulletCost()
    {
        if (Input.GetAxis("Mouse ScrollWheel")<0)
        {
            OnButtonMDown();
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            OnButtonPDown();
        }
    }
    public void OnButtonPDown()
    {
        gunGos[costIndex / 4].SetActive(false);
        costIndex++;
        costIndex = (costIndex>oneShootCosts.Length-1)?0: costIndex;
        gunGos[costIndex / 4].SetActive(true);
        oneShootCostText.text = "$" + oneShootCosts[costIndex];
    }
    public void OnButtonMDown()
    {
        gunGos[costIndex / 4].SetActive(false);
        costIndex--;
        costIndex = (costIndex < 0) ? oneShootCosts.Length - 1 : costIndex;
        gunGos[costIndex / 4].SetActive(true);
        oneShootCostText.text = "$" + oneShootCosts[costIndex];

    }
}
