using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemViewController : SingletonMonobehavior<ItemViewController>
{
	[SerializeField]
	Image itemPortrait;
	[SerializeField]
	Text commentorText;
	[SerializeField]
	GameObject overlayText;
	protected override void Awake()
	{
		base.Awake();
		this.Clear();
	}
	public void SetContent(Sprite itemSprite, string comment, bool hasOverlayText = false)
	{
		this.itemPortrait.sprite = itemSprite;
		this.commentorText.text = comment;

		this.itemPortrait.gameObject.SetActive(true);
		this.commentorText.enabled = true;
		this.overlayText.gameObject.SetActive(hasOverlayText);
	}
	public void Clear()
	{
		this.itemPortrait.sprite = null;
		this.commentorText.text = "";

		this.itemPortrait.gameObject.SetActive(false);
		this.commentorText.enabled = false;
	}

	internal void SetContent(object commentorSprite, object itemSprite, object comment)
	{
		throw new NotImplementedException();
	}
}
