using System;
using Utils.Reactivity;

namespace Model.Windows
{
	public class AbstractWindowModel: IModel
	{
		public Observable<bool> IsMinimized { get; } = new Observable<bool>(false);
		public Observable<bool> InteractiveState { get; } = new Observable<bool>(false);

		private Action OnCloseCallback { get; set; }

		public event Action OnHideStart;
		public event Action OnHideEnd;

		public void Close()
		{
			OnCloseCallback?.Invoke();
		}

		public void Minimize()
		{
			IsMinimized.Value = true;
		}

		public void Maximize()
		{
			IsMinimized.Value = false;
		}

		public void StartHide()
		{
			OnHideEnd?.Invoke();
		}
		
		public void HideEnd()
		{
			OnHideStart?.Invoke();
		}

		public void SetInteractiveState(bool interactive)
		{
			InteractiveState.Value = interactive;
		}

		public void SetOnCloseCallback(Action onClose)
		{
			OnCloseCallback = onClose;
		}
	}
}