﻿using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using TN_InterviewTest;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LockstepController _lockstepController;

    [SerializeField] private GameObject _player1Camera, _player2Camera;


    private void OnEnable()
    {
        _lockstepController.OnPlayerJoined += OnPlayerJoined;
    }

    private void OnPlayerJoined(int playerID)
    {
        Camera.main.gameObject.SetActive(false);

        if(playerID == 1)
        {
            _player1Camera.SetActive(true);
            _player2Camera.SetActive(false);
        }
        else if (playerID == 2)
        {
            _player2Camera.SetActive(true);
            _player1Camera.SetActive(false);
        }
    }

    private void Update()
    {
        //if (Input.GetKeyUp(KeyCode.A))
        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 10000.0f, LayerMask.GetMask("Ground")))
            { 
                Debug.Log("You selected the " + hit.transform.name); // ensure you picked right object
                var hitPosition = hit.point; 

                //var xpos = Random.Range(-100, 100);
                var xpos = (int)hitPosition.x;
                //var zpos = Random.Range(-100, 100);
                var zpos = (int)hitPosition.z;

                //var speed = Random.Range(4, 6);
                var speed = 5;
                var direction = UnityEngine.Random.Range(0, 359);
                //var direction = 45;

                var action = new CreateUnit(PhotonNetwork.LocalPlayer.ActorNumber,
                                            _lockstepController.LockStepTurnID,
                                            "CreateUnit",
                                            System.Guid.NewGuid().ToString(),
                                            xpos,
                                            zpos,
                                            speed,
                                            direction
                                            );

                _lockstepController.CreateAction(action);
            }

        }
    }
}
