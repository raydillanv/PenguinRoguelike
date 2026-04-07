using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehavior : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float collisionCheckDistance = 0.1f;
    public string obstacleTag = "Obstacle";

    private Vector2 _inputDirection;
    private Vector2 _velocity;
    private Vector2 _lastPosition;
    private Collider2D _collider;

    public Vector2 Velocity => _velocity;

    [Header("Trail Drawing")]
    public LineRenderer lineRenderer;
    public float minPointDistance = 0.1f;
    public int maxTrailPoints = 50;

    [Header("Turn Detection")]
    public float turnThreshold = 30f;

    [Header("Rune Detection")]
    public RuneDetector runeDetector;
    public float detectionInterval = 0.2f;

    private List<Vector2> trailPoints = new List<Vector2>();
    // private List<TurnPoint> turnPoints = new List<TurnPoint>();

    private Vector2 _lastTurnPosition;
    private Vector2 _directionAtLastTurn;
    private float _detectionTimer;

    void Start()
    {
        _collider = GetComponent<Collider2D>();
        _lastPosition = transform.position;
        _lastTurnPosition = transform.position;
    }

    void Update()
    {
        HandleMovement();
        HandleTrail();
        // HandleRuneDetection();

        // Track velocity
        Vector2 currentPos = transform.position;
        _velocity = (currentPos - _lastPosition) / Time.deltaTime;
        _lastPosition = currentPos;
    }

    void HandleMovement()
    {
        _inputDirection = Vector2.zero;

        if (Keyboard.current.leftArrowKey.isPressed || Keyboard.current.aKey.isPressed)
            _inputDirection.x = -1;
        if (Keyboard.current.rightArrowKey.isPressed || Keyboard.current.dKey.isPressed)
            _inputDirection.x = 1;
        if (Keyboard.current.upArrowKey.isPressed || Keyboard.current.wKey.isPressed)
            _inputDirection.y = 1;
        if (Keyboard.current.downArrowKey.isPressed || Keyboard.current.sKey.isPressed)
            _inputDirection.y = -1;

        if (_inputDirection == Vector2.zero)
            return;

        _inputDirection = _inputDirection.normalized;
        Vector2 movement = _inputDirection * moveSpeed * Time.deltaTime;

        // Check X movement
        if (movement.x != 0 && !CanMove(new Vector2(movement.x, 0)))
            movement.x = 0;

        // Check Y movement
        if (movement.y != 0 && !CanMove(new Vector2(0, movement.y)))
            movement.y = 0;

        transform.position += (Vector3)movement;
    }

    bool CanMove(Vector2 direction)
    {
        if (_collider == null)
            return true;

        RaycastHit2D[] hits = Physics2D.BoxCastAll(
            _collider.bounds.center,
            _collider.bounds.size,
            0f,
            direction.normalized,
            direction.magnitude + collisionCheckDistance
        );

        foreach (var hit in hits)
        {
            if (hit.collider != null && hit.collider != _collider && hit.collider.CompareTag(obstacleTag))
                return false;
        }

        return true;
    }

    void HandleTrail()
    {
        Vector2 currentPos = transform.position;

        if (trailPoints.Count == 0)
        {
            trailPoints.Add(currentPos);
            UpdateLineRenderer();
            return;
        }

        Vector2 lastPos = trailPoints[trailPoints.Count - 1];
        if (Vector2.Distance(lastPos, currentPos) < minPointDistance)
            return;

        trailPoints.Add(currentPos);
        if (trailPoints.Count > maxTrailPoints)
            trailPoints.RemoveAt(0);
        UpdateLineRenderer();

        // Calculate direction from last turn position to current
        Vector2 currentDirection = (currentPos - _lastTurnPosition).normalized;

        // Need enough distance from last turn to measure direction
        if (Vector2.Distance(_lastTurnPosition, currentPos) < minPointDistance * 3)
            return;

        // First movement after a turn - just record direction
        if (_directionAtLastTurn.sqrMagnitude < 0.001f)
        {
            _directionAtLastTurn = currentDirection;
            return;
        }

        float turnAngle = Vector2.SignedAngle(_directionAtLastTurn, currentDirection);

        // if (Mathf.Abs(turnAngle) >= turnThreshold)
        // {
        //     turnPoints.Add(new TurnPoint(currentPos, turnAngle));
        //     Debug.Log($"Turn detected: {turnAngle:F1}° | Total turns: {turnPoints.Count}");
        //
        //     int maxTurns = runeDetector != null ? runeDetector.maxRuneSize + 2 : 10;
        //     if (turnPoints.Count > maxTurns)
        //         turnPoints.RemoveAt(0);
        //
        //     _lastTurnPosition = currentPos;
        //     _directionAtLastTurn = Vector2.zero; // Reset to capture new direction
        // }
    }

    void UpdateLineRenderer()
    {
        if (lineRenderer == null) return;

        lineRenderer.positionCount = trailPoints.Count;
        for (int i = 0; i < trailPoints.Count; i++)
            lineRenderer.SetPosition(i, trailPoints[i]);
    }

    // void HandleRuneDetection()
    // {
    //     if (runeDetector == null || turnPoints.Count < 2)
    //         return;
    //
    //     _detectionTimer += Time.deltaTime;
    //
    //     if (_detectionTimer >= detectionInterval)
    //     {
    //         _detectionTimer = 0f;
    //
    //         var matchedRune = runeDetector.TryDetectRune(turnPoints);
    //
    //         if (matchedRune != null)
    //             ClearTrail();
    //     }
    // }
    //
    // void ClearTrail()
    // {
    //     trailPoints.Clear();
    //     turnPoints.Clear();
    //     _lastTurnPosition = transform.position;
    //     _directionAtLastTurn = Vector2.zero;
    // }
    //
    // private void OnDrawGizmos()
    // {
    //     if (turnPoints == null || turnPoints.Count == 0)
    //         return;
    //
    //     Gizmos.color = Color.yellow;
    //     foreach (var tp in turnPoints)
    //         Gizmos.DrawWireSphere(tp.position, 0.15f);
    //
    //     Gizmos.color = Color.cyan;
    //     for (int i = 0; i < turnPoints.Count - 1; i++)
    //         Gizmos.DrawLine(turnPoints[i].position, turnPoints[i + 1].position);
    // }
}
