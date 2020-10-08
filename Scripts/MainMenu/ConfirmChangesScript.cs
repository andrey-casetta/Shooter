using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmChangesScript : MonoBehaviour
{
    [SerializeField]
    private Text countdownText;

    private float startCountdown = 14f;
    private Coroutine changeCoroutine;

    private void OnEnable()
    {
        changeCoroutine = StartCoroutine(ChangeCountdown());
        startCountdown = 14f;
        countdownText.text = startCountdown.ToString();
    }

    private IEnumerator ChangeCountdown()
    {
        yield return new WaitForSeconds(1);
        startCountdown--;
        countdownText.text = startCountdown.ToString();

        if (startCountdown > 0.5f)
        {
            changeCoroutine = StartCoroutine(ChangeCountdown());
        }
        else
        {
            StopCoroutine(changeCoroutine);
            startCountdown = 14f;
            this.gameObject.SetActive(false);
        }
    }
}
