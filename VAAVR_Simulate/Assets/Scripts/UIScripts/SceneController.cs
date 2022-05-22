using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DigitalRuby.RainMaker;


public class SceneController : MonoBehaviour
{
    #region singleton
    public static SceneController instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    enum state { Remove, Install };
    public GameObject menuPanel;
    public GameObject leftTabArea;
    public GameObject toolListPanel;
    public GameObject taskPanel;
    public GameObject taskInstallPanel;
    public GameObject taskRemovePanel;
    public GameObject airplanePanel;
    public GameObject systemMenuPanel;
    public GameObject scoreRemovePanel;
    public GameObject scoreInstallPanel;
    public GameObject videoPanel;
    public GameObject videoBtn;
    public GameObject airplaneBtn;
    public GameObject weatherLeftBtn;
    public GameObject weatherRightBtn;
    public GameObject scoreBackBtn;
    public GameObject checkScoreRemoveBtn;
    public GameObject checkScoreInstallBtn;
    public GameObject sceneBtn;
    public GameObject rainWeather;
    public GameObject dropdownSystemPart;
    public GameObject dropdownSystem;
    public GameObject dropdownPart;
    public GameObject dropdownTask;
    public GameObject examPanel;
    public GameObject messagePopUp;
    public RainScript rainScript;
  //  public Component rain;
    public Image weatherIcon;
    public Sprite rainImg;
    public Sprite sunImg;
    public Text popUpTxt;
    public string message1 = "Go to other scene will reset all progress.\nMake sure next task need to go to other scene.";
    public float rainValue;

    private state stateTask = state.Remove;

    private bool isDoingExam;
   // private int examId;

