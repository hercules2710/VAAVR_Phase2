using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CotterPin : MonoBehaviour
{
    public Rigidbody jointRigiBody;
    public GameObject cotterPin;
    public int taskIDCotterPin;

    [HideInInspector]
    public bool isAdded;

    public void AddCotterPin()
    {
        if (!TaskManager.instance.checkTaskByID(taskIDCotterPin)) return;

        // must be in order
        var cotterPinObj = Instantiate(cotterPin, this.transform);
        var task = cotterPinObj.GetComponent<Task>();
        task.taskID = taskIDCotterPin;
        cotterPinObj.GetComponent<FixedJoint>().connectedBody = jointRigiBody;
        TaskManager.instance.setObjTask(taskIDCotterPin, cotterPinObj);
        task.ActiveTask();
        isAdded = true;
    }
}
