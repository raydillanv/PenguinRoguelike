using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehavior : MonoBehaviour
{
    private Rigidbody2D _rb;

    [Header("Normal Movement")]
    public float moveSpeed = 5f;
    public float acceleration = 10f;

    [Header("Slide Movement")]
    public float slideSpeed = 8f;
    public float slideAcceleration = 5f;

    private Vector2 inputDirection;
    
    [Header("Trail Drawing")]
    public LineRenderer lineRenderer;
    public float minPointDistance = 0.15f;
    public int maxTrailPoints = 40;

    [Header("Rune Detection")]
    public RuneDetector runeDetector;
    public float detectionInterval = 0.25f;
    
    private List<Vector2> currentTrailPoints = new List<Vector2>();
    private float detectionTimer = 0f;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        addTrailPoint(transform.position);
    }
    void Update()
    {

        HandleMovement();
        HandleTrail();
        HandleRuneDetection();
        
    }

    void HandleMovement()
    {
        inputDirection = Vector2.zero;

        if (Keyboard.current.leftArrowKey.isPressed || Keyboard.current.aKey.isPressed)
            inputDirection.x = -1;
        if (Keyboard.current.rightArrowKey.isPressed || Keyboard.current.dKey.isPressed)
            inputDirection.x = 1;
        if (Keyboard.current.upArrowKey.isPressed || Keyboard.current.wKey.isPressed)
            inputDirection.y = 1;
        if (Keyboard.current.downArrowKey.isPressed || Keyboard.current.sKey.isPressed)
            inputDirection.y = -1;

        inputDirection = inputDirection.normalized;

        bool isSliding = Keyboard.current.shiftKey.isPressed;
        float targetSpeed = isSliding ? slideSpeed : moveSpeed;
        float currAcceleration = isSliding ? slideAcceleration : acceleration;

        Vector2 targetVelocity = inputDirection * targetSpeed;
        Vector2 newVelocity = Vector2.MoveTowards(_rb.linearVelocity, targetVelocity, currAcceleration * Time.deltaTime);
        _rb.linearVelocity = newVelocity;
    }
    
    void HandleTrail()
    {
        Vector2 currentPos = transform.position;

        if (currentTrailPoints.Count == 0 || Vector2.Distance(currentTrailPoints[currentTrailPoints.Count - 1], currentPos) >= minPointDistance)
        {
            addTrailPoint(currentPos);
        }
    }

    void addTrailPoint(Vector2 point)
    {
        currentTrailPoints.Add(point);

        if (currentTrailPoints.Count > maxTrailPoints)
        {
            currentTrailPoints.RemoveAt(0);
        }

        UpdateLineRenderer();
    }

    void UpdateLineRenderer()
    {
        if (lineRenderer == null) return;

        lineRenderer.positionCount = currentTrailPoints.Count;

        for (int i = 0; i < currentTrailPoints.Count; i++)
        {
            lineRenderer.SetPosition(i, currentTrailPoints[i]);
        }
    }

    void HandleRuneDetection()
    {
        if (runeDetector == null || currentTrailPoints.Count < 8)
            return;

        detectionTimer += Time.deltaTime;

        if (detectionTimer >= detectionInterval)
        {
            detectionTimer = 0f;

            bool matched = runeDetector.tryDetectRune(currentTrailPoints);

            if (matched)
            {
                ClearTrail();
            }
        }
    }

    void ClearTrail()
    {
        currentTrailPoints.Clear();
        addTrailPoint((Vector2)transform.position);
    }
    
    private void OnCollisionStay2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            Vector2 normal = contact.normal;
            float intoWall = Vector2.Dot(_rb.linearVelocity, -normal);

            if (intoWall > 0f)
            {
                _rb.linearVelocity += normal * intoWall;
            }
        }
    }
    
}
