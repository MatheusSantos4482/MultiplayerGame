using UnityEngine;

[ExecuteInEditMode]
public class PixelateEffect : MonoBehaviour
{
    public Shader shader;
    [Range(1, 512)] public int pixelSize = 128;

    private Material _mat;

    void Start()
    {
        if (shader == null)
            shader = Shader.Find("Hidden/Pixelate");

        if (shader != null && _mat == null)
            _mat = new Material(shader);
    }

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (_mat == null)
        {
            Graphics.Blit(src, dest);
            return;
        }

        _mat.SetFloat("_PixelSize", pixelSize);
        Graphics.Blit(src, dest, _mat);
    }
}
