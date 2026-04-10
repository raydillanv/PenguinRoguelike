using TMPro;
using UnityEngine;

namespace Spells
{
    public class TimeFreeze : Spell
    {
        public TMP_Text cooldownText;
        public float duration = 5f;

        private float _timeRemaining;

        public override void Cast(SpellCaster caster)
        {
            var instance = Instantiate(gameObject, caster.canvas.transform);                                                                                                                                           
            var spell = instance.GetComponent<TimeFreeze>();                                                                                                                                                           
            spell._timeRemaining = spell.duration;                                                                                                                                                                     
            Time.timeScale = 0f; 
        }

        private void Update()
        {
            _timeRemaining -= Time.unscaledDeltaTime;
            cooldownText.text = Mathf.CeilToInt(_timeRemaining).ToString();

            if (_timeRemaining <= 0f)
            {
                Time.timeScale = 1f;
                Destroy(gameObject);
            }
        }
    }
}