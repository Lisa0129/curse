using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class FlipSquare : MonoBehaviour
{
    public GameManager gameManager;
    public ClickSquare clickSquare;
    public Button[] SquareButton; //方塊按鈕
    public GameObject[] upSprite; //凸起的方塊圖片
    public GameObject[] downSprite; //凹下的方塊圖片
    public Image[] GoalImage; //目標方塊圖片
    public Sprite upGoalSprite; //凸起的目標方塊圖片
    public Sprite downGoalSprite; //凹下的目標方塊圖片
    public int[] SquareState; //方塊狀態，0為凸起，1為凹下
    public int Numberclicksquare; //點擊方塊編號
    public int FlipSquareNum; //翻轉方塊編號
    public int[] Goal; //所有方塊的目標狀態，0為凸起，1為凹下，2為不變
    public int Goalamount; //目標方塊數量

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
            if (SquareState[i] == 0){
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
            GoalImage[i].gameObject.SetActive(false);
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
            GoalImage[idx].sprite = upGoalSprite;
            GoalImage[idx].gameObject.SetActive(true);
        }

        for (int i = 5; i < 10; i++)
        {
            int idx = result[i];
            Goal[idx] = 1;
            GoalImage[idx].sprite = downGoalSprite;
            GoalImage[idx].gameObject.SetActive(true);
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
    }

    void beDown() //方塊變凹下
    {
        downSprite[FlipSquareNum].SetActive(true);
        upSprite[FlipSquareNum].SetActive(false);
        SquareState[FlipSquareNum] = 1;
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

    public void Flip() //翻轉方塊
    {
        clickSquare = FindFirstObjectByType<ClickSquare>();
        Numberclicksquare = clickSquare.clicksquareNumber;
        if(Numberclicksquare < 5)
        {
            if(Numberclicksquare == 0)
            {
                FlipSquareNum = Numberclicksquare;
                UPorDown();
                FlipSquareNum = Numberclicksquare + 1;
                UPorDown();
                FlipSquareNum = Numberclicksquare + 5;
                UPorDown();
            }
            else if(Numberclicksquare > 0 && Numberclicksquare < 4)
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
            else if(Numberclicksquare == 4)
            {
                FlipSquareNum = Numberclicksquare;
                UPorDown();
                FlipSquareNum = Numberclicksquare - 1;
                UPorDown();
                FlipSquareNum = Numberclicksquare + 5;
                UPorDown();
            }
        }
        else if(Numberclicksquare >= 5 && Numberclicksquare <= 9)
        {
            if(Numberclicksquare == 5)
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
            else if(Numberclicksquare > 5 && Numberclicksquare < 9)
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
            else if(Numberclicksquare == 9)
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
        else if(Numberclicksquare >= 10 && Numberclicksquare <= 14)
        {
            if(Numberclicksquare == 10)
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
            else if(Numberclicksquare > 10 && Numberclicksquare < 14)
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
            else if(Numberclicksquare == 14)
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
        else if(Numberclicksquare >= 15 && Numberclicksquare <= 19)
        {
            if(Numberclicksquare == 15)
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
            else if(Numberclicksquare > 15 && Numberclicksquare < 19)
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
            else if(Numberclicksquare == 19)
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
            if(Numberclicksquare == 20)
            {
                FlipSquareNum = Numberclicksquare;
                UPorDown();
                FlipSquareNum = Numberclicksquare - 5;
                UPorDown();
                FlipSquareNum = Numberclicksquare + 1;
                UPorDown();
            }
            else if(Numberclicksquare > 20 && Numberclicksquare < 24)
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
            else if(Numberclicksquare == 24)
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
            gameManager.NewRound();
            // 在這裡可以添加達成目標後的處理邏輯，例如顯示提示、進入下一關等
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
