using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    [SerializeField] private float vida;
    [SerializeField] GameObject efectoMuerte;
    public GameManager myGameManager;
    public float daño;
    
    void Start(){
        myGameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TomarDaño(float daño){
        vida -= daño;
        if(vida <= 0){
            myGameManager.AddScoreEnemies();
            Muerte();
        }


    }

    private void Muerte(){
        Instantiate(efectoMuerte, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision){
        if(collision.CompareTag("DeathZone"))
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Player")){
            collision.gameObject.GetComponent<PlayerMovement>().TomarDaño(daño);
        }

    }
}
