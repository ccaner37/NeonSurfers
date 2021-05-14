using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameManager
{
    void a();
}


public class GameManager : IGameManager
{
    void Start()
    {

    }
    

    public void a()
    {
        Debug.Log("a");
    }
}
