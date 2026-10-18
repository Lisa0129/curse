using UnityEngine;

public class ClickSquare : MonoBehaviour
{

    public int clicksquareNumber; //方塊編號
    public int clicksquareNum; //方塊編號
    public FlipSquare flipSquare;
    public GameManager gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    public void Click()
    {
        clicksquareNumber = clicksquareNum;
        flipSquare = FindFirstObjectByType<FlipSquare>();
        flipSquare.Flip();
        gameManager = FindFirstObjectByType<GameManager>();
        gameManager.move();
        flipSquare.CheckGoal();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
