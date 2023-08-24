using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationController : MonoBehaviour
{
    private Animator animator = null;

    private E_PlayerState playerState = E_PlayerState.Stand;
    private E_EnemyState enemyState = E_EnemyState.Run;

    public float Speed
    {
        get => animator.speed;
        set => animator.speed = value;
    }
    public Animator Animator
    {
        get => animator;
        set => animator = value;
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayerAnimation(E_PlayerState state)
    {
        if (playerState != state)
        {
            Speed = 1f;
            playerState = state;
            animator.Play(playerState.ToString(), 0, 0);
        }
    }

    public void EnemyAnimation(E_EnemyState state)
    {
        if (enemyState == E_EnemyState.Dead) return;

        if (enemyState != state)
        {
            Speed = 1f;
            enemyState = state;
            animator.Play(enemyState.ToString(), 0, 0);
        }
    }

    // 적 죽음 애니메이션 
    public void EnemyDeadAnimation()
    {
        Speed = 1f;
        enemyState = E_EnemyState.Dead;
        animator.Play(enemyState.ToString(), 0, 0);
    }
}
