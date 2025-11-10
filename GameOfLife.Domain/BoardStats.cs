namespace GameOfLife.Domain;

public class BoardStats
{
    public int BirthCount { get; private set; }
    public int DeathCount { get; private set; }

    public BoardStats()
    {
        BirthCount = 0;
        DeathCount = 0;
    }

    public void SubscribeToCell(Cell cell)
    {
        cell.OnBirth += OnCellBirth;
        cell.OnDeath += OnCellDeath;
    }

    public void UnsubscribeFromCell(Cell cell)
    {
        cell.OnBirth -= OnCellBirth;
        cell.OnDeath -= OnCellDeath;
    }

    private void OnCellBirth()
    {
        BirthCount++;
    }

    private void OnCellDeath()
    {
        DeathCount++;
    }
}
