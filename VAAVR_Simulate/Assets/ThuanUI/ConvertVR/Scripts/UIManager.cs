using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    //Screen object variables
    public GameObject loginUI;
    public GameObject registerUI;
    public GameObject scorePanel;
    public GameObject scorePanelDemo;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    //Functions to change the login screen UI
    public void LogOut()
    {
        scorePanel.SetActive(false);
        loginUI.SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoginScreen() //Back button
    {
        loginUI.SetActive(true);
        registerUI.SetActive(false);
    }
    public void CancelLogin()
    {
        loginUI.SetActive(false);
        scorePanelDemo.SetActive(true);
    }

    public void RegisterScreen() // Regester button
    {
        loginUI.SetActive(false);
        registerUI.SetActive(true);
    }

    public void ScorePanel()
    {
        loginUI.SetActive(false);
        scorePanel.SetActive(true);
    }
    public void ScorePanelDemo()
    {
        loginUI.SetActive(false);
        scorePanelDemo.SetActive(true);
    }

    public void GoToNLG()
    {
        PlayerPrefs.SetInt("exam", 0);
        PlayerPrefs.SetInt("CheckTaskCockpit", 0);
        PlayerPrefs.SetInt("stateMenu", 0);
        SceneManager.LoadScene("Parking 1");
    }
    public void TakeExam()
    {
        PlayerPrefs.SetInt("exam", 1);
        PlayerPrefs.SetInt("CheckTaskCockpit", 0);
        PlayerPrefs.SetInt("stateMenu", 0);
        SceneManager.LoadScene("Parking 1");
    }
}
