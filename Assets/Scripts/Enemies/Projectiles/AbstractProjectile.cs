using UnityEngine;

namespace Enemies.Projectiles
{
    public abstract class AbstractProjectile : MonoBehaviour
    {
        [Header("Projectiles")] // why plural?
        [SerializeField] protected float speed;
        [SerializeField] protected float damage;
        [SerializeField] protected float lifetime;
        [SerializeField] protected GameObject projectile; // why is this here? is this script not attached to the projectile?
    
        protected Vector2 direction;

        protected virtual void Start()
        {
            Destroy(gameObject, lifetime);
        }

        protected virtual void Update()
        {
            move();
        }

        public virtual void setDirection(Vector2 dir)
        {
            this.direction = dir.normalized;
        }
    
        protected abstract void move();
    
        protected virtual void OnCollisionEnter2D(Collision2D collision)
        {
            GameManager.instance.TakeDamage(damage);
        }
    }
}

