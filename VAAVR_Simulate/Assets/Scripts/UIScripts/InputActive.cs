using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputActive : MonoBehaviour, IPointerClickHandler
{
    public GameObject keyBoardVR;
    public void OnPointerClick(PointerEventData eventData)
    {
        var inputfield = GetComponent<InputField>();
        if (!inputfield) return;

        keyBoardVR.SetActive(true);
        VRKeyBoardManager.instance.setInputField(inputfield);
    }
}
