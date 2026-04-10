using UnityEngine;

public class Sniper : AbstractEnemy
{

    public float orbit;
    public float orbitSpeed;

    private float angle;
    private PlayerBehavior _playerScript;
    
    new void Start()
    {
        base.Start();
        maxHealth = 15f;
        moveSpeed = 5f;
        contactDamage = 5f;
        Vector2 offset = transform.position - player.transform.position;
        angle = Mathf.Atan2(offset.y, offset.x);
        
        _playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehavior>();
        attackCooldown = 2f;
        orbit = 5f;
        orbitSpeed = 1f;
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
        Vector2 predictShot = (Vector2)player.transform.position + (_playerScript.Velocity * .25f);
        
        Vector2 shootDir = predictShot - (Vector2) transform.position;
        
        AbstractProjectile proj = Instantiate(projectile, transform.position, Quaternion.identity);
        SniperProjectile projectileScript = proj.GetComponent<SniperProjectile>();
        projectileScript.setDirection(shootDir);
    }

    protected override void HandleCollision(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerBehavior>().takeDamage(contactDamage);
        }
    }
    
    
}


