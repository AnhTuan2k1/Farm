using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class NofiticationCanvas : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    public void Show(GameObject caller)
    {
        gameObject.SetActive(true);
        gameObject.transform.position = caller.transform.position;
        StartCoroutine(HideAfterDelay());
    }

    public void ShowNotification(GameObject caller, string content)
    {
        text.text = content;
        gameObject.SetActive(true);
        gameObject.transform.position = caller.transform.position;
        gameObject.transform.position 
            = new Vector3(caller.transform.position.x, caller.transform.position.y + 0.15f, caller.transform.position.z);
        StartCoroutine(HideAfterDelay());
    }

    private IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}