    void Start()
    {
        if (rainWeather)
        {
            rainWeather.GetComponent<SetTransparency>().SetT(0);
            WeatherRightBtn();
        }
    }
    public void SetStateMenu()
    {
        int stateMenu = PlayerPrefs.GetInt("stateMenu");
        switch (stateMenu)
        {
            case 1: // when user go to task remove panel but not in exam
                {
                    Debug.Log("state 1");
                    if (airplanePanel) airplanePanel.SetActive(false);

                    if (dropdownSystem) dropdownSystem.SetActive(false);

                    if (dropdownTask) dropdownTask.SetActive(true);

                    if (dropdownSystemPart) dropdownPart.SetActive(true);

                    if (sceneBtn) sceneBtn.SetActive(true);

                    OpenTaskRemovePanel();
                }
                break;
            case 2: // when user go to task remove panel but in exam
                {
                    Debug.Log("state 2");
                    if (airplanePanel) airplanePanel.SetActive(false);

                    if (dropdownSystem) dropdownSystem.SetActive(false);

                    if (sceneBtn) sceneBtn.SetActive(true);

                    OpenTaskRemovePanel();
                }
                break;
            case 3: // when user go to task install panel but not in exam
                {
                    Debug.Log("state 3");
                    if (airplanePanel) airplanePanel.SetActive(false);

                    if (dropdownSystem) dropdownSystem.SetActive(false);

                    if (dropdownTask) dropdownTask.SetActive(true);

                    if (dropdownSystemPart) dropdownPart.SetActive(true);

                    if (sceneBtn) sceneBtn.SetActive(true);

                    OpenTaskInstallPanel();
                }
                break;
            case 5: // when user in install exam but has finished all task
                {
                    Debug.Log("state 5");

                    if (airplanePanel) airplanePanel.SetActive(false);

                    if (dropdownSystem) dropdownSystem.SetActive(false);

                    if (dropdownTask) dropdownTask.SetActive(false);

                    if (dropdownSystemPart) dropdownPart.SetActive(false);

                    if (sceneBtn) sceneBtn.SetActive(true);

                    if (taskInstallPanel) taskInstallPanel.SetActive(true);
                }
                break;
        }
    }
    public void checkExam()
    {
        if (!TaskManager.instance.isDoingExam)
        {
            if (videoBtn) videoBtn.SetActive(true);

            if (airplaneBtn) airplaneBtn.SetActive(true);

            isDoingExam = false;
        }
        else
        {
            if (videoBtn) videoBtn.SetActive(false);

            if (airplaneBtn) airplaneBtn.SetActive(false);

            if (dropdownTask) dropdownTask.SetActive(false);

            isDoingExam = true;
        }
        SetStateMenu();
    }
    public void OpenToolList()
    {
        toolListPanel.SetActive(true);
    }
    public void CloseToolList()
    {
        toolListPanel.SetActive(false);
    }
    public void OpenTaskPanel()
    {
        if(taskPanel != null)
        {
            bool isActive = taskPanel.activeSelf;
            taskPanel.SetActive(!isActive);
        }
    }
    public void OpenTaskRemovePanel()
    {
        if (taskRemovePanel != null)
        {
            if (isDoingExam)
            {
                if (examPanel)
                {
                    examPanel.SetActive(false);
                }
                // examId = 1;
                PlayerPrefs.SetInt("examID",1);
                PlayerPrefs.SetInt("stateMenu", 2); // when user go to task remove panel but in exam
            }
            else
            {
                PlayerPrefs.SetInt("stateMenu", 1); // when user go to task remove panel but not in exam
            }
            bool isActive = taskRemovePanel.activeSelf;
            taskRemovePanel.SetActive(!isActive);
        }
    }
    public void OpenExamPanel()
    {
        if (examPanel != null)
        {
            bool isActive = examPanel.activeSelf;
            examPanel.SetActive(!isActive);
        }
    }
    public void OpenTaskInstallPanel() // this only call in not doing exam statement and use in dropdown task
    {
        if (taskInstallPanel != null)
        {
           
            if (isDoingExam)
            {
                if (examPanel)
                {
                    examPanel.SetActive(false);
                }
            }
            PlayerPrefs.SetInt("stateMenu", 3); // when user go to task install panel but not in exam
            bool isActive = taskInstallPanel.activeSelf;
            taskInstallPanel.SetActive(!isActive);
        }
    }
    public void OpenMenu()
    {
        if (menuPanel != null)
        {
            bool isActive = menuPanel.activeSelf;
            menuPanel.SetActive(!isActive);
        }
    }
    public void OpenVideoPanel()
    {
        if (videoPanel != null)
        {
            bool isActive = videoPanel.activeSelf;
            videoPanel.SetActive(!isActive);
        }
    }
    public void checkScoreRemove()
    {
       // scoreInstallPanel.SetActive(false);
        scoreRemovePanel.SetActive(true);
        scoreBackBtn.SetActive(true);
       // checkScoreRemoveBtn.SetActive(false);
        TaskManager.instance.checkScore();
    }
    public void checkScoreInstall ()
    {
        scoreInstallPanel.SetActive(true);
       // scoreRemovePanel.SetActive(false);
        scoreBackBtn.SetActive(true);
       // checkScoreInstallBtn.SetActive(false);
        TaskManager.instance.checkScore();
    }

