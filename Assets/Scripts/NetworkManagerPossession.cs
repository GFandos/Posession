using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManagerPossession : NetworkManager
{

    public Transform Spawn_1;
    public Transform Spawn_2;
    public Transform Spawn_3;
    public Transform Spawn_4;

    public override void OnServerAddPlayer(NetworkConnection conn)
    {

        Transform start;
        GameObject player;

        switch (numPlayers)
        {
            case 0:
                start = Spawn_1;
                break;
            case 1:
                start = Spawn_2;
                break;
            case 2:
                start = Spawn_3;
                break;
            case 3:
                start = Spawn_4;
                break;

            default:
                start = Spawn_1;
                break;
        }

        player = Instantiate(playerPrefab, start.position, start.rotation);
        NetworkServer.AddPlayerForConnection(conn, player);

    }
}
