using DG.Tweening;
using TMPro;
using UnityEngine;

public class UIHintAnimation : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private float _moveDuration;
    [SerializeField] private float _fadeDuration;
    [SerializeField] private string[] _successHints;
    [SerializeField] private string[] _failureHints;

    private Vector3 _initialPosition; 

    private void Start()
    {
        _initialPosition = _text.rectTransform.position; 
        _text.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        EventManager.TakenItemRight += EventTakenRightItem;
        EventManager.TakenItemWrong += EventTakenWrongItem;
    }

    private void OnDisable()
    {
        EventManager.TakenItemRight -= EventTakenRightItem;
        EventManager.TakenItemWrong -= EventTakenWrongItem;
    }

    private void EventTakenRightItem()
    {
        int randomItemInteger = Random.Range(0, _successHints.Length);
        Color color = Color.green;
        MoveAndFadeText(_successHints[randomItemInteger], color);
    }

    private void EventTakenWrongItem()
    {
        int randomItemInteger = Random.Range(0, _failureHints.Length);
        Color color = Color.red;
        MoveAndFadeText(_failureHints[randomItemInteger], color);
    }

    private void MoveAndFadeText(string hintContent, Color color)
    {
        _text.text = hintContent;
        _text.color = color;
        _text.rectTransform.position = _initialPosition;
        _text.DOFade(1, 0);
        _text.gameObject.SetActive(true);

        Vector3 targetPosition = _text.rectTransform.position + Vector3.up * 200f;
        _text.rectTransform.DOMove(targetPosition, _moveDuration).OnComplete(FadeOutText);
    }

    private void FadeOutText()
    {
        _text.DOFade(0f, _fadeDuration).OnComplete(() => _text.gameObject.SetActive(false));
    }
}
