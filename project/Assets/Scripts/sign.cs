using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class sign : MonoBehaviour
{
    private bool startsign=false;
    public GameObject DialogBox;
    public Text text;
    public string signtext;
    void Update(){
        if(Input.GetKeyDown(KeyCode.T)&&startsign){
            text.text=signtext;
            DialogBox.SetActive(true);
            
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")&&!startsign){
            startsign=true;
            Debug.Log("t");
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        startsign=false;
        DialogBox.SetActive(false);
        text.text=null;
    }
}
