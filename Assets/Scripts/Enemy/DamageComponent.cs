using UnityEngine;
using Dolberth.ScriptableObjects.Variables;
using Dolberth.Player.Events;
using Dolberth.Managers.GameManager;
using Zenject;
using UnityEngine.Events;

namespace Dolberth.Enemy
{
    public class DamageComponent : MonoBehaviour
    {
        public FloatReference DamageAmount;
        public UnityEvent DamageEvent;

        /// <summary>
        /// Collision trigger enter
        /// </summary>
        /// <param name="other">Other</param>
        void OnTriggerEnter(Collider other)
        {

            HealthComponent health = other.gameObject.GetComponent<HealthComponent>();
            if (health != null)
            {
                float damageTaken = health.TakeDamage(DamageAmount.Value);

                // DamageEvent.Invoke();
                if (other.gameObject.CompareTag("Player") && damageTaken > 0f)
                {
                    EventPlayerDamage damageEvent = new EventPlayerDamage()
                    {
                        health = health.HP.Value,
                        maxHealth = health.StartingHP
                    };

                    EventManager.TriggerEvent("Player.Hurt", damageEvent);
                }
            }

            // old code

        }
    }
}