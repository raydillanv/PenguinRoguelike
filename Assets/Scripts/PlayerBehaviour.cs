using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{

    // [Header("normalMovement")] 
    // public float moveSpeed;
    // public float acceleration;
    //
    // [Header("slideMovement")]
    // public float slideSpeed;
    // public float slideAcceleration;
    
    // private Vector2 currentDirection;
    // private Vector2 inputDirection;
    
    [Header("Trail Drawing")]
    public LineRenderer lineRenderer;
    public float minPointDistance = 0.15f;
    public int maxTrailPoints = 40;

    [Header("Rune Detection")]
    public RuneDetector runeDetector;
    public float detectionInterval = 0.25f;
    
    private List<Vector2> currentTrailPoints = new List<Vector2>();
    private float detectionTimer = 0f;
    
    public float speed;

    void Start()
    {
        addTrailPoint((Vector2)transform.position);
    }
    void Update()
    {

        // HandleMovement();
        HandleTrail();
        HandleRuneDetection();
        
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

    // void HandleMovement()
    // {
    //     inputDirection = Vector2.zero;
    //     
    //     if ((Keyboard.current.leftArrowKey.isPressed || Keyboard.current.aKey.isPressed))
    //     {
    //         inputDirection.x = -1;
    //     }
    //     if ((Keyboard.current.rightArrowKey.isPressed || Keyboard.current.dKey.isPressed))
    //     {
    //         inputDirection.x = 1;
    //     }
    //     if ((Keyboard.current.upArrowKey.isPressed || Keyboard.current.wKey.isPressed))
    //     {
    //         inputDirection.y = 1;
    //     }
    //     if ((Keyboard.current.downArrowKey.isPressed || Keyboard.current.sKey.isPressed))
    //     {
    //         inputDirection.y = -1;
    //     }
    //
    //     inputDirection = inputDirection.normalized;
    //     
    //     bool isSliding = Keyboard.current.shiftKey.isPressed;
    //
    //     float targetSpeed = isSliding ? slideSpeed : moveSpeed;
    //     float currAcceleration = isSliding ? slideAcceleration : slideAcceleration;
    //     
    //     Vector2 targetVelocity = inputDirection * targetSpeed;
    //     
    //     currVelocity = Vector2.Lerp(currentVelocity, targetVelocity, currentAcceleration * Time.deltaTime);
    //     
    //     transform.position += (Vector3)(currentVelocity * Time.deltaTime);
    // }
    
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
    
    
}
