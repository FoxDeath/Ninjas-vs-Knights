using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : MonoBehaviour
{
    [SerializeField] float healAmmount = 25f;

    public float GetHealAmmount() 
    {
        return healAmmount;
    }
}
