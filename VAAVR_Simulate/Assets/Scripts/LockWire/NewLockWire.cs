using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewLockWire : MonoBehaviour
{
    public float distance;
    private LockWire lockwire;
    public void checkDistance()
    {
        Collider[] placement = Physics.OverlapSphere(transform.position, distance);
        foreach(Collider collider in placement)
        {
            var lockwireComponent = collider.GetComponent<LockWire>();
           if (lockwireComponent)
            {
                lockwire = lockwireComponent;
                break;
            }
        }

        if (!lockwire) return;

        lockwire.placeLockWire();
       // gameObject.SetActive(false);
    }
}
