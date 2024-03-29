﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class TaskListManager : BaseManager<TaskListManager>,ISavedData
{
    [SerializeField] private TaskDatabase taskDB;

    public List<string> taskList { get; private set; } = new List<string>();

    public UnityEvent    onTaskRemoved;
    public int numberOfTimesAsked { get; private set; }

    private void Awake()
    {
        //Get all tasks and put it in the list of this manager  
        ResetTasks();
    }
    protected override void Start()
    {
        base.Start();
        InitializeSavedData();
    }

    public string GetTask(string TaskId)
    {
        return taskDB.GetData(TaskId).GetTask();
    }

    public void RemoveTaskFromList(string TaskId)
    {
        string task;

        if (taskDB.GetData(TaskId) == null) return;

         task= taskDB.GetData(TaskId).GetTask();

        for (int i = 0; i < taskList.Count; i++)
        {
            if (task == taskList[i])
            {
                this.onTaskRemoved.Invoke();
                taskList.Remove(taskList[i]);
            }
        }

        SaveData();
    }

    public void ResetTasks()
    {
        if (this.taskList.Count >= 1) this.taskList.Clear();
        for (int i = 0; i < taskDB.dataSet.Count; i++)
        {
            this.taskList.Add(taskDB.dataSet[i].GetTask());
            
        }
    }
    

    public void AddNumberOfTimesAsked()
    {
        this.numberOfTimesAsked += 1;
    }

    public void InitializeSavedData()
    {
        if (!SaveManager.DoesFileExist("PlayerData")) return;

        PlayerSave playerData = SaveManager.LoadData<PlayerSave>("PlayerData");
        this.taskList = playerData.savedTaskList;
    }

    public void SaveData()
    {
        SaveManager.Instance.playerSavedData.savedTaskList = this.taskList;
    }
}
