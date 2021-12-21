using Code.Utils;

namespace Code.Models
{
    public sealed class CarModel
    {
        public ResourcePath ResourcePath = new ResourcePath() { PathResource = "Prefabs/Car" };
        public float Speed { get; }

        public CarModel(float speed)
        {
            Speed = speed;
        }
    }
}