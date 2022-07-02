using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenAndCloseTagBox : MonoBehaviour
{
    public bool isOpen;
    private Animator tagBoxAnim;
    // Start is called before the first frame update
    void Start()
    {
        tagBoxAnim = GetComponent<Animator>();
        isOpen = false;
    }

    // Update is called once per frame
    
    public void Toggle()
    {
        if (isOpen)
        {
            tagBoxAnim.SetBool("IsOpen", true);
        }
        else
        {
            tagBoxAnim.SetBool("IsOpen", false);
        }
        isOpen = !isOpen;

    }
}
