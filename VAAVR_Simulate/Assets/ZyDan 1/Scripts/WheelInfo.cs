using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelInfo : MonoBehaviour
{
    public float width;
    public float diameter;
    public float mass;
    public enum RotateAxis { X, Y, Z };
    public RotateAxis rotateAxis;
    public Vector3 rotateVector;

    private float axisX = 0f;
    private float axisY = 0f;
    private float axisZ = 0f;
    // Start is called before the first frame update
    void Start()
    {
        if (rotateAxis == RotateAxis.X) axisX = 1;
        if (rotateAxis == RotateAxis.Y) axisY = 1;
        if (rotateAxis == RotateAxis.Z) axisZ = 1;

        rotateVector = new Vector3(axisX, axisY, axisZ);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward, Color.yellow);
        Debug.DrawRay(transform.position, rotateVector, Color.red);
    }
}
