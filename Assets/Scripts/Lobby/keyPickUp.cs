using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyPickUp : MonoBehaviour
{
    public int value;
    public GameObject coinEffect;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player") //Añade la key al texto y la destruye de la pantalla
        {
            gameManager.instance.AddKey(value);
            Destroy(gameObject);
            Instantiate(coinEffect, transform.position, transform.rotation);
        }
    }
}
