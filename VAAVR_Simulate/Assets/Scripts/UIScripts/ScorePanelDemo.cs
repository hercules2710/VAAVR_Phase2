using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScorePanelDemo : MonoBehaviour
{
    public GameObject scorePrefab;
    public GameObject scrollList;
    // Start is called before the first frame update
    void Start()
    {
        int exam = PlayerPrefs.GetInt("exam");
        if (exam == 1)
        {
            int examID = PlayerPrefs.GetInt("examID");
            float score = 0;
            string scoreStr = null;
            if (examID == 1)
            {
                score = PlayerPrefs.GetFloat("totalScoreRemove");
                scoreStr = "Score Remove: " + score.ToString();
            }
            else if (examID == 2)
            {
                score = PlayerPrefs.GetFloat("totalScoreInstall");
                scoreStr = "Score Install: " + score.ToString();
            }
            var scoreItem = Instantiate(scorePrefab, scrollList.transform);
            scoreItem.GetComponent<ScoreDemoItem>().setText(scoreStr);
        }
    }
}
