using UnityEngine.Events;

public static class EventManager
{
    public static event UnityAction<Item> PlayerSelectedItem;
    public static event UnityAction<Item> ItemReplacedInBasket;
    public static event UnityAction ItemTaken;
    public static event UnityAction TakenItemRight;
    public static event UnityAction TakenItemWrong;
    public static event UnityAction LevelPassed;
    public static event UnityAction LevelFailed;
    public static event UnityAction GrabAnimationTriggered;
    public static event UnityAction DanceAnimationTriggered;
    public static event UnityAction FailAnimationTriggered;

    public static void OnPlayerSelectedItem(Item item) => PlayerSelectedItem?.Invoke(item);
    public static void OnItemReplacedInBasket(Item item) => ItemReplacedInBasket?.Invoke(item);
    public static void OnItemTaken() => ItemTaken?.Invoke();
    public static void OnTakenItemRight() => TakenItemRight?.Invoke();
    public static void OnTakenItemWrong() => TakenItemWrong?.Invoke();
    public static void OnLevelPassed() => LevelPassed?.Invoke();
    public static void OnLevelFailed() => LevelFailed?.Invoke();
    public static void OnGrabAnimationTriggered() => GrabAnimationTriggered?.Invoke();
    public static void OnDanceAnimationTriggered() => DanceAnimationTriggered?.Invoke();
    public static void OnFailAnimationTriggered() => FailAnimationTriggered?.Invoke();
}
