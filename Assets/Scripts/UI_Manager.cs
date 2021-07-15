using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    // 옵션 패널이 닫히고 다시 게임을 재개하는 함수
    public void Resume()
    {
        GameManager.gm.SetActiveOption(false);
    }

    // 게임을 다시 시작하도록 하는 함수
    public void Restart()
    {
        GameManager.gm.CheckScore();

        //SceneManager.LoadScene(0);
    }

    // 앱을 종료하는 함수
    public void Quit()
    {
        Application.Quit();
    }

    // 이름을 저장하고 씬을 재시작하는 함수
    public void ConfirmSaving()
    {
        // 이름 입력 필드의 컴포넌트를 가져온다.
        InputField bestPlayer = GameManager.gm.recordPanel.GetComponentInChildren<InputField>();
        
        // 만일, 입력 필드에 두 글자 이상이 적혀있다면...
        if (bestPlayer.text.Length >= 2)
        {
            // 입력 필드의 값을 저장 장치에 저장한다.
            PlayerPrefs.SetString("playerName", bestPlayer.text);

            // 씬을 재시작한다.
            SceneManager.LoadScene("ShootingMainScene");
        }
    }

}
