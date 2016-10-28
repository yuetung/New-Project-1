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

        foreach (combatMove m in this.combatMoveList.moveList)
        {
            m.moveName = EditorGUILayout.TextField("Move Name: ", m.moveName);
        }

        GUILayout.BeginHorizontal();

        newMoveName = EditorGUILayout.TextField(newMoveName);
        if (GUILayout.Button("Add Combat Move", GUILayout.ExpandWidth(false)))
        {
            AddCombatMove(name);
            newMoveName = "New Move Name";
        }

        GUILayout.EndHorizontal();
    }

    private void AddCombatMove(string name)
    {
        combatMove m = combatMove.init(name, new combatEffect[] { combatEffect.lib["10dmgfirestanding"] });

        this.combatMoveList.moveList.Add(m);
    }
}