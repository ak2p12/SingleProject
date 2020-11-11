using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserMain : Unit
{
    private UserController Control;
    void Start()
    {
        unitInfo.MoveSpeed = 3.0f;
        Control = GetComponent<UserController>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            unitInfo.MoveSpeed += 0.1f;
        }

        if (Input.GetKey(KeyCode.E))
        {
            unitInfo.MoveSpeed -= 0.1f;
        }
    }

    public UnitInformation GetUnitInfomation()
    {
        return unitInfo;
    }
}
