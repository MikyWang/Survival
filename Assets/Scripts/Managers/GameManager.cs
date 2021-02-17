using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : Singleton<GameManager>
{
    public GameObject player;
    public List<GameObject> patrolPoints { get; private set; }
    public ControllerBase currentController { get; private set; }
    CinemachineFreeLook Cam;
    override protected void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);

        patrolPoints = GameObject.FindGameObjectsWithTag("PatrolPoint").ToList();
    }

    private void Start()
    {
        Instantiate(player);
    }

    public void RegisterController(ControllerBase controller)
    {
        currentController = controller;
        Cam = FindObjectOfType<CinemachineFreeLook>();
        Cam.Follow = controller.transform;
        Cam.LookAt = controller.transform;
    }

}
