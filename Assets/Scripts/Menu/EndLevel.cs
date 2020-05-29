using UnityEngine;

//TO DO: Add to UI Manager
public class EndLevel : MonoBehaviour
{
    public static int killedEnemies = 0;
    public int numberOfEnemies;

    public GameObject victoryScreenUI;

    //TO DO: Game ends after the Objective is destroyed or when all players are dead.
    void Update()
    {
        if(killedEnemies >= numberOfEnemies)
        {
            victoryScreenUI.SetActive(true);
        }
    }
}
