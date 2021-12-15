using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

[Serializable]
public class CreditsProductionInfo
{
	public string title = "Missing Title";
	[ResizableTextArea]
	public string description = "Missing Description";
	public Sprite productionSprite = null;
}
public class CreditsControl : MonoBehaviour
{
	[SerializeField]
	List<CreditsProductionInfo> productionLists = new List<CreditsProductionInfo>();
	[SerializeField]
	TextMeshProUGUI titleUI;
	[SerializeField]
	TextMeshProUGUI descriptionUI;
	[SerializeField]
	TextMeshProUGUI currentContent;
	[SerializeField]
	Image prodcutionImage;
	[SerializeField]
	int currentIndex = 0;
	[SerializeField]
	FadeManyTransition fadeManyTransition;
	private void Start()
	{
		if (productionLists.Count <= 0)
		{
			return;
		}
		currentIndex = 0;
		currentContent.text = (currentIndex + 1) + "/" + productionLists.Count;
		SetContent(productionLists[currentIndex]);
	}
	public void BackToMain()
	{
		GameMaster.GetInstance().LoadMainMenu();
	}
	[Button]
	public void NextItem()
	{
		if (productionLists.Count <= 0)
		{
			return;
		}
		currentIndex += 1;
		if (currentIndex >= productionLists.Count)
		{
			currentIndex = 0;
		}
		currentContent.text = (currentIndex + 1) + "/" + productionLists.Count;
		fadeManyTransition.FadeOut(() =>
				{
					this.SetContent(productionLists[currentIndex]);
				});
	}
	[Button]
	public void PreviousItem()
	{
		if (productionLists.Count <= 0)
		{
			return;
		}
		currentIndex -= 1;
		if (currentIndex < 0)
		{
			currentIndex = productionLists.Count - 1;
		}
		currentContent.text = (currentIndex + 1) + "/" + productionLists.Count;
		fadeManyTransition.FadeOut(() =>
		{
			this.SetContent(productionLists[currentIndex]);
		});

	}
	public void SetContent(CreditsProductionInfo info)
	{
		this.titleUI.text = info.title;
		this.descriptionUI.text = info.description;
		this.prodcutionImage.sprite = info.productionSprite;
		fadeManyTransition.FadeIn();
	}
}
