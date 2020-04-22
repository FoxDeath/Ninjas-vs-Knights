using UnityEngine;

public class SlingshotAnim : MonoBehaviour
{
    private static Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public static void DoAnimationTrue()
    {
        animator.SetBool("Equipped", true);
    }

    public static void DoAnimationFalse()
    {
        animator.SetBool("Equipped", false);
    }
}
