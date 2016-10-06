using UnityEngine;
using System.Collections;

public class NPC_Bay : NPC_all
{
    private bool allowedToTrigger;
    public bool player_met_Bay;

    string[,] BayLines = new string[,]
        { { "Hi I am Bay, please don't collect any of the cubes","Really, there's nothing you'll get from collecting all the cubes"},
           {"Why are you collecting the cubes? Please stop this madness!","Really, there's nothing you'll get from collecting all the cubes" },
           {"Good job, now you have to collect ","" },
           {"Well done! Hehehehe Bay will be gone soon!!!","" },
           {"Hahahaha Bay is finally gone!!!","" }};
    private int lineNo = 0;

    void Start()
    {
        player_met_Bay = false;
        NPC_Adam.Bay_is_dead = false;
    }
    IEnumerator destroyBay()
    {
        yield return new WaitForSeconds(3);
        transform.Translate(0, -10, 0);
        NPC_Adam.Bay_is_dead = true;
        Destroy(gameObject, 3.0f);
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && Input.GetKeyDown("x"))
        {
            if (lineNo != BayLines.GetLength(1) && !BayLines[eventScript.mainEventNo, lineNo].Equals("", System.StringComparison.Ordinal))
            {
                if (Player.talking == false)
                {
                    Player.talking = true;
                }
                dialogue_text = BayLines[eventScript.mainEventNo, lineNo];
                lineNo++;
            }
            else
            {
                lineNo = 0;
                Player.talking = false;
                end();
            }

            /*void OnTriggerStay(Collider other) {

                if (other.gameObject.CompareTag("Player") && Input.GetKeyDown("x") && Player.talking == false)
                {
                    Player.talking = true;
                    if (GameObject.Find("Player").GetComponent<Player>().CubesCollected == 13)
                        {
                            dialogue_text = "Damn you I am dying";
                            StartCoroutine(destroyBay());
                        }
                        else if (GameObject.Find("Player").GetComponent<Player>().CubesCollected > 0)
                        {
                            dialogue_text = "Please stop collecting the cubes otherwise I will be destroyed!!!";
                        }
                        else
                        {
                            dialogue_text = "Hi I am Bay, please don't collect any of the cubes";
                            player_met_Bay = true;

                        }
                    }
                }*/
        }
    }
}