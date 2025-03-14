using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinPickUp : MonoBehaviour
{
    public int value;

    public GameObject coinEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            gameManager.instance.AddCoins(value);
            Destroy(gameObject);
            Instantiate(coinEffect, transform.position, transform.rotation);
        }
    }
}
