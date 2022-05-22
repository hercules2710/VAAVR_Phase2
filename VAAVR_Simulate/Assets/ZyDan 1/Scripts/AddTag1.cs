using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddTag1 : MonoBehaviour
{
    public GameObject Zone;
    public GameObject Tag;
    public float distanceCheck = 0.05f;
    public float duration;

    private float distance = 0;
    private GameObject Note;
    private Rigidbody rigid;
    private GameObject attachTransform;
    private bool isAttached;


    // Start is called before the first frame update
    void Start()
    {
        isAttached = false;
        attachTransform = Zone.transform.Find("Attach Transform").gameObject;
        Note = Zone.transform.Find("Note Warning").gameObject;
        Note.SetActive(false);
        rigid = GetComponent<Rigidbody>();
        Zone.GetComponent<MeshRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(Zone.transform.position, Tag.transform.position);
        if (distance < distanceCheck && !isAttached)
        {
            //Debug.Log("1");
            //Tag.transform.position = attachTransform.transform.position;

           
            //rigid.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
           // rigid.useGravity = false;
            //isAttached = true;

            //Tag.transform.Translate((attachTransform.transform.position - Tag.transform.position) * Time.deltaTime * 10f);
            //rotate();
        }
        if(distance > distanceCheck)
        {
            GetComponent<lerpTest>().enabled = false;
            //rigid.constraints = RigidbodyConstraints.None;
            //isAttached = false;
            //rigid.useGravity = true;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Hit");
        if (other.gameObject.tag.Equals("LeftHandController") || other.gameObject.tag.Equals("RightHandController"))
        {
            if (!isAttached)
            {
                Note.SetActive(true);
                Zone.GetComponent<MeshRenderer>().enabled = true;
            }
        }
        GetComponent<lerpTest>().enabled = false;
    }

    private void OnTriggerExit(Collider other)
    {
        Zone.GetComponent<MeshRenderer>().enabled = false;
        Note.SetActive(false);
        GetComponent<lerpTest>().enabled = true;
    }

    private void rotate()
    {
        if(Vector3.Distance(Tag.transform.rotation.eulerAngles, attachTransform.transform.rotation.eulerAngles) > 0.1f)
        {
            Tag.transform.eulerAngles = Vector3.Lerp(Tag.transform.rotation.eulerAngles, attachTransform.transform.rotation.eulerAngles, Time.deltaTime);

        }
        else
        {
            Tag.transform.eulerAngles = attachTransform.transform.eulerAngles;
        }
    }
}
