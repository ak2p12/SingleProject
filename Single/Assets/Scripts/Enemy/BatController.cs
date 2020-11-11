﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;

public class BatController : Unit
{
    [HideInInspector] public Animator animatorController;
    [HideInInspector] public GameObject enemyCharacter;
    [HideInInspector] public Collider[] colliders;

    public LayerMask targerLayer;

    public bool isCatch;
    public bool isDead;
    public bool inAttackRange;
    public Vector3 targetPosition;
    public LineRenderer line;

    public BehaviorTree root;
    public Sequence sequence_1;
    public Selecter selecter_1;
    public Condition_isFind condition_isFine;
    public Condition_isDead condition_isDead;
    public Action_Roaming roaming;
    //public Action_Trace BT_trace;
    //public Action_Attack BT_attack;

    void Start()
    {
        root = new BehaviorTree();
        sequence_1 = new Sequence();
        selecter_1 = new Selecter();
        condition_isFine = new Condition_isFind();
        condition_isDead = new Condition_isDead();
        roaming = new Action_Roaming();

        condition_isFine.SetTask(roaming);
        selecter_1.AddTask(condition_isFine);
        sequence_1.AddTask(selecter_1);
        sequence_1.AddTask(condition_isDead);
        root.Init(sequence_1);

        unitInfo.MoveSpeed = 5.0f;
        unitInfo.DetectionRange = 5.0f;
        colliders = new Collider[5];

        enemyCharacter = GetComponentInChildren<Animator>().gameObject;
        animatorController = GetComponentInChildren<Animator>();
        StartCoroutine(Update_Coroution());

    }

    IEnumerator Update_Coroution()
    {
        while (true)
        {
            System.Array.Clear(colliders, 0, colliders.Length);
            //적를 발견했습니까?
            int find = Physics.OverlapSphereNonAlloc(
                   transform.position,
                   unitInfo.DetectionRange,
                   colliders,
                   targerLayer);

            if (colliders[0] != null)
                isCatch = true;
            else
                isCatch = false;

            root.Result(this);
            
            yield return null;
        }
    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.blue;
        //Gizmos.DrawWireSphere(
        //    transform.position,
        //       unitInfo.DetectionRange);

        Handles.color = Color.blue;
        Handles.DrawWireArc(transform.position , Vector3.up , -transform.position , 360.0f , 6.0f);
    }

}

public class Condition_isFind : Condition
{
    public float time;
    public override bool ChackCondition(Unit _unit)
    {
        //발견 못했다면
        if (!_unit.GetComponent<BatController>().isCatch)
        {
            if (time > 3.0f)
            {
                return true;
            }
            else
            {
                time += Time.deltaTime;
            }
            
        }

        return false;
        
    }
    public override bool Result(Unit _unit)
    {
        //발견 못하면 실행
        if (ChackCondition(_unit))
        {
            if (childTask.Result(_unit))
            {
                time = 0.0f;
                return true;
            }
        }
        return false;
    }
    public override void SetTask(Task _task)
    {
        childTask = _task;
    }
}
public class Condition_isDead : Condition
{
    public override bool ChackCondition(Unit _unit)
    {
        if (_unit.GetComponent<BatController>().isDead)
        {
            return true;
        }

        return false;
    }

    public override bool Result(Unit _unit)
    {
        //발견 못하면 실행
        if (ChackCondition(_unit))
        {
            return true;
        }
        return false;
    }

    public override void SetTask(Task _task)
    {
        childTask = _task;
    }
}
public class Condition_AttactRagne : Condition
{
    public override void SetTask(Task _task) { childTask = _task; }
    public override bool ChackCondition(Unit _unit)
    {
        if (_unit.GetComponent<BatController>().inAttackRange)
        {
            return false;
        }
        return true;
    }
    public override bool Result(Unit _unit)
    {
        //공격범위 밖에 있다면 실행
        if (ChackCondition(_unit))
        {
            return childTask.Result(_unit);
        }
        return false;
    }
}
public class Action_Roaming : ActionTask
{
    public Vector3 roamingPoint;
    public Vector3 direction;

    public override bool OnEnd(Unit _unit)
    {
        isStart = false;
        roamingPoint = Vector3.zero;
        ((BatController)_unit).animatorController.SetBool("Move", false);
        return true;
    }

    public override void OnStart(Unit _unit)
    {
        roamingPoint.x = Random.Range(0.0f, 20.0f);
        roamingPoint.z = Random.Range(0.0f, 20.0f);
        direction = roamingPoint - ((BatController)_unit).transform.position;
        direction.y = 0.0f;
        isStart = true;
    }

    public override bool OnUpdate(Unit _unit)
    {
        ((BatController)_unit).transform.rotation =
            Quaternion.Lerp(
               ((BatController)_unit).transform.rotation,
               Quaternion.LookRotation(direction.normalized),
               15.0f * Time.deltaTime);

        ((BatController)_unit).transform.Translate(direction.normalized * _unit.unitInfo.MoveSpeed * Time.deltaTime, Space.World);

        float scale = Vector3.SqrMagnitude(roamingPoint - ((BatController)_unit).transform.position);

        ((BatController)_unit).animatorController.SetBool("Move", true);
        if (1.0f > scale)
        {
            return true;
        }

        return false;
    }

    public override bool Result(Unit _unit)
    {
        if (!isStart)
            OnStart(_unit);

        if (OnUpdate(_unit))
            return OnEnd(_unit);

        return false;
    }
}
public class Action_Trace : ActionTask
{
    public override bool OnEnd(Unit _unit)
    {
        return true;
    }

    public override void OnStart(Unit _unit)
    {
        isStart = false;
    }

    public override bool OnUpdate(Unit _unit)
    {
        return true;
    }

    public override bool Result(Unit _unit)
    {
        if (!isStart)
            OnStart(_unit);

        if (OnUpdate(_unit))
            return OnEnd(_unit);

        return false;
    }
}
public class Action_Attack : ActionTask
{
    public override bool OnEnd(Unit _unit)
    {
        return true;
    }

    public override void OnStart(Unit _unit)
    {
        isStart = false;
    }

    public override bool OnUpdate(Unit _unit)
    {
        return true;
    }

    public override bool Result(Unit _unit)
    {
        if (!isStart)
            OnStart(_unit);

        if (OnUpdate(_unit))
            return OnEnd(_unit);

        return false;
    }
}