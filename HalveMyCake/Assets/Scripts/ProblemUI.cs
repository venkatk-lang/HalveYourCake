using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProblemUI : MonoBehaviour
{
    public TextMeshProUGUI[] numerator = new TextMeshProUGUI[2], denominator = new TextMeshProUGUI[2];
    public Image[] fractionLines = new Image[2];
    //public TextMeshProUGUI num1Numerator, num1Denominator;
    //public TextMeshProUGUI num2Numerator, num2Denominator;

    public TextMeshProUGUI operationSymbol;

    public TextMeshProUGUI[] floatingNums;
    public void SetProblemText(Question question)
    {
        Clear();
        for (int i = 0; i < numerator.Length; i++)
        {
            fractionLines[i].enabled = false;
            switch (question.valueType[i])
            {
                case ProblemType.Fractional:
                    // Set fractional problem text
                    numerator[i].text += NullAsEmpty(question.numerator[i]);
                    denominator[i].text += NullAsEmpty(question.denominator[i]);
                    fractionLines[i].enabled = true;
                    break;
                case ProblemType.FloatingPoint:
                    // Set floating point problem text
                    floatingNums[i].text = NullAsEmpty(question.numerator[i]) + "." + NullAsEmpty(question.denominator[i]);
                    break;
                case ProblemType.Percentage:
                    // Set percentage problem text
                    floatingNums[i].text = NullAsEmpty(question.numerator[i]) + "%";
                    break;
                case ProblemType.Visual_Bar:
                    // Set visual bar problem text
                    int localNum = int.Parse(NullAsEmpty(question.numerator[i]));
                    for (int j = 0; i < localNum; j++) 
                    {
                        // Instantiate bar segments
                        floatingNums[i].text += "+";
                    }
                    int localDen = int.Parse(NullAsEmpty(question.denominator[i]));
                    for (int j = 0; i < localDen; j++)
                    {
                        // Instantiate bar segments
                        floatingNums[i].text += "-";
                    }
                    break;
                case ProblemType.Visual_Box:
                    // Set visual box problem text
                    localNum = int.Parse(NullAsEmpty(question.numerator[i]));
                    for (int j = 0; i < localNum; j++)
                    {
                        // Instantiate bar segments
                        floatingNums[i].text += "+";
                    }
                    localDen = int.Parse(NullAsEmpty(question.denominator[i]));
                    for (int j = 0; i < localDen; j++)
                    {
                        // Instantiate bar segments
                        floatingNums[i].text += "-";
                    }
                    break;
                case ProblemType.Visual_Circle:
                    // Set visual circle problem text
                    break;
            }
        }
        operationSymbol.text = operationSymbols[question.OperationType];
    }
    public void Clear()
    {
        foreach (var num in numerator) num.text = "";
        foreach (var den in denominator) den.text = "";

        operationSymbol.text = "";

        for(int i = 0; i < numerator.Length; i++)
        {
            numerator[i].text = "";
            denominator[i].text = "";
        }
        for (int i = 0; i < floatingNums.Length; i++)
            floatingNums[i].text = "";
    }

    public Dictionary<OperationType, string> operationSymbols = new Dictionary<OperationType, string>()
    {
        { OperationType.None, ""},
        { OperationType.Addition, "+" },
        { OperationType.Subtraction, "-" },
        { OperationType.Multiplication, "×" },
        { OperationType.Division, "÷" }
    };

    public string NullAsEmpty(string str)
    {
        if (str == null)
            return "";
        return str;
    }
}
