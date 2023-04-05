using Model.Windows;
using UnityEngine;

namespace Views.Windows
{
	[RequireComponent(typeof(CanvasGroup))]
	public abstract class AbstractWindowView: AbstractView, IWindowView
	{
		protected Animator AnimatorController;
		protected CanvasGroup CanvasGroup;
		protected RectTransform Content;

		public AbstractWindowModel Model => base.Model as AbstractWindowModel;
		
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

		public void OnHideStart()
		{
			if (CanvasGroup) 
				CanvasGroup.blocksRaycasts = false;
		}

		public void OnHideEnd()
		{
			if (gameObject)
				Destroy(gameObject);
		}

		private void OnMinimizeChanged(bool needMinimize)
		{
			if (needMinimize)
				OnMinimize();
			else
				OnMaximize();
		}

		protected override void AddListeners()
		{
			if (Model == null)
				return;

			Model.IsMinimized.Changed += OnMinimizeChanged;
			Model.InteractiveState.Changed += SetInteractiveState;
			Model.OnHideStart += OnHideStart;
			Model.OnHideEnd += OnHideEnd;

			AddChildListeners();
		}
		
		protected override void RemoveListeners()
		{
			if (Model == null)
				return;
			
			Model.IsMinimized.Changed -= OnMinimizeChanged;
			Model.InteractiveState.Changed -= SetInteractiveState;
			Model.OnHideStart -= OnHideStart;
			Model.OnHideEnd -= OnHideEnd;

			RemoveChildListeners();
		}
	}
}