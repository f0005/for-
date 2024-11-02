using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class platform : MonoBehaviour
{
    public Transform pointA, pointB;
    [SerializeField] float speed;
    Transform targetpoint;
    void Start()
    {
        targetpoint=pointB;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position,pointA.position)<0.1f) targetpoint=pointB;
        if(Vector2.Distance(transform.position,pointB.position)<0.1f) targetpoint=pointA;
        transform.position=Vector2.MoveTowards(transform.position,targetpoint.position,speed*Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Debug.Log("Player collided with platform");
            collision.transform.SetParent(transform);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Debug.Log("Player exited platform");
            collision.transform.SetParent(null);
        }
    }
}
