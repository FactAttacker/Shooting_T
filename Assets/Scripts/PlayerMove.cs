using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // ������� �Է��� �޾Ƽ� ���� �Ǵ� �¿�� �̵��� �ϰ� �ʹ�.
    // ����� �Է�: �¿� ȭ��ǥ Ű, ���� ȭ��ǥ Ű, WASD Ű
    // �̵� �ʿ� ���: �ӵ�(����, �ӷ�) �ð�

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
        //    print("���� �ϰ� ���� ȭ��ǥ Ű�� �������� �˰��ִ�...");
        //}

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(h, v, 0);
        direction.Normalize();

        // direction�� ������ ������ �̵��ϰ� �ʹ�.
        // p = p0 + vt

        //finalSpeed = moveSpeed;

        // ���� Shift Ű�� ���� ������ ������ �ӵ��� 2��� �����Ѵ�.
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            finalSpeed = moveSpeed * 2;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            finalSpeed = moveSpeed;
        }
        // ���� Shitf Ű���� ���� ���� �ٽ� ���� �ӵ���� ���ƿ´�.
        //transform.position += direction * finalSpeed * Time.deltaTime;

        // �̹� �����ӿ� ������ ������ ��ǥ�� ���Ѵ�.
        Vector3 arrivePos = direction * finalSpeed * Time.deltaTime;

        // ���� ��ġ���� ���� �������� ���̸� �߻��غ���.
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

        // �÷��̾��� ���� ��ǥ�� ����Ʈ ��ǥ�� ȯ���Ѵ�.
        //Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
        //print(viewPos);

        // ȯ��� ����Ʈ ��ǥ�� 0 ~ 1������ ������ �����Ѵ�.
        //viewPos.x = Mathf.Clamp01(viewPos.x);
        //viewPos.y = Mathf.Clamp01(viewPos.y);

        // ���� ����� ����Ʈ ��ǥ�� ���� ��ǥ�� ��ȯ�ؼ� �÷��̾� ���������� �����.
        //transform.position = Camera.main.ViewportToWorldPoint(viewPos);
    }
}
