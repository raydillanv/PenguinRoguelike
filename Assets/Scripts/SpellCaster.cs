using UnityEngine;

public class SpellCaster : MonoBehaviour
{
    public static SpellCaster Instance { get; private set; }
    private Transform Player { get; set; }

    private void Awake()
    {
        Instance = this;
        Player = transform;
    }

    public Transform FindNearestEnemy(float maxRange)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Transform nearest = null;
        float minDist = maxRange;

        foreach (var enemy in enemies)
        {
            float dist = Vector2.Distance(Player.position, enemy.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                nearest = enemy.transform;
            }
        }
        return nearest;
    }

    public void OnSpellFailed()
    {
        // TODO: not enough mana feedback
    }
}
