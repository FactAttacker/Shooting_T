using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    // 사용자가 마우스 왼쪽 버튼을 클릭하면 총알을 생성하고 싶다.
    public GameObject bulletFactory;

    public Transform firePosition;

    [Range(1, 5)]
    public int bulletCount = 1;

    public float between = 8.0f;

    // 총알을 담을 탄창 변수
    //public GameObject[] magazine = new GameObject[20];
    public List<GameObject> magazine = new List<GameObject>();
    public int magazineSize = 20;

    public GameObject wareHouse;

    AudioSource audioSource;

    void Start()
    {
        // 탄창에 총알을 가득 채운다!
        for(int i = 0; i < magazineSize; i++)
        {
            GameObject go = Instantiate(bulletFactory);
            //magazine[i] = go;
            magazine.Add(go);
            // 생성된 총알들을 비활성화한다.
            go.SetActive(false);
            // 비활성화된 총알들을 창고의 자식 오브젝트로 등록한다.
            go.transform.parent = wareHouse.transform;
        }

        // 오디오 소스 컴포넌트를 가져온다.
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
            // 비활성화된 총알이 누군지 검색한다.
            //for(int i = 0; i < magazine.Length; i++)
            //{
            //    // 만일 비활성화 되어 있다면...
            //    if(!magazine[i].activeInHierarchy)
            //    {
            //        magazine[i].SetActive(true);
            //        magazine[i].transform.position = firePosition.position;
            //        magazine[i].transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            //        break;
            //    }
            //}

            // 가장 첫번째 총알을 활성화한다.
            if(magazine.Count > 0)
            {
                magazine[0].SetActive(true);
                magazine[0].transform.position = firePosition.position;
                // 활성화된 총알은 리스트로부터 제거한다.
                magazine.RemoveAt(0);
            }

            // 오디오 소스를 플레이한다.
            audioSource.Play();
        }
    }

    void FireSpecialType1()
    {
        // 1. 앵커를 최대 범위의 좌측(전체 범위 길이 * 0.5)으로 잡는다.
        Vector3 anchor = new Vector3(between * -0.5f, 1.2f, 0);

        // 2. 앵커 위치로부터 이동 간격 = 전체 범위 / (총알 갯수 + 1)
        float term = between / (float)(bulletCount + 1);

        // 3. 총알의 갯수만큼 반복해서 앵커 위치에서 이동 간격만큼 떨어진 곳에 총알을 생성한다.
        for(int i = 0; i < bulletCount; i++)
        {
            GameObject go = Instantiate(bulletFactory);
            go.transform.position = transform.position + anchor + new Vector3(term * (i + 1), 0, 0);
        }

    }

    void FireSpecialType2()
    {
        // 1. 전체 범위의 길이를 정한다.
        float range = between * (bulletCount - 1);

        // 2. 앵커를 최대 범위의 좌측으로 잡는다.
        Vector3 anchor = new Vector3(range * -0.5f, 1.2f, 0);

        // 3. 총알의 개수만큼 반복해서 앵커 위치에서 이동 간격만큼 떨어진 곳에 총알을 생성한다.
        for(int i = 0; i < bulletCount; i++)
        {
            GameObject go = Instantiate(bulletFactory);
            go.transform.position = transform.position + anchor + new Vector3(between * i, 0, 0);
        }
    }
}
