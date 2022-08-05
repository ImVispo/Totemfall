using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    private TextMeshPro textMesh;
    private Color textColor;
    private float disappearTimer;
    private float DISSAPEAR_TIMER_MAX = 0.5f;
    private Vector3 moveVector;
    private static int sortingOrder;

    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }
    public void Setup(int damage, bool isCrit)
    {
        textMesh.SetText(damage.ToString());
        //if (isCrit)
        //{
        //    textMesh.fontSize = 4;
        //    textColor = new Color(255, 53, 0, 255);
        //}
        //else
        //{
        //    textMesh.fontSize = 3;
        //    textColor = new Color(255, 163, 0, 255);
        //}
        //textMesh.color = textColor;
        textColor = textMesh.color;
        disappearTimer = DISSAPEAR_TIMER_MAX;
        moveVector = new Vector3(1, 1) * 5f;

        sortingOrder++;
        textMesh.sortingOrder = sortingOrder;
    }

    private void Update()
    {
        float moveYSpeed = 1;
        transform.position += moveVector * Time.deltaTime;
        moveVector -= moveVector * 8f * Time.deltaTime;

        if (disappearTimer > DISSAPEAR_TIMER_MAX * 0.5f)
        {
            // First half of popup
            float increaseScaleAmount = 1f;
            transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;
        } else
        {
            // Second half of popup
            float decreaseScaleAmount = 1f;
            transform.localScale -= Vector3.one * decreaseScaleAmount * Time.deltaTime;
        }

        disappearTimer -= Time.deltaTime;
        if (disappearTimer > 0) return;
        float disappearSpeed = 5;
        textColor.a -= disappearSpeed * Time.deltaTime;
        textMesh.color = textColor;
        if (textColor.a < 0) Destroy(gameObject);

    }
}
