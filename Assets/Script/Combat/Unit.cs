using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

public class Unit : MonoBehaviour {

    [SerializeField]
    private combatType primaryType;
    [SerializeField]
    private combatType secondaryType;

    private combatStance stance;

    // implementation undecided
    public int physicalAttack;
    public int physicalDefense;
    public int specialAttack;
    public int specialDefense;

    public int speed;
    [NonSerialized]
    public int delay;

    public combatMove[] moves;
    public IList<combatAbility> abilities;
    public IList<combatBuff> buffs;

    [SerializeField]
    public float maxHealth;
    private float _health;
    public float health {
        get { return this._health; }
        set {
            this._health += value;
            this._health = Mathf.Max(Mathf.Min(this.maxHealth, this._health), 0); // restricts health between max_health and 0

            if (this._health == 0) { 
                this.die();
            }
        }
    }

    [SerializeField]
    public float maxStamina;
    private float _stamina;
    public float stamina
    {
        get { return this._stamina; }
        set
        {
            this._stamina += value;
            this._stamina = Mathf.Max(Mathf.Min(this._stamina, this.maxStamina), 0);
        }
    }

    // Helper Method

    public combatManager battle
    {
        get { return this.GetComponentInParent<combatManager>(); }
    }

    // Functionality

    public void die()
    {
        this.triggerEvent(combatEvent.ON_DEATH);
        // kills this unit, to be implemented
    }

    public void triggerEvent(combatEvent e)
    {
        // triggers combat events
        // API for combatEffects to call
    }

    public void tick()
    {
        this.triggerEvent(combatEvent.SPEED_TICK);
        this.delay -= this.speed;
    }

    public void startTurn()
    {
        this.triggerEvent(combatEvent.TURN_START);
    }

    public void endTurn()
    {
        this.triggerEvent(combatEvent.TURN_END);
        battle.startNextUnitTurn();
    }
}

[CustomEditor(typeof(Unit))]
public class UnitEditior : Editor
{
    private int index = 0;
    private Unit inspected;

    void onEnable()
    {
        if (this.inspected == null)
        {
            this.inspected = target as Unit;
        }
    }

    public override void OnInspectorGUI()
    {
        List<combatMove> moveList = AssetDatabase.LoadAssetAtPath<CombatMoveList>("Assets/Script/Combat/Moves.asset").moveList;
        string[] moveNameList = moveList.ConvertAll<string>(delegate (combatMove m) { return m.moveName; }).ToArray();

        serializedObject.Update();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("primaryType"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("secondaryType"));

        EditorGUILayout.PropertyField(serializedObject.FindProperty("maxHealth"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("maxStamina"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("physicalAttack"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("physicalDefense"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("specialAttack"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("specialDefense"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("speed"));
        
        EditorGUILayout.PropertyField(serializedObject.FindProperty("moves"));
        if (serializedObject.FindProperty("moves").isExpanded)
        {
            EditorGUI.indentLevel += 1;

            EditorGUILayout.PropertyField(serializedObject.FindProperty("moves").FindPropertyRelative("Array.size"));
            
            combatMove move;
            int selected;
            int index;
            for (int j = 0; j < serializedObject.FindProperty("moves").arraySize; j++)
            {
                move = serializedObject.FindProperty("moves").GetArrayElementAtIndex(j).objectReferenceValue as combatMove;

                index = 0;
                foreach (combatMove moveCheck in moveList) {
                    if (moveCheck == move)
                    {
                        break;
                    }
                    index++;
                }

                index = index % moveList.Count;

                EditorGUI.BeginChangeCheck();
                selected = EditorGUILayout.Popup("Move " + j, index, moveNameList);
                if (EditorGUI.EndChangeCheck())
                {
                    serializedObject.FindProperty("moves").GetArrayElementAtIndex(j).objectReferenceValue = moveList[selected];
                    Debug.Log((serializedObject.FindProperty("moves").GetArrayElementAtIndex(j).objectReferenceValue as combatMove).moveName);
                }
            }

            EditorGUI.indentLevel -= 1;
        }

        serializedObject.ApplyModifiedProperties();
        
    }
}
