using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubberBand : MonoBehaviour
{
    public GameObject attachWith;

    private Vector3 savePos;
    private float current = 0;
    private float initial = 0;
    // Start is called before the first frame update
    void Start()
    {
        savePos = attachWith.transform.localPosition;

    }

    // Update is called once per frame
    void Update()
    {
        current = Mathf.Round((attachWith.transform.localPosition.x - savePos.x) * 100) * 0.01f;
        if (current != initial)
        {
            transform.localPosition = transform.localPosition + new Vector3(
                Mathf.Round((attachWith.transform.localPosition.x - savePos.x) * 100) * 0.01f,
                Mathf.Round((attachWith.transform.localPosition.y - savePos.y) * 100) * 0.01f,
                Mathf.Round((attachWith.transform.localPosition.z - savePos.z) * 100) * 0.01f);
            initial = current;
        }
        
        Debug.Log(Mathf.Round((attachWith.transform.localPosition.x - savePos.x)*100)*0.01f);
    }
}
