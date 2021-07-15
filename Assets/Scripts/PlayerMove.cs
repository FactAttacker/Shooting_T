using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // 사용자의 입력을 받아서 상하 또는 좌우로 이동을 하고 싶다.
    // 사용자 입력: 좌우 화살표 키, 상하 화살표 키, WASD 키
    // 이동 필요 요소: 속도(방향, 속력) 시간

    public float moveSpeed = 0.1f;

    float finalSpeed = 0;
    Rigidbody rb;

    void Start()
    {
        finalSpeed = moveSpeed;
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        //if(Input.GetKeyDown(KeyCode.UpArrow))
        //{
        //    print("나는 니가 윗쪽 화살표 키를 누른것을 알고있다...");
        //}

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(h, v, 0);
        direction.Normalize();

        // direction에 정해진 방향대로 이동하고 싶다.
        // p = p0 + vt

        //finalSpeed = moveSpeed;

        // 좌측 Shift 키를 누른 상태일 때에는 속도가 2배로 증가한다.
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            finalSpeed = moveSpeed * 2;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            finalSpeed = moveSpeed;
        }
        // 좌측 Shitf 키에서 손을 떼면 다시 원래 속도대로 돌아온다.
        //transform.position += direction * finalSpeed * Time.deltaTime;

        // 이번 프레임에 도착할 지점의 좌표를 구한다.
        Vector3 arrivePos = direction * finalSpeed * Time.deltaTime;

        // 현재 위치에서 도착 지점까지 레이를 발사해본다.
        Ray ray = new Ray(transform.position, direction);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, arrivePos.magnitude))
        {
            transform.position = hitInfo.point;
            transform.position -= direction * 0.5f;
        }
        else
        {
            rb.velocity = direction * finalSpeed;
        }

        // 플레이어의 월드 좌표를 뷰포트 좌표로 환산한다.
        //Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
        //print(viewPos);

        // 환산된 뷰포트 좌표를 0 ~ 1사이의 값으로 제한한다.
        //viewPos.x = Mathf.Clamp01(viewPos.x);
        //viewPos.y = Mathf.Clamp01(viewPos.y);

        // 제한 적용된 뷰포트 좌표를 월드 좌표로 변환해서 플레이어 포지션으로 덮어쓴다.
        //transform.position = Camera.main.ViewportToWorldPoint(viewPos);
    }
}
