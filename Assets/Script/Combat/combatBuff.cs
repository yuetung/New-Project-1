using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using System;

public class CombatBuff : ScriptableObject, ISerializationCallbackReceiver
{
    private const string COMBAT_BUFF_PATH = "Assets/Resources/CombatBuff/";
    
    public float potency;

    [SerializeField]
    private List<CombatEvent> _effect_keys = new List<CombatEvent>();
    [SerializeField]
    private List<List<CombatInstruction>> _effect_values = new List<List<CombatInstruction>>();

    public Dictionary<CombatEvent, List<CombatInstruction>> effects = new Dictionary<CombatEvent, List<CombatInstruction>>();

    // Serialization

    public void OnBeforeSerialize()
    {
        _effect_keys.Clear();
        _effect_values.Clear();

        foreach (KeyValuePair<CombatEvent, List<CombatInstruction>> p in effects)
        {
            _effect_keys.Add(p.Key);
            _effect_values.Add(p.Value);
        }
    }

    public void OnAfterDeserialize()
    {
        effects.Clear();

        for (int i = 0; i < Math.Min(_effect_keys.Count, _effect_values.Count); i++) {
            effects.Add(_effect_keys[i], _effect_values[i]);
        }

    }

    public CombatInstruction[] execute(CombatEvent ev)
    {
        // Return instructions to caller to execute

        if (!effects.ContainsKey(ev)) return null;

        return effects[ev].ToArray();
    }

    // Constructor

    void OnEnable()
    {
        hideFlags = HideFlags.DontSave;
    }

    public static CombatBuff init(string moveName)
    {
        string path = COMBAT_BUFF_PATH + moveName + ".asset";

        if (AssetDatabase.LoadAssetAtPath<CombatBuff>(path) == null)
        {
            CombatBuff b = ScriptableObject.CreateInstance<CombatBuff>() as CombatBuff;

            AssetDatabase.CreateAsset(b, path);
            AssetDatabase.SaveAssets();

            return b;
        } else
        {
            return init(moveName + "1");
        }
    }

    [MenuItem("Assets/Create/CombatBuff")]
    public static CombatBuff make()
    {   
        return init("New Combat Buff");
    }

}

[CustomEditor(typeof(CombatBuff))]
public class CombatBuffEditor : Editor
{
    private CombatBuff current;
    private SerializedObject trackSO;

    private bool show_effects = true;
    private List<bool> show_inst = new List<bool>();

    void OnEnable()
    {
        current = (CombatBuff)target;
        trackSO = new SerializedObject(current);

        for (int i = 0; i < current.effects.Count; i++) show_inst.Add(true);
    }

    public override void OnInspectorGUI()
    {
        trackSO.Update();
        
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(trackSO.FindProperty("potency"));
        EditorGUILayout.EndHorizontal();

        show_effects = EditorGUILayout.Foldout(show_effects, new GUIContent("Effects"));

        if (show_effects)
        {
            CombatEvent key;
            List<CombatInstruction> value;
            int i = 0;

            EditorGUI.indentLevel += 1;
            foreach (CombatEvent p in current.effects.Keys)
            {
                EditorGUILayout.BeginHorizontal();
                show_inst[i] = EditorGUILayout.Foldout(show_inst[i], GUIContent.none);
                key = (CombatEvent)EditorGUILayout.EnumPopup(p);
                value = current.effects[p];
                
                if (GUILayout.Button("-"))
                {
                    current.effects.Remove(p);
                    break;
                }
                EditorGUILayout.EndHorizontal();

                if (show_inst[i])
                {
                    EditorGUI.indentLevel += 1;
                    foreach (CombatInstruction inst in value)
                    {
                        EditorGUILayout.BeginHorizontal();
                        inst.effect = (CombatEffect.effect)EditorGUILayout.EnumPopup(inst.effect);
                        inst.potency = EditorGUILayout.FloatField(inst.potency);
                        if (GUILayout.Button("-"))
                        {
                            value.Remove(inst);
                            break;
                        }
                        EditorGUILayout.EndHorizontal();
                        
                        EditorGUILayout.BeginHorizontal();
                        inst.stance = (CombatStance)EditorGUILayout.EnumPopup(inst.stance);
                        inst.type = (CombatType)EditorGUILayout.EnumPopup(inst.type);
                        EditorGUILayout.EndHorizontal();
                        
                        EditorGUILayout.BeginHorizontal();
                        inst.buff = (CombatBuff)EditorGUILayout.ObjectField(inst.buff, typeof(CombatBuff), false);
                        inst.stat = EditorGUILayout.TextField(inst.stat);
                        EditorGUILayout.EndHorizontal();
                    }
                    EditorGUI.indentLevel -= 1;

                    EditorGUILayout.BeginHorizontal();
                    if (GUILayout.Button("Add"))
                    {
                        value.Add(new CombatInstruction());
                    }
                    EditorGUILayout.EndHorizontal();
                    
                }

                if (!current.effects.ContainsKey(key))
                {
                    current.effects.Add(key, value);
                    current.effects.Remove(p);
                    break;
                }

                i++;
            }
            EditorGUI.indentLevel -= 1;

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("+"))
            {
                current.effects.Add(CombatEvent.COMBAT_START, new CombatInstructionList());
                show_inst.Add(true);
            }
            EditorGUILayout.EndHorizontal();
        }
        
        trackSO.ApplyModifiedProperties();
    }

}