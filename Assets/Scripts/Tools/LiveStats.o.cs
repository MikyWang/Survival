using System;
using System.Collections.Generic;
using UnityEngine;

public partial class LiveStats : IObservable<LiveStats>
{
    [SerializeField] List<IObserver<LiveStats>> observers;

    public IDisposable Subscribe(IObserver<LiveStats> observer)
    {
        observers.Add(observer);
        return new Unsubscribe(observers, observer);
    }

    private void Notify()
    {
        foreach (var observer in observers)
        {
            observer.OnNext(this);
        }
    }

    /// <summary>
    /// 取消订阅类
    /// </summary>
    private class Unsubscribe : IDisposable
    {
        List<IObserver<LiveStats>> observers;
        IObserver<LiveStats> observer;
        public Unsubscribe(List<IObserver<LiveStats>> observers
        , IObserver<LiveStats> observer)
        {
            this.observer = observer;
            this.observers = observers;
        }

        public void Dispose()
        {
            if (this.observers != null)
            {
                this.observers.Remove(observer);
            }
        }
    }

}