using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dolberth.Player.Models
{
    public class PlayerData
    {

        public float Speed { set; get; }
        public int Coins { set; get; }

        public void AddCoin()
        {
            Coins += 1;
        }
    }
}