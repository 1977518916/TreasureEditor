namespace Runtime.Component.Skill
{
    public interface SkillComponent : IComponent
    {
        /// <summary>
        /// 使用技能
        /// </summary>
        /// <param name="id">0小技能1大技能</param>
        public void UseSkill(int id);
    }
}