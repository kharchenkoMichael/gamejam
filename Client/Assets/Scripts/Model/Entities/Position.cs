using UnityEngine;

namespace Assets.Scripts.Model.Entities
{
    public class Position
    {
        public float PositionX { get; set; }
        public float PositionY { get; set; }
        public float PositionZ { get; set; }

        public Position(float x, float y, float z)
        {
            PositionX = x;
            PositionY = y;
            PositionZ = z;
        }
        
        public void Update(Vector3 position)
        {
            PositionX = position.x;
            PositionY = position.y;
            PositionZ = position.z;
        }
    }
}
