using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPoint : MonoBehaviour
{
    public GameObject cpOn, cpOff;

    void Start()
    {
    }

    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger detectado con: " + other.gameObject.name); // Ver si el player entra
        if (other.CompareTag("Player"))
        {
            Debug.Log("¡Jugador murió! Activando Respawn...");
            gameManager.instance.Respawn();

            cpOff.SetActive(false);
            cpOn.SetActive(true);
        }
    }
}
