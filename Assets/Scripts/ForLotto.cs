using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForLotto : MonoBehaviour
{
    // 1 ~ 45까지의 정수 중에 하나의 값을 6번 추첨한다.
    // 단, 각각의 값이 중복되는 일이 없도록 하여야 한다.

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
                        // 중복 상태!
                        i--;
                        break;
                    }
                }
            }
        }
    }
    
}
