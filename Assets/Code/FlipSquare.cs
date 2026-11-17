using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;

public class FlipSquare : MonoBehaviour
{
    public GameManager gameManager;
    public ClickSquare clickSquare;
    public Button[] SquareButton; //方塊按鈕
    public GameObject[] upSprite; //凸起的方塊圖片
    public GameObject[] downSprite; //凹下的方塊圖片
    public Image[] UPGoalImage; //凸起時目標方塊圖片
    public Image[] DOWNGoalImage; //凹下時目標方塊圖片
    public Sprite upsunGoalSprite; //凸起的太陽目標方塊圖片
    public Sprite downsunGoalSprite; //凹下的太陽目標方塊圖片
    public Sprite upmoonGoalSprite; //凸起的月亮目標方塊圖片
    public Sprite downmoonGoalSprite; //凹下的月亮目標方塊圖片
    public int[] SquareState; //方塊狀態，0為凸起，1為凹下
    public int Numberclicksquare; //點擊方塊編號
    public int FlipSquareNum; //翻轉方塊編號
    public int[] Goal; //所有方塊的目標狀態，0為凸起，1為凹下，2為不變
    public int Goalamount; //目標方塊數量
    public bool Tool1use = false; //是否使用道具1

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    public void ReClean()//重新清理方塊狀態
    {
        Goalamount = 10;
        for (int i = 0; i < SquareButton.Length; i++)//設定凹凸
        {
            // 隨機決定狀態（0 或 1）
            SquareState[i] = Random.Range(0, 2);

            // 根據狀態設定圖片
            if (SquareState[i] == 0)
            {
                upSprite[i].SetActive(true);
                downSprite[i].SetActive(false);
            }
            else
            {
                downSprite[i].SetActive(true);
                upSprite[i].SetActive(false);
            }
        }
        // 先全部設為不變 (2)
        for (int i = 0; i < Goal.Length; i++)//設定目標
        {
            Goal[i] = 2;
            UPGoalImage[i].gameObject.SetActive(false);
            DOWNGoalImage[i].gameObject.SetActive(false);
        }
        int[] result = Enumerable.Range(0, 25)   // 0~24
                                 .OrderBy(x => Random.value) // 打亂
                                 .Take(Goalamount)      // 取前 10 個
                                 .ToArray();

        // 前 5 個設為凸起目標 (0)，後 5 個設為凹下目標 (1)
        for (int i = 0; i < 5; i++)
        {
            int idx = result[i];
            Goal[idx] = 0;
            UPGoalImage[idx].sprite = upsunGoalSprite;
            DOWNGoalImage[idx].sprite = downsunGoalSprite;
            if (SquareState[idx] == 0)
            {
                UPGoalImage[idx].gameObject.SetActive(true);
            }
            else
            {
                DOWNGoalImage[idx].gameObject.SetActive(true);
            }
        }

        for (int i = 5; i < 10; i++)
        {
            int idx = result[i];
            Goal[idx] = 1;
            UPGoalImage[idx].sprite = upmoonGoalSprite;
            DOWNGoalImage[idx].sprite = downmoonGoalSprite;
            if (SquareState[idx] == 0)
            {
                UPGoalImage[idx].gameObject.SetActive(true);
            }
            else
            {
                DOWNGoalImage[idx].gameObject.SetActive(true);
            }
        }

        // Debug 顯示結果
        foreach (int num in result)
        {
            Debug.Log("抽到目標方塊：" + num + " 狀態=" + Goal[num]);
        }
    }

    void beUP() //方塊變凸起
    {
        downSprite[FlipSquareNum].SetActive(false);
        upSprite[FlipSquareNum].SetActive(true);
        SquareState[FlipSquareNum] = 0;
        if (Goal[FlipSquareNum] < 2)
        {
            UPGoalImage[FlipSquareNum].gameObject.SetActive(true);
            DOWNGoalImage[FlipSquareNum].gameObject.SetActive(false);
        }
    }

    void beDown() //方塊變凹下
    {
        downSprite[FlipSquareNum].SetActive(true);
        upSprite[FlipSquareNum].SetActive(false);
        SquareState[FlipSquareNum] = 1;
        if (Goal[FlipSquareNum] < 2)
        {
            UPGoalImage[FlipSquareNum].gameObject.SetActive(false);
            DOWNGoalImage[FlipSquareNum].gameObject.SetActive(true);
        }
    }

    void UPorDown() //判斷方塊變換狀態
    {
        if (SquareState[FlipSquareNum] == 0)
        {
            beDown();
        }
        else
        {
            beUP();
        }
    }

    public void Tool1()
    {
        Tool1use = true;
    }

