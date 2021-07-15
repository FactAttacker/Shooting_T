using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    // ������ �ð����� ���ʹ̸� �����ϰ� �ʹ�.

    public GameObject enemySource;
    public float delayTime = 2.0f;

    float currentTime = 0;

    void Start()
    {
        
    }

    void Update()
    {
        // 1. ���� �ð��� üũ�ؼ� ������ �ð��� �Ǿ����� Ȯ���Ѵ�.
        if (currentTime > delayTime)
        {
            //  1-1. ���� ��ġ�� ������ ��ġ�� ���ʹ̸� �����Ѵ�.
            Instantiate(enemySource, transform.position, Quaternion.identity);

            //  1-2. ���� �ð��� 0���� �ʱ�ȭ�Ѵ�.
            currentTime = 0;
        }
        // 2. ���� ���������κ��� ���� �����ӱ��� �ɸ� �ð��� ���� �ð� ������ �����Ѵ�.
        else
        {
            currentTime += Time.deltaTime;
        }
    }
}
