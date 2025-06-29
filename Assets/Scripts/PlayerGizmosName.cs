using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR

public class PlayerGizmosName : MonoBehaviour
{
    [SerializeField, Tooltip("–асположение тега по отношению к »гроку")]
    private Vector3 _tagOffset = new Vector3(0, 1.5f, 0);

    // —тиль текста
    private GUIStyle style;

    private void OnDrawGizmos()
    {
        // »нициализаци€ стил€ текста - ѕочему не в Aweke?
        // ѕотому что √измос в редакторе через Awake отображатьс€ будет только после запуска игрового режима.
        // ’от€ конечно через Awake было бы производительней, так как стиль задавалс€ бы всего 1 раз.
        style = new GUIStyle();
        style.normal.textColor = Color.yellow;
        style.alignment = TextAnchor.MiddleCenter;

        // ќтоброжени€ текста с преобразованием локальных кординат в мировые
        Handles.Label(transform.TransformPoint(_tagOffset), "Player", style);
    }
}
#endif