using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserMain : Unit
{
    private UserController Control;
    void Start()
    {
        UnitInfo.MoveSpeed = 3.0f;
        Control = GetComponent<UserController>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            UnitInfo.MoveSpeed += 0.1f;
        }

        if (Input.GetKey(KeyCode.E))
        {
            UnitInfo.MoveSpeed -= 0.1f;
        }
    }

    public UnitInformation GetUnitInfomation()
    {
        return UnitInfo;
    }
}
