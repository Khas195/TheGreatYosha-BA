using NaughtyAttributes;
using UnityEngine;

public class CharacterPosition : MonoBehaviour
{
	public string characterName;
	public string positionName;
	public SpriteRenderer sprite;
	public FadeTransition transition;
	[Button]
	public void Show()
	{
		var color = this.sprite.color;
		color.a = 1;
		this.sprite.color = color;
	}
	[Button]
	public void Hide()
	{
		var color = this.sprite.color;
		color.a = 0;
		this.sprite.color = color;
	}
}
