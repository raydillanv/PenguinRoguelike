using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehavior : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;

    [Header("Rune Detection")]
    public Runes.RuneSystem runeSystem;

    [Header("Debug")]
    public bool drawPath;

    private Vector2 _input;
    private Vector2 _lastInputDirection;
    private float _sendTime;
    private List<Vector2> _pathDirections = new List<Vector2>();

    public Vector2 Velocity => _input.normalized * moveSpeed;

    private void Update()
    {
        if (_input != Vector2.zero)
            transform.position += (Vector3)(_input.normalized * (moveSpeed * Time.deltaTime));

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
        if (drawPath) _pathDirections.Add(direction);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        foreach (var contact in collision.contacts)
        {
            Vector2 push = contact.normal * Mathf.Abs(contact.separation);
            transform.position += (Vector3)push;
        }
    }

    private void OnDrawGizmos()
    {
        if (!drawPath || _pathDirections.Count < 2) return;

        // Draw directions as arrows from center
        Vector2 pos = transform.position;
        Gizmos.color = Color.cyan;
        foreach (var dir in _pathDirections)
        {
            Gizmos.DrawLine(pos, pos + dir * 0.5f);
            pos += dir * 0.5f;
        }
    }
}
