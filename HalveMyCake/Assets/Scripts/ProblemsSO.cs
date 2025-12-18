using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu()]
public class ProblemsSO : ScriptableObject
{
    public List<Problem> problem;
    const int fixedSize = 2;
    void OnValidate()
    {
        foreach (var item in problem)
        {
            if (item.question.numerator == null || item.question.numerator.Length != fixedSize)
            {
                System.Array.Resize(ref item.question.numerator, fixedSize);
            }
            if (item.question.denominator == null || item.question.denominator.Length != fixedSize)
            {
                System.Array.Resize(ref item.question.denominator, fixedSize);
            }
        }

    }
}
[CustomEditor(typeof(ProblemsSO))]
public class ProblemSOEditor : Editor
{
    int levelIndex = 0;
    int difficultyIndex = 0;
    ProblemType num1ValueType = (ProblemType)0;
    ProblemType num2ValueType = (ProblemType)0;

    int value1Numerator = 0;
    int value1Denominator = 0;

    int value2Numerator = 0;
    int value2Denominator = 0;

    OperationType operationType;

    int answerNumerator = 0;
    int answerDenominator = 0;
    int answerMultiplier = 0;
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        ProblemsSO problemsSO = (ProblemsSO)target;

        GUILayout.Space(10);

        GUILayout.BeginHorizontal();
        {
            GUILayout.Label("Index: ");
            bool minusPressed = GUILayout.Button("-", GUILayout.Width(25));
            levelIndex = EditorGUILayout.IntField("", levelIndex, new GUILayoutOption[] { GUILayout.MaxWidth(50) });
            if(GUILayout.Button("+", GUILayout.Width(25)))
            {
                levelIndex += 1;
            }else if(minusPressed)
            {
                levelIndex = levelIndex > 0 ? levelIndex - 1 : levelIndex; 
            }

            if (levelIndex >= problemsSO.problem.Count)
            {
                levelIndex = problemsSO.problem.Count;
                problemsSO.problem.AddRange(new Problem[] { new Problem() });
                return;
            }
        }


        difficultyIndex = problemsSO.problem[levelIndex].difficulty;
        num1ValueType = problemsSO.problem[levelIndex].question.value1Type;
        num2ValueType = problemsSO.problem[levelIndex].question.value2Type;
        value1Numerator = int.TryParse(problemsSO.problem[levelIndex].question.numerator[0], out int x) ? x : 0;
        value1Denominator = int.TryParse(problemsSO.problem[levelIndex].question.denominator[0], out int y) ? y : 0;
        value2Numerator = int.TryParse(problemsSO.problem[levelIndex].question.numerator[1], out int z) ? z : 0;
        value2Denominator = int.TryParse(problemsSO.problem[levelIndex].question.denominator[1], out int a) ? a : 0;
        operationType = problemsSO.problem[levelIndex].question.OperationType;
        answerNumerator = problemsSO.problem[levelIndex].answer.numerator;
        answerDenominator = problemsSO.problem[levelIndex].answer.denominator;
        answerMultiplier = problemsSO.problem[levelIndex].answer.multiplier;



