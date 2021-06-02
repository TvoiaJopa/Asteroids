using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

	[SerializeField] private string menuBGMusic;
	[SerializeField] private string levelBGMusic;
	[SerializeField] private string horrorBGMusic;

	private AudioSource _activeMusic;
	private AudioSource _inactiveMusic;

	[SerializeField] private AudioSource back;
	[SerializeField] private AudioSource ship;

	private bool ship_fly;


	public float crossFadeRate = 1.5f;
	private bool _crossFading;



	[SerializeField] private AudioSource soundSource;


	public void Startup()
	{
		//soundVolume = PlayerPrefs.GetInt("music_value", 1);

		soundVolume = PlayerPrefs.GetFloat("sound_value", 1.0f);
		musicVolume = PlayerPrefs.GetFloat("music_value", 1.0f);



	}

	public void Start()
    {

	}

    public void Update()
    {

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



	private float _musicVolume;
	public float musicVolume
	{
		get
		{
			return _musicVolume;
		}
		set
		{
			_musicVolume = value;
			ship.volume = value;
			soundSource.volume = value;



		}
	}


	// to play 2D sounds that don't have any other source
	public void PlaySound(AudioClip clip)
	{
		soundSource.PlayOneShot(clip);
	}

	public void PlaySoundUi(string ui_name)
	{
		soundSource.PlayOneShot(Resources.Load("Sounds/UI/" + ui_name) as AudioClip);
	}

	public void PlaySoundFromSounds(string fish_name)
	{
		soundSource.PlayOneShot(Resources.Load("Sounds/" + fish_name) as AudioClip);
	}

	public void PlayShipSound()
    {
		ship_fly = true;
    }

	public void NoPlayShipSound()
	{
		ship_fly = false;
	}




	public void StopMusic()
	{
		_activeMusic.Stop();
		_inactiveMusic.Stop();
	}
}
