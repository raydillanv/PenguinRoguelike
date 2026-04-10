using System;
using UnityEngine;

public class Shotgun : AbstractEnemy
{
    float targetDistance;

    public float spreadAngle = 20f; 

    void Start()
    {
        base.Start();

        maxHealth = 60f;
        moveSpeed = 4f;
        contactDamage = 8f;
        targetDistance = 12f;
        attackCooldown = 1f;
    }

    protected override void move()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance > targetDistance)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                player.position,
                moveSpeed * Time.deltaTime
            );
        }
    }

    protected override void attack()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= targetDistance)
        {
            Vector2 baseDir = (player.position - transform.position).normalized;
            
            float[] angles = { -spreadAngle / 2f, 0f, spreadAngle / 2f };

            foreach (float angle in angles)
            {
                Vector2 rotatedDir = RotateVector(baseDir, angle);

                AbstractProjectile proj = Instantiate(projectile, transform.position, Quaternion.identity);
                ShotgunProjectile projScript = proj.GetComponent<ShotgunProjectile>();
                projScript.setDirection(rotatedDir);
            }
        }
    }

    private Vector2 RotateVector(Vector2 v, float degrees)
    {
        float rad = degrees * Mathf.Deg2Rad;

        float sin = Mathf.Sin(rad);
        float cos = Mathf.Cos(rad);

        return new Vector2(
            v.x * cos - v.y * sin,
            v.x * sin + v.y * cos
        );
    }

    protected override void HandleCollision(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerBehavior>().takeDamage(contactDamage);
        }
    }
}

