using TMPro;
using UnityEngine;

public class UILevelViewer : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private LevelSettings _levelSettings;

    private void Start()
    {
        _text.text = "Level " + _levelSettings.CurrentLevel.ToString();
    }
}
