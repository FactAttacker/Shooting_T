using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    // 1. 밑으로 내려간다. 그저 한없이...
    public float moveSpeed = 6.0f;

    public int probability = 70;

    // 2. 플레이어가 있는 위치를 향해서 이동한다.
    // 2-1. 플레이어의 위치를 찾는다.
    GameObject player;

    // 0번이면 내려가기, 1번이면 쫓아가기
    int selection = 0;

    Vector3 dir;

    void Start()
    {
        player = GameObject.Find("Player");

        // 확률에 따라 1번 또는 2번의 이동 방식을 사용한다.
        int draw = Random.Range(1, 101);

        if(draw < probability)
        {
            dir = Vector3.down;
        }
        else
        {
            // 2-2. 방향 설정: target(player.transform.position) - me(transform.position)
            if (player != null)
            {
                dir = player.transform.position - transform.position;
                dir.Normalize();
            }
        }
       
    }

    void Update()
    {
        transform.position += dir * moveSpeed * Time.deltaTime;
    }

    // 충돌 처리 콜백 함수
    private void OnTriggerEnter(Collider col)
    {
        // 부딪힌 대상이 Enemy라면 상대방을 제거하고, 나도 제거한다.
        if (col.gameObject.name == "Player")
        {
            Destroy(col.gameObject);
            // 옵션 창을 활성화한다.
            GameManager.gm.SetActiveOption(true);
            // 현재 점수를 저장한다.
            GameManager.gm.SaveScore();
            Destroy(gameObject);
        }
    }
}
