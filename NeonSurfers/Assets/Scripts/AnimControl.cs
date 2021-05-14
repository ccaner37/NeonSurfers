using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimControl : MonoBehaviour
{
    public Animator anim;

    public void GetAnimator(Animator animator)
    {
        anim = animator;
    }

    public void SlideAnim()
    {
        anim.SetBool("isSliding", true);
        StartCoroutine("isSliding");
    }

    public void JumpAnim()
    {
        anim.SetBool("isJumping", true);
        StartCoroutine("isJumping");
    }

    public void Block()
    {
        anim.SetTrigger("Stumble");
    }
    public void Die()
    {
        anim.SetBool("Die", true);
    }
    public void ChangeDirection()
    {
        anim.SetTrigger("ChangeDirection");
        StartCoroutine("StartDance");
    }

    IEnumerator isSliding()
    {
        yield return new WaitForSeconds(1.10f);
        anim.SetBool("isSliding", false);
    }

    IEnumerator isJumping()
    {
        yield return new WaitForSeconds(0.8f);
        anim.SetBool("isJumping", false);
    }

    IEnumerator StartDance()
    {
        yield return new WaitForSeconds(0.8f);
        anim.SetTrigger("Dance");
    }
}
