using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public enum GameState
{
	Playing,
	GameOver, 
	WaitingForMoveToEnd
}


public class GameManager : MonoBehaviour {

	// NEW AFTER ADDED DELAYS
	public GameState State;
	[Range(0, 2f)]
	public float delay;
	private bool moveMade;
	private bool[] lineMoveComplete = new bool[4]{true, true, true, true};
	// NEW AFTER ADDED DELAYS

	public GameObject YouWonText;
	public GameObject GameOverText;
	public Text GameOverScoreText;
	public GameObject GameOverPanel;

	private Tile[,] AllTiles = new Tile[4,4];
	private List <Tile[]> columns = new List<Tile[]> ();
	private List <Tile[]> rows = new List<Tile[]> ();
	private List<Tile> EmptyTiles = new List<Tile>();

	// Use this for initialization
	void Start () 
	{
		Tile[] AllTilesOneDim = GameObject.FindObjectsOfType<Tile> ();
		foreach (Tile t in AllTilesOneDim) 
		{
			t.Number = 0;
			AllTiles[t.indRow, t.indCol] = t;
			EmptyTiles.Add (t);
		}

		columns.Add (new Tile[]{AllTiles [0, 0], AllTiles [1, 0], AllTiles [2, 0], AllTiles [3, 0]});
		columns.Add (new Tile[]{AllTiles [0, 1], AllTiles [1, 1], AllTiles [2, 1], AllTiles [3, 1]});
		columns.Add (new Tile[]{AllTiles [0, 2], AllTiles [1, 2], AllTiles [2, 2], AllTiles [3, 2]});
		columns.Add (new Tile[]{AllTiles [0, 3], AllTiles [1, 3], AllTiles [2, 3], AllTiles [3, 3]});

		rows.Add (new Tile[]{AllTiles [0, 0], AllTiles [0, 1], AllTiles [0, 2], AllTiles [0, 3]});
		rows.Add (new Tile[]{AllTiles [1, 0], AllTiles [1, 1], AllTiles [1, 2], AllTiles [1, 3]});
		rows.Add (new Tile[]{AllTiles [2, 0], AllTiles [2, 1], AllTiles [2, 2], AllTiles [2, 3]});
		rows.Add (new Tile[]{AllTiles [3, 0], AllTiles [3, 1], AllTiles [3, 2], AllTiles [3, 3]});

		State = GameState.Playing;

		Generate ();
		Generate ();
	}

	private void YouWon()
	{
		State = GameState.GameOver;
		GameOverText.SetActive (false);
		YouWonText.SetActive (true);
		GameOverScoreText.text = ScoreTracker.Instance.Score.ToString ();
		GameOverPanel.SetActive (true);
	}

	private void GameOver()
	{
		State = GameState.GameOver;
		GameOverScoreText.text = ScoreTracker.Instance.Score.ToString ();
		GameOverPanel.SetActive (true);
	}

	bool CanMove()
	{
		if (EmptyTiles.Count > 0)
			return true;
		else 
		{
			// check columns
			for(int i = 0; i< columns.Count; i++)
				for (int j = 0; j< rows.Count-1; j++)
					if (AllTiles[j, i].Number == AllTiles[j+1, i].Number)
						return true;

			// check rows
			for(int i = 0; i< rows.Count; i++)
				for (int j = 0; j< columns.Count-1; j++)
					if (AllTiles[i, j].Number == AllTiles[i, j+1].Number)
						return true;

		}
		return false;
	}

	public void NewGameButtonHandler()
	{
		Application.LoadLevel (Application.loadedLevel);
	}

	bool MakeOneMoveDownIndex(Tile[] LineOfTiles)
	{
		for (int i =0; i< LineOfTiles.Length-1; i++) 
		{
			//MOVE BLOCK 
			if (LineOfTiles[i].Number == 0 && LineOfTiles[i+1].Number != 0)
			{
				LineOfTiles[i].Number = LineOfTiles[i+1].Number;
				LineOfTiles[i+1].Number = 0;
				return true;
			}
			// MERGE BLOCK
			if (LineOfTiles[i].Number!= 0 && LineOfTiles[i].Number == LineOfTiles[i+1].Number &&
			    LineOfTiles[i].mergedThisTurn == false && LineOfTiles[i+1].mergedThisTurn == false)
			{
				LineOfTiles[i].Number *= 2;
				LineOfTiles[i+1].Number = 0;
				LineOfTiles[i].mergedThisTurn = true;
				LineOfTiles[i].PlayMergeAnimation();
				ScoreTracker.Instance.Score += LineOfTiles[i].Number;
				if (LineOfTiles[i].Number == 2048)
					YouWon();
				return true;
			}
		}
		return false;
	}

