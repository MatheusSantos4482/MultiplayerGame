using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class PlController : MonoBehaviourPunCallbacks
{
    private PlMovement plMove;

    [Header("Player Props")]
    [SerializeField] Renderer[] Baserender;
    [SerializeField] Renderer[] Secundaryrender;
    [SerializeField] TextMeshProUGUI nickNameText;

    [Header("UI Interaction")]
    public Camera playerCam;
    [SerializeField] float interactDistance;
    [SerializeField] LayerMask interactLayer;

    [HideInInspector] public RaycastHit hit;
    [HideInInspector] public bool looking;

    void Start()
    {
        plMove = GetComponent<PlMovement>();


        // ATUALIZA AS CORES ============================================================
        foreach(Renderer rend in Baserender) // Cor Base
        {
            object color;
            photonView.Owner.CustomProperties.TryGetValue("Color1", out color);
            rend.material.SetColor("_Color", (Color)color);
        }
  
        foreach (Renderer rend in Secundaryrender) // Cor Base
        {
            object color;
            photonView.Owner.CustomProperties.TryGetValue("Color2", out color);
            rend.material.SetColor("_Color", (Color)color);
        }

        // ATUALIZA O NICKNAME DO PLAYER ================================================
        nickNameText.text = photonView.Owner.NickName;
    }

    private void Update()
    {
        WorldUI();

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.P))
        {
            EditorApplication.isPaused = !EditorApplication.isPaused;
        }
#endif
    }

    void WorldUI()
    {
        Ray ray = new Ray(playerCam.transform.position, playerCam.transform.forward);
        looking = Physics.Raycast(ray, out hit, interactDistance, interactLayer);

        if (looking && Input.GetKeyDown(KeyCode.Mouse0))
        {
            Button btn;

            if (hit.collider.TryGetComponent<Button>(out btn))
            {
                btn.onClick.Invoke();
            }
        }
    }
}
