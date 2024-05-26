using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class game2 : MonoBehaviour
{
    public GameObject player;
    public Vector3 pos;
    public Vector3 rot;
    private float jumpPower=800;
    public bool isGround;
    public bool isTouch = false;
    public bool isOver = false;
    private float limit = 0f;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody rb=player.GetComponent<Rigidbody>();
          if(Input.GetKey(KeyCode.LeftArrow))
        {
            rot.y -= 3f;
        }
        if(Input.GetKey(KeyCode.RightArrow))
        {
            rot.y += 3f;
        }
        if(Input.GetKey(KeyCode.UpArrow))
        {
            player.transform.position += transform.forward*0.2f;
        }
        if(isGround == true)
        {
            if(Input.GetKeyDown(KeyCode.Slash))
            {
                isGround = false;
                rb.AddForce(new Vector3(0,jumpPower,0));
            }
        }
        if(isTouch == true)
        {
            isTouch = false;
             player.GetComponent<Rigidbody>().isKinematic=true;
            player.transform.position = new Vector3(20,105,20);
             player.GetComponent<Rigidbody>().isKinematic=false;
        }
        if(limit <= 10)
        {
            if(Input.GetKey(KeyCode.Backslash))
            {
                player.transform.position += transform.forward*0.2f;
                limit += Time.fixedDeltaTime;
            }
        }
        if(!Input.GetKey(KeyCode.Backslash))
        {
            if(limit >= 0)
            {
                limit -= Time.fixedDeltaTime;
            }
        }
        if(isOver == true)
        {
            player.GetComponent<Rigidbody>().isKinematic=true;
            player.transform.position = new Vector3(20,105,20);
            player.GetComponent<Rigidbody>().isKinematic=false;
        }
        player.transform.localEulerAngles = new Vector3(rot.x, rot.y, rot.z);
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Ground")
        {
            isGround = true;
        }
        /*if(other.gameObject.tag == "over")
        {
            isOver = true;
        }*/
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "player1")
        {
            isTouch = true;
        }
    }
}