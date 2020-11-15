using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;

public class BatController : Unit
{
    public LayerMask targetLayer;       //목표
    public Vector3 targetPoint;         //목표 이동지점
    public Vector3 targetDirection;           //목표방향

    [HideInInspector] public Animator animatorController;
    [HideInInspector] public GameObject enemyCharacter;
    [HideInInspector] public Collider[] colliders;

    [HideInInspector] public Unit target;

    [HideInInspector] public bool isCatch;
    [HideInInspector] public bool inAttackRange;
    [HideInInspector] public LineRenderer line;

    [HideInInspector] public BehaviorTree root;

    [HideInInspector] public Sequence sequence_1;
    [HideInInspector] public Sequence sequence_2;
    [HideInInspector] public Selecter selecter_1;

    [HideInInspector] public Condition_Dead condition_Dead;
    [HideInInspector] public Condition_Find condition_Fine;
    [HideInInspector] public Condition_AttackRange condition_AttackRange;
    [HideInInspector] public Condition_Arrive condition_Arrive;

    [HideInInspector] public TimeDelay timeOut_RoamingDelay;
    
    [HideInInspector] public Action_Roaming roaming;
    [HideInInspector] public Action_Trace trace;
    [HideInInspector] public Action_Attack attack;


    void Start()
    {
        root = new BehaviorTree();

        sequence_1 = new Sequence();
        sequence_2 = new Sequence();
        selecter_1 = new Selecter();

        condition_Dead = new Condition_Dead();
        condition_Fine = new Condition_Find();
        condition_AttackRange = new Condition_AttackRange();
        condition_Arrive = new Condition_Arrive();

        timeOut_RoamingDelay = new TimeDelay();

        roaming = new Action_Roaming();
        trace = new Action_Trace();
        attack = new Action_Attack();

        colliders = new Collider[5];

        //========== 공격 ===================
        condition_AttackRange.SetTask(attack);
        selecter_1.AddTask(condition_AttackRange);
        //===================================

        //========== 추적 ===================
        condition_Fine.SetTask(trace);
        selecter_1.AddTask(condition_Fine);
        //===================================

        //========== 로밍 ===================
        timeOut_RoamingDelay.SetTime(3.0f);
        sequence_2.AddTask(timeOut_RoamingDelay);
        sequence_2.AddTask(roaming);
        condition_Arrive.SetTask(sequence_2);
        selecter_1.AddTask(condition_Arrive);
        //==================================

        sequence_1.AddTask(selecter_1);
        sequence_1.AddTask(condition_Dead);

        root.Init(sequence_1);

        unitInfo.MoveSpeed = 4.0f;
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

            root.Result(this);

            //적를 발견했습니까?
            if (!isCatch)
            {
                int find = Physics.OverlapSphereNonAlloc(
                   transform.position,
                   unitInfo.DetectionRange,
                   colliders,
                   targetLayer);

                if (colliders[0] != null)
                {
                    isCatch = true;
                    target = colliders[0].gameObject.GetComponentInParent<Unit>();
                    animatorController.SetBool("Combat", true);
                }
            }

            Move();


            yield return null;
        }
    }

    private void OnDrawGizmos()
    {
        Handles.color = Color.blue;
        Handles.DrawWireArc(transform.position, Vector3.up, -transform.position, 360.0f, unitInfo.DetectionRange);

        Handles.color = Color.red;
        Handles.DrawWireArc(transform.position, Vector3.up, -transform.position, 360.0f, unitInfo.AttackRange);
    }

    public void Move()
    {
        float scale = Vector3.SqrMagnitude(targetPoint - transform.position);
        if (scale <= 1.5f)
        {
            animatorController.SetBool("Move", false);
            targetDirection = Vector3.zero;
            return;
        }

        transform.Translate(targetDirection.normalized * unitInfo.MoveSpeed * Time.deltaTime, Space.World);

        if (targetDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.Lerp(
            transform.rotation,
            Quaternion.LookRotation(targetDirection.normalized),
            15.0f * Time.deltaTime);
        }
        else if (isCatch)
        {
            transform.rotation = Quaternion.Lerp(
           transform.rotation,
           Quaternion.LookRotation( (target.GetComponent<User>().ModelTransform.position - transform.position).normalized),
           15.0f * Time.deltaTime);
        }
    }
}

public class TimeDelay : TimeOut
{
    public override bool ChackCondition(Unit _unit) { return false; }
    public override bool Result(Unit _unit)
    {
        if (originTime == 0.0f)
        {
            originTime = Time.time;
            return false;
        }
        else
        {
            float time = Time.time - originTime;
            currentTime += time;
            originTime = Time.time;
            if (currentTime >= timeDelay)
            {
                originTime = 0.0f;
                currentTime = 0.0f;
                return true;
            }
                
        }
        return false;
    }

    public override void SetTask(Task _task) { }
    public override void SetTime(float _time)
    {
        timeDelay = _time;
    }
}
public class Condition_Find : Condition
{
    public override bool ChackCondition(Unit _unit)
    {
        //발견 못했다면
        if (((BatController)_unit).isCatch == true)
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
            if (childTask != null && childTask.Result(_unit))
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
public class Condition_AttackRange : Condition
{
    public override bool ChackCondition(Unit _unit)
    {
        //공격 범위 검사
        int find = Physics.OverlapSphereNonAlloc(
               ((BatController)_unit).transform.position,
               ((BatController)_unit).unitInfo.AttackRange,
               ((BatController)_unit).colliders,
               ((BatController)_unit).targetLayer);

        if (find != 0)
        {
            return true;
        }

        return false;
    }
    public override bool Result(Unit _unit)
    {
        if (ChackCondition(_unit))
        {
            if (childTask != null && childTask.Result(_unit))
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
public class Condition_Arrive : Condition
{
    public override bool ChackCondition(Unit _unit)
    {
        if (((BatController)_unit).targetDirection == Vector3.zero)
        {
            return true;
        }

        return false;
    }
    public override bool Result(Unit _unit)
    {
        if (ChackCondition(_unit))
        {
            if (childTask != null && childTask.Result(_unit))
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
public class Condition_Dead : Condition
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
    public override bool OnEnd(Unit _unit)
    {
        isStart = false;
        return true;
    }
    public override void OnStart(Unit _unit)
    {
        ((BatController)_unit).targetPoint.x = Random.Range(-10.0f, 10.0f);
        ((BatController)_unit).targetPoint.y = 0.0f;
        ((BatController)_unit).targetPoint.z = Random.Range(-10.0f, 10.0f);
        ((BatController)_unit).targetDirection = ((BatController)_unit).targetPoint - ((BatController)_unit).transform.position;
        ((BatController)_unit).animatorController.SetBool("Move", true);
        isStart = true;
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
public class Action_Trace : ActionTask
{
    public override bool OnEnd(Unit _unit)
    {
        isStart = false;
        return true;
    }

    public override void OnStart(Unit _unit)
    {
        ((BatController)_unit).targetPoint = ((User)((BatController)_unit).target).ModelTransform.position;
        ((BatController)_unit).targetDirection = ((BatController)_unit).targetPoint - ((BatController)_unit).transform.position;
        ((BatController)_unit).animatorController.SetBool("Move", true);
        isStart = true;
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
        isStart = false;
        return true;
    }

    public override void OnStart(Unit _unit)
    {
        ((BatController)_unit).animatorController.SetTrigger("Attack");
        ((BatController)_unit).animatorController.SetBool("Move" , false);
        ((BatController)_unit).targetDirection = Vector3.zero;

        isStart = true;
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
