using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInform : MonoBehaviour
{
    // Start is called before the first frame update
    public string ids { get; private set; }
    public string scoreIds { get; private set; }
    string usernames;
    string userpasswords;
    string types;

    private string customerName;

    public void SetInvalidPassword(string username, string userpassword)
    {
        usernames = username;
        userpasswords = userpassword;
    }
    public void SetuserID(string id)
    {
        ids = id;
    }
    public void SetScoreID(string scoreId)
    {

        scoreIds = scoreId;
    }

    public string CustomerName
    {
        get { return customerName; }
        set { customerName = value; }
    }
}
