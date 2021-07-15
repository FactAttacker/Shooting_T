using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForLotto : MonoBehaviour
{
    // 1 ~ 45������ ���� �߿� �ϳ��� ���� 6�� ��÷�Ѵ�.
    // ��, ������ ���� �ߺ��Ǵ� ���� ������ �Ͽ��� �Ѵ�.

    public int[] myNumbers = new int[6];

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            for(int i = 0; i < myNumbers.Length; i++)
            {
                int num = Random.Range(1, 46);

                myNumbers[i] = num;

                for (int j = 0; j < i; j++)
                {
                    if(myNumbers[i] == myNumbers[j])
                    {
                        // �ߺ� ����!
                        i--;
                        break;
                    }
                }
            }
        }
    }
    
}
