using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObj : MonoBehaviour
{
    [System.Flags] public enum GravityDirection { X, Y, Z};
    //public GravityDirection gravityDirection;
    public Transform zone;
    public float speed;//how fast to move the obj to the target position
    public Vector3 pointer; //angle of the obj when attach to the target position
    public float distanceCheck; //the farest distance that the obj can be moved to the target position
    public bool isAttached = false;

    private Transform targetPos;
    private Rigidbody rigid;
    private Transform contactPoint;
    private Joint pointJoint;
    private Task task;
    private bool checkDist;
    [SerializeField] private float force;
    //private bool isfixed = false;

    // Start is called before the first frame update
    void Start()
    {
        checkDist = true;
        //find the contact point of the obj
        contactPoint = transform.Find("Contact Point");
        task = GetComponent<Task>();
        if(contactPoint == null)
        {
            contactPoint = transform;
        }
        //find the attach transform of the zone
        targetPos = zone.Find("Attach Transform");
        if (targetPos == null)
        {
            targetPos = zone.transform;
        }

        rigid = GetComponent<Rigidbody>();
    }


    private void FixedUpdate()
    {
        Vector3 direction = targetPos.position - contactPoint.position;
        
        //if the distance between target point and contact point and compare to the distance check
        if (direction.magnitude < distanceCheck)
        {
            //move the obj to exact the position of the target and freeze all but movement along the x-axis
            if (direction.magnitude < 0.01f)
            {
                if (task.isReverse)
                {
                    task.FinishedTask();
                }
                else
                {
                    task.ActiveTask();
                }
                transform.position = Vector3.Lerp(contactPoint.position, targetPos.position, 1);
                rigid.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY;
                if(direction.magnitude < 0.001f && !isAttached)
                {
                    checkDist = true;
                    //Debug.Log("hit");
                    addJoint();
                    isAttached = true;
                }
            }

            //move the obj close to the position of the target
            rigid.MovePosition(transform.position + direction * Time.fixedDeltaTime * speed);
            rigid.useGravity = false;

            //rotate the obj
            Quaternion targetRotation = Quaternion.Euler(pointer);
            targetRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 360 * Time.fixedDeltaTime);
            rigid.MoveRotation(targetRotation);
        }
        else// if(checkDist)
        {
            Destroy(pointJoint);
            isAttached = false;
            rigid.useGravity = true;
            rigid.constraints = RigidbodyConstraints.None;
            if (checkDist)
            {
                UnactiveTask();
                checkDist = false;
            }
        }
    }

    private void addJoint()
    {
        
        pointJoint = gameObject.AddComponent<FixedJoint>();
        Rigidbody rigid = zone.GetComponent<Rigidbody>();
        if (rigid == null)
        {
            rigid = zone.gameObject.AddComponent<Rigidbody>();
            rigid.useGravity = false;
            rigid.isKinematic = true;
        }
        pointJoint.connectedBody = rigid;
        pointJoint.breakForce = force;
    }
    private void UnactiveTask()
    {
        if (task.isReverse)
        {
            task.ActiveTask();
        }
        else
        {
            task.FinishedTask();
        }
    }
}
