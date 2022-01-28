using UnityEngine;

public class EnemyProjectile
{
        [SerializeField] private float stunTime;
        [SerializeField] private float moveSpeed;

        public float GetStunTime()
        {
                return stunTime;
        }
}
