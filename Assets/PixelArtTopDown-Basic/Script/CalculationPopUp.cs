using System.Collections;
using System.Collections.Generic;
using PixelArtTopDown_Basic.Script;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CalculationPopUp : MonoBehaviour
{
    
    public InventoryController InventoryController;
    public TopDownCharacterController TopDownCharacterController;
    public bool IsPopUpActive { get; private set; }

    public TMP_Text questionText;
    public TMP_InputField answerInput;
    public Button submitButton;
    private int correctAnswer;

    void Start()
    {
        submitButton.onClick.AddListener(CheckAnswer);
        gameObject.SetActive(false);
    }

    public void GenerateCalculation()
    {
        int a = Random.Range(1, 10);
        int b = Random.Range(1, 10);
        int operation = Random.Range(0, 4);

        switch (operation)
        {
            case 0: //Addition
                correctAnswer = a + b;
                questionText.text = $"Solve: {a} + {b} = ?";
                break;
            case 1: //Subtraction
                correctAnswer = a - b;
                questionText.text = $"Solve: {a} - {b} = ?";
                break;
            case 2: //Multiplication
                correctAnswer = a * b;
                questionText.text = $"Solve: {a} * {b} = ?";
                break;
            case 3: //Division
                b = b == 0 ? 1 : b;
                a = a * b;
                correctAnswer = a / b;
                questionText.text = $"Solve: {a} / {b} = ?";
                break;
        }

        IsPopUpActive = true;

        answerInput.text = "";
        gameObject.SetActive(true);
    }

    public void CheckAnswer()
    {

        if (int.TryParse(answerInput.text, out int userAnswer) && userAnswer == correctAnswer)
        {
            IsPopUpActive = false;
            gameObject.SetActive(false);
            //correct answer
        }
        else
        {
            //wrong answer
            gameObject.SetActive(false);
            IsPopUpActive = false;
            InventoryController.ClearInventory();
            
        }
    }
}
