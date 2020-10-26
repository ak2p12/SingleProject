using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class UserController : MonoBehaviour
{
    private UserMain userMain;
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

    private bool rollCheck;

    void Start()
    {
        rollCheck = false;
        axisComeback = 10.0f;
        animatorController = GameObject.Find("User").GetComponentInChildren<Animator>();
        userCamera = GameObject.Find("UserCamera").GetComponent<Camera>();
        userCharacter = GameObject.Find("User/RPG-Character");
        isMovingKey = false;
        userMain = GetComponent<UserMain>();
        StartCoroutine(Input_Coroutine());
    }

    IEnumerator Input_Coroutine()
    {
        while (true)
        {
            //animatorController.applyRootMotion = false;
            MouseInput();
            KeyInput();

            yield return null;
        }
    }

    private void KeyInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !animatorController.GetCurrentAnimatorStateInfo(0).IsName("Roll"))
        {
            if (Input.GetKey(KeyCode.W) )
            {
                if (Input.GetKey(KeyCode.A))
                {
                    userCharacter.transform.LookAt(userCharacter.transform.position + (new Vector3(-1, 0, 1)), Vector3.up);
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    userCharacter.transform.LookAt(userCharacter.transform.position + (new Vector3(1, 0, 1)), Vector3.up);
                }
                else
                {
                    userCharacter.transform.LookAt(userCharacter.transform.position + (new Vector3(0, 0, 1)), Vector3.up);
                }
                
            }
            else if (Input.GetKey(KeyCode.S))
            {
                if (Input.GetKey(KeyCode.A))
                {
                    userCharacter.transform.LookAt(userCharacter.transform.position + (new Vector3(-1, 0, -1)), Vector3.up);
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    userCharacter.transform.LookAt(userCharacter.transform.position + (new Vector3(1, 0, -1)), Vector3.up);
                }
                else
                {
                    userCharacter.transform.LookAt(userCharacter.transform.position + (new Vector3(0, 0, -1)), Vector3.up);
                }

            }
            else if (Input.GetKey(KeyCode.A))
            {
                userCharacter.transform.LookAt(userCharacter.transform.position + (new Vector3(-1, 0, 0)), Vector3.up);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                userCharacter.transform.LookAt(userCharacter.transform.position + (new Vector3(1, 0, 0)), Vector3.up);
            }

            animatorController.SetTrigger("Roll Trigger");
            rollCheck = true;

        }

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
            if (animatorController.GetCurrentAnimatorStateInfo(0).IsName("Roll") && animatorController.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
            {
                rollCheck = false;
            }
        }
      

        animatorController.SetFloat("Dir X", axisX);
        animatorController.SetFloat("Dir Z", axisZ);
    }
    private void MouseInput()
    {
        Vector3 mousePos = Input.mousePosition;

        ray = userCamera.ScreenPointToRay(mousePos);

        if ( Physics.Raycast(ray, out rayHit, 50.0f) && !rollCheck)
        {
            userCharacter.transform.LookAt(rayHit.point, Vector3.up);
            Debug.DrawLine(userCharacter.transform.position, rayHit.point);
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
}
