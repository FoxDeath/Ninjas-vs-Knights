using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class KunaiNadeInput : MonoBehaviour
{
    [SerializeField] GameObject kunaiPrefab;
    [SerializeField] float throwForce = 50f;
    [SerializeField] float maxKunai = 4;

    private float currentKunai;
    private GameObject bulletEmitter;
    private bool threw;

    void Start()
    {
        currentKunai = maxKunai;
        bulletEmitter = GameObject.Find("KunaiEmitter");
    }

    public void ThrowKunai()
    {
        if (currentKunai > 0f)
        {
            currentKunai--;
            StartCoroutine(ThrowKunaiBehaviour());
            threw = true;
        }
    }

    private IEnumerator ThrowKunaiBehaviour()
    {
        Transform mainCamera = gameObject.transform.Find("Main Camera").transform;
        GameObject kunai = Instantiate(kunaiPrefab, bulletEmitter.transform.position, bulletEmitter.transform.rotation);
        Rigidbody rb = kunai.GetComponent<Rigidbody>();
        rb.AddForce(mainCamera.forward * throwForce, ForceMode.VelocityChange);        
        yield return new WaitForSeconds(2f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && !threw)
        {
            if (currentKunai < maxKunai)
            {
                currentKunai++;
            }
            Destroy(gameObject);
        }
    }
}
