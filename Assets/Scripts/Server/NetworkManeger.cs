using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using ExitGames.Client.Photon;

public class NetworkManeger : MonoBehaviourPunCallbacks
{
    public static NetworkManeger nt;

    [SerializeField] TextMeshProUGUI chatLog;

    [Space]

    [Header("Loby")]
    [SerializeField] GameObject homeGui;
    [SerializeField] TextMeshProUGUI roomCodeText;
    [SerializeField] TMP_InputField nickName;

    [Space]

    [Header("Room")]
    [SerializeField] GameObject roomGUI, play;
    [SerializeField] TextMeshProUGUI playerRoomList;

    private void Awake()
    {
        nt = this;
        DontDestroyOnLoad(this);
    }
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        chatLog.text += "Conectando no servidor... \n";
    }

    public override void OnConnectedToMaster() // CONECTA NO SERVIDOR
    {
        base.OnConnectedToMaster();

        chatLog.text += "Conectou no servidor \n";
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby() // ENTRA NO LOBY
    {
        base.OnJoinedLobby();
        chatLog.text += "Entrou no loby \n";
        homeGui.SetActive(true);
        nickName.text = "";
    }

    public void CreateRoom() // CRIA UMA SALA COM UM CÓDIGO
    {
        string alfabeto = "abcdefghijklmnopqrstuvwxyz";
        string roomName = "";
        int limitCode = Random.Range(3, 7);

        for (int x=0; x < limitCode; x++)
        {
            int Uppercase = Random.Range(0, 2);
            if (Uppercase == 1)
            {
                roomName+= alfabeto[Random.Range(0, alfabeto.Length)];
            }
            else
            {
                roomName += char.ToUpper(alfabeto[Random.Range(0, alfabeto.Length)]);
            }
        }

        PlayerProps.plProps.UpdateProps();

        PhotonNetwork.CreateRoom(roomName);
    }

    public void JoinRoom(TMP_InputField roomCode) // ENTRA NUMA SALA
    {
        PlayerProps.plProps.UpdateProps();

        PhotonNetwork.JoinRoom(roomCode.text);
    }

    public override void OnJoinedRoom() // SEMPRE QUE O CLIENT ENTRAR NA SALA
    {
        base.OnJoinedRoom();

        PhotonNetwork.AutomaticallySyncScene = true;

        if (PhotonNetwork.IsMasterClient)
        {
            chatLog.text += "Criou a sala \n";
        }
        else
        {
            chatLog.text += "Entrou na sala de " + PhotonNetwork.MasterClient.NickName +"\n";
        }
        roomGUI.SetActive(true);
        roomCodeText.text = PhotonNetwork.CurrentRoom.Name;

        UpdatePlayerList();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer) // SEMPRE QUE ALGUEM NOVO ENTRA NA SALA
    {
        base.OnPlayerEnteredRoom(newPlayer);

        UpdatePlayerList();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);

        UpdatePlayerList();
    }

    void UpdatePlayerList()
    {
        play.SetActive(PhotonNetwork.LocalPlayer.IsMasterClient);

        playerRoomList.text = null;
        Player[] playerList = PhotonNetwork.PlayerList;
        for (int x = 0; x < playerList.Length; x++)
        {
            playerRoomList.text += playerList[x].NickName + "\n";
        }
    }

    public void StartGame()
    {

        PhotonNetwork.LoadLevel("Game");
    }

    public void CopyCode(TextMeshProUGUI text)
    {
        GUIUtility.systemCopyBuffer = text.text;
    }

    public GameObject GetPlayerInScene()
    {
        if (PhotonNetwork.InRoom)
        {
            GameObject player = null;

            foreach (PlController pl in FindObjectsOfType<PlController>())
            {
                if (pl.GetComponent<PhotonView>().IsMine)
                {
                    player = pl.gameObject;
                }
            }

            return player;
        }
        else return null;
    }
}
