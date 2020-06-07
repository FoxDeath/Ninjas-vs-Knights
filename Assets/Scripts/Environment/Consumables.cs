using UnityEngine;

public class Consumables : MonoBehaviour
{
    private enum consumableTypes { HEALTH, GRENADE, KUNAI, AMMO }

    [SerializeField] consumableTypes type;

    private Mirror.NetworkTransformChild networkTransformChild;

    private Vector3 movementVector = new Vector3(0f, 1f, 0f);
    private Vector3 startingPos;

    [SerializeField] int ChangeAmount = 25;
    private float movementFactor;

    void Awake()
    {
        Transform closestSpawner = FindObjectsOfType<SpawnObject>()[0].transform;

        foreach(SpawnObject obj in FindObjectsOfType<SpawnObject>())
        {
            if(obj.transform.childCount == 0)
            {
                if(Vector3.Distance(transform.position, closestSpawner.position) > Vector3.Distance(transform.position, obj.transform.position))
                {
                    closestSpawner = obj.transform;
                }
            }
        }

        transform.SetParent(closestSpawner);
        networkTransformChild = transform.parent.gameObject.AddComponent<Mirror.NetworkTransformChild>();
        networkTransformChild.target = transform;
    }

    void OnDestroy()
    {
        Destroy(networkTransformChild);
    }

    void Start()
    {
        startingPos = transform.localPosition;
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            NetworkController networkController = FindObjectOfType<NetworkController>();

            if (type == consumableTypes.HEALTH && other.GetComponent<Health>().CanAddHealth())
            {
                other.GetComponent<Health>().Heal(ChangeAmount);
                FindObjectOfType<AudioManager>().NetworkPlay("Pickup", GetComponent<AudioSource>());
                networkController.NetworkDestroy(gameObject, 0f);
            }
            else if(type == consumableTypes.GRENADE && other.GetComponent<Slingshot>())
            {
                if(other.GetComponent<Slingshot>().CanAddGrenade())
                {
                    other.GetComponent<Slingshot>().AddGrenade(ChangeAmount);
                    FindObjectOfType<AudioManager>().NetworkPlay("Pickup", GetComponent<AudioSource>());
                    networkController.NetworkDestroy(gameObject, 0f);
                }
            }
            else if(type == consumableTypes.KUNAI && other.GetComponent<KunaiNadeInput>())
            {
                if(other.GetComponent<KunaiNadeInput>().CanAddKunai())
                {
                    other.GetComponent<KunaiNadeInput>().AddKunai(ChangeAmount);
                    FindObjectOfType<AudioManager>().NetworkPlay("Pickup", GetComponent<AudioSource>());
                    networkController.NetworkDestroy(gameObject, 0f);
                }
            }
            else if (type == consumableTypes.AMMO)
            {
                if(other.GetComponent<WeaponsInputNinja>())
                {
                    if(other.GetComponent<WeaponSwitch>().GetCurrentNinjaWeapon().ToString() == "ShurikenGun")
                    {
                        ShurikenGun shurikenGun = other.GetComponentInChildren<ShurikenGun>();

                        if(shurikenGun.CanAddAmmo())
                        {
                            shurikenGun.RestockAmmo();
                        }
                    }
                    else if(other.GetComponent<WeaponSwitch>().GetCurrentNinjaWeapon().ToString() == "Bow")
                    {
                        Bow bow = other.GetComponentInChildren<Bow>();

                        if (bow.CanAddAmmo())
                        {
                            bow.RestockAmmo();
                        }
                    }
                }
                else if(other.GetComponent<WeaponsInputKnight>())
                {
                    if (other.GetComponent<WeaponSwitch>().GetCurrentKnightWeapon().ToString() == "Crossbow")
                    {
                        CrossBow crossBow = other.GetComponentInChildren<CrossBow>();

                        if (crossBow.CanAddAmmo())
                        {
                            crossBow.RestockAmmo();
                        }
                    }
                    else if (other.GetComponent<WeaponSwitch>().GetCurrentKnightWeapon().ToString() == "SpearGun")
                    {
                        SpearGun spearGun = other.GetComponentInChildren<SpearGun>();

                        if (spearGun.CanAddAmmo())
                        {
                            spearGun.RestockAmmo();
                        }
                    }
                }

                FindObjectOfType<AudioManager>().NetworkPlay("Pickup", GetComponent<AudioSource>());
            }

            StartCoroutine(networkController.NetworkDestroy(gameObject, 0f));
        }
    }
}
