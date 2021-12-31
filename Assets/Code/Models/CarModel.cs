using Code.Utils;

namespace Code.Models
{
    public sealed class CarModel
    {
        public ResourcePath ResourcePath = new ResourcePath() { PathResource = "Prefabs/Cars/Car" };
        public float Speed { get; }

        public CarModel(float speed)
        {
            Speed = speed;
        }
    }
}