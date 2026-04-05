using System;
using System.Collections.Generic;
using UnityEngine;

public class DroppedLoot : MonoBehaviour
{
    public static event Action<int> OnCurrencyLooted;

    private Transform _target;
    [SerializeField] private float AttractionRadius = 1f;
    [SerializeField] private float MinSmoothTime = 0.15f;
    [SerializeField] private float MaxSmoothTime = 0.4f;
    [SerializeField] private int CurrencyValue = 1;
    [SerializeField] private Transform _parentTransform;

    private SpriteRenderer _spriteRenderer;
    [SerializeField] private List<Sprite> _spriteVariants = new List<Sprite>();

    Vector3 _velocity = Vector3.zero;
    private float _smoothTime;
    private bool _isAttracting = false;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        int _spriteRange = UnityEngine.Random.Range(0, _spriteVariants.Count);
        _spriteRenderer.sprite = _spriteVariants[_spriteRange];

        _target = GameObject.FindWithTag("Player").transform;

        _smoothTime = UnityEngine.Random.Range(MinSmoothTime, MaxSmoothTime); // unity said random was ambigous here so i used UityEngine.Ran~
        CircleCollider2D[] colliders = GetComponents<CircleCollider2D>();
        colliders[0].radius = AttractionRadius;
        colliders[0].isTrigger = true;
    }

    void Update()
    {
        // the idea for this code in a 3d space
        //transform.position = Vector3.SmoothDamp(transform.position, Target.position, ref _velocity, Time.deltaTime * Random.Range(MinSmoothTime, MaxSmoothTime));
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
            //invokes event that is listened to by the gamemanager, passes currency value
            OnCurrencyLooted?.Invoke(CurrencyValue);
            //print("Invoked currency looted");
            Destroy(gameObject);
        }
    }

}
