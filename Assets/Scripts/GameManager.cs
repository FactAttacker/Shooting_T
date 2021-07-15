using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // single tone pattern...
    public static GameManager gm;

    public GameObject canvasUI;

    public Text[] scoreText = new Text[2];

    public GameObject recordPanel;
    public int userLifeCount = 3;

    int currentScore = 0;
    int bestScore = 0;

    bool fadeStart = false;

    public Image backImage;
    public Text label;

    float currentTime = 0;

    Color startImageColor;
    Color startlabelColor;
    int startFontSize;
    GameObject player;

    private void Awake()
    {
        if(gm == null)
        {
            gm = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        startImageColor = backImage.color;
        startlabelColor = label.color;
        startFontSize = label.fontSize;

        SetActiveOption(false);
        LoadScore();

        Time.timeScale = 1;
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SetActiveOption(true);
        }

        // 현재 점수를 UI에 반영한다.
        scoreText[0].text = "현재 점수: " + currentScore.ToString();

        // 페이드 이펙트를 실행한다.
        if (fadeStart)
        {
            FadeEffect();
        }

        if(userLifeCount < 1)
        {
            Destroy(player);
        }
    }

    // 옵션 창을 생성하는 함수
    public void SetActiveOption(bool toggle)
    {
        // UI 창을 활성화한다.
        canvasUI.SetActive(toggle);

        fadeStart = toggle;

        // 오브젝트의 시간의 흐름을 멈춘다.
        //if (toggle)
        //{
        //    Time.timeScale = 0;
        //}
        //else
        //{
        //    Time.timeScale = 1;
        //}
    }

    // 최고 스코어를 저장 장치에 저장한다.
    public void SaveScore()
    {
        // 만일, 현재 점수가 최고 점수를 넘어갔다면...
        if (currentScore > bestScore)
        {
            PlayerPrefs.SetInt("score", currentScore);
            print("저장 완료됐습니다.");
        }
    }

    // 저장된 점수를 최고 점수로 출력한다.
    void LoadScore()
    {
        bestScore = PlayerPrefs.GetInt("score");
        string bestName = PlayerPrefs.GetString("playerName");

        scoreText[1].text = "최고 점수: " + bestScore.ToString() + " - " + bestName;
    }


    void FadeEffect()
    {
        // 값을 최초 저장한 값에서 목표한 값으로 시간에 흐름에 따라 변화시키고 싶다.
        // Lerp 함수
        if (currentTime <= 1.0)
        {
            currentTime += Time.deltaTime * 0.5f;

            //float alpha = Mathf.Lerp(startImageColor.a, 0.8f, currentTime);
            //backImage.color = new Color(startImageColor.r, startImageColor.g, startImageColor.b, alpha);
            backImage.color = Color.Lerp(startImageColor, new Color(startImageColor.r, startImageColor.g, startImageColor.b, 0.8f), currentTime);

            //float alpha2 = Mathf.Lerp(startlabelColor.a, 1.0f, currentTime);
            //label.color = startlabelColor + new Color(0, 0, 0, alpha2);
            label.color = Color.Lerp(startlabelColor, startlabelColor + new Color(0, 0, 0, 1.0f), currentTime);

            int size = (int)Mathf.Lerp((float)startFontSize, 90.0f, currentTime);
            label.fontSize = size;
        }
        else
        {
            Time.timeScale = 0;
            fadeStart = false;
        }
    }

    // 점수를 획득하는 함수
    public void AddScore(int point)
    {
        currentScore += point;
    }

    // 최고 점수를 갱신했을 때 사용자의 이름을 입력받는 UI를 표시한다.
    public void CheckScore()
    {
        if (currentScore > bestScore)
        {
            // 옵션 패널을 비활성화한다.
            canvasUI.SetActive(false);

            // 입력 필드를 활성화한다.
            recordPanel.SetActive(true);

            // 점수 텍스트를 현재 스코어로 표시한다.
            Text recordScore = recordPanel.transform.GetChild(1).GetComponent<Text>();
            recordScore.text = currentScore.ToString();
        }
        else
        {
            SceneManager.LoadScene("ShootingMainScene");
        }
    }
}
