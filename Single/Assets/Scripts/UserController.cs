using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserController : MonoBehaviour
{
    private UserMain userMain;
    private Camera userCamera;

    void Start()
    {
        userMain = GetComponent<UserMain>();
        userCamera = GameObject.Find("UserCamera").GetComponent<Camera>();
        StartCoroutine(Input_Coroutine());
    }

    IEnumerator Input_Coroutine()
    {
        while(true)
        {
            if(Input.GetKey(KeyCode.W))
            {
                transform.Translate(
                    Vector3.forward * userMain.GetUnitInfomation().MoveSpeed  * Time.deltaTime,
                    Space.World);
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(
                    Vector3.left * userMain.GetUnitInfomation().MoveSpeed * Time.deltaTime,
                    Space.World);
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(
                    Vector3.back * userMain.GetUnitInfomation().MoveSpeed * Time.deltaTime,
                    Space.World);
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(
                    Vector3.right * userMain.GetUnitInfomation().MoveSpeed * Time.deltaTime,
                    Space.World);
            }

            Vector3 mousePos = Input.mousePosition;
            Debug.Log(userCamera.ScreenPointToRay(mousePos).ToString());

            yield return null;
        }
    }
}
//asd