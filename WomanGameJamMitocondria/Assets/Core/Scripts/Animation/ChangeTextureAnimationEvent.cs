using UnityEngine;

public class ChangeTextureAnimationEvent : MonoBehaviour
{
    [SerializeField] private Texture[] _textures;
    private Renderer _renderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void ChangeRenderTexture(int index)
    {
        _renderer.material.SetTexture("_BaseMap", _textures[index]);
    }
}
