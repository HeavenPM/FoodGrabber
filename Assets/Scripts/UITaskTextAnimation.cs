using UnityEngine;
using TMPro;
using DG.Tweening;

public class UITaskTextAnimation : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private LevelSettings _levelSettings;
    [SerializeField] private float _delay;
    [SerializeField] private float _moveDuration;
    [SerializeField] private float _scaleDuration;
    [SerializeField] private Transform _endTextPosition;

    private void Start()
    {
        _text.text = _levelSettings.TaskCountOfItems.ToString() + " - " + _levelSettings.TaskFoodType.ToString();
        StartCoroutine(AnimateText());
    }

    private System.Collections.IEnumerator AnimateText()
    {
        yield return new WaitForSeconds(_delay);
        _text.rectTransform.DOMove(_endTextPosition.position, _moveDuration);

        Vector3 targetScale = Vector3.one * 0.6f;
        _text.rectTransform.DOScale(targetScale, _scaleDuration);
    }

    private void OnEnable()
    {
        EventManager.TakenItemRight += EventItemInBasket;
    }

    private void OnDisable()
    {
        EventManager.TakenItemRight -= EventItemInBasket;
    }

    private void EventItemInBasket()
    {
        _text.text = _levelSettings.TaskCountOfItems.ToString() + " - " + _levelSettings.TaskFoodType.ToString();
    }
}
