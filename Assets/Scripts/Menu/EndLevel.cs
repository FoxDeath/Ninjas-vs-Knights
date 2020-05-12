using UnityEngine;


//TO DO: Add to UI Manager
public class EndLevel : MonoBehaviour
{
    public static int killedEnemies = 0;
    public int numberOfEnemies;

    public GameObject victoryScreenUI;

    void Update()
    {
        if(killedEnemies >= numberOfEnemies)
        {
            victoryScreenUI.SetActive(true);
        }
    }
}
