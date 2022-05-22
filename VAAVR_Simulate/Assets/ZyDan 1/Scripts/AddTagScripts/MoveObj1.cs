using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObj1 : MonoBehaviour
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
    [SerializeField] private float force;
    //private bool isfixed = false;

    // Start is called before the first frame update
    void Start()
    {

        //find the contact point of the obj
        contactPoint = transform.Find("Contact Point");
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

    // Update is called once per frame
    void Update()
    {
        //Vector3 direction = targetPos.position - contactPoint.transform.position;
        ////direction = new Vector3(direction.x, direction.y , direction.z *0f);
        //if (direction.magnitude < distanceCheck)
        //{
        //    if (!isAttached)
        //    {
        //        //Debug.Log("hit");
        //        //transform.position = Vector3.Lerp(contactPoint.transform.position, targetPos.position, 1);

        //        //pointJoint = zone.gameObject.AddComponent<FixedJoint>();
        //        //rigid = GetComponent<Rigidbody>();
        //        //pointJoint.connectedBody = rigid;
        //        //pointJoint.breakForce = force;
        //        addJoint();
        //        isAttached = true;
        //    }
        //}
        //else
        //{
        //    Destroy(pointJoint);
        //    isAttached = false;
        //}
        //Debug.Log(direction.magnitude);
    }

    private void FixedUpdate()
    {
        Vector3 direction = targetPos.position - contactPoint.position;
        Debug.Log(direction.magnitude);
        //Debug.Log(isAttached);
        //if the distance between target point and contact point and compare to the distance check
        if (direction.magnitude < distanceCheck)
        {
            //move the obj to exact the position of the target and freeze all but movement along the x-axis
            if (direction.magnitude < 0.01f)
            {
                transform.position = Vector3.Lerp(contactPoint.position, targetPos.position, 1);
                rigid.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY;
                if(direction.magnitude < 0.001f && !isAttached)
                {
                    Debug.Log("hit");
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
        else
        {
            Destroy(pointJoint);
            isAttached = false;
            rigid.useGravity = true;
            rigid.constraints = RigidbodyConstraints.None;
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
            //return;
        }
        pointJoint.connectedBody = rigid;
        pointJoint.breakForce = force;
    }
}
