using UnityEngine;

namespace DefaultNamespace
{
    public abstract class Pickable : MonoBehaviour
    {
        // Reference to the rigidbody
        private Rigidbody2D rb;
        public Rigidbody2D Rb => rb;
        /// <summary>
        /// Method called on initialization.
        /// </summary>
        private void Awake()
        {
            // Get reference to the rigidbody
            rb = GetComponent<Rigidbody2D>();
        }
    }
}