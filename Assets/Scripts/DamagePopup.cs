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
        if (isCrit)
        {
            textMesh.fontSize = 4;
            //textColor = Color.red;
            //textMesh.color = Color.red;
            textColor = new Color(1, 0.47f, 0, 1);
            textMesh.color = new Color(1, 0.47f, 0, 1);
        } else
        {
            textColor = textMesh.color;
        }
        disappearTimer = DISSAPEAR_TIMER_MAX;
        moveVector = new Vector3(1, 1) * 5f;

        sortingOrder++;
        textMesh.sortingOrder = sortingOrder;
    }

    private void Update()
    {
        transform.position += moveVector * Time.deltaTime;
        moveVector -= moveVector * 8f * Time.deltaTime;

        if (disappearTimer > DISSAPEAR_TIMER_MAX * 0.5f)
        {
            // First half of popup
            transform.localScale += Vector3.one * 1f * Time.deltaTime;
        } else
        {
            // Second half of popup
            transform.localScale -= Vector3.one * 1f * Time.deltaTime;
        }

        disappearTimer -= Time.deltaTime;
        if (disappearTimer > 0) return;
        textColor.a -= 5 * Time.deltaTime;
        textMesh.color = textColor;
        if (textColor.a < 0) Destroy(gameObject);

    }
}
