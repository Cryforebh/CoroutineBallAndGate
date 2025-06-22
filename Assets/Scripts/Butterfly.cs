using System.Collections;
using UnityEngine;

public class Butterfly : MonoBehaviour
{
    [Header("Настройки Движения")]
    [SerializeField] private Vector3 _localStart = new Vector3(-4, 0, 0);
    [SerializeField] private Vector3 _localNetural = new Vector3(0, 0, 0);
    [SerializeField] private Vector3 _localEnd = new Vector3(4, 0, 0);
    [SerializeField] private float _speedMove = 2;
    [SerializeField] private float _delayStop = 1;

    [Header("Настройки Gizmos")]
    [SerializeField] private Color _gizmosColor = Color.yellow;
    [SerializeField] private float _sphereRadius = 0.4f;

    private Rigidbody _man;
    private bool _movingToEnd = true;
    private Vector3 _worldStart;
    private Vector3 _worldNetural;
    private Vector3 _worldEnd;

    private void Start()
    {
        _man = GetComponent<Rigidbody>();
        _man.isKinematic = true;

        _worldStart = transform.TransformPoint(_localStart);
        _worldNetural = transform.TransformPoint(_localNetural);
        _worldEnd = transform.TransformPoint(_localEnd);

        StartCoroutine(MoveCoroutine());
    }

    private IEnumerator MoveCoroutine()
    {
        Vector3 target;

        while (true)
        {
            target = _movingToEnd ? _worldEnd : _worldStart;

            while (Vector3.Distance(_man.position, _worldNetural) > 0.1f)
            {
                _man.MovePosition(Vector3.Lerp(_man.position, _worldNetural, _speedMove * Time.deltaTime));
                yield return new WaitForFixedUpdate();
            }

            while (Vector3.Distance(_man.position, target) > 0.1f)
            {
                _man.MovePosition(Vector3.Lerp(_man.position, target, _speedMove * Time.deltaTime));
                yield return new WaitForFixedUpdate();
            }

            yield return new WaitForSeconds(_delayStop);
            _movingToEnd = !_movingToEnd;
        }
    }
    private void OnDrawGizmosSelected()
{
        Gizmos.color = _gizmosColor;

#if UNITY_EDITOR
        if (!Application.isPlaying)
        {
            Gizmos.DrawSphere(transform.TransformPoint(_localStart), _sphereRadius);
            Gizmos.DrawSphere(transform.TransformPoint(_localNetural), _sphereRadius);
            Gizmos.DrawSphere(transform.TransformPoint(_localEnd), _sphereRadius);
            
            Gizmos.DrawLine(transform.TransformPoint(_localStart), transform.TransformPoint(_localNetural));
            Gizmos.DrawLine(transform.TransformPoint(_localNetural), transform.TransformPoint(_localEnd));
            return;
        }
#endif
        Gizmos.DrawSphere(_worldStart, _sphereRadius);
        Gizmos.DrawSphere(_worldNetural, _sphereRadius);
        Gizmos.DrawSphere(_worldEnd, _sphereRadius);

        Gizmos.DrawLine(_worldStart,_worldNetural);
        Gizmos.DrawLine(_worldNetural, _worldEnd);
    }
}
