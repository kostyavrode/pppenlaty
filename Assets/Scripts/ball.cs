using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ball : MonoBehaviour
{
    public static ball instance;
    [SerializeField] private GameObject starEffect;
    [SerializeField] private AudioClip kickSound;
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private float force;
    [SerializeField] private Vector3 startPosition = new Vector3(12.479f, 1.443f, 16.3f);
    [SerializeField] private Transform targetForRotate;
    private Vector3 targetPosition;
    private float timer;
    private bool isKickStarted;
    private bool isInteracting;
    private void Awake()
    {
        instance = this;
        if (rigidbody==null)
        {
            rigidbody = GetComponent<Rigidbody>();
        }
    }
    private void Shoot()
    {
        if (!isInteracting)
        {
            GameManager.instance.BidBeforeKick();
            PlayAudio();
            StartCoroutine(starEffect.GetComponent<StartEffect>().deadCoroutine());
            targetPosition = (targetPosition - transform.position).normalized;
            rigidbody.AddForce(targetPosition * force + Vector3.up * 3, ForceMode.Impulse);
            int rand = Random.Range(0, 3);
            if (rand == 0)
            {
                Goalkeeper.instance.Catch();
            }
            if (rand == 1)
            {
                Goalkeeper.instance.SaveL();
            }
            if (rand == 2)
            {
                Goalkeeper.instance.SaveR();
            }
        }
    }
    public void PlayAudio()
    {
        GetComponent<AudioSource>().PlayOneShot(kickSound);
    }
    //private void OnMouseDown()
    //{
    //    isKickStarted = true;
    //}
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray,out hit))
            {
                if (hit.collider.TryGetComponent<ball>(out ball ballScript))
                {
                    isKickStarted = true;
                }
            }
        }
        if (Input.GetMouseButton(0))
        {
            if (isKickStarted)
            {
                targetForRotate.Rotate(new Vector3(90, 0, 0) * Time.deltaTime);
            }
        }
        if (Input.GetMouseButtonUp(0) && isKickStarted)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                targetPosition = hit.point;
            }
            Shoot();
            
        }
        if (isKickStarted)
        {
            timer += Time.deltaTime;
        }
        if (timer>4f && !isInteracting)
        {
            GameManager.instance.GoalNotScored();
            timer = 0;
        }
    }
    public void SetIsInteracting()
    {
        isInteracting = true;
    }
    public bool GetIsInteracting()
    {
        return isInteracting;
    }
    public void ResetBall()
    {
        timer = 0;
        Goalkeeper.instance.SetBack();
        rigidbody.isKinematic = true;
        transform.position = startPosition;
        rigidbody.velocity = Vector3.zero;
        rigidbody.isKinematic = false;
        isInteracting = false;
        isKickStarted = false;
        transform.eulerAngles = Vector3.zero;
        targetForRotate.eulerAngles = Vector3.zero;
        Debug.Log("ResetBal");
    }
}
