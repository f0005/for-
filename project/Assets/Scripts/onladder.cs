using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Onladder : MonoBehaviour
{
    private float vertical;//获取垂直方向的输入
    [SerializeField] private float Climbspeed = 5f;
    private bool ladder=false;
    private bool climbing=false;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;
    void Start(){
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    void Update(){
        vertical = Input.GetAxis("Vertical");//检测w,s键的输入
        Isladder();
        Isclimbing();
    }
    private void Isladder(){
        if(ladder&&Mathf.Abs(vertical)>0) climbing=true;
        else if(ladder&&Mathf.Abs(vertical)==0) climbing=false;

        if(ladder){    
            rb.velocity=new Vector2(rb.velocity.x,vertical*Climbspeed);
        }
    }
    private void OnTriggerStay2D(Collider2D collision) {  //玩家停留在触发器内时触发
        if(collision.CompareTag("ladder")&&(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))){
            ladder=true;
            climbing=true;
            rb.velocity=new Vector2(0,0); //清除当前速度
            anim.SetBool("f",true);
            rb.gravityScale=0;//禁止重力
        }
    }
    private void OnTriggerExit2D(Collider2D collision) { //离开
        if(collision.CompareTag("ladder")){
            ladder=false;
            climbing=false;
            anim.SetBool("f",false);
            rb.gravityScale=1;
        }
    }
    private void Isclimbing(){
        if(!climbing&&ladder) anim.speed=0; //不爬梯子冻结动画
        else anim.speed=1; 
    }
}