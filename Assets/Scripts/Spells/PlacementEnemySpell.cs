using UnityEngine;

namespace Spells
{
    public class PlacementEnemySpell : Spell
    {
        public float damage = 20f;
        public float lifetime = 2f;

        public override void Cast(SpellCaster caster)
        {
            var enemy = caster.FindNearestEnemy(Mathf.Infinity);
            if (!enemy) return;

            var instance = Instantiate(gameObject, enemy.position, Quaternion.identity);
            Destroy(instance, lifetime);
        }
    }
}
