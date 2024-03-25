using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoJugador : MonoBehaviour
{
    [SerializeField] private Transform controladorDisparo;
    [SerializeField] private GameObject bala;
    public bool isShooting = false;
    private Transform Hand01;
    private Transform Gun;
    private PlayerMovement playerMovement;
    private Rigidbody2D myrigidbody2D;

    private void Start(){ 
        myrigidbody2D = GetComponent<Rigidbody2D>();
        Hand01 = transform.GetChild(0);
        Gun = transform.GetChild(1);
        controladorDisparo = transform.GetChild(3);
        playerMovement = GetComponent<PlayerMovement>();
    }    

    private void Update(){

        if(Input.GetKey(KeyCode.E)){
            isShooting = true;
            if(!playerMovement.enSuelo){ 
                if(playerMovement.mirandoDerecha){
                    Gun.position = new Vector2(myrigidbody2D.position.x + 0.8f, myrigidbody2D.position.y + 0.03f);
                    controladorDisparo.position = new Vector2(myrigidbody2D.position.x + 1.2f, myrigidbody2D.position.y + 0.07f);
                }else{
                    Gun.position = new Vector2(myrigidbody2D.position.x - 0.8f, myrigidbody2D.position.y + 0.03f);
                    controladorDisparo.position = new Vector2(myrigidbody2D.position.x - 1.2f, myrigidbody2D.position.y + 0.07f);
                }
            }else{
                if(playerMovement.mirandoDerecha){
                    controladorDisparo.position = new Vector2(myrigidbody2D.position.x + 0.74f, myrigidbody2D.position.y - 0.63f);
                }else{
                    controladorDisparo.position = new Vector2(myrigidbody2D.position.x - 0.74f, myrigidbody2D.position.y - 0.63f);
                }
            }
        }else{
            isShooting = false;
        }

        if(Input.GetButtonDown("Fire1")){
            Disparar();
        }

    }

    private void Disparar(){
        Instantiate(bala, controladorDisparo.position,controladorDisparo.rotation);
    }

}
