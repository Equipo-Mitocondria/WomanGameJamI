using UnityEngine;

[CreateAssetMenu(fileName = "SanityEffect", menuName = "Scriptable Objects/SanityEffect")]
public class SanityEffect : ScriptableObject
{
    [SerializeField] private SanityChange _change;
    [SerializeField] private float _amount;

    public SanityChange Effect { get { return _change; } }
    public float Amount {  get { return _amount; } }
}
