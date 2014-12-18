using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class PlayerObjectRegistry {

    static Vector3 player2Pos = new Vector3(100, 0, -20);
    static Vector3 player3Pos = new Vector3(200, 0, -20);
    static Vector3 player4Pos = new Vector3(300, 0, -20);

    static int connected = 0;

    // keeps a list of all the players
    static List<PlayerObject> players = new List<PlayerObject>();

    // create a player for a connection
    // note: connection can be null
    static PlayerObject CreatePlayer(BoltConnection connection, CameraSpawnPoint token)
    {
        PlayerObject p;

        // create a new player object, assign the connection property
        // of the object to the connection was passed in
        p = new PlayerObject();
        p.connection = connection;
        p.cameraPosition = token.position;

        Debug.Log("Create player: " + players.Count);

        // if we have a connection, assign this player 
        // as the user token for the connection so that we
        // always have an easy way to get the player object 
        // for a connection
        if (p.connection != null)
        {
            p.connection.userToken = p;
        }

        // add to list of all players
        players.Add(p);

        return p;
    }

    // this simply returns the 'players' list cast to 
    // an IEnumerable<T> so that we hide the ability 
    // to modify the player list from the outside.
    public static IEnumerable<PlayerObject> allPlayers
    {
        get { return players; }
    }

    public static int connectedPlayerCount
    {
        get { return connected; }
    }

    // finds the server player by checking the 
    // .isServer property for every player object.
    public static PlayerObject serverPlayer
    {
        get { return players.First(x => x.isServer); }
    }

    // utility function which creates a server player
    public static PlayerObject CreateServerPlayer(CameraSpawnPoint token)
    {
        return CreatePlayer(null, token);
    }

    // utility that creates a client player object.
    public static PlayerObject CreateClientPlayer(BoltConnection connection, CameraSpawnPoint token)
    {
        return CreatePlayer(connection, token);
    }

    public static Vector3 PlayerConnect()
    {
        connected++;
        switch (connected)
        {
            case 1:
                return player2Pos;
                break;
            case 2:
                return player3Pos;
                break;
            case 3:
                return player4Pos;
                break;
            default:
                return new Vector3(0, 0, 0);
                break;
        }
    }

    // utility function which lets us pass in a 
    // BoltConnection object (even a null) and have 
    // it return the proper player object for it.
    public static PlayerObject GetPlayer(BoltConnection connection)
    {
        if (connection == null)
        {
            return serverPlayer;
        }

        return (PlayerObject)connection.userToken;
    }

    public static Vector3 getRightWallTeleport(PlayerObject player)
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i] == player)
            {
                //loop back to 0 index
                return players[(i + 1) % (players.Count)].leftWall;
            }
        }
        //Shouldn't happen
        return new Vector3(0f, 0f, 0f);
    }

    public static Vector3 getLeftWallTeleport(PlayerObject player)
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i] == player)
            {
                //loop back to end of list
                int nextIndex = (i - 1) < 0 ? (players.Count - 1) : (i - 1);
                return players[nextIndex].rightWall;
            }
        }
        //Shouldn't happen
        return new Vector3(0f, 0f, 0f);
    }
}
