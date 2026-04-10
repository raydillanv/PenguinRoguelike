using UnityEngine;

namespace Enemies.Projectiles
{
    public class SniperProjectile : AbstractProjectile
    {
        protected override void Start()
        {
            base.Start();
            damage = 20f;
        }
    
        protected override void move()
        {
            transform.position += (Vector3)(direction * (speed * Time.deltaTime));
        }
    }
}