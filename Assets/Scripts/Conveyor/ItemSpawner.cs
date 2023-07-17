using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private float _baseSpawnFrequency;
    [SerializeField] private float _spawnFrequencyIncrease;
    [SerializeField] private Item[] _itemsPrefabs;

    private int _currentLevel;
    
    private void Start()
    {
        _currentLevel = FindObjectOfType<LevelSettings>().CurrentLevel;
        float frequencyOfCreation = _baseSpawnFrequency / (1f - _spawnFrequencyIncrease * _currentLevel);
        StartCoroutine(CreateItemAndWait(frequencyOfCreation));
    }

    private IEnumerator CreateItemAndWait(float frequencyOfCreation)
    {
        CreateInstanceOfRandomItem();
        yield return new WaitForSeconds(frequencyOfCreation);
        StartCoroutine(CreateItemAndWait(frequencyOfCreation));
    }

    private void CreateInstanceOfRandomItem()
    {
        int randomInteger = Random.Range(0, _itemsPrefabs.Length);
        Quaternion randomRotation = _itemsPrefabs[randomInteger].transform.rotation;

        Instantiate(_itemsPrefabs[randomInteger], transform.position, randomRotation);
    }
}
