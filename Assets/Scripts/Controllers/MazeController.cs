﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MazeController : MonoBehaviour {
    public MazeBuild Build;

    public bool IsMazeBuilt;
    public bool isGameOver;
    public bool isPlayerWinner;
    private MazeGrid maze;
    private int Dimension;
    private int Sparsity;
    private Camera MenuCamera;
    private Camera MazeCamera;
    void Start()
    {
        IsMazeBuilt = false;
        Dimension = 20;
        Sparsity = 1;
    }

    void Update()
    {
        if ( IsMazeBuilt && !isGameOver)
        {
            foreach (GameObject key in Build.KeyPieces)
            {
                if (!key.GetComponent<KeyPieceController>().collected)
                {
                    return;
                }

            }

            isGameOver = true;
            isPlayerWinner = true;
        }
    }

    public void SliderValueChanged()
    {
        Dimension = (int)GameObject.Find("SizeSlider").GetComponent<Slider>().value;
        Sparsity = (int)GameObject.Find("SparsitySlider").GetComponent<Slider>().value;
    }

    public void GenerateMaze()
    {
        if (IsMazeBuilt)
        {
            DeleteMaze();
        }

        maze = RandomMaze.Apply(Dimension, Sparsity);
        Build = new MazeBuild(maze);
        IsMazeBuilt = true;
        GameObject.Find("PlayerCamera").GetComponent<Camera>().enabled = true;
    }

    public void DeleteMaze()
    {
        GameObject.Find("PlayerCamera").GetComponent<Camera>().enabled = false;
        Build.DeleteMaze();

        IsMazeBuilt = false;
    }

}