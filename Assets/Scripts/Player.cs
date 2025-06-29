using System.Collections;
using UnityEngine;

namespace Netologia.Homework
{
    public class Player : MonoBehaviour
    {
        [Header("Настройки мяча")]
        [SerializeField, Tooltip("Сюда нужно поместить Префаб Мяча")]
        private Ball _ballPrefab;
        [SerializeField, Tooltip("Начальная скорость Мяча")]
        private float _startVelocity = 80f;
        [SerializeField, Tooltip("Время жизни Мяча")]
        private float _lifetime = 3f;
        [SerializeField, Tooltip("Время респавна Мяча")]
        private float _respawnDelay = 2f;
        [SerializeField, Tooltip("Начальная кордината спавна Мяча относительно игрока")]
        private Vector3 _ballLocalOffset = new Vector3(0, 0, 1.4f);

        private Ball _currentBall;
        private bool _ready;

        private void Start() => SpawnNewBall();

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
            if (_currentBall == null) return;

            // Расчитывается мировая позиция с учетом поворота игрока
            Vector3 worldOffset = transform.TransformDirection(_ballLocalOffset);
            _currentBall.transform.position = transform.position + worldOffset;

            // Сохраняется оригинальный поворот мяча
            _currentBall.transform.rotation = Quaternion.identity;
        }

        private void LaunchBall()
        {
            _currentBall.Launch(transform.forward, _startVelocity);
            _currentBall.SetLifetime(_lifetime);
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
            _currentBall = Instantiate(
                _ballPrefab,
                transform.position + transform.TransformDirection(_ballLocalOffset),
                Quaternion.identity
            );

            _currentBall.SetKinematic(true);

            // Фиксируем оригинальный масштаб
            _currentBall.transform.localScale = _currentBall.OriginalScale;
            _ready = true;
        }
    }
}