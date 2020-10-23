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
    private GameObject game;
    private USER_LOOK userLook;
    private float axisX;
    private float axisZ;
    private bool isMovingKey;
    private float axisComeback;

    void Start()
    {
        axisComeback = 10.0f;
        animatorController = GameObject.Find("User").GetComponentInChildren<Animator>();
        userCamera = GameObject.Find("UserCamera").GetComponent<Camera>();
        game = GameObject.Find("User/RPG-Character");
        isMovingKey = false;
        userMain = GetComponent<UserMain>();
        StartCoroutine(Input_Coroutine());
    }

    IEnumerator Input_Coroutine()
    {
        while (true)
        {

            MouseInput();
            KeyInput();
            //axisX = Input.GetAxis("Horizontal");
            //axisZ = Input.GetAxis("Vertical");

            

            animatorController.applyRootMotion = false;
            Debug.Log(userLook.ToString());

            //Debug.Log("axisX : " + axisX.ToString());
            //Debug.Log("axisZ : " + axisZ.ToString());
            //Debug.Log(game.transform.eulerAngles.ToString());

            //Debug.Log(Input.GetAxis("Vertical").ToString());

            yield return null;
        }
    }

    private void KeyInput()
    {
        if (Input.GetKey(KeyCode.W) ||
            Input.GetKey(KeyCode.S) ||
            Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.D))
        {
            isMovingKey = true;
        }

        switch (userLook)
        {
            case USER_LOOK.FORWORD:
                {
                    if (Input.GetKey(KeyCode.W))
                    {
                        if (Input.GetKey(KeyCode.A))
                        {
                            Debug.Log("W + A");
                            transform.Translate(new Vector3(-1, 0, 1) * userMain.GetUnitInfomation().MoveSpeed * Time.deltaTime);

                            if (axisX > 0)
                                axisX -= axisComeback * Time.deltaTime;
                            else if (axisX < 0)
                                axisX += axisComeback * Time.deltaTime;

                            axisZ += axisComeback * Time.deltaTime;

                        }
                        else if (Input.GetKey(KeyCode.D))
                        {
                            Debug.Log("W + D");
                            transform.Translate(new Vector3(1, 0, 1) * userMain.GetUnitInfomation().MoveSpeed * Time.deltaTime);

                            if (axisX > 0)
                                axisX -= axisComeback * Time.deltaTime;
                            else if (axisX < 0)
                                axisX += axisComeback * Time.deltaTime;

                            axisZ += axisComeback * Time.deltaTime;

                        }
                        else
                        {
                            Debug.Log("W");
                            transform.Translate(Vector3.forward * userMain.GetUnitInfomation().MoveSpeed * Time.deltaTime);

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
                            Debug.Log("S + A");
                            transform.Translate(new Vector3(-1, 0, -1) * userMain.GetUnitInfomation().MoveSpeed * Time.deltaTime);

                            if (axisX > 0)
                                axisX -= axisComeback * Time.deltaTime;
                            else if (axisX < 0)
                                axisX += axisComeback * Time.deltaTime;

                            axisZ -= axisComeback * Time.deltaTime;

                        }
                        else if (Input.GetKey(KeyCode.D))
                        {
                            Debug.Log("S + D");
                            transform.Translate(new Vector3(1, 0, -1) * userMain.GetUnitInfomation().MoveSpeed * Time.deltaTime);

                            if (axisX > 0)
                                axisX -= axisComeback * Time.deltaTime;
                            else if (axisX < 0)
                                axisX += axisComeback * Time.deltaTime;

                            axisZ -= axisComeback * Time.deltaTime;
                        }
                        else
                        {
                            Debug.Log("S");
                            transform.Translate(new Vector3(0, 0, -1) * userMain.GetUnitInfomation().MoveSpeed * Time.deltaTime);

                            if (axisX > 0)
                                axisX -= axisComeback * Time.deltaTime;
                            else if (axisX < 0)
                                axisX += axisComeback * Time.deltaTime;

                            axisZ -= axisComeback * Time.deltaTime;
                        }
                    }
                    else if (Input.GetKey(KeyCode.A))
                    {
                        transform.Translate(Vector3.left * userMain.GetUnitInfomation().MoveSpeed * Time.deltaTime);

                        axisX -= axisComeback * Time.deltaTime;

                        if (axisZ > 0)
                            axisZ -= axisComeback * Time.deltaTime;
                        else if (axisZ < 0)
                            axisZ += axisComeback * Time.deltaTime;

                    }
                    else if (Input.GetKey(KeyCode.D))
                    {
                        transform.Translate(Vector3.right * userMain.GetUnitInfomation().MoveSpeed * Time.deltaTime);
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
                        if ( Input.GetKey(KeyCode.A) )
                        {
                            Debug.Log("W + A");
                            transform.Translate( new Vector3(-1, 0, 1) * userMain.GetUnitInfomation().MoveSpeed * Time.deltaTime);

                            if (axisX > 0)
                                axisX -= axisComeback * Time.deltaTime;
                            else if (axisX < 0)
                                axisX += axisComeback * Time.deltaTime;

                            axisZ += axisComeback * Time.deltaTime;

                        }
                        else if ( Input.GetKey(KeyCode.D) )
                        {
                            Debug.Log("W + D");
                            transform.Translate(new Vector3(1, 0, 1) * userMain.GetUnitInfomation().MoveSpeed * Time.deltaTime);

                            axisX += axisComeback * Time.deltaTime;

                            if (axisZ > 0)
                                axisZ -= axisComeback * Time.deltaTime;
                            else if (axisZ < 0)
                                axisZ += axisComeback * Time.deltaTime;

                        }
                        else
                        {
                            Debug.Log("W");
                            transform.Translate(Vector3.forward * userMain.GetUnitInfomation().MoveSpeed * Time.deltaTime);
                            axisX += axisComeback * Time.deltaTime;
                            axisZ += axisComeback * Time.deltaTime;
                        }
                    }
                    else if (Input.GetKey(KeyCode.S))
                    {
                        if (Input.GetKey(KeyCode.A))
                        {
                            Debug.Log("S + A");
                            transform.Translate(new Vector3(-1, 0, -1) * userMain.GetUnitInfomation().MoveSpeed * Time.deltaTime);
                            axisX -= axisComeback * Time.deltaTime;

                            if (axisZ > 0)
                                axisZ -= axisComeback * Time.deltaTime;
                            else if (axisZ < 0)
                                axisZ += axisComeback * Time.deltaTime;

                        }
                        else if (Input.GetKey(KeyCode.D))
                        {
                            Debug.Log("S + D");
                            transform.Translate(new Vector3(1, 0, -1) * userMain.GetUnitInfomation().MoveSpeed * Time.deltaTime);

                            if (axisX > 0)
                                axisX -= axisComeback * Time.deltaTime;
                            else if (axisX < 0)
                                axisX += axisComeback * Time.deltaTime;

                            axisZ -= axisComeback * Time.deltaTime;
                        }
                        else
                        {
                            Debug.Log("S");
                            transform.Translate(new Vector3(0, 0, -1) * userMain.GetUnitInfomation().MoveSpeed * Time.deltaTime);
                            axisX -= axisComeback * Time.deltaTime;
                            axisZ -= axisComeback * Time.deltaTime;
                        }
                    }
                    else if (Input.GetKey(KeyCode.A))
                    {
                        transform.Translate(Vector3.left * userMain.GetUnitInfomation().MoveSpeed * Time.deltaTime);
                        axisX -= axisComeback * Time.deltaTime;
                        axisZ += axisComeback * Time.deltaTime;
                    }
                    else if (Input.GetKey(KeyCode.D))
                    {
                        transform.Translate(Vector3.right * userMain.GetUnitInfomation().MoveSpeed * Time.deltaTime);
                        axisX += axisComeback * Time.deltaTime;
                        axisZ -= axisComeback * Time.deltaTime;
                    }
                }
                break;
            case USER_LOOK.FORWORD_RIGHT:
                {
                    if (Input.GetKey(KeyCode.W))
                    {
                        transform.Translate(Vector3.forward * userMain.GetUnitInfomation().MoveSpeed * Time.deltaTime);
                        axisZ += 3 * Time.deltaTime;
                        if (axisZ > 1)
                            axisZ = 1;
                    }
                    else if (Input.GetKey(KeyCode.S))
                    {
                        transform.Translate(Vector3.back * userMain.GetUnitInfomation().MoveSpeed * Time.deltaTime);
                        axisZ -= (3 * Time.deltaTime);
                        if (axisZ < -1)
                            axisZ = -1;
                    }
                    else
                    {
                        if (axisZ > 0)
                        {
                            axisZ -= (3 * Time.deltaTime);
                            if (axisZ < 0)
                                axisZ = 0;
                        }
                        else if (axisZ < 0)
                        {
                            axisZ += (3 * Time.deltaTime);
                            if (axisZ > 0)
                                axisZ = 0;
                        }
                    }

                    if (Input.GetKey(KeyCode.A))
                    {
                        transform.Translate(Vector3.left * userMain.GetUnitInfomation().MoveSpeed * Time.deltaTime);
                        axisX -= 3 * Time.deltaTime;
                        if (axisX < -1)
                            axisX = -1;
                    }
                    else if (Input.GetKey(KeyCode.D))
                    {
                        transform.Translate(Vector3.right * userMain.GetUnitInfomation().MoveSpeed * Time.deltaTime);
                        axisX += (3 * Time.deltaTime);
                        Debug.Log(axisZ.ToString());
                        if (axisX > 1)
                            axisX = 1;
                    }
                    else
                    {
                        if (axisX > 0)
                        {
                            axisX -= (3 * Time.deltaTime);
                            if (axisX < 0)
                                axisX = 0;
                        }
                        else if (axisX < 0)
                        {
                            axisX += (3 * Time.deltaTime);
                            if (axisX > 0)
                                axisX = 0;
                        }
                    }
                }
                break;
            case USER_LOOK.RIGHT:
                {
                    if (Input.GetKey(KeyCode.W))
                    {
                        transform.Translate(Vector3.forward * userMain.GetUnitInfomation().MoveSpeed * Time.deltaTime);
                        axisZ += 3 * Time.deltaTime;
                        if (axisZ > 1)
                            axisZ = 1;
                    }
                    else if (Input.GetKey(KeyCode.S))
                    {
                        transform.Translate(Vector3.back * userMain.GetUnitInfomation().MoveSpeed * Time.deltaTime);
                        axisZ -= (3 * Time.deltaTime);
                        if (axisZ < -1)
                            axisZ = -1;
                    }
                    else
                    {
                        if (axisZ > 0)
                        {
                            axisZ -= (3 * Time.deltaTime);
                            if (axisZ < 0)
                                axisZ = 0;
                        }
                        else if (axisZ < 0)
                        {
                            axisZ += (3 * Time.deltaTime);
                            if (axisZ > 0)
                                axisZ = 0;
                        }
                    }

                    if (Input.GetKey(KeyCode.A))
                    {
                        transform.Translate(Vector3.left * userMain.GetUnitInfomation().MoveSpeed * Time.deltaTime);
                        axisX -= 3 * Time.deltaTime;
                        if (axisX < -1)
                            axisX = -1;
                    }
                    else if (Input.GetKey(KeyCode.D))
                    {
                        transform.Translate(Vector3.right * userMain.GetUnitInfomation().MoveSpeed * Time.deltaTime);
                        axisX += (3 * Time.deltaTime);
                        Debug.Log(axisZ.ToString());
                        if (axisX > 1)
                            axisX = 1;
                    }
                    else
                    {
                        if (axisX > 0)
                        {
                            axisX -= (3 * Time.deltaTime);
                            if (axisX < 0)
                                axisX = 0;
                        }
                        else if (axisX < 0)
                        {
                            axisX += (3 * Time.deltaTime);
                            if (axisX > 0)
                                axisX = 0;
                        }
                    }
                }
                break;
            case USER_LOOK.LEFT:
                {
                    if (Input.GetKey(KeyCode.W))
                    {
                        transform.Translate(Vector3.forward * userMain.GetUnitInfomation().MoveSpeed * Time.deltaTime);
                        axisZ += 3 * Time.deltaTime;
                        if (axisZ > 1)
                            axisZ = 1;
                    }
                    else if (Input.GetKey(KeyCode.S))
                    {
                        transform.Translate(Vector3.back * userMain.GetUnitInfomation().MoveSpeed * Time.deltaTime);
                        axisZ -= (3 * Time.deltaTime);
                        if (axisZ < -1)
                            axisZ = -1;
                    }
                    else
                    {
                        if (axisZ > 0)
                        {
                            axisZ -= (3 * Time.deltaTime);
                            if (axisZ < 0)
                                axisZ = 0;
                        }
                        else if (axisZ < 0)
                        {
                            axisZ += (3 * Time.deltaTime);
                            if (axisZ > 0)
                                axisZ = 0;
                        }
                    }

                    if (Input.GetKey(KeyCode.A))
                    {
                        transform.Translate(Vector3.left * userMain.GetUnitInfomation().MoveSpeed * Time.deltaTime);
                        axisX -= 3 * Time.deltaTime;
                        if (axisX < -1)
                            axisX = -1;
                    }
                    else if (Input.GetKey(KeyCode.D))
                    {
                        transform.Translate(Vector3.right * userMain.GetUnitInfomation().MoveSpeed * Time.deltaTime);
                        axisX += (3 * Time.deltaTime);
                        Debug.Log(axisZ.ToString());
                        if (axisX > 1)
                            axisX = 1;
                    }
                    else
                    {
                        if (axisX > 0)
                        {
                            axisX -= (3 * Time.deltaTime);
                            if (axisX < 0)
                                axisX = 0;
                        }
                        else if (axisX < 0)
                        {
                            axisX += (3 * Time.deltaTime);
                            if (axisX > 0)
                                axisX = 0;
                        }
                    }
                }
                break;
            case USER_LOOK.BACK:
                {
                    if (Input.GetKey(KeyCode.W))
                    {
                        transform.Translate(Vector3.forward * userMain.GetUnitInfomation().MoveSpeed * Time.deltaTime);
                        axisZ += 3 * Time.deltaTime;
                        if (axisZ > 1)
                            axisZ = 1;
                    }
                    else if (Input.GetKey(KeyCode.S))
                    {
                        transform.Translate(Vector3.back * userMain.GetUnitInfomation().MoveSpeed * Time.deltaTime);
                        axisZ -= (3 * Time.deltaTime);
                        if (axisZ < -1)
                            axisZ = -1;
                    }
                    else
                    {
                        if (axisZ > 0)
                        {
                            axisZ -= (3 * Time.deltaTime);
                            if (axisZ < 0)
                                axisZ = 0;
                        }
                        else if (axisZ < 0)
                        {
                            axisZ += (3 * Time.deltaTime);
                            if (axisZ > 0)
                                axisZ = 0;
                        }
                    }

                    if (Input.GetKey(KeyCode.A))
                    {
                        transform.Translate(Vector3.left * userMain.GetUnitInfomation().MoveSpeed * Time.deltaTime);
                        axisX -= 3 * Time.deltaTime;
                        if (axisX < -1)
                            axisX = -1;
                    }
                    else if (Input.GetKey(KeyCode.D))
                    {
                        transform.Translate(Vector3.right * userMain.GetUnitInfomation().MoveSpeed * Time.deltaTime);
                        axisX += (3 * Time.deltaTime);
                        Debug.Log(axisZ.ToString());
                        if (axisX > 1)
                            axisX = 1;
                    }
                    else
                    {
                        if (axisX > 0)
                        {
                            axisX -= (3 * Time.deltaTime);
                            if (axisX < 0)
                                axisX = 0;
                        }
                        else if (axisX < 0)
                        {
                            axisX += (3 * Time.deltaTime);
                            if (axisX > 0)
                                axisX = 0;
                        }
                    }
                }
                break;
            case USER_LOOK.BACK_LEFT:
                {
                    if (Input.GetKey(KeyCode.W))
                    {
                        transform.Translate(Vector3.forward * userMain.GetUnitInfomation().MoveSpeed * Time.deltaTime);
                        axisZ += 3 * Time.deltaTime;
                        if (axisZ > 1)
                            axisZ = 1;
                    }
                    else if (Input.GetKey(KeyCode.S))
                    {
                        transform.Translate(Vector3.back * userMain.GetUnitInfomation().MoveSpeed * Time.deltaTime);
                        axisZ -= (3 * Time.deltaTime);
                        if (axisZ < -1)
                            axisZ = -1;
                    }
                    else
                    {
                        if (axisZ > 0)
                        {
                            axisZ -= (3 * Time.deltaTime);
                            if (axisZ < 0)
                                axisZ = 0;
                        }
                        else if (axisZ < 0)
                        {
                            axisZ += (3 * Time.deltaTime);
                            if (axisZ > 0)
                                axisZ = 0;
                        }
                    }

                    if (Input.GetKey(KeyCode.A))
                    {
                        transform.Translate(Vector3.left * userMain.GetUnitInfomation().MoveSpeed * Time.deltaTime);
                        axisX -= 3 * Time.deltaTime;
                        if (axisX < -1)
                            axisX = -1;
                    }
                    else if (Input.GetKey(KeyCode.D))
                    {
                        transform.Translate(Vector3.right * userMain.GetUnitInfomation().MoveSpeed * Time.deltaTime);
                        axisX += (3 * Time.deltaTime);
                        Debug.Log(axisZ.ToString());
                        if (axisX > 1)
                            axisX = 1;
                    }
                    else
                    {
                        if (axisX > 0)
                        {
                            axisX -= (3 * Time.deltaTime);
                            if (axisX < 0)
                                axisX = 0;
                        }
                        else if (axisX < 0)
                        {
                            axisX += (3 * Time.deltaTime);
                            if (axisX > 0)
                                axisX = 0;
                        }
                    }
                }
                break;
            case USER_LOOK.BACK_RIGHT:
                {
                    if (Input.GetKey(KeyCode.W))
                    {
                        transform.Translate(Vector3.forward * userMain.GetUnitInfomation().MoveSpeed * Time.deltaTime);
                        axisZ += 3 * Time.deltaTime;
                        if (axisZ > 1)
                            axisZ = 1;
                    }
                    else if (Input.GetKey(KeyCode.S))
                    {
                        transform.Translate(Vector3.back * userMain.GetUnitInfomation().MoveSpeed * Time.deltaTime);
                        axisZ -= (3 * Time.deltaTime);
                        if (axisZ < -1)
                            axisZ = -1;
                    }
                    else
                    {
                        if (axisZ > 0)
                        {
                            axisZ -= (3 * Time.deltaTime);
                            if (axisZ < 0)
                                axisZ = 0;
                        }
                        else if (axisZ < 0)
                        {
                            axisZ += (3 * Time.deltaTime);
                            if (axisZ > 0)
                                axisZ = 0;
                        }
                    }

                    if (Input.GetKey(KeyCode.A))
                    {
                        transform.Translate(Vector3.left * userMain.GetUnitInfomation().MoveSpeed * Time.deltaTime);
                        axisX -= 3 * Time.deltaTime;
                        if (axisX < -1)
                            axisX = -1;
                    }
                    else if (Input.GetKey(KeyCode.D))
                    {
                        transform.Translate(Vector3.right * userMain.GetUnitInfomation().MoveSpeed * Time.deltaTime);
                        axisX += (3 * Time.deltaTime);
                        Debug.Log(axisZ.ToString());
                        if (axisX > 1)
                            axisX = 1;
                    }
                    else
                    {
                        if (axisX > 0)
                        {
                            axisX -= (3 * Time.deltaTime);
                            if (axisX < 0)
                                axisX = 0;
                        }
                        else if (axisX < 0)
                        {
                            axisX += (3 * Time.deltaTime);
                            if (axisX > 0)
                                axisX = 0;
                        }
                    }
                }
                break;
        }

        //Debug.Log("isMovingKey : " + isMovingKey.ToString());
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
            {
                axisX = 1;
            }
            else if (axisX < -1)
            {
                axisX = -1;
            }

            if (axisZ > 1)
            {
                axisZ = 1;
            }
            else if (axisZ < -1)
            {
                axisZ = -1;
            }
        }

        isMovingKey = false;

        animatorController.SetFloat("Dir X", axisX);
        animatorController.SetFloat("Dir Z", axisZ);
    }
    private void MouseInput()
    {
        Vector3 mousePos = Input.mousePosition;

        ray = userCamera.ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray, out rayHit, 50.0f))
        {
            game.transform.LookAt(rayHit.point, Vector3.up);
            Debug.DrawLine(game.transform.position, rayHit.point);
        }
        
        //337.5 ~ 360.0 
        //0 ~ 22.5
        if ( (game.transform.eulerAngles.y > 337.5f && game.transform.eulerAngles.y <= 360.0f) ||
            (game.transform.eulerAngles.y >= 0.0f && game.transform.eulerAngles.y <= 22.5f) )
        {
            userLook = USER_LOOK.FORWORD;
        }
        // 22.6 ~ 67.5
        else if(game.transform.eulerAngles.y > 22.5f &&
                game.transform.eulerAngles.y <= 67.5f)
        {
            userLook = USER_LOOK.FORWORD_RIGHT;
        }
        // 67.6 ~ 112.5
        else if (game.transform.eulerAngles.y > 67.5f &&
                game.transform.eulerAngles.y <= 112.5f)
        {
            userLook = USER_LOOK.RIGHT;
        }
        // 112.6 ~ 157.5
        else if (game.transform.eulerAngles.y > 112.5f &&
                game.transform.eulerAngles.y <= 157.5f)
        {
            userLook = USER_LOOK.BACK_RIGHT;
        }
        // 157.6 ~ 202.5
        else if (game.transform.eulerAngles.y > 157.5f &&
                game.transform.eulerAngles.y <= 202.5f)
        {
            userLook = USER_LOOK.BACK;
        }
        // 202.6 ~ 247.5
        else if (game.transform.eulerAngles.y > 202.5f &&
                game.transform.eulerAngles.y <= 247.5f)
        {
            userLook = USER_LOOK.BACK_LEFT;
        }
        // 247.6 ~ 292.5
        else if (game.transform.eulerAngles.y > 247.5f &&
                game.transform.eulerAngles.y <= 292.5f)
        {
            userLook = USER_LOOK.LEFT;
        }
        // 292.5 ~ 337.4
        else if (game.transform.eulerAngles.y > 292.5f &&
                game.transform.eulerAngles.y <= 337.5f)
        {
            userLook = USER_LOOK.FORWORD_LEFT;
        }
    }
}
