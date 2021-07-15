using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    // 1. ������ ��������. ���� �Ѿ���...
    public float moveSpeed = 6.0f;

    public int probability = 70;

    // 2. �÷��̾ �ִ� ��ġ�� ���ؼ� �̵��Ѵ�.
    // 2-1. �÷��̾��� ��ġ�� ã�´�.
    GameObject player;

    // 0���̸� ��������, 1���̸� �Ѿư���
    int selection = 0;

    Vector3 dir;

    void Start()
    {
        player = GameObject.Find("Player");

        // Ȯ���� ���� 1�� �Ǵ� 2���� �̵� ����� ����Ѵ�.
        int draw = Random.Range(1, 101);

        if(draw < probability)
        {
            dir = Vector3.down;
        }
        else
        {
            // 2-2. ���� ����: target(player.transform.position) - me(transform.position)
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

    // �浹 ó�� �ݹ� �Լ�
    private void OnTriggerEnter(Collider col)
    {
        // �ε��� ����� Enemy��� ������ �����ϰ�, ���� �����Ѵ�.
        if (col.gameObject.name == "Player")
        {
            Destroy(col.gameObject);
            // �ɼ� â�� Ȱ��ȭ�Ѵ�.
            GameManager.gm.SetActiveOption(true);
            // ���� ������ �����Ѵ�.
            GameManager.gm.SaveScore();
            Destroy(gameObject);
        }
    }
}
