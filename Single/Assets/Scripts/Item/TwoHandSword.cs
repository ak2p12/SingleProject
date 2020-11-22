using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoHandSword : MonoBehaviour
{
    
    void Start()
    {
        StartCoroutine(UpDate_Coroutine());
    }

    IEnumerator UpDate_Coroutine()
    {
        while(true)
        {
            transform.Rotate(Vector3.forward,1.0f);
            yield return null;
        }
    }
}
