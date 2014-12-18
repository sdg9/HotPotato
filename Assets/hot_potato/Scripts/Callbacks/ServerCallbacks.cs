using UnityEngine;
using System.Collections;

[BoltGlobalBehaviour(BoltNetworkModes.Server, "Scene1")]
public class ServerCallbacks : Bolt.GlobalEventListener
{
    public static bool ListenServer = true;
    public static BoltEntity potato;

	void Awake()
    {
        print("S AWAKE");
        if (ListenServer)
        {
            //PlayerObjectRegistry.CreateServerPlayer();
        }
    }

    void FixedUpdate()
    {
        //Do some server logic check if required
    }

    // Callback triggered when this instance receives an incoming client connection
    public override void ConnectRequest(UdpKit.UdpEndPoint endpoint)
    {
        print("S ConnectRequest 1");
        Vector3 connectedPlayerStarting = PlayerObjectRegistry.PlayerConnect();
        print("Connected starting: " + connectedPlayerStarting);

        if (PlayerObjectRegistry.connectedPlayerCount > 4)
        {
            BoltNetwork.Refuse(endpoint, null);
        }
        else
        {
            CameraSpawnPoint csp = new CameraSpawnPoint(connectedPlayerStarting);
            //print("Connect request, setting spawn point: " + csp.position);
            BoltNetwork.Accept(endpoint, null, csp, null);
        }

    }

    public override void OnEvent (PlayerCameraState evnt)
    {
        print("Left wall: " + evnt.leftWall);
        print("Connecton: " + evnt.RaisedBy);
        foreach (PlayerObject p in PlayerObjectRegistry.allPlayers) {
            if (p.connection == evnt.RaisedBy)
            {
                print("PLAYER MATCH!");
                p.leftWall = evnt.leftWall;
                p.rightWall = evnt.rightWall;
            }
        }
    }

    public override void OnEvent(TeleportEvent evnt)
    {
        foreach (PlayerObject p in PlayerObjectRegistry.allPlayers)
        {
            if (p.connection == evnt.RaisedBy)
            {
                Vector3 newPosition;
                if (evnt.isRightWall)
                {
                    newPosition = PlayerObjectRegistry.getRightWallTeleport(p);
                }
                else
                {
                    newPosition = PlayerObjectRegistry.getLeftWallTeleport(p);
                }

                //TODO move potato
                potato.transform.position = newPosition;
                return;
            }
        }
    }

    /*
    // Callback triggered when this instance receives an incoming client connection
    public override void ConnectRequest(UdpKit.UdpEndPoint endpoint, Bolt.IProtocolToken token)
    {
        //*Example:* Accepting an incoming connection with user credentials in the data token.
        //   UserCredentials creds = (UserCredentials)token);
        ///   if(Authenticate(creds.username, creds.password)) {
        ///     BoltNetwork.Accept(connection.remoteEndPoint);
        ///   }


        print("S ConnectRequest 2");
        BoltNetwork.Accept(endpoint);
    }
    */


    /*
    public override void ConnectRequest(UdpKit.UdpEndPoint endpoint)
    {
        print("Connection request of type 1");
        BoltNetwork.Accept(endpoint);
    }
    */

    /*public override void ConnectRequest(UdpKit.UdpEndPoint endpoint, Bolt.IProtocolToken token)
    {
        print("ACcept incoming connection");
        BoltNetwork.Accept(endpoint);
    }*/

    /*public override void ConnectRequest(UdpKit.UdpEndPoint endpoint, Bolt.IProtocolToken token)
    {
        print("Connection request ");
        CameraSpawnPoint csp = new CameraSpawnPoint(new Vector3(20, 20, 20));
        print("Connect request, setting spawn point: "+ csp.position);
        BoltNetwork.Accept(endpoint, csp, csp, csp);
    }*/

    // Callback triggered when a client has become connected to this instance
    /*public override void Connected(BoltConnection connection)
    {
        print("S Connected 1");

    }*/

    // Callback triggered when a client has become connected to this instance
    public override void Connected(BoltConnection c, Bolt.IProtocolToken acceptToken)
    {
        //*Example:* Using a protocol token with the Connected callback to pass a spawnpoint position to to new player entity.
        print("S Connected 2");
        print("AcceptToken: " + acceptToken);
        //Store this location along with the user's connection
        PlayerObjectRegistry.CreateClientPlayer(c, (CameraSpawnPoint)acceptToken);
    }


    /*public override void Connected(BoltConnection c, Bolt.IProtocolToken acceptToken, Bolt.IProtocolToken connectToken)
    {
        print("S Connected 3");
    }*/


    // Callback triggered before the new local scene is loaded
    public override void SceneLoadLocalBegin(string map)
    {
        print("S SceneLoadLocalBegin");
        //if(BoltNetwork.isClient && map.Equals("GameScene") {
  ///     SplashScreen.Show(SplashScreens.GameLoad);
  ///   }
    }


    // Callback triggered before the new local scene has been completely loaded
    public override void SceneLoadLocalDone(string map)
    {
        ///   if(BoltNetwork.isClient && map.Equals("GameScene") {
        ///     SplashScreen.Hide();
        ///   }
        
        print("S SceneLoadLocalDone");
        PlayerObject p = PlayerObjectRegistry.CreateServerPlayer(new CameraSpawnPoint(new Vector3(0f, 0f, -20f)));
        //server done loading

        //Find camera
        Camera myCamera = PlayerCamera.instance.GetComponentInChildren<Camera>();

        //create potato
        potato = BoltNetwork.Instantiate(BoltPrefabs.Potato);

        //Map camera to potato script
        (potato.GetComponent<PotatoController>()).mainCamera = myCamera;

        //p.rightWall = 
    }

    
    // Callback triggered when a remote connection has completely loaded the current scene
    public override void SceneLoadRemoteDone(BoltConnection connection)
    {
        // *Example:* Instantiating and configuring a player entity on the server and then assigning control to the client.


        print("SceneLoadRemoteDone");
        //client done loading
        //PlayerObject p = (PlayerObject)connection.userToken;
        //print("Player pos: " + p.posX);
    }


}
