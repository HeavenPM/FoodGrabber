using UnityEngine;

public class UILivesViewer : MonoBehaviour
{
    [SerializeField] private LevelSettings _levelSettings;
    [SerializeField] private GameObject _livePrefab;
    [SerializeField] private Transform _livesPosition;
    [SerializeField] private float _livesPositionOffset;

    private GameObject[] _livesIcons;

    private void Start()
    {
        DrawLives(_levelSettings.LivesCount);
    }

    public void DrawLives(int livesCount)
    {
        if (_livesIcons != null)
        {
            foreach (GameObject lifeIcon in _livesIcons)
            {
                Destroy(lifeIcon);
            }
        }

        _livesIcons = new GameObject[livesCount];

        for (int i = 0; i < livesCount; i++)
        {
            GameObject lifeIcon = Instantiate(_livePrefab, _livesPosition);
            float xOffset = i * _livesPositionOffset;
            lifeIcon.transform.localPosition = new Vector3(xOffset, 0f, 0f);
            _livesIcons[i] = lifeIcon;
        }
    }

    private void OnEnable()
    {
        EventManager.TakenItemWrong += EventTakenWrongItem;
    }

    private void OnDisable()
    {
        EventManager.TakenItemWrong -= EventTakenWrongItem;
    }

    private void EventTakenWrongItem()
    {
        DrawLives(_levelSettings.LivesCount);
    }
}
