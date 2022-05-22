using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VRKeyBoard : MonoBehaviour, IPointerClickHandler
{
    private Text textKey;
    // Start is called before the first frame update
    void Start()
    {
        textKey = GetComponentInChildren<Text>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        VRKeyBoardManager.instance.input(textKey.text);
    }
}
