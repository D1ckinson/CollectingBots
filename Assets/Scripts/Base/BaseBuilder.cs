using System;
using UnityEngine;

public class BaseBuilder : MonoBehaviour
{
    [SerializeField] private int _buildCost = 5;

    public event Action BaseBuild;

    public bool IsAlreadyBuilt { get; private set; }

    private void Awake()
    {
        IsAlreadyBuilt = false;
    }

    public void Build()
    {

    }
}
