using UnityEngine;

public class Boss : AbstractEnemy
{
    
    public GameObject projectile;
    public GameObject[] enemyPrefabs;

    public int projectileCount = 20;
    public int spawnCount = 6;

    public float attackCooldown = 3f;
    public float spawnCooldown = 5f;

    private float attackTimer;
    private float spawnTimer;

    void Start()
    {
        base.Start();

        maxHealth = 500f;
        moveSpeed = 2f;
        contactDamage = 15f;

        attackTimer = attackCooldown;
        spawnTimer = spawnCooldown;
    }

    void Update()
    {
        attackTimer -= Time.deltaTime;
        spawnTimer -= Time.deltaTime;

        if (attackTimer <= 0f)
        {
            attack();
            attackTimer = attackCooldown;
        }

        if (spawnTimer <= 0f)
        {
            SpawnEnemiesCircle();
            spawnTimer = spawnCooldown;
        }
    }

    protected override void move()
    {
        
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance > 8f)
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
        ShootCircle();
    }

    
    private void ShootCircle()
    {
        for (int i = 0; i < projectileCount; i++)
        {
            float angle = i * (360f / projectileCount);
            float rad = angle * Mathf.Deg2Rad;

            Vector2 dir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));

            GameObject proj = Instantiate(projectile, transform.position, Quaternion.identity);
            AbstractProjectile projScript = proj.GetComponent<AbstractProjectile>();
            projScript.setDirection(dir);
        }
    }

    
    private void SpawnEnemiesCircle()
    {
        float radius = 3f;

        for (int i = 0; i < spawnCount; i++)
        {
            float angle = i * (360f / spawnCount);
            float rad = angle * Mathf.Deg2Rad;

            Vector2 offset = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)) * radius;

            Vector2 spawnPos = (Vector2)transform.position + offset;

            Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], spawnPos, Quaternion.identity);
        }
    }

    protected override void HandleCollision(Collision2D collision)
    {
    }
}
