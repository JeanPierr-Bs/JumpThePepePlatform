using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthPickUp : MonoBehaviour
{
    public int healAmount;
    public bool isFullhealth;
    public GameObject healthEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            Destroy(gameObject);

            //Activa el efecto del player
            Instantiate(healthEffect, playerController.instance.transform.position + new Vector3(0f, 1f, 0f), playerController.instance.transform.rotation);

            if (isFullhealth)
            {
                healthManager.Instance.ResetHealth();
            }else
            {
                healthManager.Instance.AddHealth(healAmount);
            }
        }
    }
}
