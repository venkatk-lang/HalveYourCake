using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ProblemsSO))]
public class ProblemsSOEditor : Editor
{
    private SerializedProperty problemArray;
    private int selectedIndex = -1;

    private void OnEnable()
    {
        problemArray = serializedObject.FindProperty("problem");
        if (problemArray.arraySize > 0 && (selectedIndex < 0 || selectedIndex >= problemArray.arraySize))
            selectedIndex = 0;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.LabelField("Problems List", EditorStyles.boldLabel);

        // List all problems with select and remove buttons
        if(EditorGUILayout.DropdownButton(new GUIContent("Problems"), FocusType.Passive))
        {
            GenericMenu menu = new GenericMenu();
            for (int i = 0; i < problemArray.arraySize; i++)
            {
                int index = i; // Capture index for the closure
                menu.AddItem(new GUIContent($"Problem {i + 1}"), selectedIndex == i, () =>
                {
                    selectedIndex = index;
                });
            }
            menu.ShowAsContext();
        }
        //for (int i = 0; i < problemArray.arraySize; i++)
        //{
            //EditorGUILayout.BeginHorizontal();
            //EditorGUILayout.LabelField($"Problem {i + 1}", GUILayout.Width(100));
            //if (GUILayout.Button("Select", GUILayout.Width(60)))
            //{
            //    selectedIndex = i;
            //}
            //if (GUILayout.Button("Remove", GUILayout.Width(60)))
            //{
            //    problemArray.DeleteArrayElementAtIndex(i);
            //    if (selectedIndex == i) selectedIndex = -1;
            //    else if (selectedIndex > i) selectedIndex--;
            //    break;
            //}
            //EditorGUILayout.EndHorizontal();
        //}

        EditorGUILayout.Space(10);
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add New Problem"))
        {
            problemArray.InsertArrayElementAtIndex(problemArray.arraySize);
            selectedIndex = problemArray.arraySize - 1;
        }
        if(GUILayout.Button("Remove Current Problem"))
        {
            if (selectedIndex >= 0 && selectedIndex < problemArray.arraySize)
            {
                problemArray.DeleteArrayElementAtIndex(selectedIndex);
                selectedIndex = -1;
            }
        }
        EditorGUILayout.EndHorizontal();


        EditorGUILayout.Space(20);

        // Editing panel for the selected problem
        if (selectedIndex >= 0 && selectedIndex < problemArray.arraySize)
        {
            var problemProp = problemArray.GetArrayElementAtIndex(selectedIndex);
            var questionProp = problemProp.FindPropertyRelative("question");
            var answerProp = problemProp.FindPropertyRelative("answer");

            EditorGUILayout.LabelField($"Editing Problem {selectedIndex + 1}", EditorStyles.boldLabel);
            EditorGUILayout.BeginVertical("box");

            // Difficulty
            EditorGUILayout.PropertyField(problemProp.FindPropertyRelative("difficulty"), new GUIContent("Difficulty"));

            EditorGUILayout.Space(4);

            // Value 1
            EditorGUILayout.LabelField("Value 1", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            DrawEnumElement(questionProp.FindPropertyRelative("valueType"), 0, "Type");
            DrawStringElement(questionProp.FindPropertyRelative("numerator"), 0, "Numerator");
            DrawStringElement(questionProp.FindPropertyRelative("denominator"), 0, "Denominator");
            EditorGUI.indentLevel--;

            EditorGUILayout.Space(4);

            // Value 2
            EditorGUILayout.LabelField("Value 2", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            DrawEnumElement(questionProp.FindPropertyRelative("valueType"), 1, "Type");
            DrawStringElement(questionProp.FindPropertyRelative("numerator"), 1, "Numerator");
            DrawStringElement(questionProp.FindPropertyRelative("denominator"), 1, "Denominator");
            EditorGUI.indentLevel--;

            EditorGUILayout.Space(4);

            // Operation
            EditorGUILayout.LabelField("Operation", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(questionProp.FindPropertyRelative("OperationType"), new GUIContent("Operation Type"));
            EditorGUI.indentLevel--;

            EditorGUILayout.Space(4);

            // Answer
            EditorGUILayout.LabelField("Answer", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(answerProp.FindPropertyRelative("numerator"), new GUIContent("Numerator"));
            EditorGUILayout.PropertyField(answerProp.FindPropertyRelative("denominator"), new GUIContent("Denominator"));
            EditorGUI.indentLevel--;

            EditorGUILayout.EndVertical();
        }
        else
        {
            EditorGUILayout.HelpBox("Select a problem to edit.", MessageType.Info);
        }

        serializedObject.ApplyModifiedProperties();
    }

    // Helper to draw a single enum element from a ProblemType[] array property
    private void DrawEnumElement(SerializedProperty arrayProp, int index, string label)
    {
        if (arrayProp.arraySize > index)
        {
            arrayProp.GetArrayElementAtIndex(index).enumValueIndex =
                (int)(ProblemType)EditorGUILayout.EnumPopup(label, (ProblemType)arrayProp.GetArrayElementAtIndex(index).enumValueIndex);
        }
    }

    // Helper to draw a single string element from a string array property
    private void DrawStringElement(SerializedProperty arrayProp, int index, string label)
    {
        if (arrayProp.arraySize > index)
        {
            arrayProp.GetArrayElementAtIndex(index).stringValue =
                EditorGUILayout.TextField(label, arrayProp.GetArrayElementAtIndex(index).stringValue);
        }
    }
}
