using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Resource : IObservable<Resource>
{
    public ResourceId id;
    Resource_SO _resourceData;
    Resource_SO resourceData
    {
        get
        {
            if (_resourceData == null)
            {
                _resourceData = GameObject.Instantiate(SOManager.Instance.resourceDataDic[id]);
            }
            return _resourceData;
        }
    }
    public int amount
    {
        get => resourceData.amount;
        set
        {
            resourceData.amount = value;
            Notify();
        }
    }
    [SerializeField] List<IObserver<Resource>> observers = new List<IObserver<Resource>>();
    public IDisposable Subscribe(IObserver<Resource> observer)
    {
        observers.Add(observer);
        Notify();
        return new Unsubscribe<Resource>(observers, observer);
    }
    private void Notify()
    {
        foreach (var observer in observers)
        {
            observer.OnNext(this);
        }
    }
}
