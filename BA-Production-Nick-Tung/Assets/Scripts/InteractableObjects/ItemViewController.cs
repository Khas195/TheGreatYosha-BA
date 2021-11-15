using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemViewController : SingletonMonobehavior<ItemViewController>
{
	[SerializeField]
	Image commentorPortrait;
	[SerializeField]
	Image itemPortrait;
	[SerializeField]
	Text commentorText;
	protected override void Awake()
	{
		base.Awake();
		this.Clear();
	}
	public void SetContent(Sprite commentorSprite, Sprite itemSprite, string comment)
	{
		this.commentorPortrait.sprite = commentorSprite;
		this.itemPortrait.sprite = itemSprite;
		this.commentorText.text = comment;

		this.commentorPortrait.enabled = true;
		this.itemPortrait.enabled = true;
		this.commentorText.enabled = true;
	}
	public void Clear()
	{
		this.commentorPortrait.sprite = null;
		this.itemPortrait.sprite = null;
		this.commentorText.text = "";

		this.commentorPortrait.enabled = false;
		this.itemPortrait.enabled = false;
		this.commentorText.enabled = false;
	}

	internal void SetContent(object commentorSprite, object itemSprite, object comment)
	{
		throw new NotImplementedException();
	}
}
