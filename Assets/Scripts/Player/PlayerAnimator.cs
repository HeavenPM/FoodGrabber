using System.Collections;
using DG.Tweening;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private const string DANCE = "Dance";
    private const string GRAB = "Grab";
    private const string FAIL = "Fail";

    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _finalCamera;
    [SerializeField] private Transform _headAimIKTarget;

    private void Start()
    {
        _finalCamera.gameObject.SetActive(false);
    }

    private void SetAnimation(string animationName)
    {
        _animator.SetTrigger(animationName);
    }

    private void OnEnable()
    {
        EventManager.GrabAnimationTriggered += EventGrab;
        EventManager.FailAnimationTriggered += EventFail;
        EventManager.DanceAnimationTriggered += EventDance;
    }

    private void OnDisable()
    {
        EventManager.GrabAnimationTriggered -= EventGrab;
        EventManager.FailAnimationTriggered -= EventFail;
        EventManager.DanceAnimationTriggered -= EventDance;
    }

    private void BackToStartPosition()
    {
        Vector3 initialPosition = new(0f, 0f, 0f);
        float backTime = 1f;
        transform.DOLocalMove(initialPosition, backTime);
        _finalCamera.SetActive(true);
        _headAimIKTarget.position = _finalCamera.transform.position;
    }

    private IEnumerator SetAnimationWithDelayAndBackToStart(string animationName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SetAnimation(animationName);
        BackToStartPosition();
    }

    private void EventDance()
    {
        StartCoroutine(SetAnimationWithDelayAndBackToStart(DANCE, 1f));            
    }

    private void EventFail()
    {
        StartCoroutine(SetAnimationWithDelayAndBackToStart(FAIL, 1f));
    }

    private void EventGrab()
    {
        SetAnimation(GRAB);
    }
}
