using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class InGameMenu : SingletonMonobehavior<InGameMenu>
{
    [SerializeField]
    [Required]
    Animator animator = null;
    public void HideInGameMenu()
    {
        animator.SetTrigger("HideInGameMenu");
    }
    public void ShowInGameMenu()
    {
        animator.SetTrigger("ShowInGameMenu");
    }
    public void ResumeGame()
    {
    }
    public void ExitGame()
    {
    }
    public void GoToMainMenu()
    {
    }
}
