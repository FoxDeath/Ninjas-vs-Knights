using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : MonoBehaviour
{
    [SerializeField] float healAmmount = 25f;

    private Vector3 movementVector = new Vector3(0f, 1f, 0f);
    private Vector3 startingPos;
    private float movementFactor;

    void Start()
    {
        startingPos = transform.localPosition;
    }

    public float GetHealAmmount() 
    {
        return healAmmount;
    }

    void FixedUpdate()
    {
        Oscilate();
    }

    //Rotates and moves the medpack so it looks nice
    private void Oscilate()
    {
        transform.localRotation *= Quaternion.Euler(0f, 1f, 0f);

        float cycles = Time.time / 3f;

        const float tau = Mathf.PI * 2f;
        float rawSinWave = Mathf.Sin(cycles * tau);

        movementFactor = rawSinWave / 2f + 0.5f;
        Vector3 offset = movementVector * movementFactor;
        transform.localPosition = startingPos + offset;
    }
}
