using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravityScale = 5f;
    [SerializeField] private float mouseSensitivity = 2f;
    [SerializeField] private CharacterController charController;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private GameObject playerModel;
    [SerializeField] private Animator anim;
    // [SerializeField] private float knockBackLength = .5f;
    // [SerializeField] private float knockBackForce = 5f;
    public static playerController instance;
    private Vector3 moveDirection;
    //private Vector3 knowbackDirection;
    //public bool isKnocking;
    //private float knowBackCounter;
    private bool isFirstPerson = false;
    private float rotationX = 0f;


    void Start()
    {
        playerCamera = Camera.main;
    }

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
            if (isFirstPerson)
            {
                HandleFirstPersonView();
                HandleFirstPersonMovement();
            }
            else
            {
                HandleThirdPersonMovement();
            }

            moveDirection.y += (Physics.gravity.y * gravityScale) * Time.deltaTime;

            if (charController.isGrounded && Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpForce;
            }

            charController.Move(moveDirection * Time.deltaTime);

        // Actualizar animación
        anim.SetFloat("Speed", Mathf.Abs(moveDirection.x) + Mathf.Abs(moveDirection.z));
        anim.SetBool("Grounded", charController.isGrounded);
    }

    private void HandleThirdPersonMovement()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            Vector3 dir = playerCamera.transform.forward;
            dir.y = 0;
            transform.forward = dir.normalized;
        }

        Vector3 moveDir = (transform.forward * Input.GetAxisRaw("Vertical")) + (transform.right * Input.GetAxisRaw("Horizontal"));
        moveDirection.x = moveDir.x * moveSpeed;
        moveDirection.z = moveDir.z * moveSpeed;
    }

    private void HandleFirstPersonView()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        // Mantener la cámara en la posición de la cabeza
        //playerCamera.transform.position = firstPersonCameraPosition.position;
    }

    private void HandleFirstPersonMovement()
    {
        Vector3 move = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");
        moveDirection.x = move.x * moveSpeed;
        moveDirection.z = move.z * moveSpeed;
    }

    public void SetMoveSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }

    public void SetFirstPerson(bool enable)
    {
        isFirstPerson = enable;
        Cursor.lockState = enable ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !enable;
    }
}
