using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class KunaiNadeInput : MonoBehaviour
{
    [SerializeField] GameObject kunaiPrefab;
    private GameObject bulletEmiter;

    [SerializeField] float throwForce = 100f;

    [SerializeField] int maxKunai = 4;
    private int currentKunai;

    private bool threw;

    void Start()
    {
        currentKunai = maxKunai;
        bulletEmiter = GameObject.Find("KunaiEmitter");
    }

    private void Update() 
    {
        UIManager.GetInstance().SetGrenadeCount(currentKunai);    
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
        Ray ray = GameObject.Find("Main Camera").GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit ;

        Vector3 targetPoint ;
        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(1000f);
        }

        GameObject kunai = Instantiate(kunaiPrefab, bulletEmiter.transform.position, bulletEmiter.transform.rotation);
        Rigidbody rb = kunai.GetComponent<Rigidbody>();
        rb.velocity = (targetPoint - bulletEmiter.transform.position).normalized * throwForce;

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
