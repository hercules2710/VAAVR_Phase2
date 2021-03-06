using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateDistance : MonoBehaviour
{
    public Transform tyre;
    public Transform pointer;
    public int scoreDistanceMin;
    public int scoreDistanceMax;

    private float startPosition;
    private float currentPosition;
    private float distance;
    private float savedDistance;
    private bool isCalculating;
    private GameObject text;
    private Task task;
    // Start is called before the first frame update
    void Start()
    {
        isCalculating = true;
        startPosition = tyre.position.y;
        /*if (PlaceJack.instance.isLifted)
        {
            startPosition = -0.3f;
        }*/
        distance = 0f;
        savedDistance = 0f;
        task = GetComponent<Task>();
        text = transform.Find("Text").gameObject;
        //Debug.Log("start " + startPosition);
    }

    // Update is called once per frame
    void Update()
    {
        currentPosition = tyre.position.y;
        distance = Mathf.Round((currentPosition - startPosition) * 1000f) * 0.001f;
       
        if(distance != savedDistance)
        {
            pointer.position = pointer.position + new Vector3(0f, (distance-savedDistance), 0f);
            savedDistance = distance;
        }

        float distanceCm = distance * 100f;

        if(distanceCm > 0)
        {
            text.GetComponent<TextMesh>().text = distanceCm.ToString() + " cm";
            if(distanceCm >= scoreDistanceMin && distanceCm < scoreDistanceMax && !isCalculating)
            {
                isCalculating = true;
                task.FinishedTask();
            }
        }
        else
        {
            distanceCm = 0f;
            text.GetComponent<TextMesh>().text = distanceCm.ToString() + " cm"; 
            if (isCalculating)
            {
                task.ActiveTask();
                isCalculating = false;
            }
        }
        
    }
}
