using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthManager : MonoBehaviour
{
    public static healthManager Instance;

    public int currentHealth, maxHealth;

    private void Awake()
    {
        Instance = this;
    }
    
    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {  
    }

    public void Hurt(Vector3 damageSource)
    {
        currentHealth--;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            gameManager.instance.Respawn();
        }
    }
}
