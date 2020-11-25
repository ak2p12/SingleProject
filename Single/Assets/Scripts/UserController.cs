using System.Collections;
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
    [HideInInspector] public Animator animatorController;
    private USER_LOOK userLook;
    [HideInInspector] public Vector3 rollDirection;
    private float axisX;
    private float axisZ;
    private bool pushButten;
    private float axisComeback;



    private bool rollCheck;

    [HideInInspector] public Collider[] colliders;
    

    void Start()
    {
        rollCheck = false;
        axisComeback = 3.0f;
        animatorController = GetComponentInChildren<Animator>();
        userCamera = GameObject.Find("UserCamera").GetComponent<Camera>();
        pushButten = false;
        user = GetComponent<User>();
        StartCoroutine(Input_Coroutine());
    }
    IEnumerator Input_Coroutine()
    {
        while (true)
        {
            if (animatorController.GetCurrentAnimatorStateInfo(0).IsName("Move_Blend") && !animatorController.IsInTransition(0))
                ResetAttack();

            Action_Input();
            Mouse_Input();
            Movement_Input();

            yield return null;
        }
    }
    private void Action_Input()
    {
        //구르기
        if (rollCheck == false)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                animatorController.SetTrigger("Roll_Trigger");
            }

            //마우스 왼클릭
            if (Input.GetMouseButton(0))
            {
                if ((animatorController.GetCurrentAnimatorStateInfo(0).IsName("Attack_L3") || animatorController.GetCurrentAnimatorStateInfo(0).IsName("Attack_R3")) &&
                    animatorController.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.8f && animatorController.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.3f)
                {
                    if (!user.Attack_L4 && (user.Attack_L3 || user.Attack_R3))
                    {
                        user.Attack_L4 = true;
                        animatorController.SetTrigger("Attack_L4");
                    }
                }
                else if ((animatorController.GetCurrentAnimatorStateInfo(0).IsName("Attack_L2") || animatorController.GetCurrentAnimatorStateInfo(0).IsName("Attack_R2")) &&
                    animatorController.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.8f && animatorController.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.3f)
                {
                    if (!user.Attack_L3 && (user.Attack_L2 || user.Attack_R2))
                    {
                        user.Attack_L3 = true;
                        animatorController.SetTrigger("Attack_L3");
                    }
                }
                else if ((animatorController.GetCurrentAnimatorStateInfo(0).IsName("Attack_L1") || animatorController.GetCurrentAnimatorStateInfo(0).IsName("Attack_R1")) &&
                    animatorController.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.8f && animatorController.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.3f)
                {
                    if (!user.Attack_L2 && (user.Attack_L1 || user.Attack_R1))
                    {
                        user.Attack_L2 = true;
                        animatorController.SetTrigger("Attack_L2");
                    }

                }
                else
                {
                    if (!user.Attack_L1 && !user.Attack_R1)
                    {
                        user.Attack_L1 = true;
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
                    if (!user.Attack_R4 && (user.Attack_L3 || user.Attack_R3))
                    {
                        user.Attack_R4 = true;
                        animatorController.SetTrigger("Attack_R4");
                    }
                }
                else if ((animatorController.GetCurrentAnimatorStateInfo(0).IsName("Attack_L2") || animatorController.GetCurrentAnimatorStateInfo(0).IsName("Attack_R2")) &&
                    animatorController.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.8f && animatorController.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.3f)
                {
                    if (!user.Attack_R3 && (user.Attack_L2 || user.Attack_R2))
                    {

                        user.Attack_R3 = true;
                        animatorController.SetTrigger("Attack_R3");
                    }
                }
                else if ((animatorController.GetCurrentAnimatorStateInfo(0).IsName("Attack_L1") || animatorController.GetCurrentAnimatorStateInfo(0).IsName("Attack_R1")) &&

                    (animatorController.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.6f && animatorController.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5f))
                {
                    if (!user.Attack_R2 && (user.Attack_L1 || user.Attack_R1))
                    {
                        user.Attack_R2 = true;
                        animatorController.SetTrigger("Attack_R2");
                    }
                }
                else
                {
                    if (!user.Attack_L1 && !user.Attack_R1)
                    {
                        user.Attack_R1 = true;
                        animatorController.SetTrigger("Attack_R1");
                    }
                }

            }

            //1번 장비
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                switch (user.haveWeapon_1)
                {
                    case USER_WEAPON.BAREHANDS:
                        break;
                    case USER_WEAPON.TWOHANDSWORD:
                        animatorController.SetTrigger("2HandSword_Swith_Back");
                        animatorController.SetInteger("UseWeapon" , (int)user.haveWeapon_1 );
                        break;
                    case USER_WEAPON.END:
                        break;
                }

                user.currentWeapon = user.haveWeapon_1;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                switch (user.haveWeapon_2)
                {
                    case USER_WEAPON.BAREHANDS:
                        break;
                    case USER_WEAPON.TWOHANDSWORD:
                        animatorController.SetTrigger("2HandSword_Swith_Hips");
                        animatorController.SetInteger("UseWeapon", (int)user.haveWeapon_2);
                        break;
                    case USER_WEAPON.END:
                        break;
                }
                user.currentWeapon = user.haveWeapon_2;
            }
        }
    }
    private void Movement_Input()
    {
        //구르는중이 아닐 때
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
                pushButten = true;
            }

            if (!pushButten)
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

            pushButten = false;
        }
        else //구르는 중 일때
        {
            transform.LookAt(transform.position + (rollDirection * 5.0f), Vector3.up);

            if (!animatorController.GetCurrentAnimatorStateInfo(0).IsName("Roll_End"))
                transform.Translate(rollDirection * 10.0f * Time.deltaTime, Space.World);
        }

        animatorController.SetFloat("Dir X", axisX);
        animatorController.SetFloat("Dir Z", axisZ);
    }
    private void Mouse_Input()
    {
        Vector3 mousePos = Input.mousePosition;

        ray = userCamera.ScreenPointToRay(mousePos);
        bool check = Physics.Raycast(ray, out rayHit, 50.0f, 1 << LayerMask.NameToLayer("Land"));
        if (true == check && false == rollCheck)
        {
            transform.rotation =
                Quaternion.Lerp(
                    transform.rotation,
                    Quaternion.LookRotation((rayHit.point - transform.position).normalized),
                    30.0f * Time.deltaTime);

            Debug.DrawLine(transform.position, rayHit.point);
            Debug.DrawLine(transform.position + transform.forward, rayHit.point, Color.red);
        }

        //0 ~ 22.5 , 337.5 ~ 360.0 
        if ((transform.eulerAngles.y > 337.5f && transform.eulerAngles.y <= 360.0f) ||
            (transform.eulerAngles.y >= 0.0f && transform.eulerAngles.y <= 22.5f))
        {
            userLook = USER_LOOK.FORWORD;
        }
        // 22.6 ~ 67.5
        else if (transform.eulerAngles.y > 22.5f &&
                transform.eulerAngles.y <= 67.5f)
        {
            userLook = USER_LOOK.FORWORD_RIGHT;
        }
        // 67.6 ~ 112.5
        else if (transform.eulerAngles.y > 67.5f &&
                transform.eulerAngles.y <= 112.5f)
        {
            userLook = USER_LOOK.RIGHT;
        }
        // 112.6 ~ 157.5
        else if (transform.eulerAngles.y > 112.5f &&
                transform.eulerAngles.y <= 157.5f)
        {
            userLook = USER_LOOK.BACK_RIGHT;
        }
        // 157.6 ~ 202.5
        else if (transform.eulerAngles.y > 157.5f &&
                transform.eulerAngles.y <= 202.5f)
        {
            userLook = USER_LOOK.BACK;
        }
        // 202.6 ~ 247.5
        else if (transform.eulerAngles.y > 202.5f &&
                transform.eulerAngles.y <= 247.5f)
        {
            userLook = USER_LOOK.BACK_LEFT;
        }
        // 247.6 ~ 292.5
        else if (transform.eulerAngles.y > 247.5f &&
                transform.eulerAngles.y <= 292.5f)
        {
            userLook = USER_LOOK.LEFT;
        }
        // 292.5 ~ 337.4
        else if (transform.eulerAngles.y > 292.5f &&
                transform.eulerAngles.y <= 337.5f)
        {
            userLook = USER_LOOK.FORWORD_LEFT;
        }
    }
    private void ResetAttack()
    {
        user.Attack_L1 = user.Attack_L2 = user.Attack_L3 = user.Attack_L4 = user.Attack_R1 = user.Attack_R2 = user.Attack_R3 = user.Attack_R4 = false;
    }
    public void RollStart()
    {
        rollCheck = true;

        axisX = 0.0f;
        axisZ = 0.0f;

        if (Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.A))
                rollDirection = new Vector3(-1, 0, 1);
            else if (Input.GetKey(KeyCode.D))
                rollDirection = new Vector3(1, 0, 1);
            else
                rollDirection = new Vector3(0, 0, 1);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (Input.GetKey(KeyCode.A))
                rollDirection = new Vector3(-1, 0, -1);
            else if (Input.GetKey(KeyCode.D))
                rollDirection = new Vector3(1, 0, -1);
            else
                rollDirection = new Vector3(0, 0, -1);
        }
        else if (Input.GetKey(KeyCode.A))
            rollDirection = new Vector3(-1, 0, 0);
        else if (Input.GetKey(KeyCode.D))
            rollDirection = new Vector3(1, 0, 0);
        else
            rollDirection = rayHit.point - transform.position;

        rollDirection.Normalize();

        transform.LookAt(transform.position + (rollDirection * 5.0f), Vector3.up);
    }
    public void RollEnd()
    {
        rollCheck = false;
    }
    public void UseWeaponAnimationReset(USER_WEAPON _useWeapon)
    {
        switch (_useWeapon)
        {
            case USER_WEAPON.BAREHANDS:
                break;
            case USER_WEAPON.TWOHANDSWORD:
                break;
            case USER_WEAPON.END:
                break;
        }
    }
}
