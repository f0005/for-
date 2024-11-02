using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bettle : MonoBehaviour
{
    public Transform posA, posB;
    [SerializeField] float speed;
    Transform targetpos;
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        targetpos=posA;//起始先往posA方向移动
        spriteRenderer=GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(Vector2.Distance(transform.position,posA.position)<0.1f) 
        {
            targetpos=posB;
            spriteRenderer.flipX=false;//初始朝右
        }
        if(Vector2.Distance(transform.position,posB.position)<0.1f) 
        {
            targetpos=posA;
            spriteRenderer.flipX=true;
        }
        transform.position=Vector2.MoveTowards(transform.position,targetpos.position,speed*Time.deltaTime);
    }
}
