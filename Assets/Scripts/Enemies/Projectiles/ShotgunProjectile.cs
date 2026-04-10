using UnityEngine;

namespace Enemies.Projectiles
{
    public class ShotgunProjectile : AbstractProjectile
    {
        protected override void Start()
        {
            base.Start();
            damage = 10f;
        }
        protected override void move()
        {
            transform.position += (Vector3)(direction * (speed * Time.deltaTime));
        }
    }
}