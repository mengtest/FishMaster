using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EF_MoveTo : MonoBehaviour {

    private GameObject goldCollect;
    public float moveSpeed;
	void Start () {
        goldCollect = GameObject.Find("GoldCollect");
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.MoveTowards(transform.position, goldCollect.transform.position, moveSpeed * Time.deltaTime);
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Border")
        {
            Destroy(gameObject);
        }
    }
}
