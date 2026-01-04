using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public Animator animator;
    public GameObject attackPoint;
    public float attackCooldown = 0.5f;

    private bool canAttack = true;

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (!context.performed || !canAttack)
            return;

        StartCoroutine(AttackRoutine());
    }

    private System.Collections.IEnumerator AttackRoutine()
    {
        canAttack = false;

        animator.SetTrigger("Attack");

        yield return new WaitForSeconds(attackCooldown);

        canAttack = true;
    }

    // DIPANGGIL DARI ANIMATION EVENT
    public void EnableHitbox()
    {
        attackPoint.SetActive(true);
    }

    public void DisableHitbox()
    {
        attackPoint.SetActive(false);
    }
}
