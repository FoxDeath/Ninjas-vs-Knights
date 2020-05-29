using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
