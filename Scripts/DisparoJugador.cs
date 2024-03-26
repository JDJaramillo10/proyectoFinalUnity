using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoJugador : MonoBehaviour
{
    [SerializeField] private Transform controladorDisparo;
    [SerializeField] private GameObject bala;

    public bool isShooting = false;
    //Gun = transform.GetChild(0);
    
    private void Update(){

        if(Input.GetButtonDown("Fire1")){
            Disparar();
        }
    }

    private void Disparar(){
        Instantiate(bala, controladorDisparo.position,controladorDisparo.rotation);
    }
}
