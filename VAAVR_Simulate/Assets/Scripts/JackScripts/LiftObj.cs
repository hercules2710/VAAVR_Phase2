using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftObj : MonoBehaviour
{
   // public enum LiftDirection { X,Y,Z}
   // public LiftDirection liftDirection;

    private float savePositonY;

    // Start is called before the first frame update
    void Start()
    {
        savePositonY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(savePositonY > transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, savePositonY, transform.position.z);
        }
    }
}
