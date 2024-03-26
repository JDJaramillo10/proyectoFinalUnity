using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D myrigidbody2D;

    private float movimientoHorizontal = 0f;
    [SerializeField] private float velocidadDelMovimiento;
    [SerializeField] private float suavizadoDelMovimiento;
    private Vector3 velocidad = Vector3.zero;
    public bool mirandoDerecha = true;

    [SerializeField] private float fuerzaDeSalto;
    [SerializeField] private LayerMask queEsSuelo;
    [SerializeField] private Transform controladorSuelo;
    [SerializeField] private Vector3 dimensionesCaja;
    [SerializeField] public bool enSuelo = true;

    private Transform Hand01;
    private Transform Gun;
    private Transform controladorDisparo;

    private bool salto = false;

    public float vida;

    private Animator animator;
    public GameObject flag;
    public Transform flagSpawn;

    private DisparoJugador disparoJugador;
    public GameManager myGameManager;
    private Enemigo enemigo;

    // Start is called before the first frame update
    void Start()
    {
        myrigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Hand01 = transform.GetChild(0);
        Gun = transform.GetChild(1);
        disparoJugador = GetComponent<DisparoJugador>();
        controladorDisparo = transform.GetChild(3);
        myGameManager = FindObjectOfType<GameManager>();
        enemigo = FindObjectOfType<Enemigo>();
        StartCoroutine(SpawnFlagCoRoutine(0f));   
    }

    // Update is called once per frame
    void Update()
    {
        movimientoHorizontal = Input.GetAxisRaw("Horizontal")*velocidadDelMovimiento;

        if(Input.GetKey(KeyCode.D) && enSuelo || Input.GetKey(KeyCode.A) && enSuelo || disparoJugador.isShooting){
            if(!enSuelo && disparoJugador.isShooting){
                Gun.gameObject.SetActive(true);
                Hand01.gameObject.SetActive(false);
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
                Hand01.gameObject.SetActive(true);
                Gun.gameObject.SetActive(true);
            }
            
        }else{
            Hand01.gameObject.SetActive(false);
            Gun.gameObject.SetActive(false);
        }


        if (vida <= 0)
        {
       
        FinDelJuego();
        }

         void FinDelJuego()
        {
        Debug.Log("¡El jugador ha perdido toda su vida! Fin del juego.");
        Application.Quit(); // Cierra la aplicación
        }

        

        animator.SetFloat("Horizontal", Mathf.Abs(movimientoHorizontal));
        
        animator.SetFloat("VelocidadY",myrigidbody2D.velocity.y);

        if(Input.GetButtonDown("Jump")){
            salto = true;
        }

        if(Input.GetKey(KeyCode.Space) && !enSuelo){
            
        }
    }

    private void FixedUpdate(){

        enSuelo = Physics2D.OverlapBox(controladorSuelo.position, dimensionesCaja, 0f, queEsSuelo);
        animator.SetBool("enSuelo",enSuelo);
        //Mover
        Mover(movimientoHorizontal * Time.fixedDeltaTime, salto);

        salto = false;
    }

    public void TomarDaño(float daño){
        vida -= daño;
        myGameManager.RestaVida();
        if(vida <= 0){

            PlayerDeath();
        }
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

    void OnTriggerEnter2D(Collider2D collision){
        if(collision.CompareTag("ItemGood"))
        {
            Destroy(collision.gameObject);
            myGameManager.AddScoreCoins();
        }else if(collision.CompareTag("DeathZone"))
        {
            PlayerDeath();
        }else if(collision.CompareTag("Final")){
            SceneManager.LoadScene("Level1");
        }
    }

    void PlayerDeath()
    {
        SceneManager.LoadScene("Level1");
    }

    IEnumerator SpawnFlagCoRoutine(float waitTime){
        yield return new WaitForSeconds(waitTime);
        if(myGameManager.scoreCoin == 10 || myGameManager.scoreEnemy == 5){
            Instantiate(flag, flagSpawn.position, Quaternion.identity);
        }else{
            StartCoroutine(SpawnFlagCoRoutine(0f));
        }
    }
}
