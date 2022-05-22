using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceJack : MonoBehaviour
{
    public static PlaceJack instance;
    private void Awake()
    {
        instance = this;
    }
    [SerializeField] private float radius; // y axis distance ground check point - ground; 0.088 is size of body box collider by y axis

    public Transform body;
    public Transform handle;
    public Transform cylinder1;
    public Transform dome;
    public Transform groundCheck;
    public GameObject sideViewDisplay;
    public GameObject ruler;
    public bool isPlaced;
    public bool isLifted;
    public LayerMask groundLayerMask;

    private Animator sideViewAnimator;
    // Start is called before the first frame update
    void Start()
    {
        sideViewAnimator = sideViewDisplay.GetComponent<Animator>();
        if (sideViewAnimator == null)
        {
            return;
        }
        if (isLifted)
        {
            placeJack();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (placeJack())
        {
            // make sure the jack in right position and on ground
            if (IsGrounded())
            {
                isPlaced = true;
            }
            else
            {
                isPlaced = false;
            }
            ruler.SetActive(true);
            //sideViewDisplay.SetActive(true);
            sideViewAnimator.SetBool("show", true);
        }
        else
        {
            sideViewAnimator.SetBool("show", false);
            //sideViewDisplay.SetActive(false);
            ruler.SetActive(false);
            isPlaced = false;
        }
    }

    private bool placeJack()
    {
        //measure distance between the jack and the contact dome
        float distanceCheck = Vector3.Distance(new Vector3(dome.position.x, 0f, dome.position.z), new Vector3(cylinder1.position.x, 0f, cylinder1.position.z));
        Vector3 direction = dome.position - cylinder1.position;
        //Debug.Log(distanceCheck);
        //Debug.Log(cylinder1.Find("Ring").transform.position.y - dome.position.y);

        //help to move the jack when it closes to the contact dome and the cylinder is lowwer than the dome
        if (distanceCheck <= 0.1f)
        {
            if(cylinder1.Find("Ring").transform.position.y - dome.position.y < -0.01f)
            {
                direction = new Vector3(direction.x, 0f, direction.z);
                body.GetComponent<Rigidbody>().MovePosition(body.position + direction * Time.deltaTime * 2f);
            }
            return true;
        }
        else
        {
            return false;
        }

        
        //Debug.Log(Vector3.Distance(new Vector3(dome.position.x, 0f, dome.position.z), new Vector3(cylinder1.position.x, 0f, cylinder1.position.z)));
        //if(distanceCheck <0.01f)
        //{
        //    return true;
        //}
        //else
        //{
        //    return false;
        //}

    }

    //dectect if the jack is on ground or not
    private bool IsGrounded()
    {
        if(Physics.CheckSphere(groundCheck.position + new Vector3(0.6f, 0f, 0f), radius, groundLayerMask) && 
            Physics.CheckSphere(groundCheck.position, radius, groundLayerMask))
        {
            return true;
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, radius);
        Gizmos.DrawWireSphere(groundCheck.position + new Vector3(0.6f, 0f, 0f), radius);
    }
}

