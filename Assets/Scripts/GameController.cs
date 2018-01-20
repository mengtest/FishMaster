using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public GameObject[] gunGos;
    private int costIndex = 0;
    private Text oneShootCostText;
    private int[] oneShootCosts = { 40, 50, 60, 70, 80, 90, 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };
	void Start () {
        oneShootCostText = GameObject.Find("oneShootCostText").GetComponent<Text>();

    }
	
	void Update () {
        ChangeBulletCost();
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
