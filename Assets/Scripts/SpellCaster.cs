using UnityEngine;

public class SpellCaster : MonoBehaviour
{
    public Transform player;

    public void castRune(RuneData rune)
    {
        if (rune == null)
        {
            return;
        }
        
        if (rune.spellPrefab != null && player != null)
        {
            Instantiate(rune.spellPrefab, player.position, Quaternion.identity);
        }
            
    }
}
