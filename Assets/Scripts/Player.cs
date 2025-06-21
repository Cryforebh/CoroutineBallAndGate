using System.Collections;
using UnityEngine;

namespace Netologia.Homework
{
    public class Player : MonoBehaviour
    {
        // Булева переменная, указывающая на готовность игрока к запуску мяча
        private bool _ready;

        // Ссылка на текущий мяч (Rigidbody)
        private Rigidbody _ball;


        [SerializeField]
        // Префаб мяча, используемый для создания новых экземпляров
        private Rigidbody _ballPrefab;

        [SerializeField]
        // Начальная скорость мяча
        private float _startVelocity;

        [SerializeField]
        // Время жизни мяча
        private float _lifetime;

        [SerializeField]
        // Задержка перед респавном нового мяча
        private float _respawnDelay;


        // Вызывается каждый кадр. Проверяет, готова ли игра к запуску мяча, и обрабатывает нажатие клавиши пробела для запуска мяча
        private void Update()
        {
            // Если не готовы, то метод завершается
            if (!_ready) return;

            // Если нажата клавиша пробела
            if (Input.GetKey(KeyCode.Space))
            {
                // Запускаем корутину Reloader()
                StartCoroutine(Reloader());

                // Освобождаем мяч от кинематики
                _ball.isKinematic = false;

                // Отвязываем мяч от родительского объекта
                _ball.transform.parent = null;

                // Устанавливаем скорость мяча
                _ball.velocity = transform.forward * _startVelocity;

                // Уничтожаем мяч через _lifetime секунд
                Destroy(_ball.gameObject, _lifetime);
            }
        }

        // Корутина, которая устанавливает _ready в false, ждёт _respawnDelay секунд и затем вызывает метод Spawn() для создания нового мяча
        private IEnumerator Reloader()
        {
            // Устанавливаем _ready в false
            _ready = false;

            // Ждём _respawnDelay секунд
            yield return new WaitForSeconds(_respawnDelay);

            // Создаём новый мяч
            Spawn();
        }

        // Создаёт новый мяч с помощью Instantiate, устанавливает его свойства и устанавливает _ready в true
        private void Spawn()
        {
            // Создаём новый мяч с помощью _ballPrefab
            /* _ball = Instantiate(_ballPrefab, transform);
             */
            _ball = Instantiate(_ballPrefab, transform.position, transform.rotation);

            // Устанавливаем мяч как кинематический
            _ball.isKinematic = true;

            // Устанавливаем _ready в true
            _ready = true;
        }

        // Вызывается при запуске сцены и создаёт начальный мяч
        private void Start()
        {
            // Создаём начальный мяч
            Spawn();
        }
    }
}