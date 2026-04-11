using Enemies.Projectiles;
using UnityEngine;

namespace Enemies
{
    public class Shooter : AbstractEnemy
    {
        float targetDistance;

        protected override void move()
        {
            float distance = Vector2.Distance(transform.position, player.position);
            if (distance > targetDistance)
            {
                rb.MovePosition(Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime));
            }
        }

        protected override void attack()
        {
            float distance = Vector2.Distance(transform.position, player.position);
            if (distance <= targetDistance)
            {
                Vector2 shootDir = (player.transform.position - transform.position);
        
                AbstractProjectile proj = Instantiate(projectile, transform.position, Quaternion.identity);
                ShooterProjectile projectileScript = proj.GetComponent<ShooterProjectile>();
                projectileScript.setDirection(shootDir);
            }
        }
    }
}
