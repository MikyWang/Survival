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
    float originalHeight;
    public event Action OnBuildingFinished;

    protected override void Awake()
    {
        base.Awake();
        buildingMeshTransform = transform.GetChild(0);
        buildingRender = buildingMeshTransform.GetComponent<MeshRenderer>();
        originalHeight = buildingMeshTransform.localPosition.y;
    }

    protected override void Start()
    {
        SetUp();
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
        buildingStats = Instantiate(SOManager.Instance.buildingDataDic[id]);
        buildingMeshTransform.localPosition = Vector3.down * height;
        GameManager.Instance.CallSelectedPlayerDoWork<Peasant>(builder =>
        {
            builder.Build(this);
        });
    }

    public void GlowUp()
    {
        if (isFinished) return;
        buildingProgress += progressEveryTime;
        buildingMeshTransform.localPosition = Vector3.Lerp(Vector3.down * height, new Vector3(0, originalHeight, 0), (float)buildingProgress / 100);
        Instantiate(buildParticlePrefab, transform.position, Quaternion.identity);
        buildingMeshTransform.DOComplete();
        buildingMeshTransform.DOShakeScale(.5f, .2f, 10, 90, true);

        if (isFinished)
        {
            FinishBuilding();
            return;
        }
    }

    void FinishBuilding()
    {
        buildingRender.material.DOColor(stateColors[1], "_EmissionColor", 0.1f).OnComplete(() => buildingRender.material.DOColor(stateColors[0], "_EmissionColor", .5f));
        OnBuildingFinished?.Invoke();
    }



}