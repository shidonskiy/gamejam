using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam.Scripts.Misc
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Data/LevelData", order = 1)]
    public class LevelsData : ScriptableObject
    {
        [Serializable]
        public struct LevelData
        {
            [SerializeField] private int _levelBuildId;
            [SerializeField] private string _levelName;

            public int LevelBuildId => _levelBuildId;
            public string LevelName => _levelName;
        }

        [SerializeField] private List<LevelData> _levels;

        public List<LevelData> Levels => _levels;
    }
}