using System;
using UnityEditor;
using UnityEngine;

public class IASensor : MonoBehaviour
{
    [Range(0f,360f)]
    [SerializeField] private float visionAngle;
    [SerializeField] private Color visionColor;
    [SerializeField] private Color detectedColor;
    [SerializeField] private float maxVisionDistance;
    [SerializeField] private Transform target;
    [SerializeField] private LayerMask layer;  
    private bool detected;
    
    public Color VisionColor
    {
        get => visionColor;
        set => visionColor = value;
    }

    public Color DetectedColor
    {
        get => detectedColor;
        set => detectedColor = value;
    }
    public float MaxVisionDistance => maxVisionDistance;
    public float VisionAngle => visionAngle;
    public Transform Target => target;
    public bool Detected => detected;


    public void UpdateSensor()
    {
        detected = false;
        Vector3 playerVector = target.position - transform.position;
        if (Vector3.Angle(playerVector.normalized, transform.forward) < visionAngle * 0.5f)
        {
            if (playerVector.magnitude < maxVisionDistance)
            {
                if (!Physics.Raycast(transform.position, playerVector.normalized, playerVector.magnitude, layer))
                    detected = true;
            }
        }
    }
}

[CustomEditor(typeof(IASensor))]
public class EnemyVisionSensor : Editor
{
   public void OnSceneGUI()
   {
        var ai= target as IASensor;
        float halfVisionAngle = ai.VisionAngle * 0.5f;

        Vector3 startPoint = Mathf.Cos(-halfVisionAngle  * Mathf.Deg2Rad) * ai.transform.forward +
                             Mathf.Sin(halfVisionAngle  * Mathf.Deg2Rad) * -ai.transform.right;

        Handles.color =ai.Detected? ai.DetectedColor: ai.VisionColor;
        Handles.DrawSolidArc(ai.transform.position,Vector3.up,startPoint,ai.VisionAngle,ai.MaxVisionDistance);
   }
}