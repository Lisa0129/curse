using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int steps; //剩餘使用步數
    public TextMeshProUGUI stepsText; //顯示剩餘步數的文字
    public FlipSquare flipSquare;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        NewRound();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewRound()
    {
        steps = 20;
        stepsText.text = steps.ToString();
        flipSquare = FindFirstObjectByType<FlipSquare>();
        flipSquare.ReClean();
    }

    public void move()
    {
        steps--;
        stepsText.text =  steps.ToString();
        if(steps <= 0)
        {
            Debug.Log("Game Over");
            NewRound();
        }
    }
}
