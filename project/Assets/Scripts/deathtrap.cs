using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathtrap : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if  (collision.gameObject.CompareTag("Player"))
        {
            changecolor();
        }
    } 
    private void changecolor(){
        Color c=GetComponent<SpriteRenderer>().color;
        c.a = 0.5f;
        c.r = 1f;
        GetComponent<SpriteRenderer>().color = c; 
    } 
}
