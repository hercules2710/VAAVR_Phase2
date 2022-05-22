using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lerpTest : MonoBehaviour
{
    public Transform startPos, endPos;
    public float duration;
    float time = 0f;
    public bool move;

    private bool isSaved;
    // Start is called before the first frame update
    //void Start()
    

    // Update is called once per frame
    void Update()
    {
        //StartCoroutine(LerpPosition(endPos.position, duration));
        //if (move)
        //{
        //    if (isSaved)
        //    {
        //        startPos = transform;
        //        Debug.Log(startPos.position);
        //        isSaved = false;
        //    }
        //    float rate = 1 / duration;

        //    if(time <= 1)
        //    {
        //        time += Time.deltaTime * rate;
        //        //float distCovered = time / duration;
        //        transform.position = Vector3.Lerp(startPos.position, endPos.position, time);
        //        //transform.localEulerAngles = Vector3.Lerp(startPos.localEulerAngles, new Vector3(1, 0, 1) * 90, time);
        //    }

        //}
        //else
        //{
        //    isSaved = true;
        //    time = 0f;
        //}
        //Debug.Log(time);
    }

    private void Start()
    {
        isSaved = false;
        //StartCoroutine(LerpPosition(endPos.position, duration));
        //StartCoroutine(MoveObject(startPos.position, endPos.position, 10f));
        Debug.Log("1");
    }

    IEnumerator LerpPosition(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = transform.position;

        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
    }
}
