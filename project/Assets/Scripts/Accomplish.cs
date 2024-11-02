using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Accomplish : MonoBehaviour
{

    private bool levelCompleted = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")&&!levelCompleted)//保证只调用一次
        {
            levelCompleted = true;
            Debug.Log("Level Completed");
            Invoke("CompleteLevel", 2f);//等待2秒后调用CompleteLevel函数
        }
    }
    
    private void CompleteLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
