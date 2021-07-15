using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    // ����ڰ� ���콺 ���� ��ư�� Ŭ���ϸ� �Ѿ��� �����ϰ� �ʹ�.
    public GameObject bulletFactory;

    public Transform firePosition;

    [Range(1, 5)]
    public int bulletCount = 1;

    public float between = 8.0f;

    // �Ѿ��� ���� źâ ����
    //public GameObject[] magazine = new GameObject[20];
    public List<GameObject> magazine = new List<GameObject>();
    public int magazineSize = 20;

    public GameObject wareHouse;

    AudioSource audioSource;

    void Start()
    {
        // źâ�� �Ѿ��� ���� ä���!
        for(int i = 0; i < magazineSize; i++)
        {
            GameObject go = Instantiate(bulletFactory);
            //magazine[i] = go;
            magazine.Add(go);
            // ������ �Ѿ˵��� ��Ȱ��ȭ�Ѵ�.
            go.SetActive(false);
            // ��Ȱ��ȭ�� �Ѿ˵��� â���� �ڽ� ������Ʈ�� ����Ѵ�.
            go.transform.parent = wareHouse.transform;
        }

        // ����� �ҽ� ������Ʈ�� �����´�.
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        //if(Input.GetMouseButtonDown(0))
        if(Input.GetButtonDown("Fire1"))
        {
            FireNormalType();

            //FireSpecialType1();
            //FireSpecialType2();

        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            bulletCount = Mathf.Max(bulletCount-1, 1);
        }
        else if(Input.GetKeyDown(KeyCode.E))
        {
            bulletCount = Mathf.Min(bulletCount + 1, 5);
        }
    }

    void FireNormalType()
    {
        if (bulletFactory != null)
        {
            // ��Ȱ��ȭ�� �Ѿ��� ������ �˻��Ѵ�.
            //for(int i = 0; i < magazine.Length; i++)
            //{
            //    // ���� ��Ȱ��ȭ �Ǿ� �ִٸ�...
            //    if(!magazine[i].activeInHierarchy)
            //    {
            //        magazine[i].SetActive(true);
            //        magazine[i].transform.position = firePosition.position;
            //        magazine[i].transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            //        break;
            //    }
            //}

            // ���� ù��° �Ѿ��� Ȱ��ȭ�Ѵ�.
            if(magazine.Count > 0)
            {
                magazine[0].SetActive(true);
                magazine[0].transform.position = firePosition.position;
                // Ȱ��ȭ�� �Ѿ��� ����Ʈ�κ��� �����Ѵ�.
                magazine.RemoveAt(0);
            }

            // ����� �ҽ��� �÷����Ѵ�.
            audioSource.Play();
        }
    }

    void FireSpecialType1()
    {
        // 1. ��Ŀ�� �ִ� ������ ����(��ü ���� ���� * 0.5)���� ��´�.
        Vector3 anchor = new Vector3(between * -0.5f, 1.2f, 0);

        // 2. ��Ŀ ��ġ�κ��� �̵� ���� = ��ü ���� / (�Ѿ� ���� + 1)
        float term = between / (float)(bulletCount + 1);

        // 3. �Ѿ��� ������ŭ �ݺ��ؼ� ��Ŀ ��ġ���� �̵� ���ݸ�ŭ ������ ���� �Ѿ��� �����Ѵ�.
        for(int i = 0; i < bulletCount; i++)
        {
            GameObject go = Instantiate(bulletFactory);
            go.transform.position = transform.position + anchor + new Vector3(term * (i + 1), 0, 0);
        }

    }

    void FireSpecialType2()
    {
        // 1. ��ü ������ ���̸� ���Ѵ�.
        float range = between * (bulletCount - 1);

        // 2. ��Ŀ�� �ִ� ������ �������� ��´�.
        Vector3 anchor = new Vector3(range * -0.5f, 1.2f, 0);

        // 3. �Ѿ��� ������ŭ �ݺ��ؼ� ��Ŀ ��ġ���� �̵� ���ݸ�ŭ ������ ���� �Ѿ��� �����Ѵ�.
        for(int i = 0; i < bulletCount; i++)
        {
            GameObject go = Instantiate(bulletFactory);
            go.transform.position = transform.position + anchor + new Vector3(between * i, 0, 0);
        }
    }
}