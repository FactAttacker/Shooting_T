using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPattern : MonoBehaviour
{
    // 등장 시에는 2초에 걸쳐서 특정 위치까지 서서히 내려온다.


    // 탄막 타입에 따라서 탄막 공격을 달리하게 하고 싶다.

    public GameObject missile;
    public Transform startPos;
    public Transform[] firePositions = new Transform[2];
    public float fireDelay = 0.3f;

    float currentTime = 0;
    int patternLoopCount = 0;


    void Start()
    {
        // fireDelay 간격으로 총알을 계속 발사한다.
        InvokeRepeating("FireMissile", 0, fireDelay);

       
    }

    void Update()
    {
        Pattern1();
    }

    void Pattern1()
    {
        // 나의 위치를 좌우 5미터로 2회 왕복한다.
        currentTime += Time.deltaTime;

        // 만일, 누적 시간이 1 미만이라면...
        if (patternLoopCount == 0)
        {
            if (currentTime <= 1)
            {
                transform.position = Vector3.Lerp(startPos.position, startPos.position + new Vector3(5, 0, 0), currentTime);
            }
            else
            {
                currentTime = 0;
                patternLoopCount++;
            }
        }
        else if(patternLoopCount == 1)
        {
            if (currentTime <= 2)
            {
                transform.position = Vector3.Lerp(startPos.position + new Vector3(5, 0, 0), startPos.position - new Vector3(5, 0, 0), currentTime * 0.5f);
            }
            else
            {
                currentTime = 0;
                patternLoopCount++;
            }
        }
        else if(patternLoopCount == 2)
        {
            if (currentTime <= 1)
            {
                transform.position = Vector3.Lerp(startPos.position - new Vector3(5, 0, 0), startPos.position, currentTime);
            }
            else
            {
                currentTime = 0;
                patternLoopCount++;
                CancelInvoke();
            }
        }
        
    }


    void FireMissile()
    {
        for(int i = 0; i < firePositions.Length; i++)
        {
            Instantiate(missile, firePositions[i].position, Quaternion.Euler(new Vector3(0, 0, 180)));
        }
    }
}
