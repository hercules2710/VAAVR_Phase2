using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.Playables;

public class LockWire : MonoBehaviour
{
    public GameObject LockWire2Prefab;
    public GameObject LockWire1Prefab;
    public GameObject LockWire2Placement;
    public GameObject LockWire1Placement;
    public GameObject NewLockWire;
    public PlayableDirector timeline;
    public Rigidbody hubcapkitRigi;
    public int taskIDLockWire2;
    public int taskIDLockWire1;
   // public int taskOrder;
    public void InstallLockWire()
    {
        var wire2 = Instantiate(LockWire2Prefab, LockWire2Placement.transform);
        TaskManager.instance.setObjTask(taskIDLockWire2, wire2);
        var task2 = wire2.GetComponent<Task>();
        task2.taskID = taskIDLockWire2;
        task2.ActiveTask();
        wire2.GetComponent<FixedJoint>().connectedBody = hubcapkitRigi;

        var wire2_1 = Instantiate(LockWire1Prefab, LockWire1Placement.transform);
        TaskManager.instance.setObjTask(taskIDLockWire1, wire2_1);
        var task2_1 = wire2_1.GetComponent<Task>();
        task2_1.taskID = taskIDLockWire1;
        task2_1.ActiveTask();
        wire2_1.GetComponent<FixedJoint>().connectedBody = hubcapkitRigi;

        ParkingSceneManager.instance.showHandControll();
    }
    public void placeLockWire()
    {
        if (TaskManager.instance.checkActiveNum(taskIDLockWire2))
        {
            ParkingSceneManager.instance.hideHandControll();
            timeline.Play();
            NewLockWire.SetActive(false);
        }
    }
}
