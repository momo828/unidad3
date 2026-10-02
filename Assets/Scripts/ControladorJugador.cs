using UnityEngine;

public class ControladorJugador : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 5f;

    [Header("Salto")]
    public float fuerzaSalto = 10f;
    public Transform detectorSuelo;
    public float radioDeteccion = 0.2f;
    public LayerMask capaSuelo;

    private Rigidbody2D rb;
    private float movimientoX;
    private bool estaEnSuelo;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Movimiento horizontal
        movimientoX = Input.GetAxisRaw("Horizontal");

        // Verificamos si tocamos el suelo
        if (detectorSuelo != null)
        {
            estaEnSuelo = Physics2D.OverlapCircle(detectorSuelo.position, radioDeteccion, capaSuelo);
        }

        // Detectar si se presiona la Barra Espaciadora y el jugador está en el suelo
        if (Input.GetButtonDown("Jump") && estaEnSuelo)
        {
            Saltar();
        }
    }

    void FixedUpdate()
    {
        // Aplicamos la velocidad horizontal manteniendo la fuerza vertical
        rb.linearVelocity = new Vector2(movimientoX * velocidad, rb.linearVelocity.y);
    }

    void Saltar()
    {
        // Aplicamos un impulso directo hacia arriba
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, fuerzaSalto);
    }

    // Dibuja un círculo rojo en la ventana Scene para visualizar el detector de suelo
    private void OnDrawGizmosSelected()
    {
        if (detectorSuelo != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(detectorSuelo.position, radioDeteccion);
        }
    }
}