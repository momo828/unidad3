using UnityEngine;

public class CameraScript : MonoBehaviour
{
    
    public GameObject jugador;

    void Update()
    {
        
        if (jugador != null)
        {
            
            Vector3 position = transform.position;
            
            
            position.x = jugador.transform.position.x;
            
         
            transform.position = position;
        }
    }
}