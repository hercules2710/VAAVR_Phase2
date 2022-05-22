using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskScoreItem : MonoBehaviour
{
    public Text scoreTxt;
    public Text taskName;
    // Start is called before the first frame update
    public void setTaskName(string name)
    {
        taskName.text = name;
    }
    public void setScore(float score)
    {
        scoreTxt.text = "+" + score.ToString();
    }

}
