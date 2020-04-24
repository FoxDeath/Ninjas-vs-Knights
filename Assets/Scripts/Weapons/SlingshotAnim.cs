using UnityEngine;

public class SlingshotAnim : MonoBehaviour
{
    private static Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public static void DoAnimation(bool state)
    {
        animator.SetBool("Equipped", state);
    }
}
