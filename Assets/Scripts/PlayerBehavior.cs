using System.Collections.Generic;
using Runes;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehavior : MonoBehaviour
{
    [Header("Rune Detection")]
    public RuneSystem runeSystem;
    
    private Vector2 _input;
    private Vector2 _lastInputDirection;
    private float _sendTime;
    private List<Vector2> _pathDirections = new List<Vector2>();
    private Rigidbody2D _rb;
    
    public Vector2 Velocity => _input.normalized * GameManager.instance.moveSpeed;
    private void Awake() => _rb = GetComponent<Rigidbody2D>();

    private void Start()
    {
        runeSystem ??= GameObject.FindGameObjectWithTag("RuneSystem").GetComponent<RuneSystem>();
    }

    private void FixedUpdate()
    {
        if (_input != Vector2.zero)
            _rb.MovePosition(_rb.position + _input.normalized * (GameManager.instance.moveSpeed * Time.deltaTime));

        if (_sendTime > 0 && Time.time >= _sendTime)
        {
            SendDirection(_input.normalized);
            _sendTime = 0;
        }
    }
    
    public void OnMove(InputAction.CallbackContext context)
    {
        _input = context.ReadValue<Vector2>();
        if (context.performed && _input.sqrMagnitude > 0.001f)
            _sendTime = Time.time + 0.05f;
    }

    private void SendDirection(Vector2 direction)
    {
        if (!runeSystem || direction == _lastInputDirection) return;
        runeSystem.NextDirection(direction);
        _lastInputDirection = direction;
    }
}
