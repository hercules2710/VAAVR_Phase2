using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskItem : MonoBehaviour
{
    public GameObject checkIcon;
    public void activeTask()
    {
        checkIcon.SetActive(false);
    }
    public void finishedTask()
    {
        checkIcon.SetActive(true);
    }
}
