using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goalkeeper : MonoBehaviour
{
    public static Goalkeeper instance;
    private Animator animator;
    private Vector3 startPos = new Vector3(12.5f, 1, 26);
    private void Awake()
    {
        instance = this;
        animator = GetComponent<Animator>();
    }
    public void SaveL()
    {
        animator.SetFloat("Blend", 0.5f);
        animator.SetTrigger("SaveL");
        Debug.Log("SaveL");
    }
    public void SaveR()
    {
        animator.SetFloat("Blend", 1f);
        animator.SetTrigger("SaveR");
        Debug.Log("SaveR");
    }
    public void Catch()
    {
        animator.SetTrigger("Catch");
    }    
    public void SetBack()
    {
        transform.position = startPos;
        animator.SetFloat("Blend", 0f);
    }
}
