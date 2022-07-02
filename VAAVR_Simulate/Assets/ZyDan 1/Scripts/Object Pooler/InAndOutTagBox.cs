using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InAndOutTagBox : MonoBehaviour
{
    public LayerMask handmodel;
    public bool isIn;

    // Start is called before the first frame update
    void Start()
    {
        isIn = false;
        //StartCoroutine(OnTriggerEnter());
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.gameObject;
        //if (obj.layer == 11)
        //{
        //    Debug.Log("hand");
        //    return;
        //}

        if (obj.CompareTag("Warning Tag"))
        {
            obj.transform.SetParent(this.transform);
            Rigidbody rigid = obj.GetComponent<Rigidbody>();
            //if (isIn == false)
            //{
            //    if(rigid != null)
            //    {
            //        Debug.Log("In");
            //        isIn = true;
            //        //Destroy(rigid);
            //        rigid.constraints = RigidbodyConstraints.FreezeAll;
                    
            //    }
            //    //rigid.isKinematic = true;
            //}
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject obj = other.gameObject;
        if (obj.CompareTag("Warning Tag"))
        {
            obj.transform.SetParent(null);
            //Rigidbody rigid = obj.GetComponent<Rigidbody>();
            //if (rigid == null)
            //{
            //    other.gameObject.AddComponent<Rigidbody>();
            //}
            //else
            //{
            //    return;

            //    //rigid.isKinematic = false;
            //}
            isIn = false;
            Debug.Log("out");
        }
    }

    
}
