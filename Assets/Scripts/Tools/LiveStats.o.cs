using System;
using System.Collections.Generic;
using UnityEngine;

public partial class LiveStats : IObservable<LiveStats>
{
    [SerializeField] List<IObserver<LiveStats>> observers = new List<IObserver<LiveStats>>();

    public IDisposable Subscribe(IObserver<LiveStats> observer)
    {
        observers.Add(observer);
        return new Unsubscribe<LiveStats>(observers, observer);
    }
    private void Notify()
    {
        foreach (var observer in observers)
        {
            observer.OnNext(this);
        }
    }
}