using System;
using Runtime.Data;
using Spine;
using Spine.Unity;
using UnityEngine;
using UnityTimer;

namespace Runtime.System
{
    public partial class SkillSystem
    {
        /// <summary>
        /// 技能伤害倍率
        /// </summary>
        private const float AtkRate = 2;
        private void CreateBullet(SkillData skillData, HeroEntity entity, PointDetectComponent pointDetectComponent)
        {
            GameObject go = CreateSkillGo(entity, skillData);
            HeroData heroData = entity.GetHeroData();
            BulletEntity bulletEntity = EntitySystem.Instance.CreateEntity<BulletEntity>(EntityType.BulletEntity, go);
            bulletEntity.InitBullet(EntityType.EnemyEntity, (int)(heroData.atk * AtkRate), BulletAttributeType.Penetrate,
                entity.GetFireLocation(), BattleManager.Instance.GetBulletParent());

            var point = pointDetectComponent.GetTarget().position;

            bulletEntity.AllComponentList.Add(new BulletMoveComponent(bulletEntity.GetComponent<RectTransform>(),
                point, 800f, BulletMoveType.RectilinearMotion, 2000f));

            bulletEntity.AllComponentList.Add(new DelayedDeadComponent(3f, bulletEntity));
        }

        private void CreateFall(SkillData skillData, HeroEntity entity, PointDetectComponent pointDetectComponent)
        {
            GameObject go = CreateSkillGo(entity, skillData);
            var point = pointDetectComponent.GetTarget().position;
            HeroData heroData = entity.GetHeroData();
            go.transform.SetParent(BattleManager.Instance.GetBulletParent());
            go.transform.localScale = Vector3.one;
            go.transform.position = point;
            Timer.Register(skillData.damageDelay, () =>
            {
                foreach (EnemyEntity enemyEntity in EntitySystem.Instance.GetAllEnemyEntity())
                {
                    float distance = Vector3.Distance(((RectTransform)go.transform).anchoredPosition, ((RectTransform)enemyEntity.transform).anchoredPosition);
                    if(distance <= skillData.damageRange)
                    {
                        enemyEntity.GetSpecifyComponent<EnemyStatusComponent>(ComponentType.StatusComponent).Hit((int)(entity.GetHeroData().atk * AtkRate));
                    }
                }
                Destroy(go);
            });
        }

        private void CreateHideRange(SkillData skillData, HeroEntity entity, PointDetectComponent pointDetectComponent)
        {
            GameObject go = CreateSkillGo(entity, skillData);
            entity.gameObject.SetActive(false);
            var point = pointDetectComponent.GetTarget().position;
            go.transform.SetParent(BattleManager.Instance.GetBulletParent());
            go.transform.localScale = Vector3.one;
            go.transform.position = point;
            SkeletonGraphic graphic = go.GetComponentInChildren<SkeletonGraphic>();
            Timer.Register(graphic.SkeletonData.Animations.Items[0].Duration * .9f, () =>
            {
                foreach (EnemyEntity enemyEntity in EntitySystem.Instance.GetAllEnemyEntity())
                {
                    float distance = Vector3.Distance(((RectTransform)go.transform).anchoredPosition, ((RectTransform)enemyEntity.transform).anchoredPosition);
                    if(distance <= skillData.damageRange)
                    {
                        enemyEntity.GetSpecifyComponent<EnemyStatusComponent>(ComponentType.StatusComponent).Hit((int)(entity.GetHeroData().atk * AtkRate));
                    }
                }
                Destroy(go);
                entity.gameObject.SetActive(true);
            });
        }

        private void CreateSelf(SkillData skillData, HeroEntity entity)
        {
            GameObject go = CreateSkillGo(entity, skillData);
            go.transform.localScale = Vector3.one;
            go.transform.localPosition = Vector3.zero;
            Destroy(go, 5);
        }

        private void CreateThrough(SkillData skillData, HeroEntity entity, PointDetectComponent pointDetectComponent)
        {
            SkeletonGraphic skeletonGraphic = CreateSkeletonGraphic(skillData, BattleManager.Instance.GetBulletParent());
            skeletonGraphic.rectTransform.localPosition = entity.GetFireLocation();

            Vector2 direction = pointDetectComponent.GetTarget().position - skeletonGraphic.rectTransform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            skeletonGraphic.rectTransform.rotation = Quaternion.Euler(0f, 0f, angle + skeletonGraphic.rectTransform.eulerAngles.z);
            skeletonGraphic.rectTransform.localScale = new Vector3(3, 3, 3);

            HeroData heroData = entity.GetHeroData();
            Vector3 p1 = skeletonGraphic.transform.position;
            Vector3 p2 = pointDetectComponent.GetTarget().position;

            var list = Physics2D.RaycastAll(p1, p2 - p1, 1000);
            foreach (RaycastHit2D raycastHit in list)
            {
                EnemyEntity enemyEntity = raycastHit.transform.GetComponent<EnemyEntity>();
                if(enemyEntity)
                {
                    enemyEntity.GetSpecifyComponent<EnemyStatusComponent>(ComponentType.StatusComponent).Hit((int)(heroData.atk * AtkRate));
                }
            }
        }
    }
}