namespace GameJam.Scripts.Models
{
    public class LevelModel
    {
        public string LevelName;
        public int LevelId;

        public LevelModel(string levelName, int levelId)
        {
            LevelName = levelName;
            LevelId = levelId;
        }
    }
}