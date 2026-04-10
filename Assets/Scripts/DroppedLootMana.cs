using System;
using UnityEngine;

public class DroppedLootMana : MonoBehaviour
{
    public static event Action<int> OnManaLooted;

    private Transform _target;
    [SerializeField] private float AttractionRadius = 1f;
    [SerializeField] private float MinSmoothTime = 0.15f;
    [SerializeField] private float MaxSmoothTime = 0.4f;
    [SerializeField] private int ManaValue = 1;
    [SerializeField] private Transform _parentTransform;

    Vector3 _velocity = Vector3.zero;
    private float _smoothTime;
    private bool _isAttracting = false;

    void Start()
    {
        _target = GameObject.FindWithTag("Player").transform;

        _smoothTime = UnityEngine.Random.Range(MinSmoothTime, MaxSmoothTime);
        CircleCollider2D[] colliders = GetComponents<CircleCollider2D>();
        colliders[0].radius = AttractionRadius;
        colliders[0].isTrigger = true;
    }

    void Update()
    {
        if (!_isAttracting) return;

        _parentTransform.position = Vector3.SmoothDamp(
            _parentTransform.position,
            _target.position,
            ref _velocity,
            _smoothTime
        );
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            _isAttracting = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            _isAttracting = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            OnManaLooted?.Invoke(ManaValue);
            Destroy(gameObject);
        }
    }
}