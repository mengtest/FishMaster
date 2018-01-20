using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAttr : MonoBehaviour {

    public int speed;
    public float damage;
    public GameObject webPrefab;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Border")
        {
            Destroy(gameObject);
        }

        if (collision.tag == "Fish")
        {
            GameObject web = Instantiate(webPrefab);
            web.transform.SetParent(GameObject.Find("WebHolder").transform, false);
            web.transform.position = transform.position;
            web.GetComponent<WebAttr>().damage = damage;
            Destroy(gameObject);
        }
    }
}
