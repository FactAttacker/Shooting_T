using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAction : MonoBehaviour
{
    // 나에게 부딪힌 대상을 제거한다.
    public PlayerFire pFire;


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name != "Player")
        {
            //if(collision.gameObject.name.Contains("Bullet"))
            if (collision.gameObject.name == "Bullet(Clone)")
            {
                collision.gameObject.SetActive(false);
                // 충돌한 총알을 탄창 비활성화 리스트에 다시 추가한다.
                pFire.magazine.Add(collision.gameObject);
            }
            // 충돌한 대상을 제거한다.
            else
            {
                Destroy(collision.gameObject);
            }
        }
    }
}
