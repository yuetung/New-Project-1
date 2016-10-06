using UnityEngine;
using System.Collections;

public class eventScript : MonoBehaviour
{

    public static int mainEventNo;//0 for default, 1 talked to Bay, 2 collected at least a cube, 3 collected all, 4 Bay died 
    // Use this for initialization
    void Start()
    {
        mainEventNo = 0;

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerStay(Collider other)
    {
        if (NPC_Adam.Bay_is_dead == true)
        {
            mainEventNo=4;
        }
        else if (GameObject.Find("Player").GetComponent<Player>().CubesCollected == 13 && NPC_Adam.player_met_Adam == true)
        {
            mainEventNo=3;
        }
        else if (GameObject.Find("Player").GetComponent<Player>().CubesCollected > 0 && NPC_Adam.player_met_Adam == true)
        {
            mainEventNo = 2;
        }
        else if (GameObject.Find("Bay").GetComponent<NPC_Bay>().player_met_Bay == true && NPC_Adam.player_met_Adam == true)
        {
            mainEventNo = 1;
        }
        else
        {
            mainEventNo = 0;
        }
    }
}