    public void Tool2()
    {
        List<int> wrongSquares = new List<int>();

        // 找出所有錯誤的目標方塊
        for (int i = 0; i < Goal.Length; i++)
        {
            if (Goal[i] != 2 && SquareState[i] != Goal[i])
            {
                wrongSquares.Add(i);
            }
        }

        // 沒有錯誤方塊
        if (wrongSquares.Count == 0)
        {
            Debug.Log("沒有需要修正的方塊");
            return;
        }

        // 打亂順序
        wrongSquares = wrongSquares
            .OrderBy(x => Random.value)
            .ToList();

        // 最多修正3個
        int fixCount = Mathf.Min(3, wrongSquares.Count);

        for (int i = 0; i < fixCount; i++)
        {
            int index = wrongSquares[i];

            FlipSquareNum = index;
            UPorDown();
            

            Debug.Log($"道具修正方塊：{index}");
        }

        CheckGoal();
    }

    public void Flip() //翻轉方塊
    {
        clickSquare = FindFirstObjectByType<ClickSquare>();
        Numberclicksquare = clickSquare.clicksquareNumber;
        if (Tool1use)
        {
            Tool1use = false;
            FlipSquareNum = Numberclicksquare;
            UPorDown();
        }
        else
        {
            if (Numberclicksquare < 5)
            {
                if (Numberclicksquare == 0)
                {
                    FlipSquareNum = Numberclicksquare;
                    UPorDown();
                    FlipSquareNum = Numberclicksquare + 1;
                    UPorDown();
                    FlipSquareNum = Numberclicksquare + 5;
                    UPorDown();
                }
                else if (Numberclicksquare > 0 && Numberclicksquare < 4)
                {
                    FlipSquareNum = Numberclicksquare;
                    UPorDown();
                    FlipSquareNum = Numberclicksquare - 1;
                    UPorDown();
                    FlipSquareNum = Numberclicksquare + 1;
                    UPorDown();
                    FlipSquareNum = Numberclicksquare + 5;
                    UPorDown();
                }
                else if (Numberclicksquare == 4)
                {
                    FlipSquareNum = Numberclicksquare;
                    UPorDown();
                    FlipSquareNum = Numberclicksquare - 1;
                    UPorDown();
                    FlipSquareNum = Numberclicksquare + 5;
                    UPorDown();
                }
            }
            else if (Numberclicksquare >= 5 && Numberclicksquare <= 9)
            {
                if (Numberclicksquare == 5)
                {
                    FlipSquareNum = Numberclicksquare;
                    UPorDown();
                    FlipSquareNum = Numberclicksquare - 5;
                    UPorDown();
                    FlipSquareNum = Numberclicksquare + 1;
                    UPorDown();
                    FlipSquareNum = Numberclicksquare + 5;
                    UPorDown();
                }
                else if (Numberclicksquare > 5 && Numberclicksquare < 9)
                {
                    FlipSquareNum = Numberclicksquare;
                    UPorDown();
                    FlipSquareNum = Numberclicksquare - 1;
                    UPorDown();
                    FlipSquareNum = Numberclicksquare + 1;
                    UPorDown();
                    FlipSquareNum = Numberclicksquare - 5;
                    UPorDown();
                    FlipSquareNum = Numberclicksquare + 5;
                    UPorDown();
                }
                else if (Numberclicksquare == 9)
                {
                    FlipSquareNum = Numberclicksquare;
                    UPorDown();
                    FlipSquareNum = Numberclicksquare - 1;
                    UPorDown();
                    FlipSquareNum = Numberclicksquare - 5;
                    UPorDown();
                    FlipSquareNum = Numberclicksquare + 5;
                    UPorDown();
                }
            }
            else if (Numberclicksquare >= 10 && Numberclicksquare <= 14)
            {
                if (Numberclicksquare == 10)
                {
                    FlipSquareNum = Numberclicksquare;
                    UPorDown();
                    FlipSquareNum = Numberclicksquare - 5;
                    UPorDown();
                    FlipSquareNum = Numberclicksquare + 1;
                    UPorDown();
                    FlipSquareNum = Numberclicksquare + 5;
                    UPorDown();
                }
                else if (Numberclicksquare > 10 && Numberclicksquare < 14)
                {
                    FlipSquareNum = Numberclicksquare;
                    UPorDown();
                    FlipSquareNum = Numberclicksquare - 1;
                    UPorDown();
                    FlipSquareNum = Numberclicksquare + 1;
                    UPorDown();
                    FlipSquareNum = Numberclicksquare - 5;
                    UPorDown();
                    FlipSquareNum = Numberclicksquare + 5;
                    UPorDown();
                }
                else if (Numberclicksquare == 14)
                {
                    FlipSquareNum = Numberclicksquare;
                    UPorDown();
                    FlipSquareNum = Numberclicksquare - 1;
                    UPorDown();
                    FlipSquareNum = Numberclicksquare - 5;
                    UPorDown();
                    FlipSquareNum = Numberclicksquare + 5;
                    UPorDown();
                }
            }
            else if (Numberclicksquare >= 15 && Numberclicksquare <= 19)
            {
                if (Numberclicksquare == 15)
                {
                    FlipSquareNum = Numberclicksquare;
                    UPorDown();
                    FlipSquareNum = Numberclicksquare - 5;
                    UPorDown();
                    FlipSquareNum = Numberclicksquare + 1;
                    UPorDown();
                    FlipSquareNum = Numberclicksquare + 5;
                    UPorDown();
                }
                else if (Numberclicksquare > 15 && Numberclicksquare < 19)
                {
                    FlipSquareNum = Numberclicksquare;
                    UPorDown();
                    FlipSquareNum = Numberclicksquare - 1;
                    UPorDown();
                    FlipSquareNum = Numberclicksquare + 1;
                    UPorDown();
                    FlipSquareNum = Numberclicksquare - 5;
                    UPorDown();
                    FlipSquareNum = Numberclicksquare + 5;
                    UPorDown();
                }
                else if (Numberclicksquare == 19)
                {
                    FlipSquareNum = Numberclicksquare;
                    UPorDown();
                    FlipSquareNum = Numberclicksquare - 1;
                    UPorDown();
                    FlipSquareNum = Numberclicksquare - 5;
                    UPorDown();
                    FlipSquareNum = Numberclicksquare + 5;
                    UPorDown();
                }
            }
            else if (Numberclicksquare >= 20 && Numberclicksquare <= 24)
            {
                if (Numberclicksquare == 20)
                {
                    FlipSquareNum = Numberclicksquare;
                    UPorDown();
                    FlipSquareNum = Numberclicksquare - 5;
                    UPorDown();
                    FlipSquareNum = Numberclicksquare + 1;
                    UPorDown();
                }
                else if (Numberclicksquare > 20 && Numberclicksquare < 24)
                {
                    FlipSquareNum = Numberclicksquare;
                    UPorDown();
                    FlipSquareNum = Numberclicksquare - 1;
                    UPorDown();
                    FlipSquareNum = Numberclicksquare + 1;
                    UPorDown();
                    FlipSquareNum = Numberclicksquare - 5;
                    UPorDown();
                }
                else if (Numberclicksquare == 24)
                {
                    FlipSquareNum = Numberclicksquare;
                    UPorDown();
                    FlipSquareNum = Numberclicksquare - 1;
                    UPorDown();
                    FlipSquareNum = Numberclicksquare - 5;
                    UPorDown();
                }
            }
        }
    }


