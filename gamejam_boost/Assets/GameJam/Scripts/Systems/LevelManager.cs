using System.Collections;
using GameJam.Scripts.Levels;
using GameJam.Scripts.Misc;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameJam.Scripts.Systems
{
    public class LevelManager : AbstractManager
    {
        [SerializeField] private LevelsData _levelData;

        public LevelsData LevelsData => _levelData;
        
        private Coroutine _loadLevelRoutine = null;

        public void LoadLevel(int levelBuildId)
        {
            EventSink.OnLoadLevelStart();

            if (_loadLevelRoutine != null)
            {
                StopCoroutine(_loadLevelRoutine);
            }

            StartCoroutine(LevelLoadRoutine(levelBuildId));
        }

        private IEnumerator LevelLoadRoutine(int levelBuildId)
        {
            var result = SceneManager.LoadSceneAsync(levelBuildId);

            while (!result.isDone)
            {
                yield return null;
            }

            Level level = null;
            Scene scene = SceneManager.GetActiveScene();
            foreach (var root in scene.GetRootGameObjects())
            {
                if (root.TryGetComponent(out level))
                {
                    break;
                }
            }
            
            if(level != null)
            {
                EventSink.OnLoadLevelFinish(level);
            }
        }
        
    }
}