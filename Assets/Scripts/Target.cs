using System.Collections;
using UnityEngine;


//TO DO: Refactor(Radu)
public class Target : MonoBehaviour
{
    private Rigidbody myRigidbody;

    [SerializeField] float health = 50f;
    private AudioManager audioManager;
    public bool charged;
    private bool dead;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        myRigidbody = GetComponent<Rigidbody>();

        dead = false;
    }

    public void TakeDamage(float damage)
    {
        if(!dead)
        {
            audioManager.Play("Hit", GetComponent<AudioSource>());
            health -= damage;

            if (health <= 0f)
            {
                Die();
            }
            if (charged)
            {
                StartCoroutine("ResetCharged");
            }
        }
    }

    IEnumerator ResetCharged()
    {
        yield return new WaitForSeconds(2.5f);
        charged = false;
    }

    void Die()
    {
        dead = true;
        EndLevel.killedEnemies++;
        audioManager.Play("EnemyDying", GetComponent<AudioSource>());
        Destroy(gameObject, 1f);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name.Equals("ShurikenModel"))
        {
            Vector3 force = transform.localPosition - other.transform.localPosition;
            myRigidbody.velocity = new Vector3(other.transform.right.x * force.x, other.transform.up.y * force.y, other.transform.forward.z * force.z);
        }
    }
}
