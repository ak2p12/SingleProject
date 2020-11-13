using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//기본적으로 모든 태스크는 결과값을 가지고 있다.
public abstract class Task
{
    protected bool result = false;
    public abstract bool Result(Unit _unit);
}

//메인
public class BehaviorTree : Task
{
    public Task root;
    public bool endRoot;

    public void Init(Task _task)
    {
        root = _task;
        endRoot = false;
    }

    public override bool Result(Unit _unit)
    {
        if (!endRoot)
            endRoot = root.Result(_unit);
        return endRoot;
    }
}

//Composite Task
//실질적인 루프 
public abstract class Composite : Task 
{
    public List<Task> List_childTask = new List<Task>();     //자식 태스크를 담을 리스트

    public void AddTask(Task _task)
    {
        List_childTask.Add(_task);
    }

    public List<Task> GetListTask()
    {
        return List_childTask;
    }

    public abstract override bool Result(Unit _unit);
}

//자식 노드중 하나라도 true 반환하면 즉시 true 반환
//자식 노드중 전부 false 반환하면 false 반환
public class Selecter : Composite
{
    public override bool Result(Unit _unit)
    {
        for (int i = 0; i < List_childTask.Count; ++i )
        {
            if (true == List_childTask[i].Result(_unit))
            {
                return true;
            }
        }

        return false;
    }
}

//자식 노드중 하나라도 false 반환하면 즉시 false 반환
//자식 노드중 전부 true 반환하면 true 반환
public class Sequence : Composite
{
    public override bool Result(Unit _unit)
    {
        for (int i = 0; i < List_childTask.Count; ++i)
        {
            if (false == List_childTask[i].Result(_unit))
            {
                return false;
            }
        }

        return true;
    }
}

//자식 노드 순차적 실행
//넣은 순서로 실행
public class Parallel : Composite
{
    public override bool Result(Unit _unit)
    {
        for (int i = 0; i < List_childTask.Count; ++i)
        {
            List_childTask[i].Result(_unit);
        }

        return true;
    }
}

//조건 성립하면 자식노드 실행
//자식노드가 true를 반환하면 true 반환
//조건에는 성립하지만 자식노드가 false를 반환하면 false 반환

public abstract class Decorator : Task
{
    protected Task childTask;
    public abstract void SetTask(Task _task);
    public abstract bool ChackCondition(Unit _unit);
    public abstract override bool Result(Unit _unit);
}

public abstract class Condition : Decorator
{
    public abstract override void SetTask(Task _task);
    public abstract override bool ChackCondition(Unit _unit);
    public abstract override bool Result(Unit _unit);
}

public abstract class TimeOut : Decorator
{
    protected float timeOut;
    protected float currentTime;

    public abstract override void SetTask(Task _task);
    public abstract void SetTime(float _time);
    public abstract override bool ChackCondition(Unit _unit);
    public abstract override bool Result(Unit _unit);
}

public abstract class ActionTask : Task
{
    protected bool isStart = false;
    public abstract void OnStart(Unit _unit);
    public abstract bool OnUpdate(Unit _unit);
    public abstract bool OnEnd(Unit _unit);
    public abstract override bool Result(Unit _unit);
}


