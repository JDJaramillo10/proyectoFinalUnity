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

    private void OnTriggerEnter2D(Collider2D collision)
{
    if (collision.CompareTag("Player"))
    {
        collision.GetComponent<PlayerMovement>().TomarDaño(daño);
        Destroy(gameObject);
    }
    else if (collision.CompareTag("Enemigo")) // Verifica si la colisión es con un enemigo
    {
        collision.GetComponent<Enemigo>().TomarDaño(daño);
        Destroy(gameObject);
    }
    else if (collision.CompareTag("ItemGood"))
    {
        Destroy(collision.gameObject);
    }
}

}
