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

        // ���� ������ UI�� �ݿ��Ѵ�.
        scoreText[0].text = "���� ����: " + currentScore.ToString();

        // ���̵� ����Ʈ�� �����Ѵ�.
        if (fadeStart)
        {
            FadeEffect();
        }

        if(userLifeCount < 1)
        {
            Destroy(player);
        }
    }

    // �ɼ� â�� �����ϴ� �Լ�
    public void SetActiveOption(bool toggle)
    {
        // UI â�� Ȱ��ȭ�Ѵ�.
        canvasUI.SetActive(toggle);

        fadeStart = toggle;

        // ������Ʈ�� �ð��� �帧�� �����.
        //if (toggle)
        //{
        //    Time.timeScale = 0;
        //}
        //else
        //{
        //    Time.timeScale = 1;
        //}
    }

    // �ְ� ���ھ ���� ��ġ�� �����Ѵ�.
    public void SaveScore()
    {
        // ����, ���� ������ �ְ� ������ �Ѿ�ٸ�...
        if (currentScore > bestScore)
        {
            PlayerPrefs.SetInt("score", currentScore);
            print("���� �Ϸ�ƽ��ϴ�.");
        }
    }

    // ����� ������ �ְ� ������ ����Ѵ�.
    void LoadScore()
    {
        bestScore = PlayerPrefs.GetInt("score");
        string bestName = PlayerPrefs.GetString("playerName");

        scoreText[1].text = "�ְ� ����: " + bestScore.ToString() + " - " + bestName;
    }


    void FadeEffect()
    {
        // ���� ���� ������ ������ ��ǥ�� ������ �ð��� �帧�� ���� ��ȭ��Ű�� �ʹ�.
        // Lerp �Լ�
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

    // ������ ȹ���ϴ� �Լ�
    public void AddScore(int point)
    {
        currentScore += point;
    }

    // �ְ� ������ �������� �� ������� �̸��� �Է¹޴� UI�� ǥ���Ѵ�.
    public void CheckScore()
    {
        if (currentScore > bestScore)
        {
            // �ɼ� �г��� ��Ȱ��ȭ�Ѵ�.
            canvasUI.SetActive(false);

            // �Է� �ʵ带 Ȱ��ȭ�Ѵ�.
            recordPanel.SetActive(true);

            // ���� �ؽ�Ʈ�� ���� ���ھ�� ǥ���Ѵ�.
            Text recordScore = recordPanel.transform.GetChild(1).GetComponent<Text>();
            recordScore.text = currentScore.ToString();
        }
        else
        {
            SceneManager.LoadScene("ShootingMainScene");
        }
    }
}
