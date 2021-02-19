using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : Singleton<GameManager>
{
    public GameObject player;
    public GameObject highlightingPrefab;
    public List<GameObject> patrolPoints { get; private set; }
    public List<ISelected> selectedPlayers { get; private set; }
    public List<ISelected> playersInView { get; private set; }
    CinemachineFreeLook Cam;
    override protected void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);

        patrolPoints = GameObject.FindGameObjectsWithTag("PatrolPoint").ToList();
        selectedPlayers = new List<ISelected>();
        playersInView = new List<ISelected>();
        Cam = FindObjectOfType<CinemachineFreeLook>();
    }

    private void Start()
    {
        Instantiate(player);
    }
    public void ToggleSelectors(ISelected selectedPlayer)
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
        if (selectedPlayers.Count > 0)
        {
            Cam.Follow = selectedPlayers[0].selectedObject.transform;
            Cam.LookAt = selectedPlayers[0].selectedObject.transform;
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
}
