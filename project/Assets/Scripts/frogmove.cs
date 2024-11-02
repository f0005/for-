using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frogmove : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private float coolingTime=2f;
    private bool cooling=false;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;
    private Collider2D coll;
    public Transform Player;
    private bool seeplayer=false;
    
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        StartCoroutine(Idle());
    }
    
    private void Update() {
        if(seeplayer&&!cooling) {
            StopCoroutine(Idle());
            Tojump();
        }
        else if(!cooling) {
            StartCoroutine(Idle());
        }
        UpdateAnimationState();
    }
    private IEnumerator Idle() {
        cooling=true;
        yield return new WaitForSeconds(coolingTime);
        cooling=false;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            
            seeplayer=true;
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player")){
            seeplayer=false;
        }
    }
    private void Tojump(){
        if(coll.IsTouchingLayers(groundLayer)) {
            Vector2 directiontoPlayer=Player.position-transform.position;
            if(directiontoPlayer.x>0) sr.flipX=true;
            else sr.flipX=false;
            rb.velocity=new Vector2(moveSpeed*(transform.position.x>Player.position.x?-1:1),jumpForce);
            StartCoroutine(Idle());
        }
    }
    private void UpdateAnimationState() {
        if(rb.velocity.y > 0.1f) {
            anim.SetBool("isjump", true);
            anim.SetBool("isfall", false);
        }
        else if(rb.velocity.y < -0.1f) {
            anim.SetBool("isjump", false);
            anim.SetBool("isfall", true);
        }
        else {
            anim.SetBool("isjump", false);
            anim.SetBool("isfall", false);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("youshelldie"))
        {
            Destroy(gameObject);
        }
    }
}
