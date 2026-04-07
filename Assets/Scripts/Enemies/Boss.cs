using UnityEngine;

public class Boss : AbstractEnemy
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void move()
    {
        throw new System.NotImplementedException();
    }

    protected override void attack()
    {
        throw new System.NotImplementedException();
    }

    protected override void HandleCollision(Collision2D collision)
    {
        throw new System.NotImplementedException();
    }
}
