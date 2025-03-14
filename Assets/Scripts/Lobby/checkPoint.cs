using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPoint : MonoBehaviour
{
    public GameObject cpOn, cpOff;
    private bool isActive = false;

    public int soundToPlay;

    void Start()
    {
    }

    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger detectado con: " + other.gameObject.name); // Ver si el player entra

        if (other.CompareTag("Player") && !isActive)
        {
            Debug.Log("El jugador ha tomado un respawn" + transform.position);
            gameManager.instance.SetSpawnPoint(transform.position);

            checkPoint[] allCP = FindObjectsOfType<checkPoint>(); 
            for (int i = 0; i < allCP.Length; i++)
            {
                allCP[i].cpOff.SetActive(true);
                allCP[i].cpOn.SetActive(false);
                allCP[i].isActive = false;
            }

            cpOff.SetActive(false);
            cpOn.SetActive(true);
            isActive = true;

            if (soundToPlay >= 0)
            {
                AudioManager.instance.PlaySFX(soundToPlay);
            }
        }
    }
}
