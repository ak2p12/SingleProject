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

    private float axisX;
    private float axisZ;

    void Start()
    {
        animatorController = GameObject.Find("User").GetComponentInChildren<Animator>();
        userCamera = GameObject.Find("UserCamera").GetComponent<Camera>();
        game = GameObject.Find("User/RPG-Character");

        userMain = GetComponent<UserMain>();
        StartCoroutine(Input_Coroutine());
    }

    IEnumerator Input_Coroutine()
    {
        while(true)
        {
            axisX = Input.GetAxis("Horizontal");
            axisZ = Input.GetAxis("Vertical");
            animatorController.SetFloat("Dir X", axisX);
            animatorController.SetFloat("Dir Z", axisZ);

            //오른쪽
            if (Input.GetKey(KeyCode.D))
            {
                //transform.Translate(
                    //Vector3.right * axisX * userMain.GetUnitInfomation().MoveSpeed * Time.deltaTime,
                    //Space.World);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                //transform.Translate(
                    //Vector3.left * axisX * -userMain.GetUnitInfomation().MoveSpeed * Time.deltaTime,
                    //Space.World);
            }

            if (Input.GetKey(KeyCode.W))
            {
                //transform.Translate(
                    //Vector3.forward * axisZ * userMain.GetUnitInfomation().MoveSpeed * Time.deltaTime,
                    //Space.World); ;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                //transform.Translate(
                    //Vector3.back * axisZ * -userMain.GetUnitInfomation().MoveSpeed * Time.deltaTime,
                    //Space.World);
            }

            Vector3 mousePos = Input.mousePosition;
            
            ray = userCamera.ScreenPointToRay(mousePos);

            if ( Physics.Raycast(ray , out rayHit, 50.0f) )
            {
                game.transform.LookAt(rayHit.point, Vector3.up);
                //transform.LookAt(rayHit.point,Vector3.up);
            }

            //Debug.Log("axisX : " + axisX.ToString());
            //Debug.Log("axisZ : " + axisZ.ToString());
            
            //Debug.Log(Input.GetAxis("Vertical").ToString());
            
            yield return null;
        }
    }
}
//asd