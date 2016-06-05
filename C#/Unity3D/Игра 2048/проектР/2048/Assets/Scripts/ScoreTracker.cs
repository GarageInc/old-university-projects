using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreTracker : MonoBehaviour {

	private int score;
	public static ScoreTracker Instance;
	public Text ScoreText;
	public Text HighScoreText;

	public int Score
	{
		get
		{
			return score;
		}

		set
		{
			score = value;
			ScoreText.text = score.ToString();

			if (PlayerPrefs.GetInt("HighScore") < score)
			{
				PlayerPrefs.SetInt("HighScore", score);
				HighScoreText.text = score.ToString();
			}
		}
	}

	void Awake()
	{

		//PlayerPrefs.DeleteAll ();
		Instance = this;

		if (!PlayerPrefs.HasKey ("HighScore"))
			PlayerPrefs.SetInt ("HighScore", 0);

		ScoreText.text = "0";
		HighScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
	}

}
