using UnityEngine;
using ExitGames.Client.Photon;
using Photon.Pun;
using TMPro;

public class PlayerProps : MonoBehaviourPunCallbacks
{
    public static PlayerProps plProps;
    public Hashtable plProps_ = new Hashtable();


    [Header("Color")]
    public CollorPicker baseColor;
    public CollorPicker secundaryColor;

    [Header("NickName")]
    public TMP_InputField nickName;
    private void Awake()
    {
        plProps = this;
        PhotonCustomProp.RegisterColorType();
    }

    public void UpdateProps()
    {
        // ATUALIZA O NICKNAME ========================================================
        if(nickName != null)
        {
            if (nickName.text == "")
            {
                PhotonNetwork.NickName = System.Environment.UserName;
            }
            else
            {
                PhotonNetwork.NickName = nickName.text;
            }
        }

        //ATUALIZA AS CORES ============================================================
        if(baseColor != null && secundaryColor != null)
        {
            plProps_["Color1"] = baseColor.img.color;
            plProps_["Color2"] = secundaryColor.img.color;
        }

        PhotonNetwork.LocalPlayer.SetCustomProperties(plProps_);
    }
}
