using UnityEngine;

public abstract class Ability : ScriptableObject
{

    public string abilityName = "Abstract Ability";
    public float baseCoolDown = 5f;

    public abstract void Initialize(GameObject obj);
    public abstract void TriggerAbility();
}