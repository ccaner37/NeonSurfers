using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DependencyInjection : MonoBehaviour
{
    private IGameManager gameManager;

    [Inject]
    public void Setup(IGameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    void Start()
    {
        gameManager.a();
    }

}
