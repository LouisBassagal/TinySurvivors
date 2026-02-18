using UnityEngine;

class FireballAbility : AbilityBase
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int Level { get; set; }
    public int MaxLevel { get; set; }

    public void OnEnable()
    {
        Title = "Fireball";
        Description = "Launches a fireball that explodes on impact, dealing area damage.";
        Level = 1;
        MaxLevel = 5;
    }

    protected override void Tick()
    {
        throw new System.NotImplementedException();
    }

    protected override void OnLevelUp()
    {
        throw new System.NotImplementedException();
    }
}