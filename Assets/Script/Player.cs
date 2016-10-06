using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;
    public float zslowdown;
    public Text CountText;
    public int CubesCollected;
    public Text dialogText;
    public static bool talking;

    void Start()
    {
        talking = false;
        CubesCollected = 0;
        SetCountText();
        dialogText.text = "";
        GameObject.Find("Dialogue_Box").transform.localScale = new Vector3(0, 0, 0);

    }
    void Update()
    {

        /*GameObject NPC = GameObject.Find("NPC_all");
        var playerScripts = NPC.GetComponentsInChildren<NPC_all>();
        foreach(NPC_all script in playerScripts) { 
        var distance = Vector3.Distance(this.transform.position, script.transform.position);
            if (distance < 3 && Input.GetKey("x"))
        {
            dialogText.text = script.dialogue_text;
                break;
        }
        else
        {
            dialogText.text = "";
        }
        }*/


        CharacterController controller = GetComponent<CharacterController>();
        if (talking == false){
            if (controller.isGrounded)
            {
                moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical") * zslowdown);
                moveDirection = transform.TransformDirection(moveDirection);
                moveDirection *= speed;
            if (Input.GetButton("Jump"))
            {

                moveDirection.y = jumpSpeed;
            }

        }
            else
        {
                // Apply right/left, if we are NOT grounded
                // --> Left/Right movement in mid air allowed
                moveDirection.x = Input.GetAxis("Horizontal") * speed;
                moveDirection.z = Input.GetAxis("Vertical") * speed * zslowdown;
            }
            moveDirection.y -= gravity * Time.deltaTime;
            controller.Move(moveDirection * Time.deltaTime);

        }
        if (talking== false)
        {
            dialogText.text = "";
            GameObject.Find("Dialogue_Box").transform.localScale = new Vector3(0, 0, 0);
        }

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collectible Cube"))
        {
            other.gameObject.SetActive(false);
            CubesCollected = CubesCollected + 1;
            SetCountText();
        }

    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("NPC") && Input.GetKeyDown("x"))
        {
            
            talk(other.gameObject.GetComponent<NPC_all>().getDialogue(other.gameObject.name));
            GameObject.Find("Dialogue_Box").transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    void OnTriggerExit(Collider other)
    {
        
    }

    void SetCountText()
    {
        CountText.text = "Cubes Collected: " + CubesCollected.ToString();
    }

    void talk(string msg)
    {
        dialogText.text = msg;
    }
}