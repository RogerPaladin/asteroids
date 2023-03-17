using UnityEngine;

namespace Assets.Scripts.UI.WindowsSystem
{
	[RequireComponent(typeof(CanvasGroup))]
	public class AbstractWindowView: MonoBehaviour
	{
		protected Animator AnimatorController;
		protected CanvasGroup CanvasGroup;
		protected RectTransform Content;
		
		protected void Awake()
		{
			AnimatorController = GetComponent<Animator>();
			CanvasGroup = GetComponent<CanvasGroup>();
		}

		public void OnMinimize()
		{
			if (CanvasGroup) 
				CanvasGroup.blocksRaycasts = false;
		}

		public void OnMaximize()
		{
			if (CanvasGroup) 
				CanvasGroup.blocksRaycasts = true;
		}

		public void SetInteractiveState(bool interactive)
		{
			if (CanvasGroup) CanvasGroup.interactable = interactive;
		}

		public void OnStartHide()
		{
			if (CanvasGroup) 
				CanvasGroup.blocksRaycasts = false;
		}

		public void OnHideEnd()
		{
			if (gameObject)
				Destroy(gameObject);
		}
	}
}