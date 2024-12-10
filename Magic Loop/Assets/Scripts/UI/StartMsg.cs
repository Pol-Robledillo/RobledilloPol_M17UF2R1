using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMsg : MonoBehaviour
{
    public float timeToWait = 1f;
    void Start()
    {
        StartCoroutine(ShowStartMsg());
    }
    private IEnumerator ShowStartMsg()
    {
        yield return new WaitForSeconds(timeToWait);
        gameObject.SetActive(false);
    }
}
