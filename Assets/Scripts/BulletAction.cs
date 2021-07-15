using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class BulletAction : MonoBehaviour
{
    // 나는 생성되면 그냥 위쪽으로 갈란다.. 한없이...
    public float moveSpeed = 10.0f;
    
    public GameObject explosionPrefab;
    
    public enum BulletType
    {
        UserBullet,
        BossMissile
    }

    // 총알의 타입 변수
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

    // 충돌 처리 콜백 함수
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
        // 부딪힌 대상이 Enemy라면 상대방을 제거하고, 나도 제거한다.
        if (col.gameObject.tag == "Enemy")
        {
            // 폭발 이펙트 오브젝트를 생성한 뒤 이펙트를 실행한다.
            GameObject go = Instantiate(explosionPrefab, col.transform.position, Quaternion.identity);
            ParticleSystem ps = go.GetComponentInChildren<ParticleSystem>();
            ps.Play();

            //Collider[] cols = Physics.OverlapSphere(transform.position, 6.0f, 1 << 8);

            // 방식 1
            //for(int i = 0; i < cols.Length; i++)
            //{
            //    Destroy(cols[i].gameObject);
            //}

            // 방식 2
            //foreach(Collider enemy in cols)
            //{
            //    Destroy(enemy.gameObject);
            //}

            // 방식 3
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
            // 부딪힌 순간에 플레이어의 좌표를 저장한다.
            //Vector3 playerPos = col.gameObject.transform.position;
            //print(playerPos);

            GameManager.gm.userLifeCount--;
            Destroy(gameObject);
        }
    }


}
