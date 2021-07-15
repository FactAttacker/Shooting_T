using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpComprehension : MonoBehaviour
{
    public float result = 1.0f;
    public float playSpeed = 1.0f;

    float currentTime = 0;

    Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        if (currentTime <= 1.0f)
        {
            currentTime += Time.deltaTime * (1.0f / playSpeed);


            result = Mathf.Lerp(1, 10, currentTime);

            Vector3 test = new Vector3(result, 0, 0);
            transform.position = startPos + test;
        }
        else
        {
            currentTime = 0;
        }
    }
}
