using UnityEngine;
using Photon.Pun;
using ExitGames.Client.Photon;

// Uma classe estática para registrar os tipos personalizados
public static class PhotonCustomProp
{
    private static bool _isRegistered = false;

    // O método principal para chamar o registro
    public static void RegisterColorType()
    {
        if (_isRegistered)
        {
            return;
        }

        // Registra o tipo Color com um código de identificação ('C')
        PhotonPeer.RegisterType(typeof(Color), (byte)'C', SerializeColor, DeserializeColor);
        _isRegistered = true;
    }

    // Método que converte o objeto Color em um array de bytes
    private static byte[] SerializeColor(object customObject)
    {
        Color color = (Color)customObject;
        int size = 4 * sizeof(float);
        byte[] bytes = new byte[size];
        int index = 0;

        Protocol.Serialize(color.r, bytes, ref index);
        Protocol.Serialize(color.g, bytes, ref index);
        Protocol.Serialize(color.b, bytes, ref index);
        Protocol.Serialize(color.a, bytes, ref index);

        return bytes;
    }

    // Método que reconstrói o objeto Color a partir dos bytes
    private static object DeserializeColor(byte[] bytes)
    {
        Color color = new Color();
        int index = 0;

        Protocol.Deserialize(out color.r, bytes, ref index);
        Protocol.Deserialize(out color.g, bytes, ref index);
        Protocol.Deserialize(out color.b, bytes, ref index);
        Protocol.Deserialize(out color.a, bytes, ref index);

        return color;
    }
}