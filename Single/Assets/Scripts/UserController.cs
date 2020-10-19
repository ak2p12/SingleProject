using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserController : MonoBehaviour
{
    private UserMain userMain;

    void Start()
    {
        userMain = GetComponent<UserMain>();
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


            yield return null;
        }
    }
}
