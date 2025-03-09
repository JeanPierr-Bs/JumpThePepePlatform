using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hurtPlayer : MonoBehaviour
{
    [SerializeField] private int damageAmount = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            healthManager.Instance.Hurt(transform.position);
        }
    }

}
