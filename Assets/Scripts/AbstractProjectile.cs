using UnityEngine;

public abstract class AbstractProjectile : MonoBehaviour
{
    public enum ProjectileOwnership
    {
        enemy, 
        player
    }
    
    [Header("Projectiles")] 
    [SerializeField] protected float speed;
    [SerializeField] protected float damage;
    [SerializeField] protected float lifetime;
    [SerializeField] protected ProjectileOwnership owner;
    [SerializeField] protected GameObject projectile;
    
    protected Vector2 direction;

    protected virtual void Start()
    {
        Destroy(gameObject, lifetime);
    }

    protected virtual void Update()
    {
        move();
    }

    public virtual void setDirection(Vector2 direction)
    {
        this.direction = direction.normalized;
    }
    
    protected abstract void move();
    
}

