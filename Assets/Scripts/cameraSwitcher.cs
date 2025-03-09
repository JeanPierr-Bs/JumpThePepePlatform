using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraSwitcher : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook thirdPersonCamera;
    [SerializeField] private CinemachineVirtualCamera firstPersonCamera;

    private bool isFirstPerson = false;
    private void Start()
    {
        // Asegurar que el juego comience en tercera persona
        thirdPersonCamera.Priority = 10;
        firstPersonCamera.Priority = 0;
        Cursor.lockState = CursorLockMode.Locked; // Bloquear cursor
        Cursor.visible = false;
        playerController.instance.SetFirstPerson(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            isFirstPerson = !isFirstPerson;
            SwitchCamera();
        }
    }

    void SwitchCamera()
    {
        if (isFirstPerson)
        {
            firstPersonCamera.Priority = 10;
            thirdPersonCamera.Priority = 0;
            Cursor.lockState = CursorLockMode.Locked;//Bloquea el mouse
            Cursor.visible = false;
            playerController.instance.SetFirstPerson(true);
        }
        else
        {
            // Asegurar que el juego comience en tercera persona
            thirdPersonCamera.Priority = 10;
            firstPersonCamera.Priority = 0;
            Cursor.lockState = CursorLockMode.Locked; // Bloquear cursor
            Cursor.visible = false;
            playerController.instance.SetFirstPerson(false);

            // Alinear la cámara detrás del personaje
            thirdPersonCamera.m_XAxis.Value = playerController.instance.transform.eulerAngles.y;
            thirdPersonCamera.m_YAxis.Value = 0.5f; // Ajusta esto según la altura deseada

        }
    }

    
}
