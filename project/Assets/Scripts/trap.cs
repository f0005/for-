using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
达不到效果，这个脚本没用
*/
public class trap : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if  (collision.gameObject.CompareTag("Player"))//从这一步开始就实现不了，deathtrap也一样
        {
            Debug.Log("Player has triggered the trap");
            Changeappearance(0.5f);
        }
    }
   private void OnTriggerExit(Collider collision)
    {
        if  (collision.gameObject.CompareTag("Player"))
        {
            Changeappearance(1f);
        }
    }
    private void Changeappearance(float alpha)
    {
        Color color = GetComponent<SpriteRenderer>().color;
        color.a = alpha;
        GetComponent<SpriteRenderer>().color = color;
    }
}
