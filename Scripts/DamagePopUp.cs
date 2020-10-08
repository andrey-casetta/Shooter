using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamagePopUp : MonoBehaviour
{
    private Text _textMesh;
    private Color textColor;
    private Color initialColor;
    private float disappearTimer;
    private Vector3 moveVector;
    private const float DISAPPEAR_TIMER_MAX = 1f;

    public static GameObject Create(Vector3 position, int damageAmount)
    {
        GameObject dmgPopUpGO = ObjectPoolerManager.instance.GetPooledObject(PoolObjectType.PopUpText);
        DamagePopUp dmgPopup = dmgPopUpGO.GetComponent<DamagePopUp>();
        dmgPopUpGO.transform.position = position;
        dmgPopup.Setup(damageAmount);
        return dmgPopUpGO;
    }

    private void OnEnable()
    {
        disappearTimer = DISAPPEAR_TIMER_MAX;
        moveVector = new Vector3(.7f, 1) * 10f;
        transform.localScale = new Vector3(1, 1, 1);
        textColor.a = 1;
        _textMesh.color = initialColor;
    }

    private void Awake()
    {
        _textMesh = transform.GetChild(0).transform.GetChild(0).GetComponent<Text>();
        textColor = _textMesh.color;
        initialColor = _textMesh.color;
    }

    private void Update()
    {
        transform.position += moveVector * Time.deltaTime;
        moveVector -= moveVector * 8f * Time.deltaTime;

        if (disappearTimer > DISAPPEAR_TIMER_MAX * .5f)
        {
            float increaseScaleAmount = 1f;
            transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;
        }
        else
        {
            float decreaseScaleAmount = 1f;
            transform.localScale -= Vector3.one * decreaseScaleAmount * Time.deltaTime;
        }

        disappearTimer -= Time.deltaTime;
        if (disappearTimer < 0)
        {
            float disappearSpeed = 2f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            _textMesh.color = textColor;
            if (textColor.a < 0)
            {
                ObjectPoolerManager.instance.CoolObject(this.gameObject, PoolObjectType.PopUpText);
            }
        }
    }


    public void Setup(int damageAmount)
    {
        _textMesh.text = damageAmount.ToString();
    }
}
