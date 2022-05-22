using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VRKeyBoardManager : MonoBehaviour
{
    public static VRKeyBoardManager instance;

    private void Awake()
    {
        instance = this;
    }

    public InputField userInput;
    private InputField inputField;

    public void input(string text)
    {
        if (!inputField) return;

        inputField.text += text;
    }
    public void setInputField(InputField field)
    {
        inputField = field;
    }
    public void Deactive()
    {
        //inputField.text = KeyCode.Return;
        inputField = null;
        gameObject.SetActive(false);
    }
    public void deleteKey()
    {
        if (!inputField) return;

        if (inputField.text.Length < 1) return;

        inputField.text = inputField.text.Substring(0, inputField.text.Length - 1);
    }
    public void spaceKey()
    {
        if (!inputField) return;

        inputField.text += " "; 
    }
}
