using Interfaces.Models;

namespace Interfaces.Services
{
    public interface IMotorbikeService
    {
        void AddMotorbike(IMotorbike motorbike);
        void DeleteMotorbike(IMotorbike motorbike);
        void UpdateMotorbike(IMotorbike motorbike);
        int GetNewId(int id);

    }
}
