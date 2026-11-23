using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    public int TimeLevel = 1; //時間等級
    public float ghostTime = 40f;
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
        if (ghostTime > 0)
        {
            ghostTime -= Time.deltaTime;
            TimeText.text = Mathf.CeilToInt(ghostTime).ToString();
        }
        else
        {
            if (!isGhostTime)
            {
                TimeText.text = "0";
                isGhostTime = true;
                flipSquare = FindFirstObjectByType<FlipSquare>();
                flipSquare.GhostTime();
            }
        }
    }

    public void NewRound()
    {
        endGamePanel.SetActive(false);
        AddPoint();
        Time.timeScale = 1f; //恢復遊戲
        steps = 20;
        stepsText.text = steps.ToString();
        if (TimeLevel == 1)
        {
            ghostTime = 40f;
        }
        else if (TimeLevel == 2)
        {
            ghostTime = 30f;
        }
        else if (TimeLevel == 3)
        {
            ghostTime = 20f;
        }
        else if(TimeLevel == 4)
        {
            ghostTime = 10f;
        }
        isGhostTime = false;
        if (TimeLevel < 4)
        {
            TimeLevel++;
        }
        flipSquare = FindFirstObjectByType<FlipSquare>();
        flipSquare.ReClean();
    }


    public void AddPoint()
    {
        if (Win)
        {
            point = point + 100 + (steps * 2);
        }
        else
        {
            point =  0;
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

    public void NextRound()
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
        if(ghostTime > 0)
        {
            ghostTime = ghostTime + 5f;
        }
    }
}
