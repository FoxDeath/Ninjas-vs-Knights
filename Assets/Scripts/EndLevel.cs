using UnityEngine;

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
