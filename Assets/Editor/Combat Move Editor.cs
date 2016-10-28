using UnityEditor;
using UnityEngine;

using System.Collections.Generic;

public class CombatMoveEditor : EditorWindow
{

    private static readonly string path = "Assets/Script/Combat/Moves.asset";
    private string newMoveName = "New Move Name";

    public CombatMoveList combatMoveList;

    [MenuItem("Window/Combat Move Editor")]
    public static void Init()
    {
        EditorWindow.GetWindow(typeof(CombatMoveEditor)).Show();
    }

    void OnEnable()
    {
        this.combatMoveList = AssetDatabase.LoadAssetAtPath(path, typeof(CombatMoveList)) as CombatMoveList;
    }

    void OnGUI()
    {
        if (this.combatMoveList == null)
        {
            this.combatMoveList = CombatMoveList.init();

            AssetDatabase.CreateAsset(this.combatMoveList, path);
            AssetDatabase.SaveAssets();
        }

        GUILayout.BeginHorizontal();
        GUILayout.Label("Moves!");
        GUILayout.EndHorizontal();
        
        foreach (CombatMove m in this.combatMoveList.moveList)
        {
            if (m != null)
            {
                m.name = EditorGUILayout.TextField("Move Name: ", m.name);
            }
        }

        GUILayout.BeginHorizontal();

        newMoveName = EditorGUILayout.TextField(newMoveName);
        if (GUILayout.Button("Add Combat Move", GUILayout.ExpandWidth(false)))
        {
            AddCombatMove(newMoveName);
            newMoveName = "New Move Name";
        }

        GUILayout.EndHorizontal();

        if (GUI.changed)
        {
            EditorUtility.SetDirty(this.combatMoveList);
        }
    }

    private void AddCombatMove(string name)
    {
        CombatMove m = CombatMove.init(name, new string[] { "10dmgfirestanding" });

        this.combatMoveList.moveList.Add(m);
    }
}