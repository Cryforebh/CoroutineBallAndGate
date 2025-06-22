using UnityEngine;

public class Rotator : MonoBehaviour
{
    [Header("Настройка скорости угла вращения")]
    [SerializeField]
    private Vector3 _rotate; // Углы вращения (настраивается в инспекторе)

    private Rigidbody _rb;

    private void Start()
    {
        // Получаем физическое тело объекта
        _rb = GetComponent<Rigidbody>();

        // Проверяем наличие Rigidbody
        if (_rb == null)
        {
            Debug.LogError("Rigidbody не найден! Добавьте компонент Rigidbody к объекту.");
            return;
        }

        // Устанавливаем тело как кинематическое
        _rb.isKinematic = true;
    }

    private void FixedUpdate()
    {
        // Вращение через физический движок
        if (_rb != null)
        {
            Quaternion deltaRotation = Quaternion.Euler(_rotate * Time.fixedDeltaTime);
            _rb.MoveRotation(_rb.rotation * deltaRotation);
        }
    }
}
