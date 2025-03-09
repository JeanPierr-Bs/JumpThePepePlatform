using System.Collections;
using System.Collections.Generic;
using UnityEditor.Recorder;
using UnityEngine;

public class healthManager : MonoBehaviour
{
    public static healthManager Instance;

    //public int currentHealth, maxHealth;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        //currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {  
    }

    /*public void Hurt(Vector3 damageSource)
    {
        currentHealth--;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            gameManager.instance.Respawn();
        }
    }*/
}
