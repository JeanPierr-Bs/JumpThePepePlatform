using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public static gameManager instance;

    public int soundToPlay;

    private Vector3 respawnPosition;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        respawnPosition = playerController.instance.transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpase();
        }
    }

    public void Respawn()
    {
        StartCoroutine(RespawnWaiter());
    }

    public IEnumerator RespawnWaiter()
    {
        Debug.Log("Jugador cayó fuera del mapa, reduciendo velocidad...");

        // Reducir la velocidad antes de reaparecer
        playerController.instance.SetMoveSpeed(2f); // Velocidad baja antes del respawn

        //Desactiva al jugador
        playerController.instance.gameObject.SetActive(false);
        cameraController.instance.cmBrain.enabled = false;

        //Desactiva la UI
        UIManager.Instance.fadeToBlack = true;

        yield return new WaitForSeconds(2f);

        UIManager.Instance.fadeFromBlack = true;

        //Restaura la posicion del jugador
        playerController.instance.transform.position = respawnPosition;
        Debug.Log("El jugador acaba de aparecer en: " + respawnPosition);

        //Restaura la velocidad del jugador
        playerController.instance.SetMoveSpeed(25f); // Restaurar velocidad normal

        //Reactiva al jugador
        playerController.instance.gameObject.SetActive(true);
        cameraController.instance.cmBrain.enabled = true;

        //Activa el sonido de Respawn
        AudioManager.instance.PlaySFX(soundToPlay);
    }

    public void SetSpawnPoint(Vector3 newSpawnPoint)
    {
        respawnPosition = newSpawnPoint;
        Debug.Log("Spawn Set");
    }

    public void PauseUnpase()
    {
        if (UIManager.Instance.pauseScreen.activeInHierarchy)
        {
            UIManager.Instance.pauseScreen.SetActive(false);
            Time.timeScale = 1f;

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            // Habilitar movimiento del jugador
            playerController.instance.enabled = true;

        }
        else
        {
            UIManager.Instance.pauseScreen.SetActive(true);
            UIManager.Instance.CloseOptions();
            Time.timeScale = 0f;

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            // Deshabilitar movimiento del jugador
            playerController.instance.enabled = false;

        }
    }
}
