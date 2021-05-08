using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BotaoNovoJogo : MonoBehaviour
{
	public void Acao()
	{
		SceneManager.LoadScene("Debate", LoadSceneMode.Single);
	}
}
