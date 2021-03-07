using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class SkillBase : IObservable<SkillBase>
{
    List<IObserver<SkillBase>> observers = new List<IObserver<SkillBase>>();
    public IDisposable Subscribe(IObserver<SkillBase> observer)
    {
        observers.Add(observer);
        return new Unsubscribe<SkillBase>(observers, observer);
    }

    void Notify()
    {
        foreach (var observer in observers)
        {
            observer.OnNext(this);
        }
    }
}
