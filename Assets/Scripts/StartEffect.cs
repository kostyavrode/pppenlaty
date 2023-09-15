using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartEffect : MonoBehaviour
{
    public ParticleSystem particleSystem;
    public static StartEffect instance;
    private void Awake()
    {
        instance = this;
        if (particleSystem==null)
        particleSystem.GetComponent<ParticleSystem>();
        particleSystem.Stop();
    }
    private void OnEnable()
    {
        //StartCoroutine(deadCoroutine());
        //particleSystem.Play();
    }
    private void Update()
    {
        transform.eulerAngles = new Vector3(0, -180f, 0);
    }
    public IEnumerator deadCoroutine()
    {
        particleSystem.Play();
        yield return new WaitForSeconds(0.25f);
        particleSystem.Stop();
    }
}
