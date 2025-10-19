using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoadPlayer : MonoBehaviour
{
    [SerializeField] bool TrasnfeereCanvaCam;
    [SerializeField] GameObject placeHollderCam;
    [SerializeField] Canvas canvas;

    GameObject localPlayerObj;
    private void Awake()
    {
        localPlayerObj = PhotonNetwork.Instantiate(Path.Combine("Players", "Player"), transform.position, Quaternion.identity);
    }

    private void Start()
    {
        if (TrasnfeereCanvaCam)
        {
            canvas.worldCamera = localPlayerObj.GetComponent<PlController>().playerCam;
            placeHollderCam.SetActive(false);
        }
    }
}
