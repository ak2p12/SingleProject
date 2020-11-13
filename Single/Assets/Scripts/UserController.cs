﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class UserController : MonoBehaviour
{
    private User user;
    private Camera userCamera;
    private Ray ray;
    private RaycastHit rayHit;
    private Animator animatorController;
    private GameObject userCharacter;
    private USER_LOOK userLook;
    private float axisX;
    private float axisZ;
    private bool isMovingKey;
    private float axisComeback;

    private bool isAttack_L1;
    private bool isAttack_L2;
    private bool isAttack_L3;
    private bool isAttack_L4;

    private bool isAttack_R1;
    private bool isAttack_R2;
    private bool isAttack_R3;
    private bool isAttack_R4;

    private bool rollCheck;

    [HideInInspector] public Collider[] colliders;
    public LayerMask targerLayer;

    void Start()
    {
        rollCheck = false;
        axisComeback = 3.0f;
        animatorController = GetComponentInChildren<Animator>();
        userCamera = GameObject.Find("UserCamera").GetComponent<Camera>();
        userCharacter = GetComponentInChildren<Animator>().gameObject;
        isMovingKey = false;
        user = GetComponent<User>();
        StartCoroutine(Input_Coroutine());
    }

    IEnumerator Input_Coroutine()
    {
        while (true)
        {
            if (animatorController.GetCurrentAnimatorStateInfo(0).IsName("Move_Blend") && !animatorController.IsInTransition(0))
            {
                ResetAttack();
            }

            //int find = Physics.OverlapSphereNonAlloc(
            //       userCharacter.transform.position,
            //       5,
            //       colliders,
            //       targerLayer);

            //Debug.Log(find.ToString());

            Action_Input();
            Mouse_Input();
            Movement_Input();
            Attack_Input();
                
            yield return null;
        }
    }
    private void Action_Input()
    {
        //구르기 회피 
        if (Input.GetKeyDown(KeyCode.Space) && rollCheck == false)
        {
            rollCheck = true;
            if ( !animatorController.GetCurrentAnimatorStateInfo(0).IsName("Roll_Start") && !animatorController.GetCurrentAnimatorStateInfo(0).IsName("Roll_Loop") && 
                !animatorController.IsInTransition(0))
            {
                animatorController.SetTrigger("Roll_Start_Trigger");
            }
        }

        if (animatorController.GetCurrentAnimatorStateInfo(0).IsName("Roll_Loop") || animatorController.GetCurrentAnimatorStateInfo(0).IsName("Roll_Start"))
        {
            userCharacter.transform.position += userCharacter.transform.forward * 10.0f * Time.deltaTime;
            
        }
    }
    private void Movement_Input()
    {
        if (rollCheck == false)
        {
            switch (userLook)
            {
                case USER_LOOK.FORWORD:
                    {
                        if (Input.GetKey(KeyCode.W))
                        {
                            if (Input.GetKey(KeyCode.A))
                            {
                                axisX -= axisComeback * Time.deltaTime;
                                axisZ += axisComeback * Time.deltaTime;
                            }
                            else if (Input.GetKey(KeyCode.D))
                            {
                                axisX += axisComeback * Time.deltaTime;
                                axisZ += axisComeback * Time.deltaTime;
                            }
                            else
                            {
                                if (axisX > 0)
                                    axisX -= axisComeback * Time.deltaTime;
                                else if (axisX < 0)
                                    axisX += axisComeback * Time.deltaTime;
                                axisZ += axisComeback * Time.deltaTime;
                            }
                        }
                        else if (Input.GetKey(KeyCode.S))
                        {
                            if (Input.GetKey(KeyCode.A))
                            {
                                axisX -= axisComeback * Time.deltaTime;
                                axisZ -= axisComeback * Time.deltaTime;
                            }
                            else if (Input.GetKey(KeyCode.D))
                            {
                                axisX += axisComeback * Time.deltaTime;
                                axisZ -= axisComeback * Time.deltaTime;
                            }
                            else
                            {
                                if (axisX > 0)
                                    axisX -= axisComeback * Time.deltaTime;
                                else if (axisX < 0)
                                    axisX += axisComeback * Time.deltaTime;
                                axisZ -= axisComeback * Time.deltaTime;
                            }
                        }
                        else if (Input.GetKey(KeyCode.A))
                        {
                            axisX -= axisComeback * Time.deltaTime;
                            if (axisZ > 0)
                                axisZ -= axisComeback * Time.deltaTime;
                            else if (axisZ < 0)
                                axisZ += axisComeback * Time.deltaTime;
                        }
                        else if (Input.GetKey(KeyCode.D))
                        {
                            axisX += axisComeback * Time.deltaTime;
                            if (axisZ > 0)
                                axisZ -= axisComeback * Time.deltaTime;
                            else if (axisZ < 0)
                                axisZ += axisComeback * Time.deltaTime;
                        }
                    }
                    break;
                case USER_LOOK.FORWORD_LEFT:
                    {
                        if (Input.GetKey(KeyCode.W))
                        {
                            if (Input.GetKey(KeyCode.A))
                            {
                                if (axisX > 0)
                                    axisX -= axisComeback * Time.deltaTime;
                                else if (axisX < 0)
                                    axisX += axisComeback * Time.deltaTime;
                                axisZ += axisComeback * Time.deltaTime;
                            }
                            else if (Input.GetKey(KeyCode.D))
                            {
                                axisX += axisComeback * Time.deltaTime;

                                if (axisZ > 0)
                                    axisZ -= axisComeback * Time.deltaTime;
                                else if (axisZ < 0)
                                    axisZ += axisComeback * Time.deltaTime;

                            }
                            else
                            {
                                axisX += axisComeback * Time.deltaTime;
                                axisZ += axisComeback * Time.deltaTime;
                            }
                        }
                        else if (Input.GetKey(KeyCode.S))
                        {
                            if (Input.GetKey(KeyCode.A))
                            {
                                axisX -= axisComeback * Time.deltaTime;

                                if (axisZ > 0)
                                    axisZ -= axisComeback * Time.deltaTime;
                                else if (axisZ < 0)
                                    axisZ += axisComeback * Time.deltaTime;

                            }
                            else if (Input.GetKey(KeyCode.D))
                            {
                                if (axisX > 0)
                                    axisX -= axisComeback * Time.deltaTime;
                                else if (axisX < 0)
                                    axisX += axisComeback * Time.deltaTime;

                                axisZ -= axisComeback * Time.deltaTime;
                            }
                            else
                            {
                                axisX -= axisComeback * Time.deltaTime;
                                axisZ -= axisComeback * Time.deltaTime;
                            }
                        }
                        else if (Input.GetKey(KeyCode.A))
                        {
                            axisX -= axisComeback * Time.deltaTime;
                            axisZ += axisComeback * Time.deltaTime;
                        }
                        else if (Input.GetKey(KeyCode.D))
                        {
                            axisX += axisComeback * Time.deltaTime;
                            axisZ -= axisComeback * Time.deltaTime;
                        }
                    }
                    break;
                case USER_LOOK.FORWORD_RIGHT:
                    {
                        if (Input.GetKey(KeyCode.W))
                        {
                            if (Input.GetKey(KeyCode.A))
                            {
                                axisX -= axisComeback * Time.deltaTime;

                                if (axisZ > 0)
                                    axisZ -= axisComeback * Time.deltaTime;
                                else if (axisZ < 0)
                                    axisZ += axisComeback * Time.deltaTime;

                            }
                            else if (Input.GetKey(KeyCode.D))
                            {
                                axisZ += axisComeback * Time.deltaTime;

                                if (axisX > 0)
                                    axisX -= axisComeback * Time.deltaTime;
                                else if (axisX < 0)
                                    axisX += axisComeback * Time.deltaTime;
                            }
                            else
                            {
                                axisX -= axisComeback * Time.deltaTime;
                                axisZ += axisComeback * Time.deltaTime;
                            }
                        }
                        else if (Input.GetKey(KeyCode.S))
                        {
                            if (Input.GetKey(KeyCode.A))
                            {
                                if (axisX > 0)
                                    axisX -= axisComeback * Time.deltaTime;
                                else if (axisX < 0)
                                    axisX += axisComeback * Time.deltaTime;

                                axisZ -= axisComeback * Time.deltaTime;

                            }
                            else if (Input.GetKey(KeyCode.D))
                            {
                                axisX += axisComeback * Time.deltaTime;

                                if (axisZ > 0)
                                    axisZ -= axisComeback * Time.deltaTime;
                                else if (axisZ < 0)
                                    axisZ += axisComeback * Time.deltaTime;


                            }
                            else
                            {
                                axisX += axisComeback * Time.deltaTime;
                                axisZ -= axisComeback * Time.deltaTime;
                            }
                        }
                        else if (Input.GetKey(KeyCode.A))
                        {
                            axisX -= axisComeback * Time.deltaTime;
                            axisZ -= axisComeback * Time.deltaTime;
                        }
                        else if (Input.GetKey(KeyCode.D))
                        {
                            axisX += axisComeback * Time.deltaTime;
                            axisZ += axisComeback * Time.deltaTime;
                        }
                    }
                    break;
                case USER_LOOK.RIGHT:
                    {
                        if (Input.GetKey(KeyCode.W))
                        {
                            if (Input.GetKey(KeyCode.A))
                            {
                                axisX -= axisComeback * Time.deltaTime;
                                axisZ -= axisComeback * Time.deltaTime;

                            }
                            else if (Input.GetKey(KeyCode.D))
                            {
                                axisX -= axisComeback * Time.deltaTime;
                                axisZ += axisComeback * Time.deltaTime;
                            }
                            else
                            {
                                axisX -= axisComeback * Time.deltaTime;

                                if (axisZ > 0)
                                    axisZ -= axisComeback * Time.deltaTime;
                                else if (axisZ < 0)
                                    axisZ += axisComeback * Time.deltaTime;
                            }
                        }
                        else if (Input.GetKey(KeyCode.S))
                        {
                            if (Input.GetKey(KeyCode.A))
                            {
                                axisX += axisComeback * Time.deltaTime;
                                axisZ -= axisComeback * Time.deltaTime;
                            }
                            else if (Input.GetKey(KeyCode.D))
                            {
                                axisX += axisComeback * Time.deltaTime;
                                axisZ += axisComeback * Time.deltaTime;
                            }
                            else
                            {
                                axisX += axisComeback * Time.deltaTime;

                                if (axisZ > 0)
                                    axisZ -= axisComeback * Time.deltaTime;
                                else if (axisZ < 0)
                                    axisZ += axisComeback * Time.deltaTime;
                            }
                        }
                        else if (Input.GetKey(KeyCode.A))
                        {
                            if (axisX > 0)
                                axisX -= axisComeback * Time.deltaTime;
                            else if (axisX < 0)
                                axisX += axisComeback * Time.deltaTime;

                            axisZ -= axisComeback * Time.deltaTime;
                        }
                        else if (Input.GetKey(KeyCode.D))
                        {
                            if (axisX > 0)
                                axisX -= axisComeback * Time.deltaTime;
                            else if (axisX < 0)
                                axisX += axisComeback * Time.deltaTime;

                            axisZ += axisComeback * Time.deltaTime;
                        }
                    }
                    break;
                case USER_LOOK.LEFT:
                    {
                        if (Input.GetKey(KeyCode.W))
                        {
                            if (Input.GetKey(KeyCode.A))
                            {
                                axisX += axisComeback * Time.deltaTime;
                                axisZ += axisComeback * Time.deltaTime;

                            }
                            else if (Input.GetKey(KeyCode.D))
                            {
                                axisX += axisComeback * Time.deltaTime;
                                axisZ -= axisComeback * Time.deltaTime;
                            }
                            else
                            {
                                axisX += axisComeback * Time.deltaTime;

                                if (axisZ > 0)
                                    axisZ -= axisComeback * Time.deltaTime;
                                else if (axisZ < 0)
                                    axisZ += axisComeback * Time.deltaTime;
                            }
                        }
                        else if (Input.GetKey(KeyCode.S))
                        {
                            if (Input.GetKey(KeyCode.A))
                            {
                                axisX -= axisComeback * Time.deltaTime;
                                axisZ += axisComeback * Time.deltaTime;

                            }
                            else if (Input.GetKey(KeyCode.D))
                            {
                                axisX -= axisComeback * Time.deltaTime;
                                axisZ -= axisComeback * Time.deltaTime;
                            }
                            else
                            {
                                axisX -= axisComeback * Time.deltaTime;

                                if (axisZ > 0)
                                    axisZ -= axisComeback * Time.deltaTime;
                                else if (axisZ < 0)
                                    axisZ += axisComeback * Time.deltaTime;
                            }
                        }
                        else if (Input.GetKey(KeyCode.A))
                        {
                            if (axisX > 0)
                                axisX -= axisComeback * Time.deltaTime;
                            else if (axisX < 0)
                                axisX += axisComeback * Time.deltaTime;

                            axisZ += axisComeback * Time.deltaTime;
                        }
                        else if (Input.GetKey(KeyCode.D))
                        {
                            if (axisX > 0)
                                axisX -= axisComeback * Time.deltaTime;
                            else if (axisX < 0)
                                axisX += axisComeback * Time.deltaTime;

                            axisZ -= axisComeback * Time.deltaTime;
                        }
                    }
                    break;
                case USER_LOOK.BACK:
                    {
                        if (Input.GetKey(KeyCode.W))
                        {
                            if (Input.GetKey(KeyCode.A))
                            {
                                axisX += axisComeback * Time.deltaTime;
                                axisZ -= axisComeback * Time.deltaTime;

                            }
                            else if (Input.GetKey(KeyCode.D))
                            {
                                axisX -= axisComeback * Time.deltaTime;
                                axisZ -= axisComeback * Time.deltaTime;
                            }
                            else
                            {
                                if (axisX > 0)
                                    axisX -= axisComeback * Time.deltaTime;
                                else if (axisX < 0)
                                    axisX += axisComeback * Time.deltaTime;

                                axisZ -= axisComeback * Time.deltaTime;
                            }
                        }
                        else if (Input.GetKey(KeyCode.S))
                        {
                            if (Input.GetKey(KeyCode.A))
                            {
                                axisX += axisComeback * Time.deltaTime;
                                axisZ += axisComeback * Time.deltaTime;
                            }
                            else if (Input.GetKey(KeyCode.D))
                            {
                                axisX -= axisComeback * Time.deltaTime;
                                axisZ += axisComeback * Time.deltaTime;
                            }
                            else
                            {
                                if (axisX > 0)
                                    axisX -= axisComeback * Time.deltaTime;
                                else if (axisX < 0)
                                    axisX += axisComeback * Time.deltaTime;

                                axisZ += axisComeback * Time.deltaTime;
                            }
                        }
                        else if (Input.GetKey(KeyCode.A))
                        {
                            axisX += axisComeback * Time.deltaTime;

                            if (axisZ > 0)
                                axisZ -= axisComeback * Time.deltaTime;
                            else if (axisZ < 0)
                                axisZ += axisComeback * Time.deltaTime;
                        }
                        else if (Input.GetKey(KeyCode.D))
                        {
                            axisX -= axisComeback * Time.deltaTime;

                            if (axisZ > 0)
                                axisZ -= axisComeback * Time.deltaTime;
                            else if (axisZ < 0)
                                axisZ += axisComeback * Time.deltaTime;
                        }
                    }
                    break;
                case USER_LOOK.BACK_LEFT:
                    {
                        if (Input.GetKey(KeyCode.W))
                        {
                            if (Input.GetKey(KeyCode.A))
                            {
                                axisX += axisComeback * Time.deltaTime;

                                if (axisZ > 0)
                                    axisZ -= axisComeback * Time.deltaTime;
                                else if (axisZ < 0)
                                    axisZ += axisComeback * Time.deltaTime;

                            }
                            else if (Input.GetKey(KeyCode.D))
                            {
                                if (axisX > 0)
                                    axisX -= axisComeback * Time.deltaTime;
                                else if (axisX < 0)
                                    axisX += axisComeback * Time.deltaTime;

                                axisZ -= axisComeback * Time.deltaTime;
                            }
                            else
                            {
                                axisX += axisComeback * Time.deltaTime;
                                axisZ -= axisComeback * Time.deltaTime;
                            }
                        }
                        else if (Input.GetKey(KeyCode.S))
                        {
                            if (Input.GetKey(KeyCode.A))
                            {
                                if (axisX > 0)
                                    axisX -= axisComeback * Time.deltaTime;
                                else if (axisX < 0)
                                    axisX += axisComeback * Time.deltaTime;

                                axisZ += axisComeback * Time.deltaTime;

                            }
                            else if (Input.GetKey(KeyCode.D))
                            {
                                axisX -= axisComeback * Time.deltaTime;

                                if (axisZ > 0)
                                    axisZ -= axisComeback * Time.deltaTime;
                                else if (axisZ < 0)
                                    axisZ += axisComeback * Time.deltaTime;


                            }
                            else
                            {
                                axisX -= axisComeback * Time.deltaTime;
                                axisZ += axisComeback * Time.deltaTime;
                            }
                        }
                        else if (Input.GetKey(KeyCode.A))
                        {
                            axisX += axisComeback * Time.deltaTime;
                            axisZ += axisComeback * Time.deltaTime;
                        }
                        else if (Input.GetKey(KeyCode.D))
                        {
                            axisX -= axisComeback * Time.deltaTime;
                            axisZ -= axisComeback * Time.deltaTime;
                        }
                    }
                    break;
                case USER_LOOK.BACK_RIGHT:
                    {
                        if (Input.GetKey(KeyCode.W))
                        {
                            if (Input.GetKey(KeyCode.A))
                            {
                                if (axisX > 0)
                                    axisX -= axisComeback * Time.deltaTime;
                                else if (axisX < 0)
                                    axisX += axisComeback * Time.deltaTime;

                                axisZ -= axisComeback * Time.deltaTime;
                            }
                            else if (Input.GetKey(KeyCode.D))
                            {
                                axisX -= axisComeback * Time.deltaTime;

                                if (axisZ > 0)
                                    axisZ -= axisComeback * Time.deltaTime;
                                else if (axisX < 0)
                                    axisZ += axisComeback * Time.deltaTime;
                            }
                            else
                            {
                                axisX -= axisComeback * Time.deltaTime;
                                axisZ -= axisComeback * Time.deltaTime;
                            }
                        }
                        else if (Input.GetKey(KeyCode.S))
                        {
                            if (Input.GetKey(KeyCode.A))
                            {
                                axisX += axisComeback * Time.deltaTime;

                                if (axisZ > 0)
                                    axisZ -= axisComeback * Time.deltaTime;
                                else if (axisZ < 0)
                                    axisZ += axisComeback * Time.deltaTime;

                            }
                            else if (Input.GetKey(KeyCode.D))
                            {
                                if (axisX > 0)
                                    axisX -= axisComeback * Time.deltaTime;
                                else if (axisX < 0)
                                    axisX += axisComeback * Time.deltaTime;

                                axisZ += axisComeback * Time.deltaTime;
                            }
                            else
                            {
                                axisX += axisComeback * Time.deltaTime;
                                axisZ += axisComeback * Time.deltaTime;
                            }
                        }
                        else if (Input.GetKey(KeyCode.A))
                        {
                            axisX += axisComeback * Time.deltaTime;
                            axisZ -= axisComeback * Time.deltaTime;
                        }
                        else if (Input.GetKey(KeyCode.D))
                        {
                            axisX -= axisComeback * Time.deltaTime;
                            axisZ += axisComeback * Time.deltaTime;
                        }
                    }
                    break;
            }

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                isMovingKey = true;
            }

            if (!isMovingKey)
            {
                if (axisZ > 0)
                {
                    axisZ -= (axisComeback * Time.deltaTime);
                    if (axisZ < 0)
                        axisX = 0;

                }
                else if (axisZ < 0)
                {
                    axisZ += (axisComeback * Time.deltaTime);
                    if (axisZ > 0)
                        axisZ = 0;
                }

                if (axisX > 0)
                {
                    axisX -= (axisComeback * Time.deltaTime);
                    if (axisX < 0)
                        axisX = 0;
                }
                else if (axisX < 0)
                {
                    axisX += (axisComeback * Time.deltaTime);
                    if (axisX > 0)
                        axisX = 0;
                }
            }
            else
            {
                if (axisX > 1)
                    axisX = 1;
                else if (axisX < -1)
                    axisX = -1;
                if (axisZ > 1)
                    axisZ = 1;
                else if (axisZ < -1)
                    axisZ = -1;
            }

            isMovingKey = false;
        }
        else
        {
            if (animatorController.GetCurrentAnimatorStateInfo(0).IsName("Roll_End") && animatorController.IsInTransition(0))
            {
                rollCheck = false;
            }
        }

        animatorController.SetFloat("Dir X", axisX);
        animatorController.SetFloat("Dir Z", axisZ);
    }
    private void Mouse_Input()
    {
        Vector3 mousePos = Input.mousePosition;

        ray = userCamera.ScreenPointToRay(mousePos);

        if ( ( Physics.Raycast(ray, out rayHit, 50.0f, 1 << LayerMask.NameToLayer("Land")) ) && (rollCheck == false) )
        {
            userCharacter.transform.LookAt(rayHit.point, Vector3.up);
            Debug.DrawLine(userCharacter.transform.position, rayHit.point);
            Debug.DrawLine(userCharacter.transform.position + userCharacter.transform.forward, rayHit.point, Color.red);
        }
        
        //337.5 ~ 360.0 
        //0 ~ 22.5
        if ( (userCharacter.transform.eulerAngles.y > 337.5f && userCharacter.transform.eulerAngles.y <= 360.0f) ||
            (userCharacter.transform.eulerAngles.y >= 0.0f && userCharacter.transform.eulerAngles.y <= 22.5f) )
        {
            userLook = USER_LOOK.FORWORD;
        }
        // 22.6 ~ 67.5
        else if(userCharacter.transform.eulerAngles.y > 22.5f &&
                userCharacter.transform.eulerAngles.y <= 67.5f)
        {
            userLook = USER_LOOK.FORWORD_RIGHT;
        }
        // 67.6 ~ 112.5
        else if (userCharacter.transform.eulerAngles.y > 67.5f &&
                userCharacter.transform.eulerAngles.y <= 112.5f)
        {
            userLook = USER_LOOK.RIGHT;
        }
        // 112.6 ~ 157.5
        else if (userCharacter.transform.eulerAngles.y > 112.5f &&
                userCharacter.transform.eulerAngles.y <= 157.5f)
        {
            userLook = USER_LOOK.BACK_RIGHT;
        }
        // 157.6 ~ 202.5
        else if (userCharacter.transform.eulerAngles.y > 157.5f &&
                userCharacter.transform.eulerAngles.y <= 202.5f)
        {
            userLook = USER_LOOK.BACK;
        }
        // 202.6 ~ 247.5
        else if (userCharacter.transform.eulerAngles.y > 202.5f &&
                userCharacter.transform.eulerAngles.y <= 247.5f)
        {
            userLook = USER_LOOK.BACK_LEFT;
        }
        // 247.6 ~ 292.5
        else if (userCharacter.transform.eulerAngles.y > 247.5f &&
                userCharacter.transform.eulerAngles.y <= 292.5f)
        {
            userLook = USER_LOOK.LEFT;
        }
        // 292.5 ~ 337.4
        else if (userCharacter.transform.eulerAngles.y > 292.5f &&
                userCharacter.transform.eulerAngles.y <= 337.5f)
        {
            userLook = USER_LOOK.FORWORD_LEFT;
        }
    }
    private void Attack_Input()
    {
        //마우스 왼클릭
        if (Input.GetMouseButton(0))
        {
            if ( (animatorController.GetCurrentAnimatorStateInfo(0).IsName("Attack_L3") || animatorController.GetCurrentAnimatorStateInfo(0).IsName("Attack_R3")) &&
                animatorController.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.8f && animatorController.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.3f)
            {
                if (!isAttack_L4 && (isAttack_L3 || isAttack_R3))
                {
                    isAttack_L4 = true;
                    animatorController.SetTrigger("Attack_L4");
                }
            }
            else if ((animatorController.GetCurrentAnimatorStateInfo(0).IsName("Attack_L2") || animatorController.GetCurrentAnimatorStateInfo(0).IsName("Attack_R2")) &&
                animatorController.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.8f && animatorController.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.3f)
            {
                if ( !isAttack_L3 && (isAttack_L2 || isAttack_R2))
                {
                    isAttack_L3 = true;
                    animatorController.SetTrigger("Attack_L3");
                }
            }
            else if ((animatorController.GetCurrentAnimatorStateInfo(0).IsName("Attack_L1") || animatorController.GetCurrentAnimatorStateInfo(0).IsName("Attack_R1")) &&
                animatorController.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.8f && animatorController.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.3f)
            {
                if ( !isAttack_L2 && (isAttack_L1 || isAttack_R1) )
                {
                    isAttack_L2 = true;
                    animatorController.SetTrigger("Attack_L2");
                }
                
            }
            else
            {
                if ( !isAttack_L1 && !isAttack_R1)
                {
                    isAttack_L1 = true;
                    animatorController.SetTrigger("Attack_L1");
                }
            }
            
        }
        //마우스 오른클릭
        else if (Input.GetMouseButton(1))
        {
            if ((animatorController.GetCurrentAnimatorStateInfo(0).IsName("Attack_L3") || animatorController.GetCurrentAnimatorStateInfo(0).IsName("Attack_R3")) &&
                 animatorController.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.8f && animatorController.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.3f)
            {
                if (!isAttack_R4 && (isAttack_L3 || isAttack_R3))
                {
                    isAttack_R4 = true;
                    animatorController.SetTrigger("Attack_R4");
                }
            }
            else if ((animatorController.GetCurrentAnimatorStateInfo(0).IsName("Attack_L2") || animatorController.GetCurrentAnimatorStateInfo(0).IsName("Attack_R2")) &&
                animatorController.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.8f && animatorController.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.3f)
            {
                if (!isAttack_R3 && (isAttack_L2 || isAttack_R2))
                {
                    
                    isAttack_R3 = true;
                    animatorController.SetTrigger("Attack_R3");
                }
            }
            else if ( (animatorController.GetCurrentAnimatorStateInfo(0).IsName("Attack_L1") || animatorController.GetCurrentAnimatorStateInfo(0).IsName("Attack_R1") ) &&
                 
                (animatorController.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.6f && animatorController.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5f) )
            {
                if (!isAttack_R2 && ( isAttack_L1 || isAttack_R1 ))
                {
                    isAttack_R2 = true;
                    animatorController.SetTrigger("Attack_R2");
                }
            }
            else
            {
                if (!isAttack_L1 && !isAttack_R1)
                {
                    isAttack_R1 = true;
                    animatorController.SetTrigger("Attack_R1");
                }
            }

        }
    }
    private void ResetAttack()
    {
        isAttack_L1 = isAttack_L2 = isAttack_L3 = isAttack_L4 = isAttack_R1 = isAttack_R2 = isAttack_R3 = isAttack_R4 = false;
    }

    private void OnDrawGizmos()
    {
    }

}
