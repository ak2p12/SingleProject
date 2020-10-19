using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform TargetTransform;
    public float Distance;
    public float Height;
    public float Soft;

    void Start()
    {
        StartCoroutine(UpdateCoroutine());
    }
    IEnumerator UpdateCoroutine()
    {
        while (true)
        {
            //transform.position = Vector3.Lerp(
            //    TargetTransform.position,
            //    transform.position,
            //    );
            yield return null;
        }
    }

    //void Update()
    //{
        
    //}
}
