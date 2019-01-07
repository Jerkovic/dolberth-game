using System;
using UnityEngine;
using Zenject;
using Dolberth.Player.Events;
using Dolberth.Managers.GameManager;

namespace Dolberth.Player
{
    public class CollisionComponent : MonoBehaviour
    {

        private Boolean isLevelCleared = false;
        private Boolean isHurting = false;

        public GameManager _gameManager;

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

            if (coll.gameObject.CompareTag("Goal") && coll.bounds.Contains(transform.position) && isLevelCleared == false)
            {
                EventManager.TriggerEvent("Player.CompleteLevel", null);
                transform.GetComponent<InputControllerComponent>().enabled = false;
                isLevelCleared = true;
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

            if (coll.gameObject.CompareTag("Enemy") && isHurting == false)
            {
                EventManager.TriggerEvent("Player.Hurt", null);
            }
        }
    }
}