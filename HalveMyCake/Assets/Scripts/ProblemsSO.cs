using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu()]
public class ProblemsSO : ScriptableObject
{
    public List<Problem> problem;
    const int fixedSize = 2;
    void OnValidate()
    {
        for (int i = 0; i < problem.Count; i++)
        {
            var item = problem[i];
            if (item.question.numerator == null || item.question.numerator.Length != fixedSize)
            {
                System.Array.Resize(ref item.question.numerator, fixedSize);
            }
            if (item.question.denominator == null || item.question.denominator.Length != fixedSize)
            {
                System.Array.Resize(ref item.question.denominator, fixedSize);
            }
            if (item.question.valueType == null || item.question.valueType.Length != fixedSize)
            {
                System.Array.Resize(ref item.question.valueType, fixedSize);
            }
        }

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

    public ProblemType[] valueType = new ProblemType[2];
    //public ProblemType value1Type => valueType == null ? ProblemType.Fractional : valueType.Length < 1 ? ProblemType.Fractional : valueType[0];
    //public ProblemType value2Type => valueType == null ? ProblemType.Fractional : valueType.Length < 2 ? ProblemType.Fractional : valueType[1];
    public string[] numerator;
    public string[] denominator;
    public OperationType OperationType;
    // Constructor updated to initialize all fields to avoid CS0171
    public Question(ProblemType value1Type = ProblemType.Fractional, ProblemType value2Type = ProblemType.Fractional, OperationType operationType = OperationType.None)
    {
        this.valueType = new ProblemType[2];
        this.valueType[0] = value1Type;
        this.valueType[1] = value2Type;
        this.numerator = new string[2]; // Default to an array of size 2
        this.denominator = new string[2]; // Default to an array of size 2
        this.OperationType = operationType;
    }
}
[System.Serializable]
public class Solution
{
    public int numerator;
    public int denominator;
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