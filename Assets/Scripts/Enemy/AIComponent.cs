using UnityEngine.AI;
using UnityEngine;
using Dolberth.Maze.Models;
using Zenject;
using Dolberth.Managers.GameManager;

namespace Dolberth.Enemy
{
    public class AIComponent : MonoBehaviour
    {

        private NavMeshAgent _agent;
        private GameManager _gameManager;

        [Inject]
        private void Construct(GameManager gameManager)
        {
            _gameManager = gameManager;
        }

        /// <summary>
        /// Awake this instance.
        /// </summary>
        private void Awake()
        {
            _agent = transform.GetComponent<NavMeshAgent>();

        }

        /// <summary>
        /// Start this instance.
        /// </summary>
        private void Start()
        {

            _setNewRandomAgentDestination();
        }

        /// <summary>
        /// Sets a new random agent destination.
        /// </summary>
        private void _setNewRandomAgentDestination()
        {

            Cell destinationCell = _gameManager.GetMaze().FindRandomEmptyCell();
            _agent.SetDestination(new Vector3(destinationCell.x, .5f, destinationCell.z));
        }

        /// <summary>
        /// Update this instance.
        /// </summary>
		private void Update()
        {

            _hasAgentReachedDestination();
        }

        /// <summary>
        /// Collision trigger enter. Move this
        /// </summary>
        /// <param name="coll">Coll.</param>
		private void OnTriggerEnter(Collider coll)
        {
            if (coll.gameObject.CompareTag("Coin"))
            {
                int x = (int)coll.transform.position.x;
                int y = (int)coll.transform.position.z;

                if (_gameManager.GetMaze().PickUpCoin(x, y))
                {
                    Destroy(coll.gameObject);
                }
            }
        }

        /// <summary>
        /// Has the agent reached destination.
        /// </summary>
        private void _hasAgentReachedDestination()
        {
            float distanceToTarget = Vector3.Distance(transform.position, _agent.destination);
            if (distanceToTarget < .5f)
            {
                _setNewRandomAgentDestination();
            }
        }
    }
}