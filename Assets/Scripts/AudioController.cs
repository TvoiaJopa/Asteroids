using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
	[SerializeField] private AudioSource soundSource;
	[SerializeField] private AudioSource back;
	[SerializeField] private AudioSource ship;
	private bool ship_fly;
	private GameController gameController;

	public void Startup()
	{
		//Load saved sound volume
		soundVolume = PlayerPrefs.GetFloat("sound_value", 1.0f);
	}
	private void Awake()
	{
		//Find gameController on Scene
		gameController = GameObject.FindGameObjectsWithTag("GameController")[0].GetComponent<GameController>();

	}
	public void Start()
	{

	}

	public void Update()
	{
		//If pause and others conditions ship muted
		if (gameController.GetGameCon() != GameController.GameCondition.Game)
		{
			ship.Stop();
			ship.loop = false;
		}


		if (ship_fly)
		{
			if (ship.isPlaying)
			{

			}
			else
			{
				ship.PlayOneShot(Resources.Load("Sounds/" + "ship_gas") as AudioClip);
				ship.loop = true;
			}
		}
		else
		{
			ship.Stop();
			ship.loop = false;

		}

	}


	public float soundVolume
	{
		get { return AudioListener.volume; }
		set { AudioListener.volume = value; }
	}

	public bool soundMute
	{
		get { return AudioListener.pause; }
		set { AudioListener.pause = value; }
	}



	// to play 2D sounds that don't have any other source
	public void PlaySound(AudioClip clip)
	{
		soundSource.PlayOneShot(clip);
	}
	public void PlaySoundFromSounds(string name)
	{
		soundSource.PlayOneShot(Resources.Load("Sounds/" + name) as AudioClip);
	}

	public void PlayShipSound(bool play)
	{
		ship_fly = play;
	}
}
