using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlCarry : MonoBehaviour
{
    [Header("Carry")]
    public Transform carryPlace;
    [SerializeField] GameObject interactMsg;
    [SerializeField] float interactDistance;
    [SerializeField] LayerMask interactLayer;
    [SerializeField] PlMovement plMove;

    private void Update()
    {
        PickUp();
    }

    bool carryng;
    GameObject carryObj;
    void PickUp()
    {
        Ray ray = new Ray(plMove.cam.transform.position, plMove.cam.transform.forward);
        RaycastHit rayHit;
        interactMsg.SetActive((Physics.Raycast(ray, out rayHit, interactDistance, interactLayer) && !carryng));

        if(Input.GetButtonDown("Interact") && carryng)
        {
            gameObject.GetComponent<PhotonView>().RPC("DropUpdate", RpcTarget.All, carryObj.GetComponent<PhotonView>().ViewID);
            carryObj = null;
            carryng = false;

            return;
        }

        if (Input.GetButtonDown("Interact") && interactMsg.activeSelf)
        {
            carryObj = rayHit.transform.gameObject;
            gameObject.GetComponent<PhotonView>().RPC("PickUpPropUpdate", RpcTarget.All, gameObject.GetComponent<PhotonView>().ViewID, carryObj.GetComponent<PhotonView>().ViewID);
            carryng = true;
        }
    }

    [PunRPC]
    void PickUpPropUpdate(int playerIdView, int objIdView)
    {
        GameObject carryObjOnPlayer = PhotonView.Find(objIdView).gameObject;
        Player playerOwner = PhotonView.Find(playerIdView).Owner;
        Transform propPlace = PhotonView.Find(playerIdView).GetComponent<PlCarry>().carryPlace;

        carryObjOnPlayer.GetComponent<PhotonView>().TransferOwnership(playerOwner);

        carryObjOnPlayer.transform.SetParent(propPlace);
        carryObjOnPlayer.transform.localPosition = Vector3.zero; carryObjOnPlayer.transform.localRotation = Quaternion.identity;

        carryObjOnPlayer.GetComponent<Rigidbody>().isKinematic = true;
        carryObjOnPlayer.GetComponent<Collider>().enabled = false;
    }

    [PunRPC]
    void DropUpdate(int objIdView)
    {
        GameObject carryObjOnPlayer = PhotonView.Find(objIdView).gameObject;

        carryObjOnPlayer.transform.SetParent(null);

        carryObjOnPlayer.GetComponent<Rigidbody>().isKinematic = false;
        carryObjOnPlayer.GetComponent<Collider>().enabled = true;
    }
}
