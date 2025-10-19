using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class PlMovement : MonoBehaviourPunCallbacks
{
    [Header("Movement")]
    public Transform cam;
    [SerializeField] private float speed;
    [SerializeField] float mouseSensitivity;
    private float xRotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Physics.IgnoreLayerCollision(6, 6, true);
    }

    public void Movement()
    {
        float moveX = Input.GetAxis("Horizontal")* speed * Time.deltaTime;
        float moveZ = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        transform.Translate(moveX, 0, moveZ);
    }

    void CamControll()
    {
        // Pega o input do mouse
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotação vertical da câmera (olhar para cima/baixo)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Limita a rotação

        cam.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rotação horizontal do corpo do jogador (girar o corpo)
        transform.Rotate(Vector3.up * mouseX);
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            Movement();
            CamControll();
        }
        else
        {
            cam.gameObject.SetActive(false);
            this.enabled = false;
        }
    }

}