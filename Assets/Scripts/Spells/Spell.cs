using Enemies;
using UnityEngine;

namespace Spells
{
    public abstract class Spell : MonoBehaviour
    {
        public float manaCost;
        public float damage = 20f;
        public float lifetime = 2f;
        
        public virtual void Cast(SpellCaster caster)
        {
            var doesReduce = GameManager.instance.ReduceMana(manaCost);
            if (doesReduce) OnCast(caster);
            else caster.OnSpellFailed();
        }
        public abstract void OnCast(SpellCaster caster);

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
            {
                collision.gameObject.GetComponent<AbstractEnemy>()?.TakeDamage(damage);
            }
        }
        
    }   
}