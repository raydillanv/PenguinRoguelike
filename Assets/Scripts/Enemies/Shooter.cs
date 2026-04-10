using System;
using UnityEngine;
public class ShooterProjectile : AbstractProjectile
{
    protected override void move()
    {
        transform.position += (Vector3)(direction * (speed * Time.deltaTime));
    }
}
public class Shooter : AbstractEnemy
{
    float targetDistance;
    private GameObject projectile;
    
    void Start()
    {
        base.Start();
        
        maxHealth = 50f;
        moveSpeed = 5f;
        contactDamage = 5f;
        targetDistance = 15f;
    }
    

    protected override void move()
    {
        float distance = Vector2.Distance(transform.position, player.position);
        if (distance > targetDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }

    protected override void attack()
    {
        float distance = Vector2.Distance(transform.position, player.position);
        if (distance <= targetDistance)
        {
            Vector2 shootDir = (player.transform.position - transform.position);
        
            GameObject proj = Instantiate(projectile, transform.position, Quaternion.identity);
            AbstractProjectile projectileScript = proj.GetComponent<ShooterProjectile>();
            projectileScript.setDirection(shootDir);
        }
    }

    protected override void HandleCollision(Collision2D collision)
    {
        
    }
}
