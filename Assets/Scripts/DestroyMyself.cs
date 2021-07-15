using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMyself : MonoBehaviour
{
    public float delayTime = 1.0f;

    float currentTime = 0;

    void Update()
    {
        if(delayTime < currentTime)
        {
            Destroy(gameObject);
        }

        currentTime += Time.deltaTime;
    }
}
