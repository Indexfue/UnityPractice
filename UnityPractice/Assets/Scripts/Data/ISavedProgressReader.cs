using Data;

namespace UnityPractice.Character
{
    public interface ISavedProgressReader
    {
        void Load(PlayerProgress playerProgress);
    }
}