using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationController : MonoBehaviour
{
    private Animator animator = null;

    private E_PlayerState aniState = E_PlayerState.Stand;

    public float Speed
    {
        get => animator.speed;
        set => animator.speed = value;
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayerAnimation(E_PlayerState state)
    {
        if (aniState != state)
        {
            Speed = 1f;
            aniState = state;
            animator.Play(aniState.ToString(), 0, 0);
        }
    }
}
