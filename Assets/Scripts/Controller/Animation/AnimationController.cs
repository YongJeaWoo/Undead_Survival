using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationController : MonoBehaviour
{
    private Animator animator = null;

    private E_AniState aniState = E_AniState.Stand;

    public float Speed
    {
        get => animator.speed;
        set => animator.speed = value;
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void AnimationPlay(E_AniState state)
    {
        if (aniState != state)
        {
            Speed = 1f;
            aniState = state;
            animator.Play(aniState.ToString(), 0, 0);
        }
    }
}
