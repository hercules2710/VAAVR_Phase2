using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrakeSwift : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.tag.Equals("BrakeTrigger"))
        {
            var task = GetComponent<Task>();
            if (task)
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
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.transform.tag.Equals("BrakeTrigger"))
        {
            var task = GetComponent<Task>();
            if (task)
            {
                if (task.isReverse)
                {
                    task.FinishedTask();
                }
                else
                {
                    task.ActiveTask();
                }
            }
        }
    }
}
