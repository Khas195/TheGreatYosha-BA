using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class FadeTransition : MonoBehaviour
{
	[SerializeField]
	float fadeTime = 3.0f;
	[SerializeField]
	SpriteRenderer sprite;

	[SerializeField]
	[ReadOnly]
	float currentTime = 0.0f;
	[SerializeField]
	[ReadOnly]
	float targetAlpha = 0.0f;
	[SerializeField]
	[ReadOnly]
	float startAlpha = 1.0f;

	[SerializeField]
	float fadeInAlpha = 1.0f;
	[SerializeField]
	float fadeOutAlpha = 0.0f;


	[SerializeField]
	[ReadOnly]
	bool isTransitioning = false;
	[SerializeField]
	bool isFadeOut = false;
	[SerializeField]
	bool startFadeOut = false;
	Action callBack = null;
	private void Start()
	{
		if (startFadeOut)
		{
			FadeOut();
		}
	}
	void Update()
	{
		if (isTransitioning)
		{
			var color = sprite.color;
			color.a = Mathf.Lerp(startAlpha, targetAlpha, currentTime / fadeTime);
			sprite.color = color;
			if (currentTime >= fadeTime)
			{
				isTransitioning = false;
				if (this.callBack != null)
				{
					this.callBack();
					this.callBack = null;
				}
			}
			currentTime += Time.deltaTime;
		}
	}



	[Button]
	public void FadeIn(Action newCallback = null)
	{
		startAlpha = fadeOutAlpha;
		targetAlpha = fadeInAlpha;
		currentTime = 0;
		isTransitioning = true;
		this.callBack = newCallback;
		isFadeOut = false;
	}
	[Button]
	public void FadeOut(Action newCallback = null)
	{
		startAlpha = fadeInAlpha;
		targetAlpha = fadeOutAlpha;
		currentTime = 0;
		isTransitioning = true;
		this.callBack = newCallback;
		isFadeOut = true;
	}

	public bool IsFadeOut()
	{
		return isFadeOut;
	}

	[Button]
	public void SetInvisible()
	{
		isFadeOut = true;
		var color = sprite.color;
		color.a = 0;
		sprite.color = color;
		isTransitioning = false;
	}
	[Button]
	public void SetVisible()
	{
		isFadeOut = false;
		var color = sprite.color;
		color.a = 1;
		sprite.color = color;
		isTransitioning = false;
	}
	public bool IsStartFadeOut()
	{
		return startFadeOut;
	}
}
