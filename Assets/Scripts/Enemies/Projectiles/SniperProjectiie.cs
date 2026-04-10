using UnityEngine;

public class SniperProjectile : AbstractProjectile
{

    private void Start()
    {
        base.Start();
        damage = 20f;
    }
    
    protected override void move()
    {
        transform.position += (Vector3)(direction * (speed * Time.deltaTime));
    }
    
    protected override void HandleCollision(Collision2D collision)
    {
        collision.gameObject.GetComponent<PlayerBehavior>()?.takeDamage(damage);
    }
}