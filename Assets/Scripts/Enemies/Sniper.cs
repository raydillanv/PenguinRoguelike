using UnityEngine;

public class SniperProjectile : AbstractProjectile
{
    void Start()
    {
        base.Start();
        owner = ProjectileOwnership.enemy;
    }
    protected override void move()
    {
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }
}

public class Sniper : AbstractEnemy
{

    public float orbit;
    public float orbitSpeed;

    private float angle;
    private GameObject projectile;
    private GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
    
    void Start()
    {
        base.Start();
        maxHealth = 15f;
        moveSpeed = 2f;
        contactDamage = 5f;
        Vector2 offset = transform.position - player.transform.position;
        angle = Mathf.Atan2(offset.y, offset.x);
    }
    

    protected override void move()
    {
       angle += orbitSpeed *  Time.deltaTime;
       float x = player.transform.position.x + Mathf.Cos(angle) * orbit;
       float y = player.transform.position.y + Mathf.Sin(angle) * orbit;
       Vector2 target = new Vector2(x, y);
       
       transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
        
    }

    protected override void attack()
    {
        //Vector2 playerVel = playerObject.Velocity;
        //Vector2 predictShot = (Vector2)player.transform.position + (playerVel * .25f);
        
        //Vector2 shootDir = predictShot - (Vector2) transform.position;
        
        GameObject proj = Instantiate(projectile, transform.position, Quaternion.identity);

        AbstractProjectile projectileScript = proj.GetComponent<AbstractProjectile>();
        //projectileScript.setDirection(shootDir);
    }

    protected override void HandleCollision(Collision2D collision)
    {
        throw new System.NotImplementedException();
    }
}


