using UnityEngine;

public class Chaser : AbstractEnemy
{
    public bool flee;
    public float returnDistance;
    
    void Start()
    {
        base.Start();
        
        maxHealth = 30f;
        moveSpeed = 5f;
        contactDamage = 10f;
    }
    

    protected override void move()
    {
        float distance = Vector2.Distance(transform.position, player.position);
    
        if(!flee) {
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        } else {
            if(distance > returnDistance) flee = false;
            transform.position = Vector2.MoveTowards(transform.position, player.position, -1 * moveSpeed * Time.deltaTime);
        }
    }

    protected override void attack()
    {
    }


    protected override void HandleCollision(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Player")
        {
            takeDamage(10f);
        }
        
        
    }
}
