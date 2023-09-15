using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootballGate : MonoBehaviour
{
    [SerializeField] private GameObject boomPrefab;
    public AudioClip goalSound;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<ball>(out ball ballScript))
        {
            if (!ballScript.GetIsInteracting())
            {
                ballScript.SetIsInteracting();
                GameManager.instance.GoalScored();
                GameObject newObject = Instantiate(boomPrefab);
                newObject.transform.position = other.transform.position;
                GetComponent<AudioSource>().PlayOneShot(goalSound);
                Shaker.instance.StartShaking();
                StartCoroutine(EffectCoroutine(newObject));
                StartCoroutine(ResetCoroutine());

            }
        }
    }
    private IEnumerator ResetCoroutine()
    {
        yield return new WaitForSeconds(1.5f);
        ball.instance.ResetBall();
    }
    private IEnumerator EffectCoroutine(GameObject obj)
    {
        yield return new WaitForSeconds(0.8f);
        Destroy(obj);
    }
}
