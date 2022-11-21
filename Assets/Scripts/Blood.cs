using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : MonoBehaviour
{
    public GameObject poolOfBlood;

    private void OnEnable()
    {
        var pool = Instantiate(poolOfBlood);
        pool.transform.position = transform.position;
    }
}
