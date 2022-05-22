using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddTag2 : MonoBehaviour
{
    public LayerMask handmodel;
    public Transform zone;

    private GameObject note;
    private MoveObj1 moveObjScript;

    // Start is called before the first frame update
    void Start()
    {
        note = zone.Find("Note Warning").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        checkAttached();
        if (note == null)
        {
            return;
        }

        //show note and zone when dectected handmodel layer and the obj is not attached
        if (Physics.CheckSphere(transform.position, 0.05f, handmodel) && !checkAttached())
        {
            note.SetActive(true);
            zone.GetComponent<MeshRenderer>().enabled = true;
        }
        else
        {
            note.SetActive(false);
            zone.GetComponent<MeshRenderer>().enabled = false;
        }
    }
    
    private bool checkAttached()
    {
        moveObjScript = GetComponent<MoveObj1>();
        if (!moveObjScript) return false;

        if(moveObjScript.isAttached == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 0.05f);
    }
}
