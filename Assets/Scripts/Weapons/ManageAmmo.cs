using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageAmmo : MonoBehaviour
{
    private AudioManager audioManager;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     SpearGun spearGun = GetComponent<SpearGun>();
    //     if(other.tag.Equals("Ammo"))
    //     {
    //         audioManager.Play("Pickup", GetComponent<AudioSource>());
    //         spearGun.RestockAmmo();
    //         Destroy(other.gameObject);
    //     }
    // }
}
