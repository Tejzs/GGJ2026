using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Diamond")]
public class DiamondAbility : Ability
{
    
    protected override void Activate()
    {
        Debug.Log("Diamond Ability!");
    }
}