using Model.Windows;
using UnityEngine;

namespace Views.Windows
{
	[RequireComponent(typeof(CanvasGroup))]
	public abstract class AbstractWindowView: AbstractView<AbstractWindowModel>
	{
		protected CanvasGroup CanvasGroup;

		protected override void AfterAwake()
		{
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