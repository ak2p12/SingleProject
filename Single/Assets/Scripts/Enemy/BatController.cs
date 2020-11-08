using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BatController : Unit
{
    [HideInInspector] public Animator animatorController;
    [HideInInspector] public GameObject enemyCharacter;
    [HideInInspector] public Collider[] colliders;

    public LayerMask targerLayer;

    public bool isCatch;
    public bool inAttackRange;
    public Vector3 targetPosition;

    //public BehaviorTree root;
    //public Sequence BT_selecter;
    //public Action_Roaming BT_roaming;
    //public Action_Trace BT_trace;
    //public Action_Attack BT_attack;

    void Start()
    {
        UnitInfo.MoveSpeed = 5.0f;
        UnitInfo.DetectionRange = 5.0f;
        colliders = new Collider[5];

        enemyCharacter = GetComponentInChildren<Animator>().gameObject;
        animatorController = GetComponentInChildren<Animator>();
        StartCoroutine(Update_Coroution());
        
    }

    IEnumerator Update_Coroution()
    {
        while (true)
        {
            //적를 발견했습니까?
            int find = Physics.OverlapSphereNonAlloc(
                   transform.position,
                   UnitInfo.DetectionRange,
                   colliders,
                   targerLayer);

            if (colliders[0] != null)
            {
                isCatch = true;
            }

            yield return null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(
            transform.position,
               UnitInfo.DetectionRange);
    }

}

public class Condition_Find : Condition
{
    public override void SetUnit(Unit _unit) { unit = _unit; }
    public override void SetTask(Task task) { childTask = task; }
    public override bool ChackCondition()
    {
        if (unit.GetComponent<BatController>().isCatch)
        {
            return false;
        }
        return true;
    }
    public override bool Result()
    {
        //발견 못하면 실행
        if (ChackCondition())
        {
            return childTask.Result();
        }
        return false;
    }
}
public class Condition_AttactRagne : Condition
{
    public override void SetUnit(Unit _unit) { unit = _unit; }
    public override void SetTask(Task task) { childTask = task; }
    public override bool ChackCondition()
    {
        if (unit.GetComponent<BatController>().inAttackRange)
        {
            return false;
        }
        return true;
    }
    public override bool Result()
    {
        //공격범위 밖에 있다면 실행
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