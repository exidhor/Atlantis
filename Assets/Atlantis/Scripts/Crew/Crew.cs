using UnityEngine;
using System.Collections;
using MemoryManagement;

public abstract class Crew : UnityPoolObject
{
    public abstract CrewType type { get; }

    public abstract float progress01 { get; }
}
