using UnityEngine;

public class NPC_Adam : NPC_all {

    string[,] adamLines = new string[,]
        { { "Hi I am Adam, do me a favor, help me collect all the 13 cubes", "yes that's right, all 13 cubes"},
           {"So you have met Bay, don't listen to him, he's an idiot, help me collect all the cubes instead",""},
           {"Good job, now you have to collect {0} more cubes.","" },
           {"Well done! Hehehehe Bay will be gone soon!!!","" },
           {"Hahahaha Bay is finally gone!!!","" }};

    private int lineNo = 0;

    // Update is called once per frame

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && Input.GetKeyDown("x") ) {

            int eventNo = eventScript.getInstance().mainEventNo;

            if (lineNo < adamLines.GetLength(1) && adamLines[eventNo, lineNo].Length != 0) {

                eventScript.getInstance().player_met_Adam = true;

                if (Player.talking == false)
                {
                    Player.talking = true;
                }

                dialogue_text = System.String.Format(adamLines[eventNo, lineNo], 13-GameObject.Find("Player").GetComponent<Player>().CubesCollected);

                lineNo++;
                
            } else {
                
                lineNo = 0;
                Player.talking = false;
                end();

            }

            /*Player.talking = true;
            if (Bay_is_dead == true)
            {
                dialogue_text = "Hahahaha Bay is finally gone!!!";
            }
            else if (GameObject.Find("Player").GetComponent<Player>().CubesCollected == 13 && player_met_Adam == true)
            {
                dialogue_text = "Well done! Hehehehe Bay will be gone soon!!!";
            }
            else if (GameObject.Find("Player").GetComponent<Player>().CubesCollected > 0 && player_met_Adam == true)
            {
                dialogue_text = "Good job, now you have to collect "+ (13-GameObject.Find("Player").GetComponent<Player>().CubesCollected).ToString()+" more cubes";
            }
            else if (GameObject.Find("Bay").GetComponent<NPC_Bay>().player_met_Bay == true && player_met_Adam==true)
            {
                dialogue_text="So you have met Bay, don't listen to him, he's an idiot, help me collect all the cubes instead";
            }
            else
            {
                dialogue_text = "Hi I am Adam, do me a favor, help me collect all the 13 cubes";
                player_met_Adam = true;
                nextline();
                dialogue_text = "yes that's right, all 13 cubes";
                    
            }
            */

        }
    }
}