	bool MakeOneMoveUpIndex(Tile[] LineOfTiles)
	{
		for (int i =LineOfTiles.Length-1; i > 0; i--) 
		{
			//MOVE BLOCK 
			if (LineOfTiles[i].Number == 0 && LineOfTiles[i-1].Number != 0)
			{
				LineOfTiles[i].Number = LineOfTiles[i-1].Number;
				LineOfTiles[i-1].Number = 0;
				return true;
			}
			// MERGE BLOCK
			if (LineOfTiles[i].Number!= 0 && LineOfTiles[i].Number == LineOfTiles[i-1].Number &&
			    LineOfTiles[i].mergedThisTurn == false && LineOfTiles[i-1].mergedThisTurn == false)
			{
				LineOfTiles[i].Number *= 2;
				LineOfTiles[i-1].Number = 0;
				LineOfTiles[i].mergedThisTurn = true;
				LineOfTiles[i].PlayMergeAnimation();
				ScoreTracker.Instance.Score += LineOfTiles[i].Number;
				if (LineOfTiles[i].Number == 2048)
					YouWon();
				return true;
			}
		}
		return false;
	}


	
	void Generate()
	{
		if (EmptyTiles.Count > 0) 
		{
			int indexForNewNumber = Random.Range(0, EmptyTiles.Count);
			int randomNum = Random.Range(0,10);
			if (randomNum == 0)
				EmptyTiles[indexForNewNumber].Number = 4;
			else
				EmptyTiles[indexForNewNumber].Number = 2;

			EmptyTiles[indexForNewNumber].PlayAppearAnimation();

			EmptyTiles.RemoveAt(indexForNewNumber);
		}
	}
	
	// Update is called once per frame
	/*void Update () 
	{
		if (Input.GetKeyDown (KeyCode.G))
			Generate ();
	
	}*/

	private void ResetMergedFlags()
	{
		foreach (Tile t in AllTiles)
			t.mergedThisTurn = false;
	}

	private void UpdateEmptyTiles()
	{
		EmptyTiles.Clear ();
		foreach (Tile t in AllTiles) 
		{
			if(t.Number == 0)
				EmptyTiles.Add(t);
		}
	}
	
	public void Move(MoveDirection md)
	{
		Debug.Log (md.ToString () + " move.");
	    moveMade = false;
		ResetMergedFlags ();
		if (delay > 0)
			StartCoroutine (MoveCoroutine (md));
		else 
		{
			for (int i =0; i< rows.Count; i++) 
			{
				switch (md) 
				{
				case MoveDirection.Down:
					while (MakeOneMoveUpIndex(columns[i])) 
					{
						moveMade = true;
					}
					break;
				case MoveDirection.Left:
					while (MakeOneMoveDownIndex(rows[i])) 
					{
						moveMade =true;
					}
					break;
				case MoveDirection.Right:
					while (MakeOneMoveUpIndex(rows[i])) 
					{
						moveMade =true;
					}
					break;
				case MoveDirection.Up:
					while (MakeOneMoveDownIndex(columns[i])) 
					{
						moveMade =true;
					}
					break;
				}
			}

			if (moveMade) 
			{
				UpdateEmptyTiles ();
				Generate ();

				if (!CanMove())
				{
					GameOver();
				}
			
			}
		}
	}

	IEnumerator MoveOneLineUpIndexCoroutine(Tile[] line, int index)
	{
		lineMoveComplete [index] = false;
		while (MakeOneMoveUpIndex(line)) 
		{
			moveMade = true;
			yield return new WaitForSeconds(delay);
		}
		lineMoveComplete [index] = true;
	}

	IEnumerator MoveOneLineDownIndexCoroutine(Tile[] line, int index)
	{
		lineMoveComplete [index] = false;
		while (MakeOneMoveDownIndex(line)) 
		{
			moveMade = true;
			yield return new WaitForSeconds(delay);
		}
		lineMoveComplete [index] = true;
	}
	

	IEnumerator MoveCoroutine(MoveDirection md)
	{
		State = GameState.WaitingForMoveToEnd;

		// start moving each line with delays depending on MoveDirection md
		switch (md) 
		{
		case MoveDirection.Down:
			for (int i = 0; i< columns.Count; i++)
				StartCoroutine(MoveOneLineUpIndexCoroutine(columns[i], i));
			break;
		case MoveDirection.Left: 
			for (int i = 0; i< rows.Count; i++)
				StartCoroutine(MoveOneLineDownIndexCoroutine(rows[i], i));
			break;
		case MoveDirection.Right:
			for (int i = 0; i< rows.Count; i++)
				StartCoroutine(MoveOneLineUpIndexCoroutine(rows[i], i));
			break;
		case MoveDirection.Up: 
			for (int i = 0; i< columns.Count; i++)
				StartCoroutine(MoveOneLineDownIndexCoroutine(columns[i], i));
			break;

		}

		// Wait until the move is over in all lines
		while (! (lineMoveComplete[0] && lineMoveComplete[1] && lineMoveComplete[2] && lineMoveComplete[3]))
			yield return null;

		if (moveMade) 
		{
			UpdateEmptyTiles ();
			Generate ();
			
			if (!CanMove())
			{
				GameOver();
			}

		}

		State = GameState.Playing;
		StopAllCoroutines ();
	}
}
