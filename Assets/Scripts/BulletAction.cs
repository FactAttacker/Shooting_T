using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class BulletAction : MonoBehaviour
{
    // ���� �����Ǹ� �׳� �������� ������.. �Ѿ���...
    public float moveSpeed = 10.0f;
    
    public GameObject explosionPrefab;
    
    public enum BulletType
    {
        UserBullet,
        BossMissile
    }

    // �Ѿ��� Ÿ�� ����
    public BulletType bulletType = BulletType.UserBullet;

    Vector3 dir;

    void Start()
    {
        if(bulletType == BulletType.UserBullet)
        {
            dir = Vector3.up;
        }
        else if(bulletType == BulletType.BossMissile)
        {
            dir = transform.up;
        }
    }

    void Update()
    {
        transform.position += dir * moveSpeed * Time.deltaTime;
    }

    // �浹 ó�� �ݹ� �Լ�
    private void OnTriggerEnter(Collider col)
    {
        if(bulletType == BulletType.UserBullet)
        {
            PlayerCollidingProcess(col);
        }
        else if(bulletType == BulletType.BossMissile)
        {
            BossCollidingProcess(col);
        }
    }

    void PlayerCollidingProcess(Collider col)
    {
        // �ε��� ����� Enemy��� ������ �����ϰ�, ���� �����Ѵ�.
        if (col.gameObject.tag == "Enemy")
        {
            // ���� ����Ʈ ������Ʈ�� ������ �� ����Ʈ�� �����Ѵ�.
            GameObject go = Instantiate(explosionPrefab, col.transform.position, Quaternion.identity);
            ParticleSystem ps = go.GetComponentInChildren<ParticleSystem>();
            ps.Play();

            //Collider[] cols = Physics.OverlapSphere(transform.position, 6.0f, 1 << 8);

            // ��� 1
            //for(int i = 0; i < cols.Length; i++)
            //{
            //    Destroy(cols[i].gameObject);
            //}

            // ��� 2
            //foreach(Collider enemy in cols)
            //{
            //    Destroy(enemy.gameObject);
            //}

            // ��� 3
            //int i = 0;
            //while(i < cols.Length)
            //{
            //    Destroy(cols[i].gameObject);
            //    i++;
            //}

            Destroy(col.gameObject);
            GameManager.gm.AddScore(15);

            Destroy(gameObject);
        }
    }

    void BossCollidingProcess(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            // �ε��� ������ �÷��̾��� ��ǥ�� �����Ѵ�.
            //Vector3 playerPos = col.gameObject.transform.position;
            //print(playerPos);

            GameManager.gm.userLifeCount--;
            Destroy(gameObject);
        }
    }


}
