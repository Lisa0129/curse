using UnityEngine;

public class ClickSquare : MonoBehaviour
{

    public int clicksquareNumber; //方塊編號
    public FlipSquare flipSquare;
    public GameManager gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    public void Click()
    {
        flipSquare = FindFirstObjectByType<FlipSquare>();
        flipSquare.Flip();
        gameManager = FindFirstObjectByType<GameManager>();
        gameManager.move();
        flipSquare.CheckGoal();
    }

    public void B0()
    {
        clicksquareNumber = 0;
        Click();
    }

    public void B1()
    {
        clicksquareNumber = 1;
        Click();
    }

    public void B2()
    {
        clicksquareNumber = 2;
        Click();
    }

    public void B3()
    {
        clicksquareNumber = 3;
        Click();
    }

    public void B4()
    {
        clicksquareNumber = 4;
        Click();
    }

    public void B5()
    {
        clicksquareNumber = 5;
        Click();
    }

    public void B6()
    {
        clicksquareNumber = 6;
        Click();
    }

    public void B7()
    {
        clicksquareNumber = 7;
        Click();
    }

    public void B8()
    {
        clicksquareNumber = 8;
        Click();
    }

    public void B9()
    {
        clicksquareNumber = 9;
        Click();
    }

    public void B10()
    {
        clicksquareNumber = 10;
        Click();
    }

    public void B11()
    {
        clicksquareNumber = 11;
        Click();
    }

    public void B12()
    {
        clicksquareNumber = 12;
        Click();
    }

    public void B13()
    {
        clicksquareNumber = 13;
        Click();
    }

    public void B14()
    {
        clicksquareNumber = 14;
        Click();
    }

    public void B15()
    {
        clicksquareNumber = 15;
        Click();
    }

    public void B16()
    {
        clicksquareNumber = 16;
        Click();
    }

    public void B17()
    {
        clicksquareNumber = 17;
        Click();
    }

    public void B18()
    {
        clicksquareNumber = 18;
        Click();
    }

    public void B19()
    {
        clicksquareNumber = 19;
        Click();
    }

    public void B20()
    {
        clicksquareNumber = 20;
        Click();
    }

    public void B21()
    {
        clicksquareNumber = 21;
        Click();
    }

    public void B22()
    {
        clicksquareNumber = 22;
        Click();
    }

    public void B23()
    {
        clicksquareNumber = 23;
        Click();
    }

    public void B24()
    {
        clicksquareNumber = 24;
        Click();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
