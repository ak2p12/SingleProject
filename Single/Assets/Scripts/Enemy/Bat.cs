using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    //private Sequence root;
    //private Task temp1;
    //private Task temp2;

    // Start is called before the first frame update
    void Start()
    {
        //root = new Sequence();
        //temp1 = new BT_Move("1번");
        //temp2 = new BT_Move("2번");
        //root.AddTask(temp1);
        //root.AddTask(temp1);

    }

    // Update is called once per frame
    void Update()
    {
        //if (root.Result())
        //{
        //  Debug.Log("메인 종료");
        //}
    }


}


//public class Condition : Decorator
//{
//    public override bool ChackCondition()
//    {
//        if (AD == true)
//            return true;

//        return false;
//    }

//    public override bool Result()
//    {
//        if (ChackCondition())
//        {
//            return childTask.Result();
//        }

//        return false;
//    }
//}



// 실제 행동실행
//public class BT_Move : Task
//{
//    private bool isStart = false;
//    private float time = 9999;
//    private string name;
//    public BT_Move(string Text)
//    {
//        name = Text;
//    }

//    private void OnStart()
//    {
//        isStart = true;
//        Debug.Log(name + "시작");
//        time = 0;
//    }

//    private bool OnUpdate()
//    {
//        Debug.Log(name + "업데이트");
//        time += Time.deltaTime;

//        if (time > 5.0f)
//            return true;
//        return false;
//    }

//    private bool OnEnd()
//    {
//        Debug.Log(name + "종료");
//        isStart = false;
//        return true;
//    }

//    public override bool Result()
//    {
//        if (!isStart)
//            OnStart();

//        if (OnUpdate())
//            return OnEnd();

//        return false;
//    }
//}