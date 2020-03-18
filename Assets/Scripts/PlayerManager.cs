using UnityEngine;


//TO DO: Add to some kind of manager sometime
public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public GameObject player;

    void Awake()
    {
        instance = this;
    }
}
