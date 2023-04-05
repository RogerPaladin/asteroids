using System;
using Utils.Reactivity;

namespace Model.Windows
{
	public class AbstractWindowModel: IModel
	{
		public Observable<bool> CanClose { get; private set; } = new Observable<bool>(true);
		public Observable<bool> IsMinimized { get; private set; } = new Observable<bool>(false);
		public Observable<bool> IsOpening { get; protected set; } = new Observable<bool>(false);
		public Observable<bool> IsClosing { get; protected set; } = new Observable<bool>(false);
		
		public Observable<bool> InteractiveState { get; protected set; } = new Observable<bool>(false);
		
		public Observable<Action> OnCloseCallback { get; protected set; } = new Observable<Action>(null);

		public event Action OnHideStart;
		public event Action OnHideEnd;

		public void Close()
		{
			IsClosing.Value = true;
			
			OnCloseCallback?.Value?.Invoke();
		}

		public void Minimize()
		{
			IsMinimized.Value = true;
		}

		public void Maximize()
		{
			IsMinimized.Value = false;
		}

		public void Start()
		{
			IsOpening.Value = true;
		}

		public void AnimationEnd()
		{
			IsOpening.Value = false;
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
			OnCloseCallback.Value = onClose;
		}
	}
}