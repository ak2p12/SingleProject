using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : Unit
{
    [HideInInspector] public UserController Control;
    [HideInInspector] public Collider[] colliders;

    public LayerMask targetLayer;
    public USER_WEAPON currentWeapon;
    public USER_WEAPON haveWeapon_1;
    public USER_WEAPON haveWeapon_2;
    public GameObject[] shoulderWeaponObject;
    public GameObject[] hipWeaponObject;
    public GameObject[] useWeaponObject;

    [HideInInspector] public bool Attack_L1;
    [HideInInspector] public bool Attack_L2;
    [HideInInspector] public bool Attack_L3;
    [HideInInspector] public bool Attack_L4;

    [HideInInspector] public bool Attack_R1;
    [HideInInspector] public bool Attack_R2;
    [HideInInspector] public bool Attack_R3;
    [HideInInspector] public bool Attack_R4;

    private void OnEnable()
    {
        unitInfo.MoveSpeed = 3.0f;
        unitInfo.AttackRange = 2.0f;
        unitInfo.AttackPower = 10.0f;
        unitInfo.MaxHP = unitInfo.CurrentHP = 100.0f;
    }
    void Start()
    {
        //haveWeapon_1 = USER_WEAPON.BAREHANDS;
        //haveWeapon_2 = USER_WEAPON.BAREHANDS;
        colliders = new Collider[10];
        Control = GetComponent<UserController>();
        currentWeapon = USER_WEAPON.BAREHANDS;
        StartCoroutine(Update_Coroution());
    }

    IEnumerator Update_Coroution()
    {
        while (true)
        {
            yield return null;
        }
    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = new Color(255, 0, 255);
        //Gizmos.DrawWireSphere(L_HandTransform.position, 0.7f);
    }
    public void Attack()
    {
        System.Array.Clear(colliders, 0, colliders.Length);

        switch (currentWeapon)
        {
            case USER_WEAPON.BAREHANDS: //맨손
                {
                    if (Control.animatorController.GetCurrentAnimatorStateInfo(0).IsName("Attack_L4") ||
                        Control.animatorController.GetCurrentAnimatorStateInfo(0).IsName("Attack_R4"))
                    {
                        int find = Physics.OverlapSphereNonAlloc(
                            transform.position + (transform.forward * 1.3f),
                            1.0f,
                            colliders,
                            targetLayer);

                        if (0 != find)
                        {
                            foreach (Collider collider in colliders)
                            {
                                if (null == collider)
                                    break;
                                collider.gameObject.GetComponent<Unit>().unitInfo.CurrentHP -= (unitInfo.AttackPower * 1.4f);
                            }
                        }
                    }
                    else if (Control.animatorController.GetCurrentAnimatorStateInfo(0).IsName("Attack_L3") ||
                        Control.animatorController.GetCurrentAnimatorStateInfo(0).IsName("Attack_R3"))
                    {
                        int find = Physics.OverlapSphereNonAlloc(
                            transform.position + (transform.forward * 1.3f),
                            1.0f,
                            colliders,
                            targetLayer);

                        if (0 != find)
                        {
                            foreach (Collider collider in colliders)
                            {
                                if (null == collider)
                                    break;
                                collider.gameObject.GetComponent<Unit>().unitInfo.CurrentHP -= (unitInfo.AttackPower * 1.3f);
                            }
                        }
                    }
                    else if (Control.animatorController.GetCurrentAnimatorStateInfo(0).IsName("Attack_L2") ||
                        Control.animatorController.GetCurrentAnimatorStateInfo(0).IsName("Attack_R2"))
                    {
                        int find = Physics.OverlapSphereNonAlloc(
                            transform.position + (transform.forward * 1.3f),
                            1.0f,
                            colliders,
                            targetLayer);

                        if (0 != find)
                        {
                            foreach (Collider collider in colliders)
                            {
                                if (null == collider)
                                    break;
                                collider.gameObject.GetComponent<Unit>().unitInfo.CurrentHP -= (unitInfo.AttackPower * 1.2f);
                            }
                        }
                    }
                    else if (Control.animatorController.GetCurrentAnimatorStateInfo(0).IsName("Attack_L1") ||
                        Control.animatorController.GetCurrentAnimatorStateInfo(0).IsName("Attack_R1"))
                    {
                        int find = Physics.OverlapSphereNonAlloc(
                            transform.position + (transform.forward * 1.3f),
                            1.0f,
                            colliders,
                            targetLayer);

                        if (0 != find)
                        {
                            foreach (Collider collider in colliders)
                            {
                                if (null == collider)
                                    break;
                                collider.gameObject.GetComponent<Unit>().unitInfo.CurrentHP -= (unitInfo.AttackPower * 1.1f);
                            }
                        }
                    }
                }

                break;
        }
    }

    public void WeaponSwap(int _have)
    {
        if (1 == _have)
        {
            switch (haveWeapon_1)
            {
                case USER_WEAPON.BAREHANDS:
                    break;
                case USER_WEAPON.TWOHANDSWORD:
                    foreach (GameObject _gameObj in useWeaponObject)
                    {
                        if (null == _gameObj)
                            continue;
                        else if ("Use_2HandSword" == _gameObj.name)
                            _gameObj.SetActive(true);
                        else
                            _gameObj.SetActive(false);
                    }
                    foreach (GameObject _gameObj in shoulderWeaponObject)
                    {
                        if (null == _gameObj)
                            continue;
                        _gameObj.SetActive(false);
                    }
                    break;
                case USER_WEAPON.DUALSWORD:
                    foreach (GameObject _gameObj in useWeaponObject)
                    {
                        if (null == _gameObj)
                            continue;
                        else if ("Use_L_DualSword" == _gameObj.name)
                            _gameObj.SetActive(true);
                        else if("Use_R_DualSword" == _gameObj.name)
                            _gameObj.SetActive(true);
                        else
                            _gameObj.SetActive(false);
                    }
                    foreach (GameObject _gameObj in shoulderWeaponObject)
                    {
                        if (null == _gameObj)
                            continue;
                        _gameObj.SetActive(false);
                    }
                    break;
                case USER_WEAPON.END:
                    break;
                
            }


        }
        else if (2 == _have)
        {
            switch (haveWeapon_2)
            {
                case USER_WEAPON.BAREHANDS:
                    break;
                case USER_WEAPON.TWOHANDSWORD:
                    foreach (GameObject _gameObj in useWeaponObject)
                    {
                        if (null == _gameObj)
                            continue;
                        else if ("Use_2HandSword" == _gameObj.name)
                            _gameObj.SetActive(true);
                        else
                            _gameObj.SetActive(false);
                    }
                    foreach (GameObject _gameObj in hipWeaponObject)
                    {
                        _gameObj.SetActive(false);
                    }
                    break;
                case USER_WEAPON.DUALSWORD:
                    foreach (GameObject _gameObj in useWeaponObject)
                    {
                        if (null == _gameObj)
                            continue;
                        else if ("Use_L_DualSword" == _gameObj.name)
                            _gameObj.SetActive(true);
                        else if ("Use_R_DualSword" == _gameObj.name)
                            _gameObj.SetActive(true);
                        else
                            _gameObj.SetActive(false);
                    }
                    foreach (GameObject _gameObj in hipWeaponObject)
                    {
                        if (null == _gameObj)
                            continue;
                        _gameObj.SetActive(false);
                    }
                    break;
                case USER_WEAPON.END:
                    break;

            }
        }

    }
}
