using System;
using UnityEngine;
using Zenject;
using Dolberth.Player.Events;
using Dolberth.Managers.GameManager;

namespace Dolberth.Player
{
    public class CollisionComponent : MonoBehaviour
    {

        private GameManager _gameManager;

        [Inject]
        private void Construct(GameManager gameManager)
        {
            _gameManager = gameManager;

        }

        /// <summary>
        /// Raises the trigger stay event.
        /// </summary>
        void OnTriggerStay(Collider coll)
        {

            if (coll.gameObject.CompareTag("Goal") && coll.bounds.Contains(transform.position))
            {

                EventManager.TriggerEvent("Player.CompleteLevel", null);
                transform.GetComponent<InputControllerComponent>().enabled = false;
                Destroy(coll.gameObject);
            }
        }

        /// <summary>
        /// Raises the trigger enter event.
        /// </summary>
        void OnTriggerEnter(Collider coll)
        {

            if (coll.gameObject.CompareTag("Coin"))
            {

                int x = (int)coll.transform.position.x;
                int y = (int)coll.transform.position.z;


                if (_gameManager.GetMaze().PickUpCoin(x, y))
                {
                    _gameManager.GetPlayerData().AddCoin();

                    EventPlayerPickUpCoin coinEvent = new EventPlayerPickUpCoin()
                    {
                        totalCoins = _gameManager.GetPlayerData().Coins,
                        position = coll.gameObject.transform.position,
                        coin = coll.gameObject
                    };

                    EventManager.TriggerEvent("Player.PickupCoin", coinEvent);
                    Destroy(coll.gameObject);
                }
            }

            if (coll.gameObject.CompareTag("Enemy"))
            {
                _gameManager.GetPlayerData().TakeDamage(1f);
                EventPlayerDamage damageEvent = new EventPlayerDamage()
                {
                    health = 0,
                    maxHealth = 0
                };

                EventManager.TriggerEvent("Player.Hurt", damageEvent);
            }
        }
    }
}