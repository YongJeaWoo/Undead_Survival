using SingletonComponent.Component;

public class GameManager : SingletonComponent<GameManager>
{
    private int exp = 0;
    private int maxExp = 100;
    private int level = 1;
    private int monsterKilled = 0;
    private float maxGameTime = 300f;
    private float currentTime = 0f;

    private void Update()
    {
        
    }

    public int GetExp() => exp;
    public int GetMaxExp() => maxExp;
    public int GetLevel() => level;
    public int GetKill() => monsterKilled;

    public void AddKill()
    {
        monsterKilled++;
    }

    public void AddExperience(int amount)
    {
        exp += amount;

        if (exp >= maxExp)
        {
            LevelUp();
        }
    }

    public void RemoveExpereince(int amount)
    {
        exp -= amount;

        if (exp < 0)
        {
            exp = 0;
        }
    }

    private void LevelUp()
    {
        level++;
        exp -= maxExp;
    }

    private void GameOver()
    {

    }

    #region Singleton

    protected override void AwakeInstance()
    {
        
    }

    protected override bool InitInstance()
    {
        return true;
    }

    protected override void ReleaseInstance()
    {
        
    }

    #endregion
}
