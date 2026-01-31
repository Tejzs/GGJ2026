using UnityEngine;

public abstract class Ability : ScriptableObject
{
    public string abilityName;
    public float cooldown;

    protected float lastUsedTime;

    public bool CanUse()
    {
        return Time.time >= lastUsedTime + cooldown;
    }

    public void TryUse(GameObject player)
    {
        if (!CanUse()) return;

        Activate();
        lastUsedTime = Time.time;
    }

    protected abstract void Activate();
}