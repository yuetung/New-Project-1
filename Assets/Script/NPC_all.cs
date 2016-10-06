using UnityEngine;
using System.Collections;

public class NPC_all : MonoBehaviour
{

    protected bool in_dialogue;

    public static string dialogue_text;
    public static int line;
    public bool x_key_pressed;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


    }

        
    public string getDialogue(string npcName)
    {
        string diag = dialogue_text;

        return diag;
    }
    public void nextline()
    {
        x_key_pressed = false;
        while (x_key_pressed==false)
        {
            if (Input.GetKeyDown("x"))
            {
                x_key_pressed = true;
            }
        }
    }
    public void end()
    {

    }
}
