using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPortal : MonoBehaviour
{
    public GameManager gameManager;
    private void OnEnable()
    {
        gameManager = GameManager.instance;
        gameManager.openPortal += OpenThisPortal;
        gameManager.OpenPortalEvent += OpenThisPortal;
    }
    public void OpenThisPortal()
    {
        gameObject.SetActive(true);
        gameManager.guideArrow.GetComponent<GuideArrow>().target = transform;
        gameManager.guideArrow.SetActive(true);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.SetActive(false);
            gameManager.GameOver();
        }
    }
}
