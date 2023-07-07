using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

	//spawnpoints array
	public GameObject[] EnemySpawnPoint, HatContainer;
	public GameObject Enemy, Heart, Power, FirstTimePlay, Instruction,BG;

	//GAME STATE
	public  enum gameState {PLAY,PAUSE,GAMEOVER,LEVELUPGRADE,FIRSTTIME};
	public static gameState State;

	//Wave state for genrating enemy
	public enum Wave{WAIT,SPAWN};
	public static Wave wave; 

	//how many enemies to genrate
	public static int Stage, NumOfEnemy;

	GameObject Hat, Ninja;

	//on which spwan point enemy is instantiated
	int[] EnemyPostionValue;

	void Awake()
	{
		//sets the spawn points
		EnemySpawnPoint = GameObject.FindGameObjectsWithTag("SpawnPoint");
		Ninja = GameObject.FindGameObjectWithTag ("NinjaContainer");
	}

	void Start () 
	{
		//if first time play than shows the instruction
		if (PlayerPrefs.GetInt ("FirstTime", 1) == 1) {
			State = gameState.FIRSTTIME;
		} 
		else 
		{
			FirstTimePlay.SetActive(false);
			Instruction.gameObject.SetActive (false);
			StartGame ();
		}
		//sets the camera size
		float horizontalResolution = 1920f;
		float currentAspect = (float)Screen.width / (float)Screen.height;
		Camera.main.orthographicSize = horizontalResolution / currentAspect / 200;
	
		SetBackGroundScale ();
	}

	public void StartGame()
	{
		//starts the game
		State = gameState.PLAY;

		Stage = 1;
		NumOfEnemy = 1;

		//checks if enemy is alive or not after 1sec
		Invoke ("CheckEnemy", 1f);

		initiateHat ();
	}

	void Update () 
	{
		//if all enemies are died and wave is set to spawn and game state is play than it spawns the enemy
		if (GameObject.FindGameObjectWithTag ("EnemyContainer") == null && wave == Wave.SPAWN && State == gameState.PLAY) 
		{
			StartCoroutine (spawnEnemy());
		}
	}
		
	void CheckEnemy()
	{
		//set wave to spawn so enemy arives
		//and if all enemies are died it sets wave to spawn
		if (GameObject.FindGameObjectWithTag ("EnemyContainer") == null && State == gameState.PLAY)
			wave = Wave.SPAWN;
		//check again if any enemy is alive
		Invoke ("CheckEnemy", 1f);
	}
		
	IEnumerator spawnEnemy()
	{
		//as this coroutine runs wave state get sets to wait
		wave = Wave.WAIT;

		//sp = SpawnPoint at which position to instantiate enemy
		int sp;

		//sets the array size depending on number of enemy
		EnemyPostionValue =  new int[NumOfEnemy];

		//set all position default to -1
		for (int i = 0; i < NumOfEnemy; i++)
			EnemyPostionValue[i] = -1;

		//initiate the enemy
		for (int i = 0; i < NumOfEnemy; i++) 
		{
			//gets the random position
			sp = getRandomPosition ();
			//checks if enemy already exits there or not
			sp = checkSpawnPoint (sp,i);

			//instantiate the enemy
			Instantiate (Enemy, EnemySpawnPoint [sp].transform.position + new Vector3 (0.57f, 1f, 0f), EnemySpawnPoint [sp].transform.rotation);

			//waits for 0.5sec to spawn other enemy
			yield return new WaitForSeconds (0.5f);
		}
		//increase the num of enemy
		if (NumOfEnemy < 6 && Stage % 3 ==0)
			NumOfEnemy++;
		if (GameObject.FindWithTag ("NinjaContainer") != null)
		{
			//spawn the health power
			if (Stage > 3 && Stage % 2 == 0 && GameObject.FindWithTag ("NinjaContainer").GetComponent<NinjaHealth> ().hitPoint < GameObject.Find ("ScoreUI").GetComponent<Score> ().HealthBar.maxValue && State == gameState.PLAY)
				initiateHeart ();
		}
		//spawn the shield power
		if (Stage % 3 == 0 && GameObject.FindWithTag ("Shield") == null) 
		{
			if (GameObject.FindWithTag ("PowerUp") == null) 
			{
				initiatePower ();
			}
		}
		Stage++;
	}
	//gets the random spawn point
	int getRandomPosition()
	{
		return Random.Range (0, 6);
	}

	int checkSpawnPoint(int sp,int x)
	{
		//unique spawn position default true
		bool unique = true;

		//checks if this position is already taken or not if taken unique is set to false
		for (int i = 0; i < x; i++) 
			if (EnemyPostionValue[i] == sp) 
				unique = false;
		//if not unique gets the again random position and checks again
		if (!unique) 
		{
			sp = getRandomPosition ();
			sp = checkSpawnPoint (sp,x);
		}

		//store the position to array
		EnemyPostionValue[x] = sp;
		//return the position to initiate
		return sp;
	}
	//increase the health of ninja based on dameged
	public void increaseHealth()
	{
		int healthPoint, currentHealth;
		currentHealth = GameObject.FindWithTag("NinjaContainer").GetComponentInParent<NinjaHealth> ().hitPoint;
		if (currentHealth == PlayerPrefs.GetInt ("MaxHitPoint",10))
			return;
		healthPoint = Random.Range(1,PlayerPrefs.GetInt("MaxHitPoint",10) + 1 - currentHealth);
		GameObject.FindWithTag ("NinjaContainer").GetComponentInParent<NinjaHealth> ().hitPoint += healthPoint;

		GameObject.Find ("ScoreUI").GetComponent<Score> ().UpdateHealth ();
		GameObject.Find ("ScoreUI").GetComponent<Score> ().displayHealthIncreaseTxt (healthPoint);

		GameObject.Find ("NinjaPlateform").GetComponentInChildren<ParticleSystem> ().Play ();
		Invoke ("stopHeartParticleSystem",1f);
	}
	//stop health increase effect
	void stopHeartParticleSystem()
	{
		GameObject.Find ("NinjaPlateform").GetComponentInChildren<ParticleSystem> ().Stop ();
	}
	//initiate heart at randon position
	void initiateHeart()
	{
		Vector3 pos = new Vector3 (Random.Range(-4f,7f),Random.Range(-2f,3f),1f); 
		Instantiate (Heart,pos, Heart.transform.rotation);
	}
	//initiate the hat
	void initiateHat()
	{
		LocalData data= DatabaseManager.Instance.GetLocalData();
		
		int index = PlayerPrefs.GetInt("Hat", 0);

		if (data != null)
			index = data.Hat;

		//PlayerPrefs.SetInt ("Hat",12);
		//index = PlayerPrefs.GetInt("Hat");

		Debug.Log ("HAT : "+index);

		Hat = HatContainer [index];
		Instantiate(Hat,Hat.transform.position,Hat.transform.rotation).transform.SetParent(Ninja.transform);
	}
	//initiate shield power
	void initiatePower()
	{
		Vector3 pos = new Vector3 (Random.Range(-4f,7f),Random.Range(-2f,3f),1f);
		Instantiate (Power,pos ,Power.transform.rotation);
	}
	//sets the image size based on screen size
	void SetBackGroundScale()
	{
		float height = Camera.main.orthographicSize * 2;
		float width = height * Screen.width/ Screen.height;

		Sprite s = BG.GetComponent<SpriteRenderer> ().sprite;

		float unitWidth = s.textureRect.width / s.pixelsPerUnit;
		float unitHeight = s.textureRect.height / s.pixelsPerUnit;

		BG.transform.localScale = new Vector3(width / unitWidth, height / unitHeight) + new Vector3(0.02f,0.02f,0.02f);
	}

}
