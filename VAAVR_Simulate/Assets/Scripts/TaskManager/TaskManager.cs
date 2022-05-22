using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    #region singleton
    public static TaskManager instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion
    [System.Serializable]

    // task ID is number[] of task in task list
    public class taskObj
    {
        public string taskName;
        public string taskInstallName;
        public GameObject objHasTask;
        public int taskOrder; // if task is reverse - obj must have order same as last step task
        public float score;
        [HideInInspector]
        public GameObject taskUI;
        [HideInInspector]
        public GameObject taskInstallUI;
        [HideInInspector]
        public GameObject taskRemoveScoreUI;
        [HideInInspector]
        public GameObject taskInstallScoreUI;
    }
    enum state{  Remove, Install };
    public List<taskObj> taskObjsList;
    public GameObject taskListUI;
    public GameObject taskInstallListUI;
    public GameObject scoreRemoveScrollList;
    public GameObject scoreInstallScrollList;
    public Text totalScoreRemoveTxt;
    public Text totalScoreInstallTxt;
    public GameObject taskUIPrefab;
    public GameObject scoreItemPrefab;
    [HideInInspector]
    public int taskActiveNum;
    [HideInInspector]
    public float totalScoreInstall = 0;
    [HideInInspector]
    public float totalScoreRemove = 0;
    [HideInInspector]
    public bool isDoingExam;
    private bool isReinstall;
    private state stateTask = state.Remove;
    private int maxOrder = 0;
    // Start is called before the first frame update
    void Start()
    {
        var checkExam = PlayerPrefs.GetInt("exam");
        if(checkExam == 0)
        {
            isDoingExam = false;
        }
        else
        {
            isDoingExam = true;
        }
        SceneController.instance.checkExam();

        isReinstall = false;
        // must be in order
        SortTask();
        ResetTask();
        ResetTaskInstall();
        checkTask();
        setCurrentTask();
    }
    public void ResetTask()
    {
        foreach(taskObj taskObjNode in taskObjsList)
        {
            taskObjNode.taskUI = Instantiate(taskUIPrefab, taskListUI.transform);
            if (scoreItemPrefab)
            {
                taskObjNode.taskRemoveScoreUI = Instantiate(scoreItemPrefab, scoreRemoveScrollList.transform);
                taskObjNode.taskRemoveScoreUI.GetComponent<TaskScoreItem>().setTaskName(taskObjNode.taskName);
            }
            Text text = taskObjNode.taskUI.GetComponent<Text>();
            if (!text)
            {
                text = taskObjNode.taskUI.GetComponentInChildren<Text>();
            }
            text.text = taskObjNode.taskName;
           // taskObjNode.taskUI.SetActive(false);
        }
    }
    public void ResetTaskInstall()
    {
        for(int i = taskObjsList.Count-1; i >= 0; i--)
        {
            taskObjsList[i].taskInstallUI = Instantiate(taskUIPrefab, taskInstallListUI.transform);
            if (scoreItemPrefab)
            {
                taskObjsList[i].taskInstallScoreUI = Instantiate(scoreItemPrefab, scoreInstallScrollList.transform);
                taskObjsList[i].taskInstallScoreUI.GetComponent<TaskScoreItem>().setTaskName(taskObjsList[i].taskInstallName);
            }
            Text textInstall = taskObjsList[i].taskInstallUI.GetComponent<Text>();
            if (!textInstall)
            {
                textInstall = taskObjsList[i].taskInstallUI.GetComponentInChildren<Text>();
            }
            textInstall.text = taskObjsList[i].taskInstallName;
            if (taskObjsList[i].objHasTask)
            {
                taskObjsList[i].objHasTask.GetComponent<Task>().taskID = i;
            }
        }
    }
    private void SortTask()
    {
        if (taskObjsList.Count < 1) return;

        taskObj temp;
        for(int i = 0; i < taskObjsList.Count -1 ; i++)
        {
            for(int j = i+1;j<taskObjsList.Count; j++)
            {
                if (taskObjsList[i].taskOrder > taskObjsList[j].taskOrder)
                {
                    temp = taskObjsList[i];
                    taskObjsList[i] = taskObjsList[j];
                    taskObjsList[j] = temp;
                }
            }
        }
        taskActiveNum = taskObjsList[0].taskOrder;
       // taskActiveInstallNum = taskActiveNum;
        maxOrder = taskObjsList[taskObjsList.Count - 1].taskOrder;
    }
    public void setCurrentTask()
    {
        int orderCount = 0;
        int orderFinished = 0;
        int orderInstall = 0;
        int orderInstallCount = 0;


        foreach (taskObj taskObjNode in taskObjsList)
        {
            if (taskActiveNum == maxOrder + 1)
            {
                if(stateTask == state.Remove)
                {
                    enableMaxOrder();
                    orderCount++;
                    break;
                }
            }
            if (taskObjNode.objHasTask)
            {
                var taskComponent = taskObjNode.objHasTask.GetComponent<Task>();
                if (taskObjNode.taskOrder == taskActiveNum)
                {
                    // taskObjNode.taskUI.SetActive(true);
                    if (taskComponent.isFinished)
                    {
                        orderFinished++;
                    }
                    else
                    {
                        taskObjNode.objHasTask.transform.tag = "isActiveTask";
                        var collider = taskObjNode.objHasTask.GetComponent<Collider>();
                        if (collider) collider.enabled = true;
                    }
                    orderCount++;
                }
                else if (taskObjNode.taskOrder > taskActiveNum)
                {
                    if (taskComponent.isFinished)
                    {
                        taskObjNode.objHasTask.transform.tag = "Untagged";
                    }
                }
                if (stateTask == state.Install)
                {
                    var taskActiveInstall = taskActiveNum;
                    if (!isReinstall)
                    {
                        taskActiveInstall = taskActiveNum - 1;
                    }
                    else
                    {
                        taskActiveInstall = taskActiveNum;
                    }


                    if (taskObjNode.taskOrder == taskActiveInstall)
                    {
                        taskObjNode.objHasTask.transform.tag = "isActiveTask";
                        if (!taskComponent.isFinished)
                        {
                            orderInstall++;
                        }
                        orderInstallCount++;
                    }
                }
            }
            else // when task do not have obj
            {
                if (taskObjNode.taskOrder == taskActiveNum)
                {
                    orderFinished++;
                    orderCount++;
                }

                if (stateTask == state.Install)
                {
                    var taskActiveInstall = taskActiveNum;
                    if (!isReinstall)
                    {
                        taskActiveInstall = taskActiveNum - 1;
                    }
                    else
                    {
                        taskActiveInstall = taskActiveNum;
                    }

                    if(taskObjNode.taskOrder == taskActiveInstall)
                    {
                        orderInstallCount++;
                    }
                }
            }
        }


        switch (stateTask)
        {
            case state.Remove:
                if (orderFinished >= orderCount)
                {
                    isReinstall = false;
                    taskActiveNum++;
                    setCurrentTask();
                }
                break;

            case state.Install:
                if (orderInstall >= orderInstallCount && taskActiveNum >0)
                {
                    taskActiveNum--;
                  //  isInstalled = true;
                    isReinstall = false;
                    setCurrentTask();
                }
               /* else if(taskActiveNum > 0 && isInstalled && orderInstall < orderInstallCount)
                {
                    isInstalled = false;
                    setCurrentTask();
                }
                /*else if(orderInstall > 0 && !isReinstall)
                {
                    isReinstall = true;
                    taskActiveNum--;
                }*/
                break;
        }
        Debug.Log(taskActiveNum);
    }
    private void checkTask()
    {
        int countFinished = 0;
        foreach(taskObj taskObjNode in taskObjsList)
        {
            // taskObjNode.score = 10 / 19;
            if (taskObjNode.objHasTask)
            {
                if (taskObjNode.objHasTask.GetComponent<Task>().isFinished)
                {
                    if (!isDoingExam)
                    {
                        taskObjNode.taskInstallUI.GetComponent<TaskItem>().activeTask();
                        taskObjNode.taskUI.GetComponent<TaskItem>().finishedTask();
                    }
                    countFinished++;
                }
                else
                {
                    if (!isDoingExam)
                    {
                        taskObjNode.taskInstallUI.GetComponent<TaskItem>().finishedTask();
                        taskObjNode.taskUI.GetComponent<TaskItem>().activeTask();
                    }
                }
            }
            else
            {
                taskObjNode.taskInstallUI.GetComponent<TaskItem>().activeTask();
                taskObjNode.taskUI.GetComponent<TaskItem>().finishedTask();
            }
           
        }
    }
    public void checkScore()
    {
        totalScoreInstall = 0;
        totalScoreRemove = 0;
        foreach (taskObj taskObjNode in taskObjsList)
        {
            var removeScoreUI = taskObjNode.taskRemoveScoreUI.GetComponent<TaskScoreItem>();
            var installScoreUI = taskObjNode.taskInstallScoreUI.GetComponent<TaskScoreItem>();
            var score = taskObjNode.score;
            if (taskObjNode.objHasTask)
            {
                if (taskObjNode.objHasTask.GetComponent<Task>().isFinished)
                {
                    // totalScoreInstall -= score;
                    totalScoreRemove += score;
                    removeScoreUI.setScore(score);
                    installScoreUI.setScore(0);
                }
                else
                {
                    totalScoreInstall += score;
                    // totalScoreRemove -= taskObjNode.score;
                    removeScoreUI.setScore(0);
                    installScoreUI.setScore(score);
                }
            }
            else
            {
                removeScoreUI.setScore(score);
                installScoreUI.setScore(0);
            }
        }
        totalScoreInstall = Mathf.Round(totalScoreInstall);
        totalScoreRemove = Mathf.Round(totalScoreRemove);
        totalScoreInstallTxt.text =totalScoreInstall.ToString();
        totalScoreRemoveTxt.text = totalScoreRemove.ToString();
    }
    private void enableMaxOrder()
    {
        foreach(taskObj taskObj in taskObjsList)
        {
           if( taskObj.taskOrder == maxOrder)
            {
                taskObj.objHasTask.transform.tag = "isActiveTask";
            }
        }
    }
    public bool isAllTaskFinished()
    {
        int countFinished = 0;
        foreach (taskObj task in taskObjsList)
        {
            if (task.objHasTask)
            {
                if (task.objHasTask.GetComponent<Task>().isFinished)
                {
                    countFinished++;
                }
            }
        }
        if (countFinished == taskObjsList.Count)
        {
             return true;
        }
        return false;
    }
    public bool checkTaskUnfinishedInAll(int id)
    {
        int countFinished = 0;
        foreach (taskObj task in taskObjsList)
        {
            if (task.objHasTask)
            {
                if (!task.objHasTask.GetComponent<Task>().isFinished || taskObjsList[id].objHasTask.GetComponent<Task>().isFinished)
                {
                    countFinished++;
                }
            }
        }
        if (countFinished == taskObjsList.Count)
        {
            return true;
        }
        return false;
    }
    public void UpdateTask(int taskID)
    {
        if (!isDoingExam)
        { // Destroy(taskObjsList[taskID].taskUI);
            var taskItem = taskObjsList[taskID].taskUI;
            if (taskItem)
            {
                taskItem.GetComponent<TaskItem>().finishedTask();
            }
            var taskInstallItem = taskObjsList[taskID].taskInstallUI;
            if (taskInstallItem)
            {
                taskInstallItem.GetComponent<TaskItem>().activeTask();
            }
            // taskObjsList.Remove(taskObjsList[taskID]);
            // SortTask();
            // totalScoreRemove += taskObjsList[taskID].score;
            // totalScoreInstall -= taskObjsList[taskID].score;
        }

        stateTask = state.Remove;
        isReinstall = true;
        setCurrentTask();
    }
    public void UpdateTaskInstall(int taskID)
    {
        if (!isDoingExam)
        { 
            // Destroy(taskObjsList[taskID].taskUI);
            var taskItem = taskObjsList[taskID].taskUI;
            if (taskItem)
            {
                taskItem.GetComponent<TaskItem>().activeTask();
            }
            var taskInstallItem = taskObjsList[taskID].taskInstallUI;
            if (taskInstallItem)
            {
                taskInstallItem.GetComponent<TaskItem>().finishedTask();
            }
            // taskObjsList.Remove(taskObjsList[taskID]);
            // SortTask();
            // totalScoreRemove -= taskObjsList[taskID].score;
            // totalScoreInstall += taskObjsList[taskID].score;
        }

        stateTask = state.Install;
        setCurrentTask();
    }
    public bool checkActiveNum(int taskID)
    {
        Debug.Log(isReinstall);
        if(taskObjsList[taskID].taskOrder == taskActiveNum)
        {
            isReinstall = true;
            return true;
        }
        if(taskObjsList[taskID].taskOrder == taskActiveNum-1 && !isReinstall)
        {
            return true;
        }
        return false;
    }
    public void setObjTask(int taskID, GameObject obj)
    {
        taskObjsList[taskID].objHasTask = obj;
    }
    public bool checkTaskByID(int taskID)
    {
        var taskObj = taskObjsList[taskID].objHasTask;
        if (!taskObj)
        {
            return true;
        }
        else if(taskObj.GetComponent<Task>().isFinished)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
