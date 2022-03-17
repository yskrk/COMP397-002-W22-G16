/*
 * OptionsUIControls.cs 
 * Joshua Eagles - 301078033
 * Ethan San Juan-Cheong - 301069513
 * Last Modified: 2022-03-04
 * 
 * Handles the logic for the controls on the options screen
 *
 * Revision History:
 * 2022-02-13 - Initial Creation
 * 2022-03-04 - Unlock cursor when entering this scene
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsUIControls : MonoBehaviour
{
	public AudioMixer musicMixer;
	public AudioMixer soundEffectMixer;
	public Slider musicVolumeSlider;
	public Slider soundEffectVolumeSlider;

	void Start()
	{
		Cursor.lockState = CursorLockMode.None;

		musicMixer.GetFloat("Volume", out float musicVolume);
		musicVolumeSlider.value = Mathf.Pow(10, musicVolume / 20);

		soundEffectMixer.GetFloat("Volume", out float soundEffectVolume);
		soundEffectVolumeSlider.value = Mathf.Pow(10, soundEffectVolume / 20);
	}

	public void OnBackButton_Pressed()
	{
		SceneManager.LoadScene("MainMenu");
	}

	public void OnMusicVolumeSlider_Changed()
	{
		musicMixer.SetFloat("Volume", Mathf.Log10(musicVolumeSlider.value) * 20);
	}

	public void OnSoundEffectVolumeSlider_Changed()
	{
		soundEffectMixer.SetFloat("Volume", Mathf.Log10(soundEffectVolumeSlider.value) * 20);
	}
}
