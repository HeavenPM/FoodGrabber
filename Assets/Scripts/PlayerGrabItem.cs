using UnityEngine;
using DG.Tweening;

public class PlayerGrabItem : MonoBehaviour
{
    public bool IsGrabbing => _isGrabbing;

    [SerializeField] private Transform _handIKTarget;
    [SerializeField] private Transform _headAimIKTarget;
    [SerializeField] private Transform _handBone;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _grabDistance;

    private Item _item;
    private bool _isGrabbing = false;
    private bool _isMoving = false;
    private float _itemPositionX;
    private readonly float _cutoffTime = 1f;

    private void Update()
    {
        if (_isGrabbing)
        {
            _headAimIKTarget.position = _item.transform.position;
            _handIKTarget.position = _item.transform.position;

            MovePlayerIfItemTooFar();
        }
    }

    private void MovePlayerIfItemTooFar()
    {
        if (!_isMoving)
        {
            float distanceToItem = Vector3.Distance(_handBone.position, _item.transform.position);

            if (_grabDistance < distanceToItem)
            {
                Vector3 newPosition = new Vector3(_itemPositionX, transform.position.y, transform.position.z);
                transform.DOMove(newPosition, _cutoffTime).OnComplete(() => _isMoving = false);
                _isMoving = true;
            }
        }
    }

    private void OnAnimationGrabbedItem()
    {
        _item.transform.SetParent(_handBone, true);
        EventManager.OnItemTaken();
    }

    private void OnAnimationStoredItem()
    {
        _item.transform.SetParent(null);
        _isGrabbing = false;
        EventManager.OnItemReplacedInBasket(_item);
    }

    private void OnEnable()
    {
        EventManager.PlayerSelectedItem += EventPlayerSelectedItem;
    }

    private void OnDisable()
    {
        EventManager.PlayerSelectedItem -= EventPlayerSelectedItem;
    }

    private void EventPlayerSelectedItem(Item item)
    {
        _isGrabbing = true;
        _item = item;
        _itemPositionX = _item.transform.position.x;

        float offsetX = 0.3f;
        float offsetY = 0.5f;
        float offsetZ = 0.5f;

        _handIKTarget.position = new(_item.transform.position.x + offsetX,
            _item.transform.position.y + offsetY,
            _item.transform.position.z + offsetZ);
        EventManager.OnGrabAnimationTriggered();
    }
}
