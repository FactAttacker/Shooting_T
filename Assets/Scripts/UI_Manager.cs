using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    // �ɼ� �г��� ������ �ٽ� ������ �簳�ϴ� �Լ�
    public void Resume()
    {
        GameManager.gm.SetActiveOption(false);
    }

    // ������ �ٽ� �����ϵ��� �ϴ� �Լ�
    public void Restart()
    {
        GameManager.gm.CheckScore();

        //SceneManager.LoadScene(0);
    }

    // ���� �����ϴ� �Լ�
    public void Quit()
    {
        Application.Quit();
    }

    // �̸��� �����ϰ� ���� ������ϴ� �Լ�
    public void ConfirmSaving()
    {
        // �̸� �Է� �ʵ��� ������Ʈ�� �����´�.
        InputField bestPlayer = GameManager.gm.recordPanel.GetComponentInChildren<InputField>();
        
        // ����, �Է� �ʵ忡 �� ���� �̻��� �����ִٸ�...
        if (bestPlayer.text.Length >= 2)
        {
            // �Է� �ʵ��� ���� ���� ��ġ�� �����Ѵ�.
            PlayerPrefs.SetString("playerName", bestPlayer.text);

            // ���� ������Ѵ�.
            SceneManager.LoadScene("ShootingMainScene");
        }
    }

}
