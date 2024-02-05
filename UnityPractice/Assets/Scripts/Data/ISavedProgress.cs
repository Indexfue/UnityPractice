using Data;

namespace UnityPractice.Character
{
    public interface ISavedProgress : ISavedProgressReader
    {
        public void SaveProgress(PlayerProgress playerProgress);
    }
}