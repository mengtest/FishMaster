using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebAttr : MonoBehaviour
{

    public float disapperTime;//消失时间
    public float damage;//攻击
    private void Start()
    {
        Destroy(gameObject, disapperTime);//销毁自身
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Fish")
        {
            //collision.gameObject.GetComponent<FishAttr>().TakeDamage(damage);
            collision.SendMessage("TakeDamage", damage);//给鱼发消息把,伤害传递过去
        }
    }
}
