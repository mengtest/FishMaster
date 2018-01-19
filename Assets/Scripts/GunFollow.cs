using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFollow : MonoBehaviour {

    private RectTransform UGUICanvas;
    private Vector3 mousePos;
	void Start () {
        UGUICanvas = GameObject.Find("Order90Canvas").GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {

        //计算夹角
        RectTransformUtility.ScreenPointToWorldPointInRectangle(UGUICanvas, new Vector2(Input.mousePosition.x, Input.mousePosition.y), Camera.main, out mousePos);
        float z;
        if (mousePos.x>transform.position.x)
        {
            z = -Vector3.Angle(Vector3.up, mousePos - transform.position);
        }
        else
        {
            z = Vector3.Angle(Vector3.up, mousePos - transform.position);
        }
        transform.localRotation = Quaternion.Euler(0, 0, z);
    }
}
