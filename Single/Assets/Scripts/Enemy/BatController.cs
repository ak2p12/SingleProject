using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;

public class BatController : Unit
{
    public LayerMask targetLayer;

    [HideInInspector] public Animator animatorController;
    [HideInInspector] public GameObject enemyCharacter;
    [HideInInspector] public Collider[] colliders;

    [HideInInspector] public Unit target;

    [HideInInspector] public bool isCatch;
    [HideInInspector] public bool inAttackRange;
    [HideInInspector] public LineRenderer line;

    [HideInInspector] public BehaviorTree root;
    [HideInInspector] public Sequence sequence_1;
    [HideInInspector] public Selecter selecter_1;
    [HideInInspector] public Condition_isDead condition_isDead;
    [HideInInspector] public Condition_isFind condition_isFine;
    [HideInInspector] public Condition_inAttackRange condition_inAttackRange;
    
    [HideInInspector] public TimeOut_Delay timeOut_RoamingDelay;
    [HideInInspector] public TimeOut_Delay timeOut_AttackDelay;
    [HideInInspector] public Action_Roaming roaming;
    [HideInInspector] public Action_Trace trace;
    [HideInInspector] public Action_Attack attack;
    

    void Start()
    {
        root = new BehaviorTree();
        sequence_1 = new Sequence();
        selecter_1 = new Selecter();
        condition_isDead = new Condition_isDead();
        condition_isFine = new Condition_isFind();
        condition_inAttackRange = new Condition_inAttackRange();
        timeOut_RoamingDelay = new TimeOut_Delay();
        timeOut_AttackDelay = new TimeOut_Delay();
        roaming = new Action_Roaming();
        trace = new Action_Trace();
        attack = new Action_Attack();
        colliders = new Collider[5];

        timeOut_RoamingDelay.SetTask(roaming);
        timeOut_RoamingDelay.SetTime(3.0f);
        condition_isFine.SetTask(timeOut_RoamingDelay);
        
        selecter_1.AddTask(timeOut_RoamingDelay);

        condition_inAttackRange.SetTask(trace);
        selecter_1.AddTask(condition_inAttackRange);

        timeOut_AttackDelay.SetTask(attack);
        timeOut_AttackDelay.SetTime(1.5f);
        selecter_1.AddTask(timeOut_AttackDelay);

        sequence_1.AddTask(selecter_1);

        sequence_1.AddTask(condition_isDead);

        root.Init(sequence_1);

        unitInfo.MoveSpeed = 5.0f;
        unitInfo.DetectionRange = 10.0f;
        unitInfo.AttackRange = 2.0f;

        enemyCharacter = GetComponentInChildren<Animator>().gameObject;
        animatorController = GetComponentInChildren<Animator>();
        StartCoroutine(Update_Coroution());

    }

    IEnumerator Update_Coroution()
    {
        while (true)
        {
            System.Array.Clear(colliders, 0, colliders.Length);

            ////적를 발견했습니까?
            //if (!isCatch)
            //{
            //    int find = Physics.OverlapSphereNonAlloc(
            //       transform.position,
            //       unitInfo.DetectionRange,
            //       colliders,
            //       targetLayer);

            //    if (colliders[0] != null)
            //    {
            //        target = colliders[0].gameObject.GetComponentInParent<Unit>();
            //        animatorController.SetBool("Combat" , true);
            //        isCatch = true;
            //    }
            //}

            root.Result(this);

            yield return null;
        }
    }

    private void OnDrawGizmos()
    {
        Handles.color = Color.blue;
        Handles.DrawWireArc(transform.position , Vector3.up , -transform.position , 360.0f , unitInfo.DetectionRange);

        Handles.color = Color.red;
        Handles.DrawWireArc(transform.position, Vector3.up, -transform.position, 360.0f, unitInfo.AttackRange);
    }

}

public class TimeOut_Delay : TimeOut
{
    public override bool ChackCondition(Unit _unit)
    {
        currentTime += Time.deltaTime;
        if (timeOut <= currentTime)
            return true;

        return false;
    }

    public override bool Result(Unit _unit)
    {
        if (ChackCondition(_unit))
        {
            if (childTask.Result(_unit))
            {
                currentTime = 0.0f;
                return true;
            }
        }
        return false;
    }

    public override void SetTask(Task _task)
    {
        childTask = _task;
    }

