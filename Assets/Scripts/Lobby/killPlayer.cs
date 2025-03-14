using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killPlayer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Vaya, Vaya, el jugador ha muerto");
            gameManager.instance.Respawn();
        }
    }
}
