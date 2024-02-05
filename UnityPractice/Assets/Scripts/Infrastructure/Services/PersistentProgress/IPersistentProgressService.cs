using Data;

namespace UnityPractice.Infrastructure.Services.PersistentProgress
{
    public interface IPersistentProgressService : IService
    {
        public PlayerProgress Progress { get; set; }    
    }
}