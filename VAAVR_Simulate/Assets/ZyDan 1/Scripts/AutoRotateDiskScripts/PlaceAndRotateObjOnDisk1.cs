using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlaceAndRotateObjOnDisk1 : MonoBehaviour
{
    //add script in the obj that will contact with another obj
    public GameObject obj;

    private AutoRotateDisk1 autoRotateDiskScript;
    private Vector3 center;
    private Rigidbody rb;
    private Vector3 direction;
    [SerializeField] private float moveObjSpeed;
    [SerializeField] private float disCheck;
    private float rotateSpeed;

    // Start is called before the first frame update
    void Start()
    {
        center = new Vector3(transform.position.x, 0f, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        //detect if disk has something on it or not
        RaycastHit hitInfo;
        Ray ray = new Ray(this.transform.position, this.transform.up);
        if (Physics.Raycast(ray, out hitInfo, disCheck))
        {
            //Debug.Log(hitInfo.collider.name);
            obj = hitInfo.collider.transform.parent.gameObject;
            rb = obj.GetComponent<Rigidbody>();
            direction = center - new Vector3(obj.transform.position.x, 0f, obj.transform.position.z);
        }
        else
        {
            obj = null;
            rb = null;
        }

        synchronizeSpeed();
    }

    private void FixedUpdate()
    {
        MoveObj(direction);
        //rotate and freeze obj if its position closes to the center
        if (direction.magnitude < 0.01f && obj != null)
        {
            RotateObj();
            FreezeObj();
        }
    }

    //move the obj to the center of disk
    private void MoveObj(Vector3 direction)
    {
        if (obj != null && obj.transform.up.y == 1f)
        {
            direction = new Vector3(direction.x, 0, direction.z);
            rb.velocity = direction * moveObjSpeed * Time.fixedDeltaTime;
        }
    }

    //rotate the obj
    private void RotateObj()
    {
        Quaternion rotation = Quaternion.Euler(new Vector3(0, rotateSpeed, 0));
        rb.MoveRotation(rotation * rb.rotation);
    }
    
    public void FreezeObj()
    {
        //freeze the obj if obj is up and has been rotating
        if (rotateSpeed > 0f && obj.transform.up.y == 1f && obj !=null)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
        else
        {
            rb.constraints = RigidbodyConstraints.None;
        }
    }

    private float synchronizeSpeed()
    {
        autoRotateDiskScript = GetComponentInParent<AutoRotateDisk1>();
        if(!autoRotateDiskScript)
        {
            rotateSpeed = 0;
        }
        else
        {
            rotateSpeed = autoRotateDiskScript.speed;
        }
        //synchronize speed with the disk
        return rotateSpeed;
    }
}
