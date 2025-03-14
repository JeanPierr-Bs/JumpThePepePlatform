using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class cameraController : MonoBehaviour
{
    public static cameraController instance;
    [SerializeField] public CinemachineBrain cmBrain;

    private void Awake()
    {
        instance = this;   
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
