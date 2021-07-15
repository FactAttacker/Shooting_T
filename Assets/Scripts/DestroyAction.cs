using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAction : MonoBehaviour
{
    // ������ �ε��� ����� �����Ѵ�.
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
                // �浹�� �Ѿ��� źâ ��Ȱ��ȭ ����Ʈ�� �ٽ� �߰��Ѵ�.
                pFire.magazine.Add(collision.gameObject);
            }
            // �浹�� ����� �����Ѵ�.
            else
            {
                Destroy(collision.gameObject);
            }
        }
    }
}
