using System.Collections;
using System.Linq;
using UnityEngine;

public interface IUnitTarget
{
    public Transform Transform { get; }
    public UnitTargetType TargetType { get; }
}
