using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStatsObserver
{
    public void StatsChanged<T>(T value);
}
