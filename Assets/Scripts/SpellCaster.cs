using UnityEngine;

public class SpellCaster : MonoBehaviour
{
    public Canvas canvas;
    public Transform player;

    private void Start()
    {
        canvas = FindAnyObjectByType<Canvas>();
        player = FindAnyObjectByType<PlayerBehavior>().transform;
    }

    public Transform FindNearestEnemy(float maxRange)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Transform nearest = null;
        float minDist = maxRange;

        foreach (var enemy in enemies)
        {
            if (!enemy) continue;

            float dist = Vector2.Distance(player.position, enemy.transform.position);
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
