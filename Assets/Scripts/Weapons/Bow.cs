using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class Bow : MonoBehaviour
{
    [SerializeField] GameObject regularArrowObj;
    [SerializeField] GameObject fireArrowObj;
    [SerializeField] GameObject slowArrowObj;
    [SerializeField] GameObject explosiveArrowObj;
    [SerializeField] GameObject emmiter;
    private Animator animator;
    private AudioManager audioManager;

    private Quaternion startingRotation;

    public enum arrowTypes
    {
        Regular,
        Fire,
        Slow,
        Explosion
    }

    private arrowTypes currentType;

    private float charge;
    [SerializeField] float chargeMax;
    [SerializeField] float chargeRate;

    [SerializeField] int maxArrows = 10;
    private int currentRegularArrows;
    private int currentFireArrows;
    private int currentSlowArrows;
    private int currentExplosiveArrows;

    private bool charging;
    private bool canShoot = true;

    public void SetCharging(bool charging)
    {
        this.charging = charging;
    }

    void Start()
    {
        charging = false;
        charge = 0f;
        currentType = arrowTypes.Regular;
        currentRegularArrows = maxArrows;
        currentFireArrows = maxArrows;
        currentSlowArrows = maxArrows;
        currentExplosiveArrows = maxArrows;
        UIManager.GetInstance().SetMaxAmmo(maxArrows);
        UIManager.GetInstance().SetCurrentAmmo(currentRegularArrows);
        startingRotation = transform.localRotation;
        animator = GetComponent<Animator>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    void Update()
    {
        if (charging && charge < chargeMax)
        {
            charge += Time.deltaTime * chargeRate;
        }

        switch(currentType)
        {
            case arrowTypes.Fire:
                UIManager.GetInstance().SetCurrentAmmo(currentFireArrows);
                break;

            case arrowTypes.Regular:
                UIManager.GetInstance().SetCurrentAmmo(currentRegularArrows);
                break;

            case arrowTypes.Slow:
                UIManager.GetInstance().SetCurrentAmmo(currentSlowArrows);
                break;
                
            case arrowTypes.Explosion:
                UIManager.GetInstance().SetCurrentAmmo(currentExplosiveArrows);
                break;
        }

        if(GetComponentInParent<PlayerMovement>().GetMoving())
        {
            animator.SetBool("Moving", true);
        }
        else
        {
            animator.SetBool("Moving", false);
        }
    }

    public bool CanShoot()
    {
        if ((currentType == arrowTypes.Explosion && currentExplosiveArrows > 0) || (currentType == arrowTypes.Fire && currentFireArrows > 0) ||
                    (currentType == arrowTypes.Regular && currentRegularArrows > 0) || (currentType == arrowTypes.Slow && currentSlowArrows > 0))
        {
            if (!UIManager.GetInstance().GetArrowMenuState())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public void Fire()
    {
        if(CanShoot())
        {
            StartCoroutine(FireBehaviour());
            animator.SetTrigger("Firing");
            audioManager.Play("ShurikenShoot");
        }
        else
        {
            return;
        }
    }

    private IEnumerator FireBehaviour()
    {
        if(canShoot)
        {
            switch (currentType)
            {
                case arrowTypes.Regular:
                    currentRegularArrows--;
                    UIManager.GetInstance().SetCurrentAmmo(currentRegularArrows);
                    InstantiateArow(regularArrowObj);
                    break;

                case arrowTypes.Fire:
                    currentFireArrows--;
                    UIManager.GetInstance().SetCurrentAmmo(currentFireArrows);
                    InstantiateArow(fireArrowObj);
                    break;

                case arrowTypes.Explosion:
                    currentExplosiveArrows--;
                    UIManager.GetInstance().SetCurrentAmmo(currentExplosiveArrows);
                    InstantiateArow(explosiveArrowObj);
                    break;

                case arrowTypes.Slow:
                    currentSlowArrows--;
                    UIManager.GetInstance().SetCurrentAmmo(currentSlowArrows);
                    InstantiateArow(slowArrowObj);
                    break;
            }
            charging = false;
            charge = 0f;
            canShoot = false;

            yield return new WaitForSeconds(1f);

            canShoot = true;
        }
    }

    private void InstantiateArow(GameObject arrowType)
    {
        Ray ray = GameObject.Find("Main Camera").GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(1000f);
        }

        Rigidbody arrow = Instantiate(arrowType, emmiter.transform.position, emmiter.transform.rotation).GetComponent<Rigidbody>();
        arrow.AddForce((targetPoint - emmiter.transform.position).normalized * charge, ForceMode.Impulse);
    }

    public void SetArrowMenuState(bool state)
    {
        UIManager.GetInstance().SetArrowMenuState(state);
    }

    public void SetCurrentArrow(string name)
    {
        switch(name)
        {
            case "Fire":
                currentType = arrowTypes.Fire;
                UIManager.GetInstance().SetCurrentAmmo(currentFireArrows);
                break;

            case "Regular":
                currentType = arrowTypes.Regular;
                UIManager.GetInstance().SetCurrentAmmo(currentRegularArrows);
                break;

            case "Slow":
                currentType = arrowTypes.Slow;
                UIManager.GetInstance().SetCurrentAmmo(currentSlowArrows);
                break;
                
            case "Explosive":
                currentType = arrowTypes.Explosion;
                UIManager.GetInstance().SetCurrentAmmo(currentExplosiveArrows);
                break;
        }
    }

    public void SetInactive()
    {
        transform.localRotation = startingRotation;
        gameObject.SetActive(false);
    }
}
