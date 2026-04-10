using UnityEngine;

namespace Enemies.Projectiles
{
    public class ShooterProjectile : AbstractProjectile
    {
        protected override void Start()
        {
            base.Start();
            damage = 8f;
        }
    
        protected override void move()
        {
            transform.position += (Vector3)(direction * (speed * Time.deltaTime));
        }
    }
}