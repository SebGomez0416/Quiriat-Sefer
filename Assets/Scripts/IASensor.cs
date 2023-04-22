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
    private bool detected;

    public bool Detected => detected;
    public float MaxVisionDistance => maxVisionDistance;
    public Color DetectedColor => detectedColor;
    public Color VisionColor => visionColor;
    
    public float VisionAngle
    {
        get => visionAngle;
        set => visionAngle = value;
    }

    private void Update()
    {
        detected = false;
        Vector3 playerVector = target.position - transform.position;

        if (Vector3.Dot(playerVector.normalized, transform.forward) > Mathf.Cos(VisionAngle))
        {
            if (playerVector.magnitude < maxVisionDistance)
                detected = true;
            
        }
    }
}
[CustomEditor(typeof(IASensor))]
public class EnemyVisionSensor : Editor
{
   public void OnSceneGUI()
   {
        var ai= target as IASensor;

        Vector3 startPoint = Mathf.Cos(-ai.VisionAngle * Mathf.Deg2Rad)*ai.transform.forward +
                             Mathf.Sin(ai.VisionAngle* Mathf.Deg2Rad)* -ai.transform.right;
        

        Handles.color =ai.Detected? ai.DetectedColor: ai.VisionColor;
        Handles.DrawSolidArc(ai.transform.position,Vector3.up,startPoint,ai.VisionAngle*2f,ai.MaxVisionDistance);

   }
}