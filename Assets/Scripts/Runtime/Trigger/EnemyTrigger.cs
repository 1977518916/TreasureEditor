using System;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    public int attackValue;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Contains("Hero")) 
        {
            other.GetComponent<HeroEntity>().GetSpecifyComponent<HeroStatusComponent>(ComponentType.StatusComponent).Hit(attackValue);
            Destroy(gameObject);
        }
    }
}
