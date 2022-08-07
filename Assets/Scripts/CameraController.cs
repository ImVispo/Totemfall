using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Space]
    [Header("Settings")]
    [SerializeField] private Transform _target;
    public float cameraSpeed;

    private void FixedUpdate()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 newPos;
        newPos.x = Mathf.Lerp(mousePos.x, _target.position.x, 0.8f);
        newPos.y = Mathf.Lerp(mousePos.y, _target.position.y, 0.8f);
        transform.position = Vector2.Lerp((Vector2)transform.position, newPos, cameraSpeed);
        Vector3 pos = transform.position;
        pos.z = -10;
        transform.position = pos;
    }
}