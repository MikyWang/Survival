using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Building : IObservable<Building>
{
    public BuildingId id;
    List<IObserver<Building>> observers = new List<IObserver<Building>>();
    public IDisposable Subscribe(IObserver<Building> observer)
    {
        observers.Add(observer);
        return new Unsubscribe<Building>(observers, observer);
    }
    void Notify()
    {
        foreach (var observer in observers)
        {
            observer.OnNext(this);
        }
    }
}