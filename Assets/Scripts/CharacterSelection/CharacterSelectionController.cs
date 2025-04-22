using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectionController : MonoBehaviour
{
    public int selectedCharacterID => _selectedCharacterID;
    private int _selectedCharacterID = 1;

    private CharacterSelectionView _characterSelectionView;

    private const string SelectedCharacterKey = "SelectedCharacterID";

    private void Awake()
    {
        LoadLastSelectedCharacter();
    }

    private void Start()
    {
        _characterSelectionView = GetComponent<CharacterSelectionView>();
        
        _characterSelectionView.ControllCharModels(_selectedCharacterID);
    }

    private void LoadLastSelectedCharacter()
    {
        // Спроба завантажити збережений ID
        if (PlayerPrefs.HasKey(SelectedCharacterKey))
        {
            _selectedCharacterID = PlayerPrefs.GetInt(SelectedCharacterKey);
        }
        else
        {
            _selectedCharacterID = 1; // За замовчуванням перший персонаж
        }
    }
    public void PlayerSelected(int characterID) // Викликається з ShopController
    {
        _selectedCharacterID = characterID;
        
        // Зберігаємо у PlayerPrefs
        PlayerPrefs.SetInt(SelectedCharacterKey, _selectedCharacterID);
        PlayerPrefs.Save();

        _characterSelectionView.ControllCharModels(_selectedCharacterID);
    }
}