using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TabGroup : MonoBehaviour
{
    public GameObject menuCanvas;
    public List<TabButton> tabButtons;
    public Sprite tabIdle;
    public Sprite tabHover;
    public Sprite tabActive;
    public TabButton selectedTab;
    public List<GameObject> objectsToSwap;
    public int chekCockpitTaskID;

    private int sceneNum;
    private bool isChooseTask;

    private void Start()
    {
         
    }
    public void Subscribe(TabButton button)
    {
        if(tabButtons == null)
        {
            tabButtons = new List<TabButton>();
        }

        tabButtons.Add(button);
    }

    public void OnTabEnter(TabButton button)
    {
        ResetTabs();
        if(selectedTab == null || button != selectedTab)
        {
            button.backGround.sprite = tabHover;
        }    
    }
    public void OnTabExit(TabButton button)
    {
        ResetTabs();
    }
    public void OnTabSelected (TabButton button)
    {
        selectedTab = button;
        ResetTabs();
        button.backGround.sprite = tabActive;
        int index = button.transform.GetSiblingIndex();
        for(int i=0; i<objectsToSwap.Count; i++)
        {
            if(i == index)
            {
                objectsToSwap[i].SetActive(true);
            }    
            else
            {
                objectsToSwap[i].SetActive(false);
            }    
        }


    }

    public void ResetTabs()
    {
        foreach(TabButton button in tabButtons)
        {
            if(selectedTab != null && button == selectedTab) { continue; }
            button.backGround.sprite = tabIdle;
        }    
    }

    public void GoBtn()
    {
        if (!selectedTab) return;

        int stateMenu = PlayerPrefs.GetInt("stateMenu");
        sceneNum = selectedTab.transform.GetSiblingIndex();
        sceneNum += 1;
        if(SceneManager.GetActiveScene().buildIndex == 3)
        {
            if (TaskManager.instance.isAllTaskFinished())
            {
                PlayerPrefs.SetInt("CheckTaskCockpit", 1);
            }
            else
            {
                PlayerPrefs.SetInt("CheckTaskCockpit", 0);
            }
        }
        switch (sceneNum)
        {
            case 1:
                {
                   if(stateMenu == 4)
                    {
                        SceneManager.LoadScene("Parking 2");
                        ParkingSceneManager.instance.resetHandManager();
                        return;
                    }
                   else if(stateMenu == 5)
                    {
                        SceneManager.LoadScene("Parking 1");
                        ParkingSceneManager.instance.resetHandManager();
                        return;
                    }
                }
                break;
            case 3:
                {
                    int checkTaskCockpit = PlayerPrefs.GetInt("CheckTaskCockpit");

                    if (SceneManager.GetActiveScene().buildIndex == 4)
                    {
                        if (stateMenu == 4)
                        {
                            if (TaskManager.instance.checkTaskUnfinishedInAll(chekCockpitTaskID))
                            {
                                PlayerPrefs.SetInt("stateMenu", 5);
                                PlayerPrefs.SetInt("CheckTaskCockpit", 0);
                            }
                        }
                    }
                    if (checkTaskCockpit == 0)
                    {
                        SceneManager.LoadScene("Cockpit 1");
                        ParkingSceneManager.instance.resetHandManager();
                        return;
                    }
                    else
                    {
                        SceneManager.LoadScene("Cockpit 2");
                        ParkingSceneManager.instance.resetHandManager();
                        return;
                    }
                }
                break;
        }


        /*  if(sceneNum == 2)
          {
               if (TaskManager.instance.isDoingExam)
               {
                   int examID = PlayerPrefs.GetInt("examID");
                   if (examID == 2)
                   {
                       SceneManager.LoadScene("Cockpit 2");
                       ParkingSceneManager.instance.resetHandManager();
                       return;
                   }
               }
               else
               {

               }

          }*/
        SceneManager.LoadScene(sceneNum);
        ParkingSceneManager.instance.resetHandManager();
    }
    public void exitBtn()
    {
        menuCanvas.SetActive(false);
    }
}
