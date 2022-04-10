using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationScript : MonoBehaviour
{
    private Animator Animator;

    private void Awake()
    {
        Animator = GetComponent<Animator>();
    }
    public void Walk(bool walk)
    {
        Animator.SetBool(AnimationTags.walkParameter, walk);
    }
    public void Run(bool run)
    {
        Animator.SetBool(AnimationTags.runParameter, run);
    }
    public void Attack()
    {
        Animator.SetTrigger(AnimationTags.attackTrigger);
    }
    public void Dead()
    {
        Animator.SetTrigger(AnimationTags.deadTrigger);
    }
}
