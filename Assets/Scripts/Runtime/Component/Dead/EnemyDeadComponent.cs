public class EnemyDeadComponent : DeadComponent
{
    public long EntityID { get; set; }
    
    public EnemyDeadComponent(long entityID)
    {
        EntityID = entityID;
    }
    
    public void Dead()
    {
        EntitySystem.Instance.EnemyDead(EntityID);
    }

    public void Tick(float time)
    {
        
    }

    public void Release()
    {

    }
}
