using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : Unit
{
    [HideInInspector] public UserController Control;
    //public Transform ModelTransform;

    void Start()
    {
        unitInfo.MoveSpeed = 3.0f;
        Control = GetComponent<UserController>();
    }

    void Update()
    {

    }

    public UnitInformation GetUnitInfomation()
    {
        return unitInfo;
    }
}
