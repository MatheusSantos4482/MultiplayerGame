using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class CollorPicker : MonoBehaviourPunCallbacks
{
    [SerializeField] Slider Rslider;
    [SerializeField] Slider Gslider;
    [SerializeField] Slider Bslider;
    public Image img;
    [SerializeField] Renderer[] render;

    private void Update()
    {
        img.color = new Color(Rslider.value, Gslider.value, Bslider.value);

        // ATUALIZA AS CORES ============================================================
        foreach (Renderer rend in render) // Cor Base
        {
            rend.material.SetColor("_Color", img.color);
        }
    }
}
