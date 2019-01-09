using UnityEngine;

namespace Dolberth.Camera
{

    public class FollowTargetComponent : MonoBehaviour
    {

        public Transform target;
        public float distance = 3.0f;
        public float height = 4.0f;
        public float heightDamping = 1.0f;

        /// <summary>
        /// Late update.
        /// </summary>
        void LateUpdate()
        {
            if (!target) return;

            float wantedHeight = target.position.y + height;
            float currentHeight = transform.position.y;

            currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

            transform.position = target.position;
            transform.position -= Vector3.forward * distance;

            transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);

            transform.LookAt(target);
        }
    }
}