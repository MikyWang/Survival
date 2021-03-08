using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public partial class Building : IObservable<Building>
{
    public BuildingId id;
    Transform buildingMeshTransform;
    MeshRenderer buildingRender;
    [ColorUsage(true, true)]
    [SerializeField] Color[] stateColors;
    [SerializeField] private ParticleSystem buildParticlePrefab;
    List<IObserver<Building>> observers = new List<IObserver<Building>>();
    List<ISelected> selectedPlayers => GameManager.Instance.selectedPlayers;
    float height => headUITransform.localPosition.y - 1;
    protected override void Start()
    {
        buildingStats = Instantiate(SOManager.Instance.buildingDataDic[id]);
        buildingMeshTransform = transform.GetChild(0);
        buildingRender = buildingMeshTransform.GetComponent<MeshRenderer>();
        SetUp();
        CallBuilders();
    }
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
    private void SetUp()
    {
        buildingMeshTransform.localPosition = Vector3.down * height;
    }

    public void GlowUp()
    {
        buildingProgress += progressEveryTime;
        if (isFinished)
        {
            FinishBuilding();
            return;
        }

        buildingMeshTransform.localPosition = Vector3.Lerp(Vector3.down * height, new Vector3(0, height, 0), (float)buildingProgress / 100);
        Instantiate(buildParticlePrefab, transform.position, Quaternion.identity);
        buildingMeshTransform.DOComplete();
        buildingMeshTransform.DOShakeScale(.5f, .2f, 10, 90, true);
    }

    private void FinishBuilding()
    {
        buildingRender.material.DOColor(stateColors[1], "_EmissionColor", .1f).OnComplete(() => buildingRender.material.DOColor(stateColors[0], "_EmissionColor", .5f));
    }

    void CallBuilders()
    {
        foreach (var player in selectedPlayers)
        {
            if (player.selectedObject.TryGetComponent<Peasant>(out var builder))
            {
                builder.Build(this);
            }
        }
    }

}