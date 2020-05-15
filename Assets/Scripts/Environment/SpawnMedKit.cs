using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMedKit : MonoBehaviour
{
    public GameObject medKit;
    float timer = 0f;
    void Start()
    {
        Instantiate(medKit, new Vector3(0, 0, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        //if()
        {
            timer += Time.deltaTime;    
        
            if(timer >= 15)
            {
                Instantiate(medKit, new Vector3(0, 0, 0), Quaternion.identity);
            }
        }
        
    }
}
