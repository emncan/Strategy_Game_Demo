using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Create unit.
/// factory design pattern is used.
/// </summary>
abstract class UnitFactory  
{
    public abstract GameObject CreateUnit();
}

class SoldierFactory : UnitFactory
{
    public override GameObject CreateUnit()
    {
        GameObject soldierPrefab = Resources.Load<GameObject>("Prefabs/Unit/Soldier");

        return soldierPrefab;
    }
}
