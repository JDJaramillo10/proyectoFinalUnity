using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BordeMovement : MonoBehaviour
{
    private Rigidbody2D myrigidbody2D;
    public Transform bordeFinal;
    [SerializeField] private float velocidadDesplazamiento = 2f;


    // Start is called before the first frame update
    void Start()
    {
        myrigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x + 11.91f <= bordeFinal.position.x){ 
            myrigidbody2D.velocity = new Vector2(velocidadDesplazamiento, myrigidbody2D.velocity.y);
        }else{
            myrigidbody2D.velocity = Vector2.zero;
        }
    }

    /*
    void OnTriggerEnter2D(Collider2D collision){
        if(collision.CompareTag("GarbageCollector")){
            final = true;
        }
    }*/



    
}
