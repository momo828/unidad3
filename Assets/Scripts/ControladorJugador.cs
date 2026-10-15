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

    [Header("Disparo")]
    public GameObject prefabBala; // Aquí pondremos la plantilla de la bala
    public Transform puntoDisparo; // De dónde sale la bala
    public float tiempoEntreDisparos = 0.25f; // Previene lag por spam de botones
    private float ultimoDisparo;

    private Rigidbody2D rb;
    private float movimientoX;
    private bool estaEnSuelo;
    
    private SpriteRenderer spriteRenderer;
    private Animator anim; // El "cerebro" de las animaciones

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>(); // Lo conectamos al inicio
    }

    void Update()
    {
        movimientoX = Input.GetAxisRaw("Horizontal");

    
        if (movimientoX < 0)
        {
            spriteRenderer.flipX = true;
            anim.SetBool("caminando", true); // Activa caminar
        }
        else if (movimientoX > 0)
        {
            spriteRenderer.flipX = false;
            anim.SetBool("caminando", true); // Activa caminar
        }
        else
        {
            anim.SetBool("caminando", false); 
        }

        
        if (detectorSuelo != null)
        {
            estaEnSuelo = Physics2D.OverlapCircle(detectorSuelo.position, radioDeteccion, capaSuelo);
        }

        if (Input.GetButtonDown("Jump") && estaEnSuelo)
        {
            Saltar();
        }

        
        if (Input.GetButtonDown("Fire1") && Time.time > ultimoDisparo + tiempoEntreDisparos)
        {
            Disparar();
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(movimientoX * velocidad, rb.linearVelocity.y);
    }

    void Saltar()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, fuerzaSalto);
    }

    void Disparar()
    {
        ultimoDisparo = Time.time;
        
        
        anim.SetTrigger("disparar");

      
        if (prefabBala != null && puntoDisparo != null)
        {
            GameObject balaNueva = Instantiate(prefabBala, puntoDisparo.position, Quaternion.identity);
            BalaScript scriptBala = balaNueva.GetComponent<BalaScript>();
            
            
            if (scriptBala != null)
            {
                scriptBala.ConfigurarDireccion(spriteRenderer.flipX);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (detectorSuelo != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(detectorSuelo.position, radioDeteccion);
        }
    }
}