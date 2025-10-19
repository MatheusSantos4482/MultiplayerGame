using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCreator : MonoBehaviour
{
    private PlController localPlayer;
    [SerializeField] GameObject roomPanel;
    private void Start()
    {
        localPlayer = NetworkManeger.nt.GetPlayerInScene().GetComponent<PlController>();
    }

    public void CreateRoomUI(GameObject room)
    {
        GameObject roomInstantiated = PhotonNetwork.Instantiate("UI/" + room.name, localPlayer.hit.point, Quaternion.identity);
        roomInstantiated.transform.SetParent(roomPanel.transform);
        roomInstantiated.GetComponent<RoomUI>().player = localPlayer;
        roomInstantiated.GetComponent<RoomUI>().panel = roomPanel.transform;

        roomInstantiated.transform.localScale = room.transform.localScale;
    }
}
