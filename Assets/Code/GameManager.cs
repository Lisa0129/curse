using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public int steps; //剩餘使用步數
    public TextMeshProUGUI stepsText; //顯示剩餘步數的文字
    public bool Win = false; //是否勝利
    //public int ThisPoint; //本局分數
    public TextMeshProUGUI ThisPointText; //顯示本局分數的文字
    public int point; //分數
    public TextMeshProUGUI pointText; //顯示分數的文字
    public FlipSquare flipSquare;
    public bool TimeStop; //是否暫停時間
    public int TimeLevel = 1; //時間等級
    public float ghostTime = 10f;
    public TextMeshProUGUI TimeText; //顯示剩餘時間的文字
    public bool isGhostTime = false; //是否進入怨靈時間
    public GameObject endGamePanel; //遊戲結束面板
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        NewRound();
    }

    // Update is called once per frame
    void Update()
    {
        if (ghostTime > 0 && !TimeStop)
        {
            ghostTime -= Time.deltaTime;
            TimeText.text = Mathf.CeilToInt(ghostTime).ToString();
        }
        else
        {
            if (!isGhostTime && !TimeStop)
            {
                TimeText.text = "0";
                isGhostTime = true;
                flipSquare = FindFirstObjectByType<FlipSquare>();
                flipSquare.GhostTime();
            }
        }
    }

    IEnumerator BeforeNew()
    {
        yield return new WaitForSeconds(2f);
        flipSquare = FindFirstObjectByType<FlipSquare>();
        flipSquare.ReClean();
        TimeStop = false;
        
    }

    public void NewRound() //刷新
    {
        Time.timeScale = 1f; //恢復遊戲
        TimeStop = true;
        endGamePanel.SetActive(false);
        flipSquare = FindFirstObjectByType<FlipSquare>();
        flipSquare.BeforeClean();
        StartCoroutine(BeforeNew());
        AddPoint();
        steps = 20;
        stepsText.text = steps.ToString();
        if (TimeLevel == 1)
        {
            ghostTime = 10f;
        }
        else if (TimeLevel == 2)
        {
            ghostTime = 9f;
        }
        else if (TimeLevel == 3)
        {
            ghostTime = 8f;
        }
        else if (TimeLevel == 4)
        {
            ghostTime = 7f;
        }
        else if (TimeLevel == 5)
        {
            ghostTime = 6f;
        }
        else if (TimeLevel == 6)
        {
            ghostTime = 5f;
        }
        isGhostTime = false;
        if (TimeLevel < 6)
        {
            TimeLevel++;
        }
        
    }


    public void AddPoint()
    {
        if (Win)
        {
            point = point + 100 + (steps * 2);
        }
        else
        {
            point = 0;
        }
        pointText.text = point.ToString();
    }

    public void endGame()
    {
        Debug.Log("Game Over");
        ThisPointText.text = point.ToString();
        endGamePanel.SetActive(true);
        Time.timeScale = 0f; //暫停遊戲
    }

    public void NextRound()//下一局
    {
        TimeLevel = 1; // 重置時間等級
        AddPoint();
        NewRound();
    }



    public void move()
    {
        steps--;
        stepsText.text = steps.ToString();
        if (steps <= 0)
        {
            Win = false;
            endGame();
        }
    }

    public void Tool3()
    {
        if (ghostTime > 0)
        {
            ghostTime = ghostTime + 5f;
        }
    }
}
