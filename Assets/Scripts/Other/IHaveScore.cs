using System;

public interface IHaveScore
{
    public event Action<int> ScoreChange;
}