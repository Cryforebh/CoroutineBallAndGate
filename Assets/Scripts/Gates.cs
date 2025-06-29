using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Collider))]
    public class Gates : MonoBehaviour
    {
        [SerializeField, ReadOnly]
        private int _numberOfHits = 0;

        private Collider _gatesCollider;

        public int NumberOfHits => _numberOfHits;

        private void Awake()
        {
            _gatesCollider = GetComponent<Collider>();
            // Автоматически делаем коллайдер триггером
            if (_gatesCollider != null)
            {
                _gatesCollider.isTrigger = true;
            }
            else
            {
                Debug.LogError("Коллайдер не установлен!");
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Ball>(out var ball))
            {
                ball.Realese();
                _numberOfHits += 1;
            }
        }
    }
}
