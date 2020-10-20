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
        //StartCoroutine(UpdateCoroutine());
    }
    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(
                transform.position,
                TargetTransform.position + (Vector3.back * Distance) + (Vector3.up * Height),
                Time.deltaTime * Soft);

        transform.LookAt(TargetTransform);
    }

    //IEnumerator UpdateCoroutine()
    //{
    //    while (true)
    //    {
            
    //        yield return null;
    //    }
    //}

    //void Update()
    //{
        
    //}
}
