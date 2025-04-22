using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectionView : MonoBehaviour
{
    [Header("InPlayerGameObject")]
    
    [SerializeField] private GameObject[] _characters;

    public void ControllCharModels(int ID)
    {
        int charIndex = ID - 1;

        foreach (var character in _characters)
        {
            character.SetActive(false);
        }
        
        _characters[charIndex].SetActive(true);
    }
}
