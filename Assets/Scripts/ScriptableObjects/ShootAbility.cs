using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Shoot")]
public class ShootAbility : Ability
{

    public int gunDamage = 1;
    public float weaponRange = 50f;
    public float hitForce = 100f;
    public Color laserColor = Color.white;

    public override void Initialize(GameObject obj)
    {

    }

    public override void TriggerAbility()
    {
        //rcShoot.Fire();
    }


}