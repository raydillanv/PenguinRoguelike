using UnityEngine;

namespace Spells
{
    public class PlacementEnemySpell : Spell
    {
        public bool useAnimatorLength = true;

        private void OnValidate()
        {
            if (useAnimatorLength && TryGetComponent<Animator>(out var animator) && animator.runtimeAnimatorController)
            {
                var clips = animator.runtimeAnimatorController.animationClips;
                if (clips.Length > 0) lifetime = clips[0].length;
            }
        }

        public override void OnCast(SpellCaster caster)
        {
            var enemy = caster.FindNearestEnemy(Mathf.Infinity);
            if (!enemy) return;

            var instance = Instantiate(gameObject, enemy.position, Quaternion.identity);
            var spell = instance.GetComponent<PlacementEnemySpell>();

            float duration = spell.lifetime;
            if (spell.useAnimatorLength && instance.TryGetComponent<Animator>(out var animator))
            {
                Debug.Log($"State info: {animator.GetCurrentAnimatorStateInfo(0)}");
                duration = animator.GetCurrentAnimatorStateInfo(0).length;
            }

            Destroy(instance, duration);
        }
    }
}
