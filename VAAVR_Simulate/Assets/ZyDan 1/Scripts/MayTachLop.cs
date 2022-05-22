using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MayTachLop : MonoBehaviour
{
    public Animator MTLAnimator;
    public Transform liftUp;
    public Transform slideLeft;
    public Transform slideRight;
    public Transform rotatorLeft;
    public Transform leftRotatorUpPos;
    public Transform leftRotatorDownPos;
    public Transform rotatorRight;
    public Transform rightRotatorUpPos;
    public Transform rightRotatorDownPos;
    public GameObject wheel;
    public bool UpDown = false;

    private Vector3 targetPos;
    private bool start = false;
    private Vector3 savePos;
    private bool isSaved =false;
    private float speed = 0f;
    private WheelInfo wheelInfoScript;
    private Vector3 rotateDirection;
    private float width;
    private float diameter;
    private float mass;

    // Start is called before the first frame update
    void Start()
    {
        wheel = null;
        MTLAnimator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //get wheel info, if wheel don't have info, all function be cancelled
        if(wheel !=null)
        {
            wheelInfoScript = wheel.GetComponent<WheelInfo>();
            if (!wheelInfoScript)
            {
                return;
            }
            else
            {
                diameter = wheelInfoScript.diameter;
                width = wheelInfoScript.width;
                mass = wheelInfoScript.mass;
            }
        }
      
        saveWheelPos();

        if (start)
        {
            if (detectWheel())
            {
                MoveWheel();
                Debug.DrawRay(slideLeft.position, slideLeft.forward, Color.red);

                Vector3 direction = targetPos - slideLeft.position;
                if (moveLiftUp(direction))
                {
                    float slideDistance = Vector3.Distance(slideRight.position, slideLeft.position);
                    float rotatorDistance = Vector3.Distance(rotatorLeft.position, rotatorRight.position);
                    //Debug.Log(slideDistance.magnitude);
                    if (slideDistance > width + 0.5f)
                    {
                        moveSlide(slideLeft, targetPos, slideLeft.position);
                        moveSlide(slideRight, targetPos, slideRight.position);
                    }

                    if (slideDistance <= width + 0.5f)
                    {
                        //remove rigidbody of wheel to lift it up with the liftUp
                        if (wheel.GetComponent<Rigidbody>())
                        {
                            Destroy(wheel.GetComponent<Rigidbody>());
                        }
                        //Debug.Log("hit");
                        LiftWheel();
                        //Debug.Log(rotatorDistance);
                        if (rotatorDistance >= width + 0.05f)
                        {
                            MoveRotator(rotatorLeft, leftRotatorDownPos);
                            MoveRotator(rotatorRight, rightRotatorDownPos);
                        }
                        else
                        {
                            //rotate wheel around slide's axis
                            speed = speed + Time.deltaTime * 10;
                            speed = Mathf.Clamp(speed, 0f, 10f);
                            wheel.transform.RotateAround(wheel.transform.position, slideLeft.forward, speed);

                            MTLAnimator.enabled = true;
                        }
                    }
                }
            }
        }
        else
        {
            MoveRotator(rotatorLeft, leftRotatorUpPos);
            MoveRotator(rotatorRight, rightRotatorUpPos);

            MTLAnimator.enabled = false;

            //stop wheel
            speed = speed - Time.deltaTime * 10f;
            speed = Mathf.Clamp(speed, 0f, 10f);
            if (wheel != null)
            {
                wheel.transform.RotateAround(wheel.transform.position, slideLeft.forward, speed);
            }

            if (speed <= 0)
            {
                Vector3 direction = savePos - slideLeft.position;
                if (moveLiftUp(direction))
                {
                    float slideDistance = Vector3.Distance(slideLeft.position, slideRight.position);
                    //Debug.Log(slideDistance);
                    if (slideDistance <= 1.25f)
                    {
                        moveSlide(slideLeft, slideLeft.position, targetPos);
                        moveSlide(slideRight, slideRight.position, targetPos);
                        if(wheel != null)
                        {
                            wheel.transform.parent = null;
                            if (!wheel.GetComponent<Rigidbody>())
                            {
                                //add rigidbody component to wheel
                                wheel.AddComponent<Rigidbody>();
                                wheel.GetComponent<Rigidbody>().mass = mass;
                            }
                            wheel.layer = 6;
                        }
                        wheel = null;
                    }
                }
            }
        }
    }

    private bool detectWheel() // detect any wheel between two slide
    {
        Collider[] hitColliders = Physics.OverlapBox(slideLeft.position + Vector3.forward * 0.5f, new Vector3(0.1f, 0.1f, 1f));
        if (hitColliders.Length < 1)
        {
            wheel = null;
            targetPos = slideLeft.transform.parent.position;
            return false;
        }

        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.transform.gameObject.transform.tag.Equals("Smooth"))
            {
                //get wheel
                if (hitCollider.transform.root == hitCollider.transform)
                {
                    wheel = hitCollider.transform.gameObject;
                }
                else
                {
                    wheel = hitCollider.transform.parent.gameObject;
                }

                targetPos = hitCollider.transform.position;
                wheel.transform.SetParent(liftUp);
                wheel.layer = 0;

                return true;
            }
        }
        return false;
    }

    private bool moveLiftUp(Vector3 direction) 
    {
        direction = new Vector3(0f, direction.y, 0f);
        liftUp.Translate(direction * Time.deltaTime);
        
        if(direction.magnitude < 0.01f)
        {
            return true;
        }
        return false;
    }

    private void moveSlide(Transform slide, Vector3 target, Vector3 start)
    {
        Vector3 direction = target - start;
        direction = new Vector3(0f, 0f, - direction.z);
        slide.Translate(direction * Time.deltaTime/2);
    }

    private void LiftWheel()
    {
        Vector3 direction = rotatorLeft.position - slideLeft.position;
        direction = new Vector3(0f, direction.y - diameter/2 - 0.3f, 0f);
        liftUp.Translate(direction * Time.deltaTime);
    }

    private void MoveRotator(Transform rotator, Transform targetPos)
    {
        rotator.position = Vector3.MoveTowards(rotator.position, targetPos.position, Time.deltaTime/2);
    }

    public void Up()
    {
        start = !start;
    }

    public Vector3 saveWheelPos()
    {
        if (wheel != null && !isSaved)
        {
            savePos = wheel.transform.position;
            isSaved = true;
        }

        if(wheel == null)
        {
            isSaved = false;
            savePos = slideLeft.position;
        }
        
        return savePos;
    }

    public void MoveWheel() // move wheel to between both slide
    {
        Vector3 targetPos = (slideLeft.position + slideRight.position)/2;
        targetPos = new Vector3(targetPos.x, wheel.transform.position.y, targetPos.z);
        wheel.transform.position = Vector3.MoveTowards(wheel.transform.position, targetPos, Time.deltaTime/2);
        rotateWheel();
    }

    public void rotateWheel() // rotate wheel's rotate direction to align with slide's rotate direction 
    {
        rotateDirection = wheelInfoScript.rotateVector;
        if (rotateDirection == new Vector3(0, 1, 0))
        {
            float angle = Vector3.SignedAngle(wheel.transform.up, slideLeft.forward, Vector3.up);
            Debug.DrawRay(wheel.transform.position, wheel.transform.right);
            //Debug.Log(angle);
            
            if(Mathf.Abs(angle) != 0 || Mathf.Abs(angle) != 180)
            {
                if (angle > 90 || angle < -90)
                {
                    Debug.Log("-90 - 90");
                    wheel.transform.rotation = Quaternion.RotateTowards(wheel.transform.rotation, Quaternion.LookRotation(-slideLeft.up, Vector3.up), 90 * Time.deltaTime);
                }
                if (angle > -90 && angle < 90)
                {
                    Debug.Log("90 - -90");
                    wheel.transform.rotation = Quaternion.RotateTowards(wheel.transform.rotation, Quaternion.LookRotation(slideLeft.up, Vector3.up), 90 * Time.deltaTime);
                }
            }
            Debug.Log("up");
        }

        if (rotateDirection == new Vector3(0, 0, 1))
        {
            float angle = Vector3.SignedAngle(wheel.transform.forward, slideLeft.forward, Vector3.up);
            Debug.DrawRay(wheel.transform.position, wheel.transform.right);
            //Debug.Log(angle);
            
            if(Mathf.Abs(angle) != 0 || Mathf.Abs(angle) != 180)
            {
                if (angle > 90 || angle < -90)
                {
                    Debug.Log("-90 - 90");
                    wheel.transform.rotation = Quaternion.RotateTowards(wheel.transform.rotation, Quaternion.LookRotation(-slideLeft.forward, Vector3.up), 90 * Time.deltaTime);
                }
                if (angle > -90 && angle < 90)
                {
                    Debug.Log("90 - -90");
                    wheel.transform.rotation = Quaternion.RotateTowards(wheel.transform.rotation, Quaternion.LookRotation(slideLeft.forward, Vector3.up), 90 * Time.deltaTime);
                }
            }
            Debug.Log("forward");
        }
        
        
        if (rotateDirection == new Vector3(1, 0, 0)) 
        {
            float angle = Vector3.SignedAngle(wheel.transform.right, slideLeft.forward, Vector3.up);
            Debug.DrawRay(wheel.transform.position, wheel.transform.right);
            //Debug.Log(angle);
            if(Mathf.Abs(angle) != 0 || Mathf.Abs(angle) != 180)
            {
                if (angle > 90 || angle < -90)
                {
                    Debug.Log("-90 - 90");
                    wheel.transform.rotation = Quaternion.RotateTowards(wheel.transform.rotation, Quaternion.LookRotation(slideLeft.right, Vector3.up), 90 * Time.deltaTime);
                }
                if (angle > -90 && angle < 90)
                {
                    Debug.Log("90 - -90");
                    wheel.transform.rotation = Quaternion.RotateTowards(wheel.transform.rotation, Quaternion.LookRotation(-slideLeft.right, Vector3.up), 90 * Time.deltaTime);
                }
            }
            Debug.Log("right");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(slideLeft.position + Vector3.forward*0.5f, new Vector3(0.1f, 0.1f, 1f));
    }
}
