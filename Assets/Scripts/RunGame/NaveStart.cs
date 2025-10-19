using UnityEngine;
using Photon.Pun;
using ExitGames.Client.Photon;

public class NaveStart : MonoBehaviour
{
    void Start()
    {
        object roomNames;
        object roomPos;
        PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue("roomNames", out roomNames);
        PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue("roomPos", out roomPos);

        Hashtable roomNames_ = (Hashtable)roomNames;
        Hashtable roomPos_ = (Hashtable)roomPos;

        foreach (var room in roomNames_.Keys) {

            PhotonNetwork.Instantiate("NaveRooms/Rooms/" + (string)roomNames_[room], (Vector3)roomPos_[room], Quaternion.identity);
        }
    }
}
