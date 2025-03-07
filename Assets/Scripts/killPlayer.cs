using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killPlayer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger detectado con: " + other.gameObject.name); // Verificar qué colisiona

        if (other.CompareTag("Player"))
        {
            Debug.Log("¡Jugador murió! Activando Respawn...");
            gameManager.instance.Respawn();
        }
    }
}
