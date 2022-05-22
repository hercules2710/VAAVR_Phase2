using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolSlot : MonoBehaviour
{
    public string toolType;
    public Image icon;
    public Text toolText;
    public void getTool()
    {
        ToolsBox.instance.getToolBtn(toolType);
    }
    public void setName()
    {
        toolText.text = toolType;
    }
    public void setIcon(Sprite sprite)
    {
        icon.sprite = sprite;
    }
}
