using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderMove1 : MonoBehaviour
{
    public Transform cylinder1;
    public Transform cylinder2;
    public Transform lowerLimit;
    public GameObject objHit;
    public float speed;

    private bool isContact;
    private bool isPlaced;
    private float distanceTravelled;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //make sure both of cylinder only move up
        cylinder1.localPosition = new Vector3(-0.333f, cylinder1.localPosition.y, 0f);
        cylinder2.localPosition = new Vector3(-0.333f, cylinder2.localPosition.y, 0f);
        cylinder1.localRotation = Quaternion.identity;
        cylinder2.localRotation = Quaternion.identity;

        //to limit cylinder movement
        if (cylinder1.localPosition.y <= lowerLimit.localPosition.y)
        {
            cylinder1.position = lowerLimit.position;
        }
        if (cylinder2.localPosition.y <= lowerLimit.localPosition.y)
        {
            cylinder2.position = lowerLimit.position;
        }

        distanceTravelled = Vector3.Distance(new Vector3(0, lowerLimit.localPosition.y, 0), new Vector3(0, cylinder1.localPosition.y, 0));

        //make sure that jack is placed in right position
        isPlaced = GetComponentInParent<PlaceJack>().isPlaced;

        checkContact();

        //freeze body when cylinder contacted with landing gear
        if (isContact && isPlaced)
        {
            GameObject.Find("Body").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
        else
        {
            GameObject.Find("Body").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }
    }

    public void MoveDown() // cylinder goes down
    {
        if (speed > 0)
        {
            speed = -speed;
        }

        if (isContact || objHit != null)
        {
            cylinder1.position = cylinder1.position + Vector3.up * speed * Time.deltaTime;
            cylinder2.position = cylinder2.position + Vector3.up * speed * Time.deltaTime;
        }

        MoveObj();
    }

    public void Stop() // cylinder stops
    {

    }

    private void checkContact() //check if small cylinder is contacted with something
    {
        RaycastHit hitInfo;

        if (Physics.BoxCast(cylinder1.position + Vector3.up * 0.044f, new Vector3(0.01f, 0.01f, 0.01f), Vector3.up, out hitInfo, Quaternion.identity, 0.1f))
        {
            Debug.Log(hitInfo);
            objHit = hitInfo.transform.root.gameObject;
            isContact = true;
        }
        else
        {
            objHit = null;
            isContact = false;
        }
    }

    public void MoveUp() // cylinder goes up
    {
        speed = Mathf.Abs(speed);
        if (distanceTravelled < 0.08f)
        {
            cylinder1.position = cylinder1.position + Vector3.up * speed * Time.deltaTime;
            MoveObj();
        }
        if (distanceTravelled >= 0.08f && distanceTravelled < 0.16f)
        {
            cylinder1.position = cylinder1.position + Vector3.up * speed * Time.deltaTime;
            cylinder2.position = cylinder2.position + Vector3.up * speed * Time.deltaTime;
            MoveObj();
        }
    }

    private void MoveObj() // lift the object without rigibody
    {
        if (objHit != null && objHit.layer == LayerMask.NameToLayer("CanBeLifted"))
        {
            objHit.transform.position = objHit.transform.position + new Vector3(0f, speed * Time.deltaTime, 0f);
        }
    }
}
