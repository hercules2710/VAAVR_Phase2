using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AuthenManager : MonoBehaviour
{
    public static AuthenManager instance;
    private void Awake()
    {
        instance = this;
    }

    [Header("Login")]
    public InputField emailLogin;
    public InputField passwordLogin;
    //public Button loginButtonUser;
    public Text warningText;

    [Header("Register")]
    public InputField emailRegister;
    public InputField passwordRegister;
    public InputField passwordConfirm;
    //public Button registerButtonUser;
    public Text errorMessageRe;

    private void Start()
    {
        int exam = PlayerPrefs.GetInt("exam");
        if(exam == 1)
        {
            int examID = PlayerPrefs.GetInt("examID");
            float score = 0;
            if (examID == 1)
            {
                score = PlayerPrefs.GetFloat("totalScoreRemove");
                SaveScoreBtn("Exam Removal: " + score.ToString());
            }
            else if( examID == 2)
            {
                score = PlayerPrefs.GetFloat("totalScoreInstall");
                SaveScoreBtn("Exam Installation: " + score.ToString());
            }
        }
    }
    public void SaveScoreBtn(string score)
    {
        string user_id = Main.instaince.userInform.ids;
        if (user_id == null) return;
        StartCoroutine(SaveScore(score, user_id));
    }
    public void LoginButton()
    {
        //SceneManager.LoadScene(0);
        StartCoroutine(LoginUser());
    }
    public void RegisterButton()
    {
        StartCoroutine(RegisterUser());
    }

    public IEnumerator LoginUser()
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", emailLogin.text);
        form.AddField("loginPass", passwordLogin.text);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/VR_Data_Save/Login.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                warningText.text = "Cannot connnect to localhost";
                if (emailLogin.text == "DEMO" && passwordLogin.text == "VAAVR2021")
                {
                    UIManager.instance.CancelLogin();
                }
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                //Main.instaince.UserInform.SetInvalidPassword(UsernameLogin.text, PasswordLogin.text);

                Main.instaince.userInform.SetuserID(www.downloadHandler.text);

                if (www.downloadHandler.text.Contains("passlogin, Invalid Password") || www.downloadHandler.text.Contains("userlogin, Invalid Username"))
                {
                    warningText.text = "Invalid Username and Password";
                }
                else
                {
                    UIManager.instance.ScorePanel();
                }
            }

        }
    }

    public IEnumerator RegisterUser()
    {


        WWWForm form = new WWWForm();
        form.AddField("registerUser", emailRegister.text);
        form.AddField("registerPass", passwordRegister.text);
        form.AddField("registerPassCo", passwordConfirm.text);


        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/VR_Data_Save/Register.php", form))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                errorMessageRe.text = "Cannot connnect to localhost";
            }
            else
            {

                Debug.Log(www.downloadHandler.text);
                if (www.downloadHandler.text.Contains("available, Username is already taken"))
                {
                    errorMessageRe.text = "Username is already taken";
                }
                else if (www.downloadHandler.text.Contains("Use 6 to 50 characters for your username"))
                {
                    errorMessageRe.text = "Use 6 to 50 characters for your username";
                }
                else if (www.downloadHandler.text.Contains("Use 6 characters or more for your password"))
                {
                    errorMessageRe.text = "Use 6 characters or more for your password";
                }
                else if (www.downloadHandler.text.Contains("Username is required"))
                {
                    errorMessageRe.text = "Username is required";
                }
                else if (www.downloadHandler.text.Contains("Password is required"))
                {
                    errorMessageRe.text = "Password is required";
                }
                else if (www.downloadHandler.text.Contains("message, Created Sucessfully"))
                {
                    errorMessageRe.text = "Created Sucessfully";
                }
            }
        }
    }

    public IEnumerator SaveScore(string totalscore, string user_id)
    {

        WWWForm form = new WWWForm();
        form.AddField("total_score", totalscore);
        form.AddField("user_id", user_id);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/VR_Data_Save/TotalScore.php", form))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                //teacher_id = StartCoroutine(GetTeacherID(teacher_id)).ToString();
                user_id = Main.instaince.userInform.ids;
            }
        }
    }

    public IEnumerator GetScoreDS(string user_id, System.Action<string> callback)
    {
        user_id = Main.instaince.userInform.ids;
        WWWForm form = new WWWForm();
        form.AddField("user_id", user_id);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/VR_Data_Save/GetScoreDS.php", form))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                //Show result as text
                Debug.Log(www.downloadHandler.text);
                if (www.downloadHandler.text == "0")
                {
                    Debug.Log("Cannot get list from data");
                }
                else
                {
                    string jsonArray = www.downloadHandler.text;
                    //Call callback funtion to pass results
                    callback(jsonArray);
                }
            }
        }
    }

    public IEnumerator GetScore(string id, System.Action<string> callback)
    {
        WWWForm form = new WWWForm();
        form.AddField("id", id);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/VR_Data_Save/GetScore.php", form))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                //Show result as text
                Debug.Log(www.downloadHandler.text);
                string jsonArray = www.downloadHandler.text;
                //Call callback funtion to pass results

                callback(jsonArray);
            }
        }
    }
}