        GUILayout.EndHorizontal();
        GUILayout.Space(25);
        GUILayout.BeginHorizontal();
        {
            GUILayout.Label("Difficulty");
            difficultyIndex = EditorGUILayout.IntField("", difficultyIndex, new GUILayoutOption[] { GUILayout.MaxWidth(50) });
        }
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        {
            GUILayout.Label("Value 1 Type: ");
            num1ValueType = (ProblemType)EditorGUILayout.Popup((int)num1ValueType, new string[] {
                "Fractional",
                "FloatingPoint",
                "Percentage",
                "Visual_Bar",
                "Visual_Box",
                "Visual_Circle"
            }, new GUILayoutOption[] { });
            //num1ValueType = (ValueType)GUILayout.SelectionGrid((int)num1ValueType, new string[] {
            //    "Fractional",
            //    "FloatingPoint",
            //    "Percentage",
            //    "Visual_Bar",
            //    "Visual_Box",
            //    "Visual_Circle"
            //}, 6);
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Num1Numerator");
        value1Numerator = EditorGUILayout.IntField(value1Numerator, new GUILayoutOption[] { GUILayout.MaxWidth(50) });
        GUILayout.EndHorizontal();



        GUILayout.BeginHorizontal();
        GUILayout.Label("Num1Denominator");
        value1Denominator = EditorGUILayout.IntField(value1Denominator, new GUILayoutOption[] { GUILayout.MaxWidth(50) });
        GUILayout.EndHorizontal();


        GUILayout.BeginHorizontal();
        {
            GUILayout.Label("Value 2 Type: ");
            num2ValueType = (ProblemType)EditorGUILayout.Popup((int)num2ValueType, new string[] {
                "Fractional",
                "FloatingPoint",
                "Percentage",
                "Visual_Bar",
                "Visual_Box",
                "Visual_Circle"
            }, new GUILayoutOption[] { });
        }
        //num2ValueType = (ValueType)GUILayout.SelectionGrid((int)num2ValueType, new string[] {
        //        "Fractional",
        //        "FloatingPoint",
        //        "Percentage",
        //        "Visual_Bar",
        //        "Visual_Box",
        //        "Visual_Circle"
        //    }, 6);
        //}
        GUILayout.EndHorizontal();


        GUILayout.BeginHorizontal();
        GUILayout.Label("Num2Numerator");
        value2Numerator = EditorGUILayout.IntField(value2Numerator, new GUILayoutOption[] { GUILayout.MaxWidth(50) });
        GUILayout.EndHorizontal();



        GUILayout.BeginHorizontal();
        GUILayout.Label("Num2Denominator");
        value2Denominator = EditorGUILayout.IntField(value2Denominator, new GUILayoutOption[] { GUILayout.MaxWidth(50) });
        GUILayout.EndHorizontal();


        GUILayout.BeginHorizontal();
        GUILayout.Label("OperationType");
        operationType = (OperationType)EditorGUILayout.Popup((int)operationType, new string[] {
                "None",
                "Addition",
                "Subtraction",
                "Multiplication",
                "Division"
            }, new GUILayoutOption[] { });
        //operationType = (OperationType)GUILayout.SelectionGrid((int)operationType, new string[] {
        //        "None",
        //        "Addition",
        //        "Subtraction",
        //        "Multiplication",
        //        "Division"
        //    }, 5);
        GUILayout.EndHorizontal();


        GUILayout.BeginHorizontal();
        GUILayout.Label("AnswerNumerator");
        answerNumerator = EditorGUILayout.IntField(answerNumerator, new GUILayoutOption[] { GUILayout.MaxWidth(50) });
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("AnswerDenominator");
        answerDenominator = EditorGUILayout.IntField(answerDenominator, new GUILayoutOption[] { GUILayout.MaxWidth(50) });
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("AnswerMultiplier");
        answerMultiplier = EditorGUILayout.IntField(answerMultiplier, new GUILayoutOption[] { GUILayout.MaxWidth(50) });
        GUILayout.EndHorizontal();

        problemsSO.problem[levelIndex] = new Problem()
        {
            difficulty = difficultyIndex,
            question = new Question()
            {
                value1Type = num1ValueType,
                value2Type = num2ValueType,
                numerator = new string[] { value1Numerator.ToString(), value2Numerator.ToString() },
                denominator = new string[] { value1Denominator.ToString(), value2Denominator.ToString() },
                OperationType = operationType
            },
            answer = new Solution()
            {
                numerator = answerNumerator,
                denominator = answerDenominator,
                multiplier = answerMultiplier
            }
        };
    }
}
[System.Serializable]
public class Problem
{
    public int difficulty;
    public Question question;
    public Solution answer;
}
[System.Serializable]
public class Question
{
    public ProblemType value1Type;
    public ProblemType value2Type;
    public string[] numerator = new string[2];
    public string[] denominator = new string[2];
    public OperationType OperationType;
}
[System.Serializable]
public class Solution
{
    public int numerator;
    public int denominator;
    public int multiplier;
}
public enum OperationType
{
    None,
    Addition,
    Subtraction,
    Multiplication,
    Division
}
public enum ProblemType
{
    Fractional,
    FloatingPoint,
    Percentage,
    Visual_Bar,
    Visual_Box,
    Visual_Circle
}