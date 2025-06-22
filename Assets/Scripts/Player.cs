using System.Collections;
using UnityEngine;

namespace Netologia.Homework
{
    public class Player : MonoBehaviour
    {
        [Header("Настройки мяча")]
        [SerializeField, Tooltip("Сюда нужно поместить Префаб Мяча")] 
        private Rigidbody _ballPrefab;
        [SerializeField, Tooltip("Начальная скорость Мяча")] 
        private float _startVelocity = 10f;
        [SerializeField, Tooltip("Время жизни Мяча")] 
        private float _lifetime = 3f;
        [SerializeField, Tooltip("Время респавна Мяча")] 
        private float _respawnDelay = 2f;
        [SerializeField, Tooltip("Начальная кордината спавна Мяча относительно игрока")] 
        private Vector3 _ballLocalOffset = new Vector3(0, 0, 2f);

        private bool _ready;
        private Rigidbody _ball;
        private Vector3 _originalBallScale;

        private void Update()
        {
            if (!_ready) return;

            // Обновляем позицию мяча относительно игрока
            UpdateBallPosition();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(ReloadCoroutine());
                LaunchBall();
            }
        }

        private void UpdateBallPosition()
        {
            if (_ball == null) return;

            // Расчитывается мировая позиция с учетом поворота игрока
            Vector3 worldOffset = transform.TransformDirection(_ballLocalOffset);
            _ball.transform.position = transform.position + worldOffset;

            // Сохраняется оригинальный поворот мяча
            _ball.transform.rotation = Quaternion.identity;
        }

        private void LaunchBall()
        {
            _ball.isKinematic = false;
            _ball.transform.parent = null;
            _ball.velocity = transform.forward * _startVelocity;
            Destroy(_ball.gameObject, _lifetime);
        }

        private IEnumerator ReloadCoroutine()
        {
            _ready = false;
            yield return new WaitForSeconds(_respawnDelay);
            SpawnNewBall();
        }

        private void SpawnNewBall()
        {
            // Создаем мяч независимо от родителя
            GameObject ballInstance = Instantiate(
                _ballPrefab.gameObject,
                transform.position + transform.TransformDirection(_ballLocalOffset),
                Quaternion.identity
            );

            _ball = ballInstance.GetComponent<Rigidbody>();
            _ball.isKinematic = true;

            // Фиксируем оригинальный масштаб
            ballInstance.transform.localScale = _originalBallScale;

            _ready = true;
        }

        private void Start()
        {
            // Сохраняем оригинальный размер из префаба
            _originalBallScale = _ballPrefab.transform.localScale;
            SpawnNewBall();
        }
    }
}