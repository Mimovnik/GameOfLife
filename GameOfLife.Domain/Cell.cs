namespace GameOfLife.Domain;

public class Cell : IEquatable<Cell>
{
    private bool _isAlive;
    
    public event Action OnDeath = delegate { };
    public event Action OnBirth = delegate { };
    
    public bool IsAlive
    {
        get => _isAlive;
        set
        {
            if (_isAlive == value) return;
            
            var wasAlive = _isAlive;
            _isAlive = value;
            
            if (wasAlive && !_isAlive)
            {
                OnDeath.Invoke();
            }
            else if (!wasAlive && _isAlive)
            {
                OnBirth.Invoke();
            }
        }
    }
    
    public Coords Coords { get; }

    public Cell(bool isAlive, Coords coords)
    {
        IsAlive = isAlive;
        Coords = coords;
    }

    public bool Equals(Cell? other)
    {
        if (other is null) return false;
        return IsAlive == other.IsAlive && Coords.Equals(other.Coords);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Cell);
    }

    public override int GetHashCode()
    {
        int hash = 17;
        hash = HashCode.Combine(hash, IsAlive.GetHashCode(), Coords.GetHashCode());
        return hash;
    }
}