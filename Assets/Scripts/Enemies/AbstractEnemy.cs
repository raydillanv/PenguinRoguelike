using UnityEngine;

namespace Enemies
{
    public abstract class AbstractEnemy : MonoBehaviour
    {
        [Header("Stats")] 
        [SerializeField] protected float maxHealth;
        [SerializeField] protected float moveSpeed;
        [SerializeField] protected float contactDamage;

        protected float currentHealth;
        protected Transform player;
        public GameObject fishLoot;
        [SerializeField] protected AbstractProjectile projectile;

        protected virtual void Start()
        {
            currentHealth = maxHealth;
            player = GameObject.FindWithTag("Player")?.transform;
        }

        protected virtual void Update()
        {
            move();
            attack();
        }

        public virtual void TakeDamage(float damage)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                die();
            }
        }

        protected virtual void die()
        {
            Destroy(gameObject);
            for (int i = 0; i < Random.Range(0, 3); i++)
            {
                Instantiate(fishLoot, transform.position, Quaternion.identity);
            }
        }
    
        protected abstract void move();
        protected abstract void attack();

        public float getContactDamage()
        {
            return contactDamage;
        }

        protected virtual void OnCollisionEnter2D(Collision2D collision)
        {
            HandleCollision(collision);
        }
    
        protected abstract void HandleCollision(Collision2D collision);

    }
}
