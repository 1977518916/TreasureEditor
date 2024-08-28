public class HeroDeadComponent : DeadComponent
{
    public long EntityID { get; set; }

    public HeroDeadComponent(long entityID)
    {
        EntityID = entityID;
    }

    public void Tick(float time)
    {
        
    }

    public void Release()
    {
        
    }
    
    public void Dead()
    {
        
    }
}
