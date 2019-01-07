using UnityEngine;

namespace Dolberth.Generic
{
    public class SpinComponent : MonoBehaviour
    {

        public float speed = 20f;

        /// <summary>
        /// Start this instance.
        /// </summary>
        private void Start()
        {
            transform.Rotate(Vector3.forward, Random.Range(1, 360));
        }
        /// <summary>
        /// Update this instance.
        /// </summary>
        void Update()
        {
            transform.Rotate(Vector3.forward, speed * Time.deltaTime);
        }
    }
}
