using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class KunaiNadeInput : MonoBehaviour
{
    private Transform bulletEmitter;

    [SerializeField] GameObject kunaiPrefab;

    [SerializeField] float throwForce = 100f;

    [SerializeField] int maxKunai = 4;
    private int currentKunai;
    private bool threw;

    void Awake()
    {
        bulletEmitter = transform.Find("KunaiEmitter");
    }

    void Start()
    {
        currentKunai = maxKunai;
    }

    private void Update() 
    {
        UIManager.GetInstance().SetGrenadeCount(currentKunai, GetComponentInChildren<NinjaUI>());    
    }

    public void ThrowKunai()
    {
        if(!GetComponent<PlayerMovement>().isLocalPlayer)
        {
            return;
        }

        if (currentKunai > 0f)
        {
            currentKunai--;
            StartCoroutine(ThrowKunaiBehaviour());
            threw = true;
        }
    }

    private IEnumerator ThrowKunaiBehaviour()
    {
        Ray ray = transform.Find("Main Camera").GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
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

        GetComponent<NetworkController>().NetworkSpawn(kunaiPrefab.name, bulletEmitter.position, bulletEmitter.rotation, (targetPoint - bulletEmitter.transform.position).normalized * throwForce);       
        yield return new WaitForSeconds(2f);
    }

    public void AddKunai(int n)
    {
        if (currentKunai < maxKunai)
        {
            currentKunai += n;
        }
    }

    public bool CanAddKunai()
    {
        if (currentKunai < maxKunai)
        {
            return true;
        }

        return false;
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
