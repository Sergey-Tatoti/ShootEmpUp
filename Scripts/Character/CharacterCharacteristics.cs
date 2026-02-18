using UnityEngine;

[CreateAssetMenu( fileName = "Characteristics", menuName = "Character/New Characteristics" )]

public sealed class CharacterCharacteristics : ScriptableObject
{
    [SerializeField] public int Health;
    [SerializeField] public int MoveSpeed;
}