    public override void SetTime(float _time)
    {
        timeOut = _time;
        currentTime = 0.0f;
    }
}
public class Condition_isFind : Condition
{
    public override bool ChackCondition(Unit _unit)
    {
        //발견 못했다면
        if (((BatController)_unit).isCatch == false)
        {
            int find = Physics.OverlapSphereNonAlloc(
                 ((BatController)_unit).transform.position,
                 ((BatController)_unit).unitInfo.DetectionRange,
                 ((BatController)_unit).colliders,
                 ((BatController)_unit).targetLayer);

            if (((BatController)_unit).colliders[0] != null)
            {
                ((BatController)_unit).animatorController.SetBool("Combat", true);
                ((BatController)_unit).target = ((BatController)_unit).colliders[0].gameObject.GetComponentInParent<Unit>();
                ((BatController)_unit).isCatch = true;
                return false;
            }

            return true;

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
public class Condition_inAttackRange : Condition
{
    public override bool ChackCondition(Unit _unit)
    {
        if (((BatController)_unit).isCatch == true)
        {
            int find = Physics.OverlapSphereNonAlloc(
                   ((BatController)_unit).transform.position,
                   ((BatController)_unit).unitInfo.AttackRange,
                   ((BatController)_unit).colliders,
                   ((BatController)_unit).targetLayer);

            if (find == 0)
            {
                ((BatController)_unit).inAttackRange = false;
                return true;
            }
            else
            {
                ((BatController)_unit).inAttackRange = true;
                return false;
            }
        }

        return false;
    }
    public override bool Result(Unit _unit)
    {
        if (ChackCondition(_unit))
        {
            if (childTask.Result(_unit))
            {
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
        roamingPoint.x = Random.Range(-10.0f, 10.0f);
        roamingPoint.z = Random.Range(-10.0f, 10.0f);
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
        else
        {
            int find = Physics.OverlapSphereNonAlloc(
                 ((BatController)_unit).transform.position,
                 ((BatController)_unit).unitInfo.DetectionRange,
                 ((BatController)_unit).colliders,
                 ((BatController)_unit).targetLayer);

            if (((BatController)_unit).colliders[0] != null)
            {
                ((BatController)_unit).target = ((BatController)_unit).colliders[0].gameObject.GetComponentInParent<Unit>();
                ((BatController)_unit).animatorController.SetBool("Move", false);
                ((BatController)_unit).animatorController.SetBool("Combat", true);
                ((BatController)_unit).isCatch = true;
                return true;
            }
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
    public Vector3 direction;

    public override bool OnEnd(Unit _unit)
    {
        //((BatController)_unit).animatorController.SetBool("Move", false);
        isStart = false;
        return true;
    }

    public override void OnStart(Unit _unit)
    {
        ((BatController)_unit).animatorController.SetBool("Move", true);
        isStart = true;
    }

    public override bool OnUpdate(Unit _unit)
    {
        Vector3 targetPosition = ((User)((BatController)_unit).target).ModelTransform.position;
        direction = targetPosition - ((BatController)_unit).transform.position;

        ((BatController)_unit).transform.rotation =
            Quaternion.Lerp(
               ((BatController)_unit).transform.rotation,
               Quaternion.LookRotation(direction.normalized),
               15.0f * Time.deltaTime);

        ((BatController)_unit).transform.Translate(direction.normalized * (_unit.unitInfo.MoveSpeed * 0.9f) * Time.deltaTime, Space.World);

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
    public Vector3 direction;

    public override bool OnEnd(Unit _unit)
    {
        isStart = false;
        return true;
    }

    public override void OnStart(Unit _unit)
    {
        ((BatController)_unit).animatorController.SetBool("Move" , false);
        ((BatController)_unit).animatorController.SetTrigger("Attack");
        isStart = true;
    }

    public override bool OnUpdate(Unit _unit)
    {
        if (((BatController)_unit).isCatch == true  && ((BatController)_unit).inAttackRange == true)
        {
            Vector3 targetPosition = ((User)((BatController)_unit).target).ModelTransform.position;
            direction = targetPosition - ((BatController)_unit).transform.position;

            ((BatController)_unit).transform.rotation =
                Quaternion.Lerp(
                   ((BatController)_unit).transform.rotation,
                   Quaternion.LookRotation(direction.normalized),
                   15.0f * Time.deltaTime);


            if (((BatController)_unit).animatorController.GetCurrentAnimatorStateInfo(0).IsName("Attack") &&
               ((BatController)_unit).animatorController.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.8f)
            {
                return true;
            }
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
