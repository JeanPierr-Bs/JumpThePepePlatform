using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravityScale = 5f;
    //[SerializeField] private float rotateSpeed = 5f;
    [SerializeField] private CharacterController charController;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private GameObject playerModel;
    [SerializeField] private Animator anim;
    public static playerController instance;
    private Vector3 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        playerCamera = Camera.main;
    }

    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            Vector3 dir = playerCamera.transform.forward;
            dir.y = 0;
            transform.forward = dir.normalized;
        }
        moveDirection.y += (Physics.gravity.y * gravityScale) * Time.deltaTime;

        Vector3 moveDir = (transform.forward * Input.GetAxisRaw("Vertical")) + (transform.right * Input.GetAxisRaw("Horizontal"));
        moveDirection.x = moveDir.x * moveSpeed;
        moveDirection.z = moveDir.z * moveSpeed;

        if (charController.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpForce;
            }
        }

        charController.Move(moveDirection * Time.deltaTime);

        //Actualizar animacion
        anim.SetFloat("Speed", Mathf.Abs(moveDirection.x) + Mathf.Abs(moveDirection.z));
        anim.SetBool("Grounded", charController.isGrounded);
    }
}
