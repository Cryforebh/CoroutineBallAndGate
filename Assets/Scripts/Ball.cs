using UnityEngine;

[RequireComponent(typeof(Rigidbody))] // Гарантируем наличие Rigidbody
public class Ball : MonoBehaviour
{
    private Rigidbody _rb;
    private Vector3 _originalScale;

    public Vector3 OriginalScale => _originalScale;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        // Сохраняем оригинальный размер
        _originalScale = transform.localScale;
    }

    public void Launch(Vector3 direction, float force)
    {
        _rb.isKinematic = false;
        _rb.velocity = direction * force;
    }

    public void SetKinematic(bool state) => _rb.isKinematic = state;

    public void SetLifetime(float lifetime) => Destroy(gameObject, lifetime);

    public void Realese() => Destroy(gameObject);
}
