using UnityEngine;

public class DisparoEnemigo : MonoBehaviour
{
    [SerializeField] private Transform controladorDisparo;
    [SerializeField] private GameObject bala;
    private Transform playerTransform; // Referencia al transform del jugador
    private float distanciaDeDisparo = 5f; // Distancia mínima para disparar
    private float intervaloDeDisparo = 2f; // Intervalo entre disparos
    private float tiempoUltimoDisparo; // Tiempo del último disparo

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // Buscar el jugador por su etiqueta
        controladorDisparo = transform.GetChild(0); // Obtener el controlador de disparo del enemigo
        tiempoUltimoDisparo = -intervaloDeDisparo; // Iniciar el temporizador de disparo
    }

    private void Update()
    {
        // Calcular la distancia entre el enemigo y el jugador
        float distanciaAlJugador = Vector2.Distance(transform.position, playerTransform.position);

        // Verificar si el jugador está dentro del rango de disparo y ha pasado el tiempo suficiente desde el último disparo
        if (distanciaAlJugador < distanciaDeDisparo && Time.time > tiempoUltimoDisparo + intervaloDeDisparo)
        {
            // Apuntar al jugador
            Vector2 direccion = playerTransform.position - transform.position;
            float angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
            controladorDisparo.rotation = Quaternion.Euler(0f, 0f, angulo);

            // Disparar
            Disparar();
            tiempoUltimoDisparo = Time.time; // Actualizar el tiempo del último disparo
        }
    }
    private void Disparar()
{
    // Instantiar la bala
    GameObject nuevaBala = Instantiate(bala, controladorDisparo.position, controladorDisparo.rotation);
    
   
    Physics2D.IgnoreCollision(nuevaBala.GetComponent<Collider2D>(), GetComponent<Collider2D>());
}

}
