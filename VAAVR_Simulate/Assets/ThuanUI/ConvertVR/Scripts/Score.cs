using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SimpleJSON;
using UnityEngine.UI;

public class Score : MonoBehaviour
{

    public static Score instance;
    private void Awake()
    {
        instance = this;
    }

    public static Score intances;
    Action<string> _createScoreCallBack;
    GameObject[] gameObjecttoFind;
    // Start is called before the first frame update
    public void Start()
    {
        _createScoreCallBack = (jsonArrayString) => {
            StartCoroutine(CreateClassroomsRoutine(jsonArrayString));
        };

        CreateScoreDetail();
    }

    public void CreateScoreDetail()
    {
        DeleteData();
        string user_id = Main.instaince.userInform.ids;
        StartCoroutine(Main.instaince.authenmanager.GetScoreDS(user_id, _createScoreCallBack));

    }

    public void DeleteData()
    {
        gameObjecttoFind = GameObject.FindGameObjectsWithTag("Destroy");
        foreach (GameObject g in gameObjecttoFind)
        {
            Destroy(g.gameObject);
        }
    }
    IEnumerator CreateClassroomsRoutine(string jsonArrayString)
    {
        //Parsing string json is in array
        JSONArray jsonArray = JSON.Parse(jsonArrayString) as JSONArray;
        int dem = jsonArray.Count - 11;
        int m = 0;
        for (int j = jsonArray.Count - 1; j < jsonArray.Count; j++)
        {
            dem++;
            for (int i = jsonArray.Count - 1; i >= dem; i--)
            {
                if (i >= 0)
                {
                    //Create local variables
                    bool isDone = false;
                    string scoreId = jsonArray[i].AsObject["id"];

                    JSONObject scoreInfoJson = new JSONObject();

                    //Create a call back to get the infomation from Web.cs

                    Action<string> getScoreInfoCallback = (itemInfo) =>
                    {
                        isDone = true;
                        JSONArray tempArray = JSON.Parse(itemInfo) as JSONArray;
                        scoreInfoJson = tempArray[0].AsObject;
                    };

                    StartCoroutine(Main.instaince.authenmanager.GetScore(scoreId, getScoreInfoCallback));

                    yield return new WaitUntil(() => isDone == true);

                    GameObject score = Instantiate(Resources.Load("Prefabs/Score") as GameObject);
                    score.transform.SetParent(this.transform);
                    score.transform.localScale = Vector3.one;
                    score.transform.localPosition = Vector3.zero;

                    m++;
                    int stt = m;
                    string sttScore = stt.ToString();
                    //switch(stt)
                    //{
                    //    default : sttScore = stt + "TH";break;

                    //    case 1: sttScore = "1ST"; break;
                    //    case 2: sttScore = "2ND"; break;
                    //    case 3: sttScore = "3RD"; break;
                    //}
                    score.transform.Find("STT").GetComponent<Text>().text = sttScore;
                    score.transform.Find("Total_Score").GetComponent<Text>().text = scoreInfoJson["total_score"];
                }
                else
                {
                    Debug.Log("Cannot get list information from data");
                }
            }
        }
        yield return null;
    }
}
