using Dolberth.Managers.GameManager;
using UnityEngine;
using System;
using System.Collections.Generic;
using Zenject;


namespace Dolberth.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class InputControllerComponent : MonoBehaviour
    {

        private Rigidbody _rigidBody;
        private GameManager _gameManager;

        [Inject]
        private void Construct(GameManager gameManager)
        {
            _gameManager = gameManager;
        }

        /// <summary>
        /// Start this instance.
        /// </summary>
        void Start()
        {

            _rigidBody = GetComponent<Rigidbody>();
        }

        /// <summary>
        /// FixedUpdate.
        /// </summary>
        void FixedUpdate()
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
            _rigidBody.AddForce(movement * _gameManager.GetPlayerData().Speed);
        }
    }
}