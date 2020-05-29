using Mirror;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PlayerSpawnSystem : NetworkBehaviour
{
    private static List<Transform> spawnPoints = new List<Transform>();

    private int nextIndex = 0;

    public static void AddSpawnPoint(Transform transform)
    {
        spawnPoints.Add(transform);

        spawnPoints = spawnPoints.OrderBy(x => x.GetSiblingIndex()).ToList();

    }

    public static void RemoveSpawnPoint(Transform transform) => spawnPoints.Remove(transform);

    public override void OnStartServer() => NetworkManagerLobby.OnServerReadied += SpawnPlayer;

    [ServerCallback]
    private void OnDestroy() => NetworkManagerLobby.OnServerReadied -= SpawnPlayer;

    [Server]
    public void SpawnPlayer(NetworkConnection conn, GameObject player)
    {
        Transform spawnPoint = spawnPoints.ElementAtOrDefault(nextIndex);

        GameObject playerPrefab = player;

        GameObject playerInstance = Instantiate(playerPrefab, spawnPoints[nextIndex].position, spawnPoints[nextIndex].rotation);

        
        NetworkServer.Destroy(conn.identity.gameObject);

        NetworkServer.Spawn(playerInstance, conn);

        NetworkServer.ReplacePlayerForConnection(conn, playerInstance);

        FindObjectOfType<NetworkManagerLobby>().GamePlayers.Add(playerInstance);

        nextIndex++;
    }
}
