using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombateJugador : MonoBehaviour
{
    [SerializeField] private float vida;
    // Start is called before the first frame update
    public void TomarDaño(float daño){
        vida -= daño;
        if(vida <= 0){
            Destroy(gameObject);
        }
    }
}