    public void GhostTime() //怨靈時間
    {
        int num = Random.Range(0, 9);
        int ghostSquareNum;
        if (num == 0)
        {
            ghostSquareNum = 6;
        }
        else if (num == 1)
        {
            ghostSquareNum = 7;
        }
        else if (num == 2)
        {
            ghostSquareNum = 8;
        }
        else if (num == 3)
        {
            ghostSquareNum = 11;
        }
        else if (num == 4)
        {
            ghostSquareNum = 12;
        }
        else if (num == 5)
        {
            ghostSquareNum = 13;
        }
        else if (num == 6)
        {
            ghostSquareNum = 16;
        }
        else if (num == 7)
        {
            ghostSquareNum = 17;
        }
        else
        {
            ghostSquareNum = 18;
        }
        FlipSquareNum = ghostSquareNum - 6;
        UPorDown();
        FlipSquareNum = ghostSquareNum - 5;
        UPorDown();
        FlipSquareNum = ghostSquareNum - 4;
        UPorDown();
        FlipSquareNum = ghostSquareNum - 1;
        UPorDown();
        FlipSquareNum = ghostSquareNum;
        UPorDown();
        FlipSquareNum = ghostSquareNum + 1;
        UPorDown();
        FlipSquareNum = ghostSquareNum + 4;
        UPorDown();
        FlipSquareNum = ghostSquareNum + 5;
        UPorDown();
        FlipSquareNum = ghostSquareNum + 6;
        UPorDown();
    }

    public void CheckGoal() //檢查是否達成目標
    {
        bool isGoalAchieved = true;
        for (int i = 0; i < Goal.Length; i++)
        {
            if (Goal[i] != 2 && SquareState[i] != Goal[i])
            {
                isGoalAchieved = false;
                break;
            }
        }

        if (isGoalAchieved)
        {
            Debug.Log("恭喜達成目標！");
            gameManager = FindFirstObjectByType<GameManager>();
            gameManager.Win = true;
            gameManager.endGame();
            // 在這裡可以添加達成目標後的處理邏輯，例如顯示提示、進入下一關等
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
