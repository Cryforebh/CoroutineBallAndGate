using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MoverCustom : MonoBehaviour
{
    [Header("Настройки движения")]
    [SerializeField, Tooltip("Начало движения - кордината")]
    private Vector3 _startLocal = new Vector3(-4f, 0f, 0f);
    [SerializeField, Tooltip("Конец движения - кордината")]
    private Vector3 _endLocal = new Vector3(4f, 0f, 0f);
    [SerializeField, Tooltip("Скорость движения")]
    private float _speed = 1f;
    [SerializeField, Tooltip("Задержка перед изменением направления движения")]
    private float _delay = 2f;

    private Rigidbody _rb;
    private bool _movingToEnd = true;
    private Vector3 _worldStart; // Фиксированные позиции
    private Vector3 _worldEnd;   // при старте игры

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.isKinematic = true;

        // Фиксируются стартовые позиции
        _worldStart = transform.TransformPoint(_startLocal);
        _worldEnd = transform.TransformPoint(_endLocal);

        StartCoroutine(MovementRoutine());
    }

    private IEnumerator MovementRoutine()
    {
        while (true)
        {
            Vector3 target = _movingToEnd ? _worldEnd : _worldStart;

            while (Vector3.Distance(_rb.position, target) > 0.01f)
            {
                _rb.MovePosition(Vector3.MoveTowards(_rb.position, target, _speed * Time.deltaTime));
                yield return new WaitForFixedUpdate();
            }

            yield return new WaitForSeconds(_delay);
            _movingToEnd = !_movingToEnd;
        }
    }

#if UNITY_EDITOR

    [Header("Настройки Gizmos")]
    [SerializeField, Tooltip("Цвет линии движения")]
    private Color _gizmoColor = Color.yellow;
    [SerializeField, Tooltip("Радиус точек по бокам")]
    private float _sphereRadius = 0.4f;

    private void OnDrawGizmos()
    {
        Gizmos.color = _gizmoColor;

        // В редакторе показывает текущие точки

        if (!Application.isPlaying)
        {
            Gizmos.DrawSphere(transform.TransformPoint(_startLocal), _sphereRadius);
            Gizmos.DrawSphere(transform.TransformPoint(_endLocal), _sphereRadius);
            Gizmos.DrawLine(transform.TransformPoint(_startLocal), transform.TransformPoint(_endLocal));
            return;
        }


        // Во время игрового режима показывает зафиксированные точки
        Gizmos.DrawSphere(_worldStart, _sphereRadius);
        Gizmos.DrawSphere(_worldEnd, _sphereRadius);
        Gizmos.DrawLine(_worldStart, _worldEnd);
    }
#endif
}
