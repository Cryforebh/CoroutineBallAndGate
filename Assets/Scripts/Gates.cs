using UnityEngine;

namespace Assets.Scripts
{
    public class Gates : MonoBehaviour
    {
        [SerializeField, ReadOnly]
        private int _score; // Текущий счет игрока

        public int Score => _score;

        private void Start()
        {
            // Автоматически делаем коллайдер триггером
            Collider collider = GetComponent<Collider>();
            if (collider != null)
            {
                collider.isTrigger = true;
            }
            else
            {
                Debug.LogError("Добавьте коллайдер к воротам!");
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            // Проверяем, является ли объект мячом
            Ball ball = other.GetComponent<Ball>();

            if (ball != null)
            {
                Destroy(ball.gameObject); // Уничтожаем мяч
                _score = _score + 10; // Увеличиваем счет | Если правильно понял "_score++" дороже чем "_score = _score + 10", поэтому написал так
                Debug.Log($"Счет: {_score}"); // Выводим в консоль
            }
        }
    }
}
