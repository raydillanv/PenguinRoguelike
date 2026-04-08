using UnityEngine;

public class HomingBall : MonoBehaviour
{
    public float speed = 8f;
    public float turnSpeed = 5f;
    public float lifetime = 5f;
    public float damage = 10f;
    public float maxRange = 15f;

    private Transform target;

    private void Start()
    {
        target = SpellCaster.Instance?.FindNearestEnemy(maxRange);
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        if (!target)
        {
            transform.Translate(Vector3.up * (speed * Time.deltaTime));
            return;
        }

        Vector2 dir = ((Vector2)target.position - (Vector2)transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, angle), turnSpeed * Time.deltaTime);
        transform.Translate(Vector3.up * (speed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;

        var enemy = other.GetComponent<AbstractEnemy>();
        if (enemy) enemy.TakeDamage(damage);

        Destroy(gameObject);
    }
}