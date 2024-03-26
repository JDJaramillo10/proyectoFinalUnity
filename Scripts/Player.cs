using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Rigidbody2D myrigidbody2D;
    private bool salto = false;
    private float movimientoHorizontal = 40f;
    private Animator animator;
    public bool mirandoDerecha = true;
    private Vector3 velocidad = Vector3.zero;
    private Transform Gun;
    private DisparoJugador disparoJugador;
    [SerializeField] private float velocidadDelMovimiento;
    [SerializeField] private Transform controladorSuelo;
    [SerializeField] private Vector3 dimensionesCaja;
    [SerializeField] private LayerMask queEsSuelo;
    [SerializeField] private float fuerzaDeSalto;
    [SerializeField] private float suavizadoDelMovimiento;
    [SerializeField] public bool enSuelo = true;

    void Start()
    {
        myrigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Gun = transform.GetChild(0);
        disparoJugador = GetComponent<DisparoJugador>();
    }


    void Update()
    {
        movimientoHorizontal = Input.GetAxisRaw("Horizontal")*velocidadDelMovimiento;
        animator.SetFloat("Horizontal", Mathf.Abs(movimientoHorizontal));

        if(Input.GetKey(KeyCode.D) && enSuelo || Input.GetKey(KeyCode.A) && enSuelo || disparoJugador.isShooting){
           if(!enSuelo && disparoJugador.isShooting){
                //Gun.gameObject.SetActive(true);
                //Hand01.gameObject.SetActive(false);
                if(mirandoDerecha){
                    Gun.position = new Vector2(myrigidbody2D.position.x + 0.8f, myrigidbody2D.position.y + 0.03f);
                }else{
                    Gun.position = new Vector2(myrigidbody2D.position.x - 0.8f, myrigidbody2D.position.y + 0.03f);
                }
            }else{
                if(mirandoDerecha){
                    Gun.position = new Vector2(myrigidbody2D.position.x + 0.4465f, myrigidbody2D.position.y - 0.6644f);
                }else{
                    Gun.position = new Vector2(myrigidbody2D.position.x - 0.4465f, myrigidbody2D.position.y - 0.6644f);
                }
                //Hand01.gameObject.SetActive(true);
                //Gun.gameObject.SetActive(true);
           }
            
        }
    //     else{
    //         //Hand01.gameObject.SetActive(false);
    //         Gun.gameObject.SetActive(false);
    //    }

        if(Input.GetButtonDown("Jump")){
            salto = true;
        }
    }
    private void FixedUpdate(){
        enSuelo = Physics2D.OverlapBox(controladorSuelo.position, dimensionesCaja, 0f, queEsSuelo);
        animator.SetBool("enSuelo", enSuelo);
        Mover(movimientoHorizontal * Time.fixedDeltaTime, salto);
        salto = false;
    }
    private void Mover(float mover, bool saltar){
        Vector3 velocidadObjetivo = new Vector2(mover, myrigidbody2D.velocity.y);
        myrigidbody2D.velocity = Vector3.SmoothDamp(myrigidbody2D.velocity, velocidadObjetivo, ref velocidad, suavizadoDelMovimiento);
        if(mover > 0 && !mirandoDerecha){
            Girar();
        }else if(mover < 0 && mirandoDerecha){
            Girar();
        }

        if(enSuelo && saltar){
            enSuelo = false;
            myrigidbody2D.AddForce(new Vector2(0f, fuerzaDeSalto));
        }
    }
    private void Girar(){
        mirandoDerecha = !mirandoDerecha;
        transform.eulerAngles = new Vector3(0,transform.eulerAngles.y + 180,0);
    }
    private void OnDrawGizmos(){
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(controladorSuelo.position, dimensionesCaja);
    }
}
