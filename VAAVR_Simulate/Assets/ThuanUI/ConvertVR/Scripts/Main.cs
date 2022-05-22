using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public static Main instaince;
    public AuthenManager authenmanager;

    public UserInform userInform;
    // Start is called before the first frame update
    void Start()
    {
        instaince = this;
        authenmanager = GetComponent<AuthenManager>();
        userInform = GetComponent<UserInform>();
    }
}
