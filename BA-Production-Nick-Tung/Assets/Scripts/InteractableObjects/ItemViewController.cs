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
	}
	public void SetContent(Sprite commentorSprite, Sprite itemSprite, string comment)
	{
		this.commentorPortrait.sprite = commentorSprite;
		this.itemPortrait.sprite = itemSprite;
		this.commentorText.text = comment;
	}
	public void Clear()
	{
		this.commentorPortrait.sprite = null;
		this.itemPortrait.sprite = null;
		this.commentorText.text = "";
	}

}
