using UnityEngine;

public class BlendShape : MonoBehaviour
{
    SkinnedMeshRenderer skinnedMeshRenderer;
    Mesh skinnedMesh;
    [SerializeField] float blendOne = 0f;
    [SerializeField] float blendSpeed = 1f;
    [SerializeField] bool isClosing = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        skinnedMesh = GetComponent<SkinnedMeshRenderer>().sharedMesh;
    }

    // Update is called once per frame
    void Update()
    {
        if (isClosing)
        {
            skinnedMeshRenderer.SetBlendShapeWeight(0, blendOne);
            blendOne += blendSpeed;
            if (blendOne >= 100f)
                isClosing = false;
        }
        else
        {
            skinnedMeshRenderer.SetBlendShapeWeight(0, blendOne);
            blendOne -= blendSpeed;
            if (blendOne <= 0f)
                isClosing = true;
        }
    }
}
