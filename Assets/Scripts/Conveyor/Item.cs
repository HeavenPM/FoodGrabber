using System.Collections;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class Item : MonoBehaviour
{
    public enum FoodTypes
    {
        Meat,
        Fruit,
        Vegetable,
        Cake
    }

    public FoodTypes FoodType => _foodType;

    [SerializeField] private FoodTypes _foodType;

    private int _currentLevel;
    private bool _isSelected = false;
    private bool _isDragging = false;
    private bool _wasKinematic = false;

    private LevelSettings _levelSettings;
    private float _baseFrequency;
    private float _frequencyIncrease;
    private float _conveyorSpeed;

    private PlayerGrabItem _playerGrabItem;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _playerGrabItem = FindObjectOfType<PlayerGrabItem>();
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.isKinematic = true;

        _levelSettings = FindObjectOfType<LevelSettings>();
        _currentLevel = _levelSettings.CurrentLevel;
        _baseFrequency = _levelSettings.ItemBaseFrequency;
        _frequencyIncrease = _levelSettings.ItemFrequencyIncrease;
        _conveyorSpeed = _baseFrequency / (1f + _frequencyIncrease * _currentLevel);
    }

    private void Update()
    {
        if (!_isDragging) MoveAlongConveyor();
    }

    private void MoveAlongConveyor()
    {
        Vector3 conveyorDirection = new(1f, 0f, 0f);
        transform.position += _conveyorSpeed * Time.deltaTime * conveyorDirection;
    }

    private void OnMouseDown()
    {
        if (!_playerGrabItem.IsGrabbing && !_isSelected)
        {
            _isSelected = true;
            EventManager.OnPlayerSelectedItem(this);
        } 
    }

    private void OnEnable()
    {
        EventManager.ItemReplacedInBasket += EventItemInBasket;
        EventManager.ItemTaken += EventThisItemTaken;
    }

    private void OnDisable()
    {
        EventManager.ItemReplacedInBasket -= EventItemInBasket;
        EventManager.ItemTaken -= EventThisItemTaken;
    }

    private void EventThisItemTaken ()
    {
        if(_isSelected) _isDragging = true;
    }

    private void EventItemInBasket(Item item)
    {
        if (_isSelected && !_wasKinematic)
        {
            _rigidbody.isKinematic = false;
            _wasKinematic = true;
            StartCoroutine(BecomeKinematic());
        }
    }

    private IEnumerator BecomeKinematic()
    {
        float timeToKinematic = 1f;
        yield return new WaitForSeconds(timeToKinematic);
        Basket basket = FindObjectOfType<Basket>();
        transform.SetParent(basket.transform, true);
        _rigidbody.isKinematic = true;
        GetComponent<BoxCollider>().enabled = false;
    }
}
