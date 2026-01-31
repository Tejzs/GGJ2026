using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Hearts")]
public class HeartsAbility : Ability
{
    protected override void Activate()
    {
        Debug.Log("Club Ability!");
    }
}