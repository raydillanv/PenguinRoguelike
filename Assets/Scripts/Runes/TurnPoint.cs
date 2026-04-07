using UnityEngine;

[System.Serializable]
public struct TurnPoint
{
    public Vector2 position;
    public float turnAngle;

    public TurnPoint(Vector2 position, float turnAngle)
    {
        this.position = position;
        this.turnAngle = turnAngle;
    }
}
