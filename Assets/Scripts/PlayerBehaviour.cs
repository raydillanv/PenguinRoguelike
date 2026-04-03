using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    public float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if((Keyboard.current.leftArrowKey.isPressed || Keyboard.current.aKey.isPressed))
        {
            Vector2 newPos = transform.position;
            newPos.x -= speed  * Time.deltaTime;
            transform.position = newPos;
        }

        if((Keyboard.current.rightArrowKey.isPressed || Keyboard.current.dKey.isPressed))
        {
            Vector2 newPos = transform.position;
            newPos.x += speed * Time.deltaTime;
            transform.position = newPos;
        }
        if((Keyboard.current.upArrowKey.isPressed || Keyboard.current.wKey.isPressed))
        {
            Vector2 newPos = transform.position;
            newPos.y += speed * Time.deltaTime;
            transform.position = newPos;
        }

        if ((Keyboard.current.downArrowKey.isPressed || Keyboard.current.sKey.isPressed))
        {
            Vector2 newPos = transform.position;
            newPos.y -= speed * Time.deltaTime;
            transform.position = newPos;
        }
        
        //for attack hotkey
        if ((Keyboard.current.spaceKey.isPressed || Keyboard.current.qKey.isPressed))
        {
            
        }
    }
}
