using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Club")]
public class ClubAbility : Ability
{
    
    protected override void Activate()
    {
        Debug.Log("Club Ability!");
    }
}