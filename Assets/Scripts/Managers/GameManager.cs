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
    // CinemachineFreeLook Cam;
    override protected void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
        // DOTween.Init();
        patrolPoints = GameObject.FindGameObjectsWithTag("PatrolPoint").ToList();
        selectedPlayers = new List<ISelected>();
        playersInView = new List<ISelected>();
        // Cam = FindObjectOfType<CinemachineFreeLook>();
    }

    private void Start()
    {
        Instantiate(player);
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
        if (selectedPlayers.Count > 0)
        {
            // Cam.Follow = selectedPlayers[0].selectedObject.transform;
            // Cam.LookAt = selectedPlayers[0].selectedObject.transform;
        }
    }

    public void SelectRangePlayers(Rect rect)
    {
        foreach (var player in selectedPlayers)
        {
            player.UnSelect();
        }
        selectedPlayers.Clear();
        // dragSize.Set(Mathf.Abs(dragSize.x / 2), 1, Mathf.Abs(dragSize.z / 2));
        // RaycastHit[] hits = Physics.BoxCastAll(dragCenter, dragSize, Vector3.up, Quaternion.identity, 0, LayerMask.GetMask("Lives"));
        // foreach (RaycastHit hit in hits)
        // {
        //     if (hit.collider.TryGetComponent(out ISelected selectedPlayer))
        //     {
        //         ToggleSelector(selectedPlayer);
        //     }
        // }
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
}
