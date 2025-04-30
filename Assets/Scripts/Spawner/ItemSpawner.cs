using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [Header("Список можливих предметів")]
    [SerializeField]private List<BaseRoadItem> availableItems;

    [Header("Точки дороги")]
    [SerializeField]private List<RoadPointController> roadPoints;
    
    private List<GameObject> spawnedItems = new List<GameObject>();

    [Header("Налаштування спавну")]
    private int minSpacing = 3;
    private int maxSpacing = 10;

    private GameController _gameController;
    private void Start()
    {
        StartCoroutine(SpawnItemsOnRoad());
        
        _gameController = ServiceLocator.GetService<GameController>();
        _gameController.OnResetGame += HandleReset;
    }

    private IEnumerator SpawnItemsOnRoad()
    {
        int index = 5;

        while (index < roadPoints.Count)
        {
            int spacing = Random.Range(minSpacing, maxSpacing + 1);
            index += spacing;

            if (index >= roadPoints.Count) break;

            BaseRoadItem prefab = availableItems[Random.Range(0, availableItems.Count)];
            BaseRoadItem instance = Instantiate(prefab);
            roadPoints[index].SetPointItem(instance);
            
            spawnedItems.Add(instance.gameObject);

            yield return null; // 1 frame delay
        }
    }

    private void HandleReset()
    {
        foreach (var item in spawnedItems)
        {
            Destroy(item);
        }
        
        StartCoroutine(SpawnItemsOnRoad());//spawn new items
    }

    private void OnDestroy()
    {
        _gameController.OnResetGame -= HandleReset;
    }
}