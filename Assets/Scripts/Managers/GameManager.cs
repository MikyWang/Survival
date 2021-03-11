using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public GameObject player;
    public GameObject highlightingPrefab;
    public List<Resource> resources;
    public List<GameObject> patrolPoints { get; private set; }
    public List<ISelected> selectedPlayers { get; private set; }
    public List<ISelected> playersInView { get; private set; }
    override protected void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
        patrolPoints = GameObject.FindGameObjectsWithTag("PatrolPoint").ToList();
        selectedPlayers = new List<ISelected>();
        playersInView = new List<ISelected>();
    }

    private void Start()
    {
        Instantiate(player);
    }
    public void CallBuildersToBuildBuilding(Building building)
    {
        foreach (var player in selectedPlayers)
        {
            if (player.selectedObject.TryGetComponent<Peasant>(out var builder))
            {
                builder.Build(building);
            }
        }
    }
    public void ToggleSelector(ISelected selectedPlayer)
    {
        if (selectedPlayers.Contains(selectedPlayer))
        {
            selectedPlayers.Remove(selectedPlayer);
            selectedPlayer.UnSelect();
        }
        else
        {
            selectedPlayers.Add(selectedPlayer);
            selectedPlayer.Select(highlightingPrefab);
        }
        SkillUIManager.Instance.UpdateSkillUI();
    }

    public void SelectRangePlayers(Rect rect)
    {
        foreach (var player in selectedPlayers)
        {
            player.UnSelect();
        }
        selectedPlayers.Clear();
        SkillUIManager.Instance.UpdateSkillUI();

        foreach (ISelected selectedPlayer in playersInView)
        {
            Vector2 screenPos = Camera.main.WorldToScreenPoint(selectedPlayer.selectedObject.transform.position);
            if (rect.Contains(screenPos))
            {
                ToggleSelector(selectedPlayer);
            }
        }
    }

    public void UpdatePlayersInView(ISelected player)
    {
        if (playersInView.Contains(player) && !player.selectedObject.transform.IsInView())
        {
            playersInView.Remove(player);
        }
        if (!playersInView.Contains(player) && player.selectedObject.transform.IsInView())
        {
            playersInView.Add(player);
        }
    }

    public void TakeResources(int woodCost = 0, int goldCost = 0, int foodCost = 0)
    {
        foreach (var resource in resources)
        {
            switch (resource.id)
            {
                case ResourceId.Wood:
                    resource.amount -= woodCost;
                    break;
                case ResourceId.Gold:
                    resource.amount -= goldCost;
                    break;
                case ResourceId.Food:
                    resource.amount -= foodCost;
                    break;
            }
        }
    }
}
