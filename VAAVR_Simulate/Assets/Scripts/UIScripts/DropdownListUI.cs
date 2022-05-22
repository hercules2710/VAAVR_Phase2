using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownListUI : MonoBehaviour
{
    public Dropdown dropdown;
    public List<Dropdown.OptionData> optionList;

    private int order;
    // Start is called before the first frame update
    void Start()
    {
        dropdown.options.Clear();

        dropdown.AddOptions(optionList);
    }
}
