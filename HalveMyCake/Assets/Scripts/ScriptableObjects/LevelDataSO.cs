using System;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using Unity.VisualScripting;


#if UNITY_EDITOR
using UnityEditor;
#endif
[CreateAssetMenu(fileName = "LevelDataSO", menuName = "Scriptable Objects/LevelDataSO", order = 1)]
public class LevelDataSO : ScriptableObject
{
    [SerializeField] public List<LevelData> levelData = new List<LevelData>();
#if UNITY_EDITOR
    private void OnValidate()
    {
        int i = 0;
        foreach(var item in levelData)
        {
            item.levelNumber = i;
            Array.Resize(ref item.starThresholds, 3);
            i++;
        }
    }
    [HideInInspector] public LevelData currLevelData = new LevelData();
#endif
}
[System.Serializable]
public class LevelData
{
    public int levelNumber;
    public int[] starThresholds = new int[3];
    public int pointsReward = 100;
}
#if UNITY_EDITOR
[CustomEditor(typeof(LevelDataSO))]
public class LevelDataSOEditor : Editor
{
    int currentLevel = -1;
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        LevelDataSO _target = target as LevelDataSO;
        GUILayout.BeginHorizontal();
        GUILayout.Label("Level: ");
        if(GUILayout.Button("<")) _target.currLevelData.levelNumber--;
        _target.currLevelData.levelNumber = EditorGUILayout.IntField(_target.currLevelData.levelNumber);
        if(GUILayout.Button(">")) _target.currLevelData.levelNumber++;
        GUILayout.Label("v0:");
        _target.currLevelData.starThresholds[0] = EditorGUILayout.IntField(_target.currLevelData.starThresholds[0]);
        GUILayout.Label("v1:");
        _target.currLevelData.starThresholds[1] = EditorGUILayout.IntField(_target.currLevelData.starThresholds[1]);
        GUILayout.Label("v2:");
        _target.currLevelData.starThresholds[2] = EditorGUILayout.IntField(_target.currLevelData.starThresholds[2]);

        //for (int i = 0; i < _target.currLevelData.starThresholds.Length; i++)
        //{
        //}
        GUILayout.EndHorizontal();
        _target.currLevelData.pointsReward = EditorGUILayout.IntField("PointsReward: ", _target.currLevelData.pointsReward);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Load"))
        {
            LoadIndex(true ,_target.currLevelData.levelNumber);
        }
        if (GUILayout.Button("Save"))
        {
            SaveToIndex(true, _target.currLevelData.levelNumber, _target.currLevelData);
        }
        GUILayout.EndHorizontal();
        void LoadIndex(bool isNum, int index)
        {
            if(_target.levelData.Count <= index)
            {
                Debug.LogError("Index out of range");
                return;
            }
            _target.currLevelData = _target.levelData[index];
        }

        void SaveToIndex(bool isNum, int index, LevelData levelData)
        {
            if (_target.levelData.Count <= index)
            {
                Debug.LogError("Index out of range");
                return;
            }
            _target.currLevelData = _target.levelData[index];
        }
    }
    
}
#endif