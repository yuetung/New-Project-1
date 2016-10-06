using UnityEngine;
using System.Collections;

public class eventScript : MonoBehaviour
{

    // lazy Singleton pattern
    // private constructor
    // static getInstance() method which accesses private singleton instance
    private eventScript()
    {
        instance = this;
    }

    private static eventScript instance;
    public static eventScript getInstance() {
        return instance;
    }

    //0 for default, 1 talked to Bay, 2 collected at least a cube, 3 collected all, 4 Bay died

    public bool player_met_Adam = false;
    public bool Bay_is_dead = false;
    public bool player_met_Bay = false;

    public int mainEventNo {
        get
        {
            if (Bay_is_dead == true)
            {
                return 4;
            }
            else if (GameObject.Find("Player").GetComponent<Player>().CubesCollected == 13 && player_met_Adam == true)
            {
                return 3;
            }
            else if (GameObject.Find("Player").GetComponent<Player>().CubesCollected > 0 && player_met_Adam == true)
            {
                return 2;
            }
            else if (player_met_Bay == true && player_met_Adam == true)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
