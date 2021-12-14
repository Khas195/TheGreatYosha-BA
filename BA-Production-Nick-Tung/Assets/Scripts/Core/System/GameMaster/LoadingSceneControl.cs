using System;
using System.Collections.Generic;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
[Serializable]
public class LoadingBackgroundInfo
{
	public Sprite background;
	[ResizableTextArea]
	public string description;
}
public class LoadingSceneControl : SingletonMonobehavior<LoadingSceneControl>
{
	[SerializeField]
	List<LoadingBackgroundInfo> backgrounds = new List<LoadingBackgroundInfo>();
	[SerializeField]
	float minLoadTime = 1f;
	[SerializeField]
	float lingerTime = 4f;
	[SerializeField]
	[ReadOnly]
	float curTime = 0.0f;
	[SerializeField]
	Image background = null;
	[SerializeField]
	TextMeshProUGUI description = null;

	[SerializeField]
	Button continueButton;
	[SerializeField]
	FadeManyTransition fadeTransition = null;
	[SerializeField]
	FadeManyTransition continueButtonFade = null;
	private void Start()
	{
		ChooseRandomBackground();
		fadeTransition.FadeIn(() =>
		{
			Invoke("FadeOut", lingerTime);
		});
	}

	private void ChooseRandomBackground()
	{
		var randIndex = UnityEngine.Random.Range(0, backgrounds.Count);
		background.sprite = backgrounds[randIndex].background;
		description.text = backgrounds[randIndex].description;
	}

	// Update is called once per frame
	void Update()
	{
		if (fadeTransition.IsInTransition() == false && fadeTransition.IsFadeOut())
		{
			ChooseRandomBackground();
			fadeTransition.FadeIn(() =>
			{
				Invoke("FadeOut", lingerTime);
			});
		}
		if (curTime < minLoadTime)
		{
			curTime += Time.deltaTime;
		}
		else
		{
			if (SceneLoadingManager.GetInstance().GetLoadingProgress() >= 1.0f)
			{
				if (continueButtonFade.IsFadeOut())
				{
					continueButtonFade.FadeIn();
					continueButton.interactable = true;
				}
			}
		}
	}
	public void FadeOut()
	{
		fadeTransition.FadeOut();
	}
	public void ContinueToScene()
	{
		SceneLoadingManager.GetInstance().FinishedLoading();
	}
}
