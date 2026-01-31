using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Spade")]
public class SpadeAbility : Ability
{
    protected override void Activate()
    {
        Debug.Log("Spade Ability!");
    }
}