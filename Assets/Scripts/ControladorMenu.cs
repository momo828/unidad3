using UnityEngine;
using UnityEngine.SceneManagement; 

public class ControladorMenu : MonoBehaviour
{
    
    public void IniciarJuego()
    {
       
        SceneManager.LoadScene("SampleScene"); 
    }

    public void SalirJuego()
    {
        Debug.Log("Cerrando el juego...");
        Application.Quit(); 
    }
}