using UnityEngine;

public class BalaScript : MonoBehaviour
{
    public float velocidad = 10f;
    private Rigidbody2D rb;
    private Vector2 direccion = Vector2.right; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
      
        Destroy(gameObject, 3f);
    }


    public void ConfigurarDireccion(bool mirandoIzquierda)
    {
        if (mirandoIzquierda)
            direccion = Vector2.left; // -1 en X
        else
            direccion = Vector2.right; // 1 en X
    }

    void FixedUpdate()
    {
       
        rb.linearVelocity = direccion * velocidad;
    }
}