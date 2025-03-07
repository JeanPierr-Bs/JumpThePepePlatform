using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killPlayer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger detectado con: " + other.gameObject.name); // Verificar qu� colisiona

        if (other.CompareTag("Player"))
        {
            Debug.Log("�Jugador muri�! Activando Respawn...");
            gameManager.instance.Respawn();
        }
    }
}
