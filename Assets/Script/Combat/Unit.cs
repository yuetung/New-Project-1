﻿using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

public class Unit : MonoBehaviour {
    
    public combatType primaryType;
    public combatType secondaryType;

    [NonSerialized]
    public combatStance stance;
    
    public int physicalAttack;
    public int physicalDefense;
    public int specialAttack;
    public int specialDefense;

    public int speed;
    [NonSerialized]
    public int delay;
    
    public CombatMove[] moves;
    public combatAbility[] abilities;
    public combatBuff[] buffs;

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

    public CombatManager battle
    {
        get { return this.GetComponentInParent<CombatManager>(); }
    }

    public bool isAlly(Unit u)
    {
        return false; // NO ONE IS MAH ALLY
    }

    public bool isPlayer()
    {
        return true; // All your unitZ are belong to US
    }

    // Functionality

    public void die()
    {
        this.triggerEvent(combatEvent.ON_DEATH);
        // kills this unit, to be implemented
        Debug.Log("Oh noes, you can't actually die!?");
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
        CombatMove[] moveList = Array.ConvertAll<UnityEngine.Object, CombatMove>(AssetDatabase.LoadAllAssetsAtPath("Assets/Database/CombatMove.asset"), (UnityEngine.Object o) => { return o as CombatMove; });
        string[] moveNameList = Array.ConvertAll<CombatMove, string>(moveList, (CombatMove m) => { return m.name; });

        serializedObject.Update();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("primaryType"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("secondaryType"));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("maxHealth"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("maxStamina"));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("physicalAttack"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("physicalDefense"));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("specialAttack"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("specialDefense"));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.PropertyField(serializedObject.FindProperty("speed"));
        
        EditorGUILayout.PropertyField(serializedObject.FindProperty("moves"));
        if (serializedObject.FindProperty("moves").isExpanded)
        {
            EditorGUI.indentLevel += 1;

            EditorGUILayout.PropertyField(serializedObject.FindProperty("moves").FindPropertyRelative("Array.size"));
            
            CombatMove move;
            SerializedProperty elem;
            int selected;
            int index;
            for (int j = 0; j < serializedObject.FindProperty("moves").arraySize; j++)
            {
                elem = serializedObject.FindProperty("moves").GetArrayElementAtIndex(j);
                move = elem.objectReferenceValue as CombatMove;

                index = 0;
                foreach (CombatMove moveCheck in moveList) {
                    if (moveCheck == move)
                    {
                        break;
                    }
                    index++;
                }

                if (index == moveList.Length)
                {
                    index = 0;
                }

                EditorGUI.BeginChangeCheck();
                selected = EditorGUILayout.Popup("Move " + j, index, moveNameList);
                if (EditorGUI.EndChangeCheck())
                {
                    //Undo.RecordObject(move, "Changed move to " + moveList[selected].moveName);

                    elem.objectReferenceValue = moveList[selected];

                    //EditorUtility.SetDirty(elem.objectReferenceValue);
                }
            }

            EditorGUI.indentLevel -= 1;
        }

        serializedObject.ApplyModifiedProperties();
    }
}
