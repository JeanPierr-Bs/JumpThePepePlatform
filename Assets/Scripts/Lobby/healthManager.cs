using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthManager : MonoBehaviour
{
    public static healthManager Instance;

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
        if (invincCounter > 0)
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
    public void Hurt()
    {
        if (invincCounter <= 0)
        {
            currentHealth--;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
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
    public void ResetHealth()
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
    public void UpdateUI()
    {
        //UIManager.Instance.stuffedLife.fillAmount = currentHealth / maxHealth;
        float scale = (float)currentHealth / maxHealth;
        UIManager.Instance.stuffedLife.transform.localScale = new Vector3(scale, 1, 1);
    }
    public void PlayerKilled()
    {
        currentHealth = 0;
        UpdateUI();
    }
}
