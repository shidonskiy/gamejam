using System;
using GameJam.Scripts.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameJam.Scripts.UI.Elements
{
    public class LevelElement : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _levelNameText;
        [SerializeField] private Button _button;

        public event Action<int> LevelSelected;

        private int _levelId;

        private void Awake()
        {
            _button.onClick.AddListener(InvokeLevelSelected);
        }

        public void Setup(LevelModel levelModel)
        {
            _levelNameText.text = levelModel.LevelName;
            _levelId = levelModel.LevelId;
        }

        private void InvokeLevelSelected()
        {
            LevelSelected?.Invoke(_levelId);
        }
    }
}