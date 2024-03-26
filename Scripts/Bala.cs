using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] private float daño;
    [SerializeField] private float tiempoDeVida;
    

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, tiempoDeVida);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * velocidad * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.CompareTag("Enemigo")){
            collision.GetComponent<Boss>().TomarDaño(daño);
            Destroy(gameObject);
        }
    }
}