    public void WeatherLeftBtn()
    {
        if (weatherIcon != null)
        {
            weatherLeftBtn.SetActive(false);
            weatherRightBtn.SetActive(true);
            weatherIcon.sprite = rainImg;

            rainWeather.SetActive(true);
            rainScript.StartRain(rainValue);
            rainWeather.GetComponent<SetTransparency>().FadeT(1f);
        }
    }
    public void WeatherRightBtn()
    {
        if (weatherIcon != null)
        {
            weatherLeftBtn.SetActive(true);
            weatherRightBtn.SetActive(false);
            weatherIcon.sprite = sunImg;
            rainScript.StopRain();
            rainWeather.GetComponent<SetTransparency>().FadeT(0f);
        }
    }
    public void OnSceneBtnPressed()
    {
        Debug.Log("task cocpit unfinished in all" + TaskManager.instance.checkTaskUnfinishedInAll(0));
        if (isDoingExam)
        {
           var animMes = messagePopUp.GetComponent<Animator>();
            if(animMes) animMes.SetBool("isActive", true);

            if (popUpTxt) popUpTxt.text = message1;
        }
        if (leftTabArea != null)
        {
            bool isActive = leftTabArea.activeSelf;
            leftTabArea.SetActive(!isActive);
            systemMenuPanel.SetActive(isActive);
        }
    }
    public void MessageBackBtn()
    {
        var animMes = messagePopUp.GetComponent<Animator>();
        if (animMes) animMes.SetBool("isActive", false);
    }
    public void OnSystemChange(int value)
    {
        if (value == 9)
        {
            dropdownSystemPart.SetActive(true);
            OnSystemPartChange(dropdownSystemPart.GetComponent<Dropdown>().value);
        }
        else
        {
            dropdownSystemPart.SetActive(false);
        }
    }
    public void OnPartChange(int value)
    {
        if (value == 10)
        {
            if (isDoingExam)
            {
                dropdownPart.SetActive(false);
                OpenExamPanel();
            }
            else
            {
                dropdownTask.SetActive(true);
                OnTaskChange(dropdownTask.GetComponent<Dropdown>().value);
            }
        }
        else
        {
            dropdownTask.SetActive(false);
        }
    }
    public void OnSystemPartChange(int value)
    {
        if (value == 2)
        {
            dropdownPart.SetActive(true);
            OnPartChange(dropdownPart.GetComponent<Dropdown>().value);
            dropdownSystemPart.SetActive(false);
            dropdownSystem.SetActive(false);
        }
        else
        {
            dropdownPart.SetActive(false);
        }
    }
    public void OnTaskChange(int value)
    {
        if (value == 0)
        {
            stateTask = state.Remove;
            taskInstallPanel.SetActive(false);
            taskRemovePanel.SetActive(true);
            if (!isDoingExam)
            {
                PlayerPrefs.SetInt("stateMenu", 1);
            }
        }
        else if (value == 1)
        {
            stateTask = state.Install;
            taskInstallPanel.SetActive(true);
            taskRemovePanel.SetActive(false);
            if (!isDoingExam)
            {
                PlayerPrefs.SetInt("stateMenu", 3);
            }
        }
        else
        {
            taskInstallPanel.SetActive(false);
            taskRemovePanel.SetActive(false); 
        }
    }
    public void OnAirplaneChange()
    {
        if (airplanePanel != null)
        {
            BackToMainMenu();
            airplanePanel.SetActive(true);
            MenuSelection();
        }
    }
    public void MenuSelection()
    {
        bool isActive = systemMenuPanel.activeSelf;
        systemMenuPanel.SetActive(!isActive);
    }
    void BackToMainMenu()
    {
        Debug.Log("back");
        dropdownPart.SetActive(false);
        dropdownTask.SetActive(false);
        dropdownSystem.SetActive(true);
        OnSystemChange(dropdownSystem.GetComponent<Dropdown>().value);
        taskInstallPanel.SetActive(false);
        taskRemovePanel.SetActive(false);
        examPanel.SetActive(false);
       /* if (checkScoreInstallBtn && checkScoreRemoveBtn)
        {
            checkScoreInstallBtn.SetActive(false);
            checkScoreRemoveBtn.SetActive(false);
        }*/
    }
    public void changeAirplane()
    {
        airplanePanel.SetActive(false);
        sceneBtn.SetActive(true);
    }
    public void InstallationExam()
    {
        PlayerPrefs.SetInt("CheckTaskCockpit", 1);
        PlayerPrefs.SetInt("examID", 2);
        PlayerPrefs.SetInt("stateMenu", 4);
        SceneManager.LoadScene("Parking 2");
        /* if (isDoingExam)
         {
            // examId = 2;
         }*/
    }
    public void scoreBackPress()
    {
        scoreInstallPanel.SetActive(false);
        scoreRemovePanel.SetActive(false);
       /* if(stateTask == state.Install)
        {
            checkScoreInstallBtn.SetActive(true);
        }
        else
        {
            checkScoreRemoveBtn.SetActive(true);
        }*/
        scoreBackBtn.SetActive(false);
    }
    public void HomeBtn()
    {
        // AuthenManager.instance.SaveScoreBtn();
        // Score.instance.CreateScoreDetail();
        PlayerPrefs.SetInt("stateMenu", 0);
        if (isDoingExam)
        {
            var examId = PlayerPrefs.GetInt("examID");
            if (examId == 1)
            {
                PlayerPrefs.SetFloat("totalScoreRemove", TaskManager.instance.totalScoreRemove);
            }
            else if( examId == 2)
            {
                PlayerPrefs.SetFloat("totalScoreInstall", TaskManager.instance.totalScoreInstall);
            }
        }
        SceneManager.LoadScene("LoginUser");
    }
}
