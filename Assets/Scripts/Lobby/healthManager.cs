using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthManager : MonoBehaviour
{
    public static healthManager Instance;
    public delegate void PlayerDied();
    public static event PlayerDied OnPlayerDeath; // Evento de muerte

    [SerializeField] private int currentHealth, maxHealth;
    [SerializeField] private float invicibleLength = 2f;
    private float invincCounter;


    
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        ResetHealth();
    }
    void Update()
    {  
        if (invincCounter > 0) //Activa y desactiva las piezas del player cuando resive daño
        {
            invincCounter -= Time.deltaTime;
            for (int i = 0; i < playerController.instance.playerPieces.Length; i++)
            {
                if(Mathf.Floor(invincCounter * 5f) % 2 == 0)
                {
                    playerController.instance.playerPieces[i].SetActive(true);
                }
                else
                {
                    playerController.instance.playerPieces[i].SetActive(false);
                }

                if (invincCounter <= 0)
                {
                    playerController.instance.playerPieces[i].SetActive(true);
                }
            }
        }
    }
    public void Hurt() //Daño al player
    {
        if (invincCounter <= 0)
        {
            currentHealth--;

            if (currentHealth <= 0)
            {
                PlayerKilled();
                gameManager.instance.Respawn();
            }
            else
            {
                playerController.instance.Knockback();
                invincCounter = invicibleLength;
            }
        }
        UpdateUI();
    }
    public void ResetHealth() //Actualiza la vida del player
    {
        currentHealth = maxHealth;
        UpdateUI();
    }
    public void AddHealth(int amountToHealth)
    {
        currentHealth += amountToHealth;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UpdateUI();
    }
    public void UpdateUI() //Actualiza el estado del texto en la vida
    {
       UIManager.Instance.healtText.text = currentHealth.ToString();
    }
    public void PlayerKilled()  //Muerte del player
    {
        currentHealth = 0;
        UpdateUI();

        OnPlayerDeath?.Invoke(); // Notificar a los enemigos
    }
}
