using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPattern : MonoBehaviour
{
    // ���� �ÿ��� 2�ʿ� ���ļ� Ư�� ��ġ���� ������ �����´�.


    // ź�� Ÿ�Կ� ���� ź�� ������ �޸��ϰ� �ϰ� �ʹ�.

    public GameObject missile;
    public Transform startPos;
    public Transform[] firePositions = new Transform[2];
    public float fireDelay = 0.3f;

    float currentTime = 0;
    int patternLoopCount = 0;


    void Start()
    {
        // fireDelay �������� �Ѿ��� ��� �߻��Ѵ�.
        InvokeRepeating("FireMissile", 0, fireDelay);

       
    }

    void Update()
    {
        Pattern1();
    }

    void Pattern1()
    {
        // ���� ��ġ�� �¿� 5���ͷ� 2ȸ �պ��Ѵ�.
        currentTime += Time.deltaTime;

        // ����, ���� �ð��� 1 �̸��̶��...
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
