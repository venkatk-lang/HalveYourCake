using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProblemUI : MonoBehaviour
{
    public TextMeshProUGUI[] numerator = new TextMeshProUGUI[2], denominator = new TextMeshProUGUI[2];
    public Image[] fractionLines = new Image[2];
    
    public TextMeshProUGUI operationSymbol;

    public TextMeshProUGUI[] floatingNums;
    public TextMeshProUGUI answerNumerator, answerDenominator;

    public CakeForQuestions[] cakeForQuestions;
    public ButterForQuestions[] butterForQuestions;
    public void SetProblemText(Question question)
    {
        Clear();
        int localNum, localDen;
        for (int i = 0; i < numerator.Length; i++)
        {
            numerator[i].gameObject.SetActive(false);
            denominator[i].gameObject.SetActive(false);
            fractionLines[i].gameObject.SetActive(false);
            floatingNums[i].gameObject.SetActive(false);
            cakeForQuestions[i].gameObject.SetActive(false);
            butterForQuestions[i].gameObject.SetActive(false);
            switch (question.valueType[i])
            {
                case ProblemType.Fractional:
                    // Set fractional problem text
                    numerator[i].gameObject.SetActive(true);
                    denominator[i].gameObject.SetActive(true);
                    numerator[i].text += NullAsEmpty(question.numerator[i]);
                    denominator[i].text += NullAsEmpty(question.denominator[i]);
                    fractionLines[i].gameObject.SetActive(true);
                    break;
                case ProblemType.FloatingPoint:
                    // Set floating point problem text
                    floatingNums[i].gameObject.SetActive(true);
                    floatingNums[i].text = NullAsEmpty(question.numerator[i]) + "." + NullAsEmpty(question.denominator[i]);
                    break;
                case ProblemType.Percentage:
                    // Set percentage problem text
                    floatingNums[i].gameObject.SetActive(true);
                    floatingNums[i].text = NullAsEmpty(question.numerator[i]) + "%";
                    break;
                case ProblemType.Visual_Bar:
                    // Set visual bar problem text
                    butterForQuestions[i].gameObject.SetActive(true);
                    localNum = int.Parse(NullAsEmpty(question.numerator[i]));
                    localDen = int.Parse(NullAsEmpty(question.denominator[i]));
                    butterForQuestions[i].SetSlices(localDen, localNum);
                    break;
                case ProblemType.Visual_Box:
                    butterForQuestions[i].gameObject.SetActive(true);
                    localNum = int.Parse(NullAsEmpty(question.numerator[i]));
                    localDen = int.Parse(NullAsEmpty(question.denominator[i]));
                    butterForQuestions[i].SetSlices(localDen, localNum);
                    break;
                case ProblemType.Visual_Circle:
                    cakeForQuestions[i].gameObject.SetActive(true);
                    localNum = int.Parse(NullAsEmpty(question.numerator[i]));
                    localDen = int.Parse(NullAsEmpty(question.denominator[i]));
                    cakeForQuestions[i].SetSlices(localDen, localNum);
                    break;
            }
        }
        operationSymbol.text = operationSymbols[question.OperationType];
    }
    public void SetAnswerText(Vector2Int value)
    {
        answerNumerator.text = value.x.ToString();
        answerDenominator.text = value.y.ToString();
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
