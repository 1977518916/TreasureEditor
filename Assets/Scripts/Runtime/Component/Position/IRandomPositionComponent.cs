using UnityEngine;

namespace Runtime.Component.Position
{
    public interface IRandomPositionComponent : IComponent
    {
        void RandomizePosition(Transform character);
    }
}