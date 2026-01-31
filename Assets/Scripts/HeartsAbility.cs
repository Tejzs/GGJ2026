using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Hearts")]
public class HeartsAbility : Ability
{
    public int att;
    protected override void Activate()
    {
        Debug.Log("Heart Ability!");
    }
}