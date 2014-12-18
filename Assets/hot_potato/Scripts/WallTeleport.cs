using UnityEngine;
using System.Collections;

public class WallTeleport : MonoBehaviour {


    private GameSetup gs;
    private Camera cam;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        if (gs == null)
        {
            gs = GameObject.FindObjectOfType<GameSetup>();
        }
        if (cam == null)
        {
            cam = PlayerCamera.instance.GetComponentInChildren<Camera>();
        }
	}
    

    //Use this logic to throw to next player's screen
    void OnTriggerExit2D(Collider2D other)
    {
        //TODO will have to change to neighbor's walls
        Vector2 rightWall = new Vector2(cam.ScreenToWorldPoint(new Vector3(Screen.width, 0f, 0f)).x + 0.5f, 0f);﻿	
        Vector2 leftWall = new Vector2(cam.ScreenToWorldPoint(new Vector3(0f, 0f, 0f)).x - 0.5f, 0f);﻿	
        Vector3 currentPos = other.gameObject.transform.position;

        if (gameObject.name == "rightWall")
        {
            //Only teleport object if it's leaving screen
            if (other.rigidbody2D.velocity.x > 0)
            {
                //Appear at right player's left wall
                //other.gameObject.transform.position = new Vector3(leftWall.x, currentPos.y, 0f);

                using (var evnt = TeleportEvent.Raise(Bolt.GlobalTargets.Everyone))
                {
                    evnt.isRightWall = true;
                }
            }
        }
        else
        {
            //Only teleport object if it's leaving screen
            if (other.rigidbody2D.velocity.x < 0)
            {
                //Appear at left player's right wall
                //other.gameObject.transform.position = new Vector3(rightWall.x, currentPos.y, 0f);
                using (var evnt = TeleportEvent.Raise(Bolt.GlobalTargets.Everyone))
                {
                    evnt.isRightWall = false;
                }
            }
        }
    }
}
