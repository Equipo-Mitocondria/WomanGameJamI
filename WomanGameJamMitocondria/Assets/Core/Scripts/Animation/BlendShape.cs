using UnityEngine;

public class BlendShape : MonoBehaviour
{
    SkinnedMeshRenderer skinnedMeshRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        skinnedMeshRenderer.SetBlendShapeWeight(0, Sanity.Instance.SanityPercentage * 100);
        skinnedMeshRenderer.SetBlendShapeWeight(1, Sanity.Instance.SanityPercentage * 100);
    }
}
