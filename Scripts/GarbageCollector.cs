using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GarbageCollector : MonoBehaviour
{


    void OnTriggerEnter2D(Collider2D collision){

        if(collision.CompareTag("Enemigo"))
        {
            Destroy(collision.gameObject);
        }else if(collision.CompareTag("Suelo"))
        {
            Destroy(collision.gameObject);
        }else if(collision.CompareTag("Player")){
            PlayerDeath();
        }
            
        
    }

    void PlayerDeath()
    {
        SceneManager.LoadScene("Level1");
    }
}
