using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CherryController : MonoBehaviour
{
    public int score=0;
    public GameObject door;
    [SerializeField] private Text scoreText;

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag=="gem"){
            
            score++;
            scoreText.text="Score: "+score;
            Destroy(collision.gameObject);
        }
        if(score==5){
            door.SetActive(false);
        }
    }
}
