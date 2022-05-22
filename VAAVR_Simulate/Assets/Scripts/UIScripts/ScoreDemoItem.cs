using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDemoItem : MonoBehaviour
{
    [SerializeField]
    public Text textScore;
    public void setText(string score)
    {
        textScore.text = score;
    }
}
