using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public static gameManager instance;
    public int soundToPlay;
    public GameObject deathEffect;
    private Vector3 respawnPosition;
    public int currentCoin;
    public int currentKey;
    private List<GameObject> collectibles = new List<GameObject>(); //Optine los coleccionables

    private void Awake()
    {
        instance = this;
        collectibles = new List<GameObject>(GameObject.FindGameObjectsWithTag("Collectible"));
    }
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        respawnPosition = playerController.instance.transform.position;

        AddCoins(0);
        AddKey(0);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpase();
        }
    }
    public void Respawn() //Guarda la posicion del player cuando muere
    {
        StartCoroutine(RespawnWaiter());
        healthManager.Instance.PlayerKilled();
    }
    public IEnumerator RespawnWaiter()
    {
        Debug.Log("Jugador cayó fuera del mapa, reduciendo velocidad...");
        playerController.instance.SetMoveSpeed(2f); // Reducir la velocidad antes de reaparecer

        playerController.instance.gameObject.SetActive(false);//Desactiva al jugador
        cameraController.instance.cmBrain.enabled = false;
        UIManager.Instance.fadeToBlack = true;//Desactiva la UI

        //Activa el efecto del player
        Instantiate(deathEffect, playerController.instance.transform.position + new Vector3(0f, 1f, 0f), playerController.instance.transform.rotation);

        yield return new WaitForSeconds(2f);

        UIManager.Instance.fadeFromBlack = true;

        playerController.instance.transform.position = respawnPosition;//Restaura la posicion del jugador
        Debug.Log("El jugador acaba de aparecer en: " + respawnPosition);

        //Reactiva al jugador
        playerController.instance.SetMoveSpeed(25f); //Restaura la velocidad del jugador
        playerController.instance.gameObject.SetActive(true);
        cameraController.instance.cmBrain.enabled = true;
        healthManager.Instance.ResetHealth();//Resetea la vida del jugador
        AudioManager.instance.PlaySFX(soundToPlay);//Activa el sonido de Respawn

        ResetText();
        RespawnCollectibles();
    }
    public void RespawnCollectibles()
    {
        foreach (GameObject collectible in collectibles)
        {
            collectible.SetActive(true); // Reactiva los coleccionables
            Debug.Log("Los objetos se activaron");
        }
    }
    public void DisableCollectible(GameObject collectible)
    {
        if (collectibles.Contains(collectible))
        {
            collectible.SetActive(false);//Desactiva los coleccionables
            Debug.Log("Los objetos se desactivaron");
        }
    }
    public void SetSpawnPoint(Vector3 newSpawnPoint) //Punto donde aparecera el player
    {
        respawnPosition = newSpawnPoint;
        Debug.Log("Spawn Set");
    }

    public void AddCoins(int coinsToAdd) //Agrega los coins a la pantalla
    {
        currentCoin += coinsToAdd;
        UIManager.Instance.coinText.text = "" + currentCoin;
    }
    public void AddKey(int keyToAdd) //Agrega las keys a la pantalla
    {
        currentKey += keyToAdd;
        UIManager.Instance.keyText.text = "" + currentKey;
    }
    public void ResetText()
    {
        // Reiniciar los coleccionables en UI
        currentCoin = 0;
        UIManager.Instance.coinText.text = "" + currentCoin;

        currentKey = 0;
        UIManager.Instance.keyText.text = "" + currentKey;
    }
    public void PauseUnpase() //Pausa rl juego y abre el menu
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
