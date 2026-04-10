using TMPro;
using UnityEngine;

namespace Spells
{
    public class TimeFreeze : Spell
    {
        public TMP_Text cooldownText;

        private float _timeRemaining;

        public override void OnCast(SpellCaster caster)
        {
            var instance = Instantiate(gameObject, caster.canvas.transform);                                                                                                                                           
            var spell = instance.GetComponent<TimeFreeze>();                                                                                                                                                           
            spell._timeRemaining = spell.lifetime;                                                                                                                                                                     
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