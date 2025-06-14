using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantArrowCompass : MonoBehaviour
{
    [SerializeField] private Transform target;

    private void Update()
    {
        if (target != null)
            transform.LookAt(target);
    }
}