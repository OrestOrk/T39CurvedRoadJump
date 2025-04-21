using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [Header("Список можливих предметів")]
    [SerializeField]private List<BaseRoadItem> availableItems;

    [Header("Точки дороги")]
    [SerializeField]private List<RoadPointController> roadPoints;

    [Header("Налаштування спавну")]
    private int minSpacing = 5;
    private int maxSpacing = 30;

    private void Start()
    {
        StartCoroutine(SpawnItemsOnRoad());
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

            yield return null; // 1 frame delay
        }
    }
}