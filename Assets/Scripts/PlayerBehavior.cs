using System.Collections.Generic;
using Runes;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehavior : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float maxMana = 100f;
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float manaRegen = 5f;

    
    [Header("Rune Detection")]
    public Runes.RuneSystem runeSystem;

    [Header("Debug")]
    public bool drawPath;

    private float _currentHealth;
    private float _currentMana;
    
    private Vector2 _input;
    private Vector2 _lastInputDirection;
    private float _sendTime;
    private List<Vector2> _pathDirections = new List<Vector2>();
    private Rigidbody2D _rb;

    public Vector2 Velocity => _input.normalized * moveSpeed;
    private void Awake() => _rb = GetComponent<Rigidbody2D>();

    private void Start()
    {
        _currentHealth = maxHealth;
        _currentMana = maxMana;
        runeSystem ??= GameObject.FindGameObjectWithTag("RuneSystem").GetComponent<RuneSystem>();
    }

    private void FixedUpdate()
    {
        if (_input != Vector2.zero)
            _rb.MovePosition(_rb.position + _input.normalized * (moveSpeed * Time.deltaTime));

        if (_sendTime > 0 && Time.time >= _sendTime)
        {
            SendDirection(_input.normalized);
            _sendTime = 0;
        }

        if (_currentMana < maxMana)
            _currentMana = Mathf.Min(_currentMana + Mathf.Ceil(manaRegen), maxMana);
    }

    public void takeDamage(float damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            die();
        }
    }

    public void AddToHealth(float value)
    {
        maxHealth += value;
    }

    public void RestoreHealth(float value)
    {
        _currentHealth = Mathf.Min(_currentHealth + Mathf.Ceil(value), maxHealth);
    }

    private void die()
    {
        // GameManager.instance.lose();
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
