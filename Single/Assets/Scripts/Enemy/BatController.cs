using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : Unit
{
    private Animator animatorController;
    private GameObject enemyCharacter;


    private BehaviorTree root;
    private Sequence BT_selecter;
    private Action_Roaming BT_roaming;
    private Action_Trace BT_trace;
    private Action_Attack BT_attack;

    
    private bool isRangeIn = false;    //공격범위?

    //private Task temp2;

    // Start is called before the first frame update
    void Start()
    {
        enemyCharacter = GetComponentInChildren<Transform>().gameObject;
        //root = new BehaviorTree();
        //selecter_1 = new Sequence();
        //root.SetTask(selecter_1);
        //selecter_1.AddTask();

        animatorController = GetComponentInChildren<Animator>();
        UnitInfo.MoveSpeed = 5.0f;
        //temp1 = new BT_Move("1번");
        //temp2 = new BT_Move("2번");
        //root.AddTask(temp1);
        //root.AddTask(temp1);
        StartCoroutine(Update_Coroution());
    }

    IEnumerator Update_Coroution()
    {
        //while (true)
        //{
        //    //if (root.Result())
        //    //{
        //    //    Debug.Log("메인 종료");
        //    //}

            yield return null;
        //}
    }
}


public class Condition_Find : Decorator
{
    private bool isFind = false;    //발견?

    public override void SetTask(Task task)
    {
        childTask = task;
    }
    public override bool ChackCondition()
    {
        //유저를 발견했습니까?

        //int find = Physics.OverlapSphereNonAlloc(
        //       new Vector3(this.transform.position.x, this.transform.position.y + (1 * this.transform.localScale.y), this.transform.position.z),
        //       movingUnitInfo.BuffRadius,
        //       collidersTeam,
        //       teamLayerMask);


        if (!isFind)
            return true;

        return false;
    }
    public override bool Result()
    {
        if (ChackCondition())
        {
            return childTask.Result();
        }

        return false;
    }
}



// 실제 행동실행
public class Action_Roaming : ActionTask
{
    public override bool OnEnd()
    {
        return true;
    }

    public override void OnStart()
    {
        isStart = false;
    }

    public override bool OnUpdate()
    {
        return true;
    }

    public override bool Result()
    {
        if (!isStart)
            OnStart();

        if (OnUpdate())
            return OnEnd();

        return false;
    }
}

public class Action_Trace : ActionTask
{
    public override bool OnEnd()
    {
        return true;
    }

    public override void OnStart()
    {
        isStart = false;
    }

    public override bool OnUpdate()
    {
        return true;
    }

    public override bool Result()
    {
        if (!isStart)
            OnStart();

        if (OnUpdate())
            return OnEnd();

        return false;
    }
}

public class Action_Attack : ActionTask
{
    public override bool OnEnd()
    {
        return true;
    }

    public override void OnStart()
    {
        isStart = false;
    }

    public override bool OnUpdate()
    {
        return true;
    }

    public override bool Result()
    {
        if (!isStart)
            OnStart();

        if (OnUpdate())
            return OnEnd();

        return false;
    }
}