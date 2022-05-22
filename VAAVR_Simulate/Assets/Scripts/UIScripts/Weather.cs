using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weather : MonoBehaviour
{
    public GameObject weatherPanel;
    private Animator animator;
    private bool isOpen;
    // Start is called before the first frame update
    void Start()
    {
        animator = weatherPanel.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LeftBtn()
    {
        animator.SetBool("Right", isOpen);
    }
    public void RightBtn()
    {

        animator.SetBool("Right", !isOpen);
    }

}
