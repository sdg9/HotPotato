using UnityEngine;
using System.Collections;

public class PlayerObject {

    //public BoltEntity player;
    public BoltConnection connection;
    //public float posX;
    //public float posY;
    public Vector3 cameraPosition;
    public Vector3 leftWall;
    public Vector3 rightWall;

    public bool isServer
    {
        get { return connection == null; }
    }

    public bool isClient
    {
        get { return connection != null; }
    }
}
