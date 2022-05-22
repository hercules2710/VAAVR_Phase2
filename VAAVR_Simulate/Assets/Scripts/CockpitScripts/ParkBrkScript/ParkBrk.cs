using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkBrk : MonoBehaviour
{
    //Note:
    //-the rotation of on and off position shouldn't be (0,0,0)
    //-to avoid "gimbal lock", the obj's rotation should be set to 0 in the untwist axis
    //change the rotation of the parent gameobject may solve two issues above

    public enum UpDirection { upX, upY, upZ}; //setting direction to move the obj
    public UpDirection upDirection;
    public enum TwistAxis { axisX, axisY, axisZ }; //setting axis to twist the obj around
    public TwistAxis twistAxis;
    public float distance; //distance to limit the obj's movement
    public Transform OnPos, OffPos; //rotation of the obj in on and off position

    private Vector3 saveLocalPos;
    private bool isPulled;
    private float xPlus = 0f, yPlus = 0f, zPlus = 0f;
    private float xRotate = 0f, yRotate = 0f, zRotate = 0f;

    // Start is called before the first frame update
    void Start()
    {
        if (upDirection == UpDirection.upX) xPlus = 1f;
        if (upDirection == UpDirection.upY) yPlus = 1f;
        if (upDirection == UpDirection.upZ) zPlus = 1f;

        if (twistAxis == TwistAxis.axisX) xRotate = 1f;
        if (twistAxis == TwistAxis.axisY) yRotate = 1f;
        if (twistAxis == TwistAxis.axisZ) zRotate = 1f;
        //Debug.Log(xRotate);
        //Debug.Log(yRotate);
        //Debug.Log(zRotate);

        isPulled = false;
        saveLocalPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        //limit the object's movement, make sure it can not go further than the distance which is set before
        transform.localPosition = new Vector3(
            Mathf.Clamp(transform.localPosition.x - saveLocalPos.x, 0f, distance) * xPlus,
            Mathf.Clamp(transform.localPosition.y - saveLocalPos.y, 0f, distance) * yPlus,
            Mathf.Clamp(transform.localPosition.z - saveLocalPos.z, 0f, distance) * zPlus);

        //freeze the obj's rotation when it is not being pulled
        if (Vector3.Distance(transform.localPosition, saveLocalPos) >= distance * 0.9)
        {
            isPulled = true;
            GetComponent<Rigidbody>().freezeRotation = false;
        }
        else
        {
            isPulled = false;
            GetComponent<Rigidbody>().freezeRotation = true;
        }

        if (isPulled)
        {
            //limit the obj's rotation between on and off position
            transform.localEulerAngles = new Vector3(
                Mathf.Clamp(transform.localEulerAngles.x, OffPos.localEulerAngles.x, OnPos.localEulerAngles.x) * xRotate,
                Mathf.Clamp(transform.localEulerAngles.y, OffPos.localEulerAngles.y, OnPos.localEulerAngles.y) * yRotate, 
                Mathf.Clamp(transform.localEulerAngles.z, OffPos.localEulerAngles.z, OnPos.localEulerAngles.z) * zRotate);
        }
        else
        {
            //when the obj is released in between on and off position, set its rotation to the closest one
            if (transform.localEulerAngles.x < (OffPos.localEulerAngles.x + OnPos.localEulerAngles.x) / 2 ||
                transform.localEulerAngles.y < (OffPos.localEulerAngles.y + OnPos.localEulerAngles.y) / 2 ||
                transform.localEulerAngles.z < (OffPos.localEulerAngles.z + OnPos.localEulerAngles.z) / 2)
            {
                transform.localEulerAngles = OffPos.localEulerAngles;
            }
            else
            {
                transform.localEulerAngles = OnPos.localEulerAngles;
            }
        }
    }
}
