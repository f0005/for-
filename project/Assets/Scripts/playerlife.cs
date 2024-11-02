using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//涉及到重启关卡
using UnityEngine.UI;

public class playerlife : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    public int maxlife=3;
    private int currentlife;
    private bool isinvincible = false;
    public Image[] LIVE;//三个樱桃三条命
    public Collider2D enemy1;
    public Collider2D enemy2;
    public GameObject ash;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentlife = maxlife;
        Updatelife();
    }

    private void TakeDamage()
    {
        currentlife--;
        Updatelife();
        if (currentlife <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(Invincible());
        }
    }
    private IEnumerator Invincible()//我只知道要用yield return new WaitForSeconds时候用IEnumerator
    {
        isinvincible = true;
        Color c=GetComponent<SpriteRenderer>().color;
        c.a = 0.5f;//调节透明度
        GetComponent<SpriteRenderer>().color = c;
        if (enemy1!= null) Physics2D.IgnoreCollision(GetComponent<Collider2D>(), enemy1, true);
        if (enemy2!= null) Physics2D.IgnoreCollision(GetComponent<Collider2D>(), enemy2, true);
        yield return new WaitForSeconds(2f);
        c.a = 1f;
        GetComponent<SpriteRenderer>().color = c;
        if (enemy1!= null) Physics2D.IgnoreCollision(GetComponent<Collider2D>(), enemy1, false);
        if (enemy2!= null) Physics2D.IgnoreCollision(GetComponent<Collider2D>(), enemy2, false);
        isinvincible = false;
    }
    private void Updatelife()
    {
        for (int i = 0; i < LIVE.Length; i++)
        {
            if (i < currentlife)
            {
                LIVE[i].gameObject.SetActive(true);//显示樱桃 
            }
            else
            {
                LIVE[i].gameObject.SetActive(false); 
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("youshelldie"))
        {
            Die();
        }
        if (collision.gameObject.CompareTag("enemy")&&!isinvincible)
        {
            TakeDamage();
        }
    }
    private void Die()
    {
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("Hurt");
    }

    public void RestartLevel()
    {
        Debug.Log("Restarting level");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("enemy")&&!isinvincible) //因为在无敌时会忽略敌人碰撞体，然后敌人碰到胶囊体trigger就噶了，所以设置无敌的时候就踩不死敌人
        {
            GameObject sash = Instantiate(ash, collision.transform.position, Quaternion.identity);//在enemy位置生成ash
            Destroy(collision.gameObject);
            ash ashcomp = sash.GetComponent<ash>(); //获取ash组件
            if(ashcomp!=null) ashcomp.Summon();//summon在ash脚本中实现
        }
    }
}
