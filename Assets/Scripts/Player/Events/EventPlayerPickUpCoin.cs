using UnityEngine;

namespace Dolberth.Player.Events
{
	public class EventPlayerPickUpCoin : IEventParam
	{
		public int numCoins;
		public int totalCoins;
		public Vector3 position;
		public GameObject coin;
	}
}