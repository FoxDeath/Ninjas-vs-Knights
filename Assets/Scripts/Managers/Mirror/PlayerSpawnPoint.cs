using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Just a transform that knows where the player should spawn
public class PlayerSpawnPoint : MonoBehaviour
{
    void Awake()
    {
        PlayerSpawnSystem.AddSpawnPoint(transform);
    }

    void OnDestroy()
    {
        PlayerSpawnSystem.RemoveSpawnPoint(transform);
    }
}
