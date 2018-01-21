using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EF_AutoMove : MonoBehaviour {

    public float speed = 1f;
    public Vector3 dir = Vector3.right;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(dir * speed * Time.deltaTime);
	}
   
}
