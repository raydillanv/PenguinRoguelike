using Enemies.Projectiles;
using UnityEngine;

namespace Enemies
{
    public abstract class AbstractEnemy : MonoBehaviour
    {
        [Header("Stats")] 
        [SerializeField] protected float maxHealth;
        [SerializeField] protected float moveSpeed;
        [SerializeField] protected float contactDamage;
        [SerializeField] protected float attackCooldown;
        [SerializeField] protected float currentCooldown;

        protected float currentHealth;
        protected Transform player;
        public GameObject fishLoot;
        [SerializeField] protected AbstractProjectile projectile;
    
        protected virtual void Start()
        {
            currentHealth = maxHealth;
            GameObject playerObject = GameObject.FindWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
            }
        
        }

        protected virtual void Update()
        {
            move();
            if (currentCooldown <= 0)
            {
                attack();
                currentCooldown = attackCooldown;
            }
            currentCooldown -= Time.deltaTime;
        
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
        
            GameManager.instance.AddKillCount(1);
        }
    
        protected abstract void move();
        protected abstract void attack();

        public float getContactDamage()
        {
            return contactDamage;
        }

        // If you want to change how collisions interact call override on OnCollisionEnter2D(Collision2D collision) in child
        protected virtual void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<PlayerBehavior>().takeDamage(contactDamage);
            }
        }
    }
}
