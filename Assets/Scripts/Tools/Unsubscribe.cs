using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 取消订阅类
/// </summary>
public class Unsubscribe<T> : IDisposable
{
    List<IObserver<T>> observers;
    IObserver<T> observer;
    public Unsubscribe(List<IObserver<T>> observers
    , IObserver<T> observer)
    {
        this.observer = observer;
        this.observers = observers;
    }

    public void Dispose()
    {
        if (observer != null && observers != null)
        {
            observers.Remove(observer);
        }
    }
}
