using UnityEngine;
using Firebase;
using Firebase.Firestore;
using Firebase.Extensions;
using System.Collections.Generic;

public class GestorBaseDatos : MonoBehaviour
{
    private FirebaseFirestore db;

    void Start()
    {
        
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
            if (task.Result == DependencyStatus.Available)
            {
                
                db = FirebaseFirestore.DefaultInstance;
                Debug.Log("Firebase inicializado correctamente.");
                
                
                GuardarAvance(150, true, false);
            }
            else
            {
                Debug.LogError("Error en las dependencias de Firebase: " + task.Result);
            }
        });
    }

    public void GuardarAvance(int totalMonedas, bool nivel1Superado, bool nivel2Superado)
    {
        DocumentReference docRef = db.Collection("jugadores").Document("jugador_01");

        Dictionary<string, object> datos = new Dictionary<string, object>
        {
            { "monedasObtenidas", totalMonedas },
            { "nivel1Completado", nivel1Superado },
            { "nivel2Completado", nivel2Superado }
        };

        docRef.SetAsync(datos).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted && !task.IsFaulted)
            {
                Debug.Log("¡ÉXITO! Datos guardados en Firestore.");
            }
            else
            {
                Debug.LogError("Fallo al subir a la nube: " + task.Exception);
            }
        });
    }
}