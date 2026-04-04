using System.Collections.Generic;
using UnityEngine;

public static class RuneMath
{
    
    public static List<Vector2> normalize(List<Vector2> inputPoints, int targetPointCount = 32)
    {
        List<Vector2> points = resample(inputPoints, targetPointCount);
        points = translateToOrigin(points);
        points = scaleToUnit(points);
        return points;
    }

    public static float comparePointLists(List<Vector2> inputPoints, List<Vector2> targetPoints)
    {
        // if (inputPoints.Count != targetPoints.Count)
        // {
        //     return float.MaxValue;
        // }

        float totalDist = 0f;

        for (int i = 0; i < inputPoints.Count; i++)
        {
            totalDist += Vector2.Distance(inputPoints[i], targetPoints[i]);
        }
        
        return totalDist/inputPoints.Count;
    }

    public static List<Vector2> resample(List<Vector2> points, int targetPointCount)
    {
        List<Vector2> output = new List<Vector2>();

        if (points.Count == 0)
        {
            return output;
        }
        
        float pathLength = getPathLength(points);
        float interval = pathLength / (targetPointCount - 1);

        float distAccumulated = 0f;
        output.Add(points[0]);

        int currentIndex = 1;
        Vector2 previousPoint = points[0];

        while (currentIndex < points.Count)
        {
            Vector2 currentPoint = points[currentIndex];
            float segmentDistance = Vector2.Distance(previousPoint, currentPoint);

            if ((distAccumulated + segmentDistance) >= interval)
            {
                float t = (interval - distAccumulated) / segmentDistance;
                Vector2 newPoint = Vector2.Lerp(previousPoint, currentPoint, t);
                output.Add(newPoint);

                previousPoint = newPoint;
                distAccumulated = 0f;
            }
            else
            {
                distAccumulated += segmentDistance;
                previousPoint = currentPoint;
                currentIndex++;
            }
        }

        while (output.Count < targetPointCount)
        {
            output.Add(points[points.Count - 1]);
        }
        
        return output;
    }

    public static List<Vector2> translateToOrigin(List<Vector2> points)
    {
        Vector2 centroid = getCentroid(points);
        List<Vector2> translated = new List<Vector2>();

        foreach (Vector2 point in points)
        {
            translated.Add(point - centroid);
        }
        
        return translated;
    }

    public static List<Vector2> scaleToUnit(List<Vector2> points)
    {
        float minX = points[0].x;
        float maxX = points[0].x;
        float minY = points[0].y;
        float maxY = points[0].y;
        
        foreach (Vector2 p in points)
        {
            if (p.x < minX) minX = p.x;
            if (p.x > maxX) maxX = p.x;
            if (p.y < minY) minY = p.y;
            if (p.y > maxY) maxY = p.y;
        }
        
        float width = maxX - minX;
        float height = maxY - minY;
        float maxDimension = Mathf.Max(width, height);

        List<Vector2> scaled = new List<Vector2>();

        if (maxDimension < 0.001f)
            return new List<Vector2>(points);

        foreach (Vector2 p in points)
        {
            scaled.Add(p / maxDimension);
        }

        return scaled;
    }

    public static float getPathLength(List<Vector2> points)
    {
        float length = 0f;
        
        for(int i = 1; i <  points.Count; i++)
        {
            length += Vector2.Distance(points[i - 1], points[i]);
        }
        
        return length;
    }

    public static Vector2 getCentroid(List<Vector2> points)
    {
        Vector2 sum = Vector2.zero;

        foreach (Vector2 point in points)
        {
            sum += point;
        }
        
        return sum/points.Count;
    }
    
}
