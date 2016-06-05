using UnityEngine;
using System.Collections;

public class CoroutineTest : MonoBehaviour {

	int coroutineCounter = 0;

	// Use this for initialization
	void Start () 
	{
		//TheMethod ();

		StartCoroutine (TheCoroutine ());
		//StartCoroutine("TheCoroutine");
	
		StartCoroutine (TheObservingCoroutine());
	}


	private void TheMethod()
	{
		int i = 0;
		while (i<10) 
		{
			Debug.Log("Method says "+ i.ToString());
			i++;
		}
	}

	IEnumerator TheCoroutine()
	{
		while (true) 
		{
			Debug.Log("Coroutine says" + coroutineCounter.ToString());
			coroutineCounter++;
			yield return new WaitForSeconds(1f);
		}
	}

	IEnumerator TheObservingCoroutine()
	{
		while (coroutineCounter< 10)
			yield return null;

		Debug.Log("The ObservingCoroutine says that counter is 10 or greater. WOOO-HOOO!!!");
	}
}
