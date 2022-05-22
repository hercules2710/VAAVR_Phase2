using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeJackMovement : MonoBehaviour
{
    public GameObject jackBody;
   // public LayerMask layerUngrabable;
   // public LayerMask layerGrabable;
   /* private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.tag.Equals("CanBeLifted"))
        {
            jackBody.layer = layerUngrabable;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.transform.tag.Equals("CanBeLifted"))
        {
            jackBody.layer = layerGrabable;
        }
    }*/
   public void FreezeJack()
    {
        jackBody.GetComponent<Rigidbody>().isKinematic = true;
        //jackBody.layer = layermask_to_layer(layerUngrabable);
    }
    public void UnFreezeJack()
    {
        jackBody.GetComponent<Rigidbody>().isKinematic = false;
    }
    public static int layermask_to_layer(LayerMask layerMask)
    {
        int layerNumber = 0;
        int layer = layerMask.value;
        while (layer > 0)
        {
            layer = layer >> 1;
            layerNumber++;
        }
        return layerNumber - 1;
    }
}
