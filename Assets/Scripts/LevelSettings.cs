using System;
using UnityEngine;

public class LevelSettings : MonoBehaviour
{
    public int CurrentLevel => _currentLevel;
    public float ItemBaseFrequency => _itemBaseFrequency;
    public float ItemFrequencyIncrease => _itemFrequencyIncrease;
    public int TaskCountOfItems => _taskCountOfItems;
    public Item.FoodTypes TaskFoodType => _taskFoodType;
    public int LivesCount => _livesCount;

    [SerializeField] private float _itemBaseFrequency;
    [SerializeField] private float _itemFrequencyIncrease;
    [SerializeField] private int _livesCount;

    private const string CURRENT_LEVEL = "CURRENT_LEVEL";
    private int _currentLevel = 1;
    private int _taskCountOfItems;
    private Item.FoodTypes _taskFoodType;

    private void Awake()
    {
        if (PlayerPrefs.HasKey(CURRENT_LEVEL)) _currentLevel = PlayerPrefs.GetInt(CURRENT_LEVEL);
        int minRandomInt = 1;
        int maxRandomInt = 6;
        _taskCountOfItems = UnityEngine.Random.Range(minRandomInt, maxRandomInt);
        _taskFoodType = GetRandomEnumValue<Item.FoodTypes>(); 
    }

    public static T GetRandomEnumValue<T>()
    {
        var values = Enum.GetValues(typeof(T));
        return (T)values.GetValue(new System.Random().Next(values.Length));
    }

    private void OnEnable()
    {
        EventManager.ItemReplacedInBasket += EventItemReplacedInBasket;
    }

    private void OnDisable()
    {
        EventManager.ItemReplacedInBasket -= EventItemReplacedInBasket;
    }

    private void EventItemReplacedInBasket(Item item)
    {
        if (item.FoodType == _taskFoodType)
        {
            _taskCountOfItems--;
            EventManager.OnTakenItemRight();
            if (_taskCountOfItems == 0)
            {
                EventManager.OnLevelPassed();
                EventManager.OnDanceAnimationTriggered();
                _currentLevel++;
                PlayerPrefs.SetInt(CURRENT_LEVEL, _currentLevel);
            }
        }
        else
        {
            _livesCount--;
            EventManager.OnTakenItemWrong();
            if (_livesCount == 0) {
                EventManager.OnLevelFailed();
                EventManager.OnFailAnimationTriggered();
            }
        } 
    }
}
