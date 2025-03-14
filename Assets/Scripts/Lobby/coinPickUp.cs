using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinPickUp : MonoBehaviour
{
    public int value;

    public GameObject coinEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") //Añade el coin al texto y lo destruye de la pantalla
        {
            gameManager.instance.AddCoins(value);

            // Desactiva en lugar de destruir
            gameManager.instance.DisableCollectible(gameObject);

            Instantiate(coinEffect, transform.position, transform.rotation);
            gameObject.SetActive(false); // En lugar de Destroy
        }
    }
}
