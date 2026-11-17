using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int steps; //剩餘使用步數
    public TextMeshProUGUI stepsText; //顯示剩餘步數的文字
    public bool Win;
    public int ThisPoint; //本局分數
    public TextMeshProUGUI ThisPointText; //顯示本局分數的文字
    public int point; //分數
    public TextMeshProUGUI pointText; //顯示分數的文字
    public FlipSquare flipSquare;
    public float ghostTime = 60f;
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
        Time.timeScale = 1f; //恢復遊戲
        steps = 20;
        stepsText.text = steps.ToString();
        ghostTime = 60f;
        isGhostTime = false;
        flipSquare = FindFirstObjectByType<FlipSquare>();
        flipSquare.ReClean();
    }

    public void countPoint()
    {
        if (Win)
        {
            ThisPoint = 100 + (steps * 2);
        }
        else
        {
            ThisPoint = 0;
        }
        ThisPointText.text = ThisPoint.ToString();
    }

    public void AddPoint()
    {
        point = point + ThisPoint;
        pointText.text = point.ToString();
    }

    public void endGame()
    {
        Debug.Log("Game Over");
        countPoint();
        endGamePanel.SetActive(true);
        Time.timeScale = 0f; //暫停遊戲
    }

    public void NextRound()
    {
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
