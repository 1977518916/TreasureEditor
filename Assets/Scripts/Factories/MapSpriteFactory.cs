using System.Collections.Generic;
using System.Data;
using QFramework;
using UnityEngine;

namespace Factories
{
    public class MapSpriteFactory : Singleton<MapSpriteFactory>, IFactory
    {
        public Sprite Create(int index)
        {
            if(ResLoaderTools.TryGetMapSprite(index, out Sprite sprite))
            {
                return sprite;
            }
            List<Sprite> externalSprites = ResLoaderTools.GetAllExternalMap();
            if(index - 6 >= externalSprites.Count || index - 6 < 0)
            {
                throw new DataException($"There is no sprite with index {index}");
            }
            return externalSprites[index - 6];
        }
    }
}