using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTask : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var cockpitTask = PlayerPrefs.GetInt("CheckTaskCockpit");
        var task = GetComponent<Task>();
        if (cockpitTask == 1)
        {
            task.FinishedTask();
        }
        else
        {
            task.ActiveTask();
        }
    }

}
