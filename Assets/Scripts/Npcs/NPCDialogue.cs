using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public GameObject dialoguePanel;//Panel del dialogo
    public TextMeshProUGUI dialogueText;//Texto del dialogo
    public string[] dialogueLines; //Lineas de adialogo
    private int currentLine = 0;

    private bool playerInRange;
    private playerController playerMovement; // Referencia al script del jugador
    private Animator playerAnimator; // Referencia al Animator del jugador

    private void Start()
    {
        dialoguePanel.SetActive(false); //panel desactivado
    }
    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Jugador presionó E para hablar");
            if (dialoguePanel.activeInHierarchy)
            {
                NextDialogue();
            }
            else
            {
                StartDialogue();
            }
        }
    }
    private void StartDialogue()
    {
        dialoguePanel.SetActive(true);
        dialogueText.text = dialogueLines[currentLine];
        // Desactivar el movimiento del jugador
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }
        // Cambiar la animación del jugador a Idle
        if (playerAnimator != null)
        {
            playerAnimator.SetFloat("Speed", 0); // Asumiendo que "Speed" controla la animación de caminar
            Debug.Log("Se forzó la animación de Idle.");
        }
    }
    private void NextDialogue()
    {
        currentLine++;

        if (currentLine < dialogueLines.Length)
        {
            dialogueText.text = dialogueLines[currentLine];
        }
        else
        {
            dialoguePanel.SetActive(false);
            currentLine = 0; // Resetear para la próxima vez

            // Reactivar el movimiento del jugador
            if (playerMovement != null)
            {
                playerMovement.enabled = true;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Jugador ha entrado en el trigger del NPC");
            playerInRange = true;
            playerMovement = other.GetComponent<playerController>(); // Obtener el script del jugador
            playerAnimator = other.GetComponentInChildren<Animator>(); // Obtener el Animator del jugador
            if (playerAnimator != null)
            {
                Debug.Log("Animator encontrado correctamente.");
            }
            else
            {
                Debug.LogError("ERROR: No se encontró el Animator en el Player.");
            }

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Jugador ha salido del trigger del NPC");
            playerInRange = false;
            // Asegurarse de reactivar el movimiento en caso de que el jugador salga mientras el diálogo está activo
            if (playerMovement != null)
            {
                playerMovement.enabled = true;
            }
        }
    }
}
