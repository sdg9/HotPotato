using UnityEngine;
using System.Collections;

[BoltGlobalBehaviour(BoltNetworkModes.Client)]
public class PlayerCallbacks : Bolt.GlobalEventListener
{

    Vector3 myCameraPos;

    // Callback triggered when trying to connect to a remote endpoint
    public override void ConnectAttempt(UdpKit.UdpEndPoint endpoint)
    {
        print("C ConnectAttempt");
    }

    public override void Connected(BoltConnection connection, Bolt.IProtocolToken acceptToken) {
        CameraSpawnPoint cameraPosition = (CameraSpawnPoint)acceptToken;

        print("Client player connected");
        if (cameraPosition != null)
        {
            print("Connected player position: " + cameraPosition.position);
        }
        else
        {
            print("Cam pos is null");
        }
        myCameraPos = cameraPosition.position;

        
        using (var evnt = PlayerCameraState.Raise(Bolt.GlobalTargets.Everyone))
        {
            evnt.rightWall = new Vector3(0, 0, 0);
            evnt.leftWall = new Vector3(0, 0, 0);
        }
   }

    public override void SceneLoadLocalBegin(string map)
    {
        print("C SceneLoadLocalBegin");
    }

    public override void SceneLoadLocalDone(string map)
    {
        print("C SceneLoadLocalDone");
        print("Instantiating camera");
        PlayerCamera.Instantiate();
        //Camera myCamera = PlayerCamera.instance.GetComponentInChildren<Camera>();
        Transform playerCamera = PlayerCamera.instance.transform;

        playerCamera.position = myCameraPos;


        
        

        /*print("Setting main cam of potato controller to cam");
        print("Setting camera position to : " + myCameraPos);
        PotatoController pc = GameObject.FindObjectOfType<PotatoController>();
        print("PC: " + pc);
        if (pc != null)
        {
            pc.mainCamera = myCamera;
            pc.transform.position = myCameraPos;
        }
        print("MYcam");

        myCamera.transform.position = myCameraPos;
        */
    }

    public override void ControlOfEntityGained(BoltEntity arg)
    {
        print("C ControlOfEntityGained");
        //print("Setting cam position");
        //PlayerCamera.instance.SetTarget(arg);
        //PlayerCamera.instance.transform.position = new Vector3(0, 0, -20);
    }


    public override void ConnectRequest(UdpKit.UdpEndPoint endpoint, Bolt.IProtocolToken token)
    {
        print("C ConnectRequest");
    }

    public override void SceneLoadRemoteDone(BoltConnection connection)
    {
        print("C SceneLoadRemoteDone");
    }

    // Callback triggered when a connection to remote server has failed
    public override void ConnectFailed(UdpKit.UdpEndPoint endpoint)
    {
        print("C ConnectFailed");
    }

    // Callback triggered when the connection to a remote server has been refused.
    public override void ConnectRefused(UdpKit.UdpEndPoint endpoint)
    {
        print("C ConnectRefused");
    }

    public override void ConnectRefused(UdpKit.UdpEndPoint endpoint, Bolt.IProtocolToken token)
    {
        print("C ConnectRefused 2");
    }

    // Callback triggered when disconnected from remote server
    public override void Disconnected(BoltConnection connection)
    {
        print("C Disconnected");
    }

    public override void Disconnected(BoltConnection connection, Bolt.IProtocolToken token)
    {
        print("C Disconnected 2");
    }
}