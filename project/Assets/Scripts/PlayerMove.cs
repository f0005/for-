using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;//涉及到按键

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;
    private float directionX = 0f; 
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private float glideForce = 3f;//滑翔垂直速度
    private int jumpCount = 0;
    [SerializeField] private int maxJumpCount = 2;
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update() {
        directionX = Input.GetAxis("Horizontal"); //获取水平方向的输入a，d键，实现水平移动
        rb.velocity = new Vector2(directionX * moveSpeed, rb.velocity.y);//水平方向的速度
        UpdateAnimationState();
        if (Input.GetButtonDown("Jump")) {//space键
            Jump();
        }
        else if (Input.GetKey(KeyCode.E)&&Mathf.Abs(rb.velocity.y) >= 0.1f&&Mathf.Abs(rb.velocity.x) >= 0.1f){
            mayCrouch();//crouch不是下蹲吗？
        }
        else anim.SetBool("e", false);
        UpdateAnimationState();     
    }

    private void mayCrouch() {//总之，这实现的是滑翔
        anim.SetBool("e", true);//按E启动
        rb.velocity = new Vector2(rb.velocity.x, -glideForce);
    }
    private void Jump() {
        anim.SetBool("e", false);
        if (jumpCount < maxJumpCount) {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount++;
            Debug.Log("Current jump count: " + jumpCount);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {//胶囊体trigger，在小狐狸脚上
        if (collision.gameObject.CompareTag("Ground")) {
            jumpCount = 0; 
            Debug.Log("Jump count reset at " + collision.gameObject.name);
        }
    }


    private void UpdateAnimationState() {
        if(rb.velocity.y<=0.1f && rb.velocity.y>=-0.1f){//小动不算动（）
            if (directionX > 0.0f) {
                anim.SetBool("running", true);
                sr.flipX = false; 
            } else if (directionX < 0.0f) {
                anim.SetBool("running", true);
                sr.flipX = true; //左右翻转，初始方向为右，所以向左走翻转
            } else {
                anim.SetBool("running", false);
            }
        }
        if(rb.velocity.y > 0.1f) {
            anim.SetBool("jumping", true);
            anim.SetBool("falling", false);
        }
        else if(rb.velocity.y < -0.1f) {
            anim.SetBool("jumping", false);
            anim.SetBool("falling", true);
        }
        else {
            anim.SetBool("jumping", false);
            anim.SetBool("falling", false);
        }
    }
}
