using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public static gameManager instance;
    private Vector3 respawnPosition;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        // Verifica si el personaje existe antes de asignar la posición de respawn
        if (playerController.instance != null)
        {
            respawnPosition = playerController.instance.transform.position;
        }
        else
        {
            Debug.LogError("PlayerController no encontrado en la escena.");
        }
    }

    void Update()
    {
    }

    public void Respawn()
    {
        StartCoroutine(RespawnWaiter());
    }

    public IEnumerator RespawnWaiter()
    {
        

        playerController.instance.gameObject.SetActive(false);
        cameraController.instance.cmBrain.enabled = false;

        UIManager.Instance.fadeFromBlack = true;

        yield return new WaitForSeconds(2f);

        UIManager.Instance.fadeFromBlack = true;

        CharacterController charController = playerController.instance.GetComponent<CharacterController>();

        if (charController != null)
        {
            charController.enabled = false;  // Desactiva el CharacterController antes de moverlo
        }

        Vector3 safeRespawn = respawnPosition + new Vector3(0, 2f, 0); // Subir la posición para evitar el suelo
        playerController.instance.transform.position = safeRespawn;

        Debug.Log("Jugador reaparecido en: " + safeRespawn); // Verificar la nueva posición

        if (charController != null)
        {
            charController.enabled = true;  // Reactivar CharacterController después de moverlo
        }

        //playerController.instance.GetComponent<CharacterController>().Move(Vector3.zero); // Detener cualquier movimiento

        cameraController.instance.cmBrain.enabled = true;
        playerController.instance.gameObject.SetActive(true);
    }

    public void SetSpawnPoint(Vector3 newSpawnPoint)
    {
        respawnPosition = newSpawnPoint;
        Debug.Log("Spawn Set at: " + respawnPosition);
    }
}
