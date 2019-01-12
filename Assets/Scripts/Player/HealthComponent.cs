using Dolberth.Enemy;
using Dolberth.ScriptableObjects.Variables;
using UnityEngine;
using UnityEngine.Events;

public class HealthComponent : MonoBehaviour
{
    public FloatVariable HP;
    public bool ResetHealthOnStart;
    public FloatReference StartingHP;
    public UnityEvent DeathEvent;
    public float invincibleTime;

    private float nextTime = 0;

    /// <summary>
    /// Start this instance.
    /// </summary>
    private void Start()
    {

        if (ResetHealthOnStart)
            HP.SetValue(StartingHP);
    }

    /// <summary>
    /// Takes the damage.
    /// </summary>
    /// <returns>The damage really taken.</returns>
    /// <param name="damage">Damage.</param>
    public float TakeDamage(float damage)
    {

        if (Time.time > nextTime)
        {
            HP.ApplyChange(-damage);
            nextTime = Time.time + invincibleTime;
            Debug.Log("apply health change " + Time.time + ", " + nextTime);

            if (HP.Value <= 0.0f)
            {
                DeathEvent.Invoke();
            }
            return damage;
        }
        else
        {
            return 0;
        }

    }
}
