using System;

namespace Assets.Scripts.UI.WindowsSystem
{
    public abstract class AbstractWindow
	{
		public bool CanClose { get; private set; } = true;
		public bool IsMinimized { get; private set; } = false;
		public bool IsOpening { get; protected set; }
		public bool IsClosing { get; protected set; }

		protected readonly AbstractWindowView AbstractView;
		protected readonly WindowsController Controller;

		protected Action OnCloseCallback = null;
		
		public virtual string ClassName
        {
            get
            {
                return GetType().Name;
            }
		}

		public AbstractWindow(WindowsController windowsController, AbstractWindowView windowAbstractView)
		{
			Controller = windowsController;
			AbstractView = windowAbstractView;
		}

		public virtual void Close()
        {
            if (!CanClose) return;

			if (IsClosing)
				return;
			
			IsClosing = true;

			if (Controller != null)
				Controller.RemoveFromController(this);

			OnCloseCallback?.Invoke();
			
			OnBeforeClose();
			StartHide();
			OnClose();
		}
		
		public virtual void Minimize()
		{
			if (IsClosing)
				return;

			AbstractView.OnMinimize();

			IsMinimized = true;
		}

		public virtual void Maximize()
		{
			AbstractView.OnMaximize();

			IsMinimized = false;
		}

		protected virtual void OnShow()
        {
        }
		
		protected virtual void Start()
        {
            OnStart();
            IsOpening = true;

			OnAnimationEnd();
		}

		protected virtual void OnAnimationEnd()
        {
			OnShow();

            IsOpening = false;
        }

        protected virtual void OnStart()
        {
			OnShowPlaySound();
        }
		
		protected virtual void OnHideStart() { }
		protected virtual void OnHideEnd()
		{
			AbstractView.OnHideEnd();
		}

		protected virtual void OnShowPlaySound()
		{

		}

        protected virtual void OnHidePlaySound()
        {

        }

        protected virtual void SetInteractiveState(bool interactive)
		{
			AbstractView.SetInteractiveState(interactive);
		}

		protected virtual void OnClose() { }
		
		protected virtual void OnBeforeClose() { }
		
        protected void StartHide()
        {
			OnHideStart();

            SetInteractiveState(false);

			AbstractView.OnStartHide();

            OnHidePlaySound();
			
			Destroy();
        }

		protected void Destroy()
        {
			OnHideEnd();
        }
		
		public void SetOnClose(Action onClose)
		{
			OnCloseCallback = onClose;
		}
	}
}
