using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishAttr : MonoBehaviour {

    public int maxNum;
    public int maxSpeed;
    public float fishGenWaitTiem = 0.5f;//每条鱼生产的间隔

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="Border")
        {
            Destroy(gameObject);
        }
    }
}
