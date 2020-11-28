using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DualSword : Item
{
    bool updown;
    float vertical;
    Vector3 centerPosition;
    Collider[] targetCollider;
    LayerMask targetLayer;

    private void OnEnable()
    {
        vertical = 0.5f;
        centerPosition = transform.position;
    }
    void Start()
    {
        targetLayer = 1 << LayerMask.NameToLayer("User");
        targetCollider = new Collider[1];
        StartCoroutine(UpDate_Coroutine());
    }
    IEnumerator UpDate_Coroutine()
    {
        while (true)
        {
            Spine();

            int find = Physics.OverlapSphereNonAlloc(
                            transform.position,
                            1.0f,
                            targetCollider,
                            targetLayer);

            if (null != targetCollider[0])
            {
                //해당 캐릭터 1번장비가 없다면
                if (USER_WEAPON.BAREHANDS == targetCollider[0].transform.GetComponent<User>().haveWeapon_1)
                {
                    targetCollider[0].transform.GetComponent<User>().haveWeapon_1 = USER_WEAPON.DUALSWORD;
                    foreach (GameObject _weapon in targetCollider[0].transform.GetComponent<User>().shoulderWeaponObject)
                    {
                        if (null == _weapon)
                            continue;
                        if (_weapon.name == "Equipment_Shoulder_L_DualSword")
                            _weapon.SetActive(true);
                        else if (_weapon.name == "Equipment_Shoulder_R_DualSword")
                            _weapon.SetActive(true);
                    }
                }
                //해당 캐릭터 2번장비가 없다면
                else if (USER_WEAPON.BAREHANDS == targetCollider[0].transform.GetComponent<User>().haveWeapon_2)
                {
                    targetCollider[0].transform.GetComponent<User>().haveWeapon_2 = USER_WEAPON.DUALSWORD;
                    foreach (GameObject _weapon in targetCollider[0].transform.GetComponent<User>().hipWeaponObject)
                    {
                        if (null == _weapon)
                            continue;
                        if (_weapon.name == "Equipment_Hip_L_DualSword")
                            _weapon.SetActive(true);
                        else if (_weapon.name == "Equipment_Hip_R_DualSword")
                            _weapon.SetActive(true);
                    }
                }

                this.gameObject.SetActive(false);
                yield break;
            }
            yield return null;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 1.0f);
    }
    void Spine()
    {
        transform.Rotate(Vector3.forward, 0.7f);

        if (true == updown)
        {
            vertical -= (Time.deltaTime * 0.3f);

            transform.position = Vector3.Lerp(
                new Vector3(centerPosition.x, centerPosition.y - (centerPosition.y * 0.5f), centerPosition.z),
                new Vector3(centerPosition.x, centerPosition.y + (centerPosition.y * 0.5f), centerPosition.z),
                vertical);

            if (0.0f >= vertical)
            {
                vertical = 0.0f;
                updown = false;
            }

        }
        else
        {
            vertical += (Time.deltaTime * 0.3f);

            transform.position = Vector3.Lerp(
                new Vector3(centerPosition.x, centerPosition.y - (centerPosition.y * 0.5f), centerPosition.z),
                new Vector3(centerPosition.x, centerPosition.y + (centerPosition.y * 0.5f), centerPosition.z),
                vertical);

            if (1.0f <= vertical)
            {
                vertical = 1.0f;
                updown = true;
            }
        }
    }

}
