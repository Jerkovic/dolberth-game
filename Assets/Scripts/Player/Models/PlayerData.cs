using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dolberth.Player.Models
{
    public class PlayerData : ScriptableObject
    {
     
        public float Speed { set; get; }
        public int Coins { set; get; }
        //public float Health { set; get; }
        //public float MaxHealth { set; get; }

        /// <summary>
        /// Adds the coin.
        /// </summary>
        public void AddCoin()
        {
            Coins += 1;
        }

        /// <summary>
        /// Takes the damage.
        /// </summary>
        /// <param name="damage">Damage.</param>
        public void TakeDamage(float damage)
        {
            //Health -= damage;
        }
    }
}