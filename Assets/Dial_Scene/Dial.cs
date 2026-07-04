using UnityEngine;

[CreateAssetMenu(fileName = "Dial", menuName = "Scriptable Objects/Dial")]

public class Dial : ScriptableObject
{
    [SerializeField]
    public Dialogue[] dials;

    [SerializeField]
    public Choice[] choices;
}
