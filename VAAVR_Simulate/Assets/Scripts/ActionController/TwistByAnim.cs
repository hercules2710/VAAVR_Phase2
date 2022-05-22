using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwistByAnim : MonoBehaviour
{
    public Transform connectPoint;
    public float activeRange;
    public LayerMask targetLayer;


     private Animator animator;
    private bool isReset = true;
    private Animation animation;
    private GameObject targetUseTool;
    private Rigidbody rigi;
    private Transform useToolObjPoint;
    private bool isFinishedTwist;
    private bool isDeactive;
    private Vector3 localConnectPointPos;
    private Quaternion localConnectPointRot;
    private TwistObj twist;
    public void Start()
    {
        rigi = transform.GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        animation = GetComponent<Animation>();
        localConnectPointPos = connectPoint.localPosition;
        localConnectPointRot = connectPoint.localRotation;
    }
    public void activeTwist()
    {
         if (!targetUseTool)
         {
             Collider[] targetCollider = Physics.OverlapSphere(connectPoint.position, activeRange, targetLayer);
             if (targetCollider.Length < 1) return;

             // set target and check if it has TwistObj Script
             foreach (Collider target in targetCollider)
             {
                 if (target.transform.gameObject.transform.tag.Equals("isActiveTask"))
                 {
                     targetUseTool = target.transform.gameObject;
                     break;
                 }
             }
             Transform[] useToolObjParts = targetUseTool.GetComponentsInChildren<Transform>();
             foreach (Transform part in useToolObjParts)
             {
                 if (part.gameObject.transform.tag.Equals("UseToolPoint"))
                 {
                     useToolObjPoint = part;
                 }
             }

             if (useToolObjPoint == null) return;


             connectPoint.SetParent(null);
             connectPoint.localScale = Vector3.one;
             transform.SetParent(connectPoint);
             connectPoint.position = useToolObjPoint.position;
             connectPoint.rotation = useToolObjPoint.rotation;
            twist = targetUseTool.GetComponent<TwistObj>();
            if (!twist) return;

            if (twist.isFinishedTwistOut) isFinishedTwist = true;
            else isFinishedTwist = false;
            isDeactive = false;
        }
        

        rigi.isKinematic = true;
        if (isReset)
        {
            isReset = false;
            animator.SetBool("IsActive", true);
        }
       // animation.Play("ToolActive");
       // animator.Play("ToolActive");

        if (!isFinishedTwist)
        {
            twist.GettingTwistOut();
            if (twist.isFinishedTwistOut)
            {
                isFinishedTwist = true;
                isDeactive = true;
                twist.isAlreadyjoint = true;
            }
        }
        else
        {
            twist.GettingTwistIn();
            if (twist.isFinishedTwistIn)
            {
                isFinishedTwist = false;
                isDeactive = true;
                twist.isAlreadyjoint = true;
            }
        }
    }
    public void deactiveTwist()
    {
        Debug.Log("Deactive");
        if (isDeactive)
        {
            if (targetUseTool)
            {
                targetUseTool = null;
                rigi.isKinematic = false;
            }
            this.transform.SetParent(null);
            connectPoint.SetParent(transform);
            connectPoint.localPosition = localConnectPointPos;
            connectPoint.localRotation = localConnectPointRot;
            twist.isAlreadyjoint = false;
            twist = null;
        }
        if (!isReset)
        {
            animator.SetBool("IsActive", false);
            isReset = true;
        }
    }
}
