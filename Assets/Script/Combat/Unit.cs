using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

public class Unit : MonoBehaviour {
    
    public CombatType primaryType;
    public CombatType secondaryType;

    [NonSerialized]
    public CombatStance stance;
    
    public int physicalAttack;
    public int physicalDefense;
    public int specialAttack;
    public int specialDefense;

    public int speed;
    [NonSerialized]
    public int delay;
    
    [SerializeField]
    public List<CombatMove> moves = new List<CombatMove>();
    [SerializeField]
    public List<CombatAbility> abilities = new List<CombatAbility>();
    [SerializeField]
    public List<CombatBuff> buffs = new List<CombatBuff>();

    [SerializeField]
    public float maxHealth;
    private float _health;
    public float health {
        get { return this._health; }
        set {
            this._health = value;
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
            this._stamina = value;
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
        battle.triggerEvent(CombatEvent.ON_DEATH, this);
        // kills this unit, to be implemented
        Debug.Log("Oh noes, you can't actually die!?");
    }

    public void triggerEvent(CombatEvent e, Unit source)
    {
        CombatEffectHandler handler;
        CombatInstruction[] instructions;

        foreach (CombatBuff b in this.buffs)
        {
            instructions = b.execute(e);

            if (instructions == null) continue;

            handler = new CombatEffectHandler(instructions);

            handler.execute(battle, source, this, b.potency);
        }
    }

    public void tick()
    {
        battle.triggerEvent(CombatEvent.SPEED_TICK, this);
        this.delay -= this.speed;
    }

    public void startTurn()
    {
        battle.triggerEvent(CombatEvent.TURN_START, this);
    }

    public void endTurn()
    {
        battle.triggerEvent(CombatEvent.TURN_END, this);
        battle.startNextUnitTurn();
    }
    
}

[CustomEditor(typeof(Unit))]
public class UnitEditior : Editor
{
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
        CombatMove[] moveList = Array.ConvertAll<UnityEngine.Object, CombatMove>(
            Resources.LoadAll("CombatMove"), 
            (UnityEngine.Object o) => { return (CombatMove)o; });

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
