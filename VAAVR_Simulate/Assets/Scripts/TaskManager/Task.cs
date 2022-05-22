using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task : MonoBehaviour
{
    public bool isReverse; // if is reverse - obj must have order same as last step task
    public bool isActive;
    public bool isFinished; // if is reverse - is finished must set to true
    [HideInInspector]
    public int taskID;
    public void FinishedTask()
    {
        isFinished = true;
        TaskManager.instance.UpdateTask(taskID);
    }
    public void ActiveTask()
    {
        isFinished = false;
        TaskManager.instance.UpdateTaskInstall(taskID);
    }
    public bool CheckActiveTask()
    {
       if(TaskManager.instance.checkActiveNum(taskID))
        {
            return true;
        }
        return false;
    }
}
