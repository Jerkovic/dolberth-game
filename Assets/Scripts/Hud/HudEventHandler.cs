using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Dolberth.Player.Events;
using Zenject;

namespace Dolberth.Hud
{
    public class HudEventHandler : MonoBehaviour
    {

        public Text coinText;
        public GameObject coinUiPrefab;
        public GameObject hudWindow;
        public Image healthBar;

        private Animation _animation;


        /// <summary>
        /// On enable.
        /// </summary>
        void OnEnable()
        {

            EventManager.StartListening("Player.PickupCoin", OnPickupCoin);
            EventManager.StartListening("Player.CompleteLevel", OnPlayerCompleteLevel);
            EventManager.StartListening("Player.Hurt", OnPlayerHurt);
        }

        /// <summary>
        /// On disable.
        /// </summary>
        void OnDisable()
        {

            EventManager.StopListening("Player.PickupCoin", OnPickupCoin);
            EventManager.StopListening("Player.CompleteLevel", OnPlayerCompleteLevel);
            EventManager.StopListening("Player.Hurt", OnPlayerHurt);
        }

        /// <summary>
        /// Start of this instance.
        /// </summary>
        void Start()
        {
            _animation = coinText.GetComponent<Animation>();
        }

        /// <summary>
        /// Raises the player hurt event.
        /// </summary>
        /// <param name="eventParam">Event parameter.</param>
        void OnPlayerHurt(IEventParam eventParam)
        {
            EventPlayerDamage playerDamage = (EventPlayerDamage)eventParam;
            healthBar.fillAmount = playerDamage.health / playerDamage.maxHealth;
        }

        /// <summary>
        /// Raises the pickup coin event.
        /// </summary>
        /// <param name="eventParam">Event parameter.</param>
        void OnPickupCoin(IEventParam eventParam)
        {

            SoundManager.instance.PlaySoundByName("coin_pickup");
            EventPlayerPickUpCoin eventPickup = (EventPlayerPickUpCoin)eventParam;

            Vector3 screenPos = UnityEngine.Camera.main.WorldToScreenPoint(eventPickup.position);
            GameObject coin = Instantiate(coinUiPrefab, screenPos, Quaternion.identity);
            coin.transform.SetParent(gameObject.transform);

            coin.transform.DOMove(coinText.transform.position, 1f)
                .SetEase(Ease.OutExpo)
                .OnComplete(() =>
                {
                    coinText.text = String.Format("{0:00000}", eventPickup.totalCoins);
                    Destroy(coin);
                    _animation.Play("hud_coins_inc");
                });
        }

        /// <summary>
        /// Update Hud on player complete level event.
        /// </summary>
        void OnPlayerCompleteLevel(IEventParam eventParam)
        {

            hudWindow.transform.localScale = Vector3.zero;
            hudWindow.SetActive(true);

            SoundManager.instance.PlaySoundByName("level_complete");
            Sequence mySequence = DOTween.Sequence();
            mySequence.Append(hudWindow.transform.DOScale(1f, 1.5f).SetEase(Ease.OutExpo));
            mySequence.AppendInterval(10);
            mySequence.Append(hudWindow.transform.DOScale(0f, 1f).SetEase(Ease.InExpo));
        }
    }
}