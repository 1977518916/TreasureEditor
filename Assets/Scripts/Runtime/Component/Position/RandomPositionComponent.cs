using UnityEngine;

namespace Runtime.Component.Position
{
    public class RandomPositionComponent : IRandomPositionComponent
    {

        public void Tick(float time)
        {

        }

        public void Release()
        {
            
        }

        public void RandomizePosition(RectTransform character)
        {
            RectTransform transform = character.parent as RectTransform;
            RectTransform child = character.GetChild(0) as RectTransform;
            character.anchoredPosition = transform.rect.size * .5f;
            character.anchoredPosition = new Vector2(character.anchoredPosition.x,
                Random.Range(-400,361));
        }

        public void BossPosition(RectTransform character)
        {
            RectTransform transform = character.parent as RectTransform;
            RectTransform child = character.GetChild(0) as RectTransform;
            character.anchoredPosition = transform.rect.size * .5f;
            character.anchoredPosition = new Vector2(character.anchoredPosition.x, 0);
        }
    }
}