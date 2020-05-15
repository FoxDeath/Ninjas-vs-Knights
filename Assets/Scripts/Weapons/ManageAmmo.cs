using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageAmmo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Ammo"))
        {
            //audioManager.Play("Pickup", GetComponent<AudioSource>());
            //RestockAmmo();
            //Destroy(other.gameObject);
            print("pls");
        }
    }
}
