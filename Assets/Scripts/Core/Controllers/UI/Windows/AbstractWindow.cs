using System;
using Model.Windows;

namespace Controllers.UI.Windows
{
    public abstract class AbstractWindow
	{
		public readonly AbstractWindowModel Model;
		protected readonly WindowsSystem System;

		public virtual string ClassName
        {
            get
            {
                return GetType().Name;
            }
		}

		public AbstractWindow(WindowsSystem windowsSystem, AbstractWindowModel model)
		{
			System = windowsSystem;

			if (model == null)
			{
				var type = GetType();
				var constructors = type.GetConstructors();
				var ctor = constructors[0];
				var parameters = ctor.GetParameters();
				Type modelRuntimeType = parameters[1].ParameterType;
				Model = (AbstractWindowModel)Activator.CreateInstance(modelRuntimeType);
			}
			else
				Model = model;
		}

		public virtual void Close()
        {
            if (!Model.CanClose.Value) return;

			if (Model.IsClosing.Value)
				return;

			Model.Close();

			if (System != null)
				System.RemoveFromController(this);

			OnBeforeClose();
			StartHide();
			OnClose();
		}
		
		public virtual void Minimize()
		{
			if (Model.IsClosing.Value)
				return;

			Model.Minimize();
		}

		public virtual void Maximize()
		{
			Model.Maximize();
		}

		protected virtual void OnShow()
        {
        }
		
		protected virtual void Start()
        {
            OnStart();
			
			Model.Start();
			
			OnAnimationEnd();
		}

		protected virtual void OnAnimationEnd()
        {
			OnShow();

			Model.AnimationEnd();
		}

        protected virtual void OnStart()
        {
			OnShowPlaySound();
        }
		
		protected virtual void OnHideStart() { }
		protected virtual void OnHideEnd()
		{
			Model.HideEnd();
		}

		protected virtual void OnShowPlaySound()
		{

		}

        protected virtual void OnHidePlaySound()
        {

        }

        protected virtual void SetInteractiveState(bool interactive)
		{
			Model.SetInteractiveState(interactive);
		}

		protected virtual void OnClose() { }
		
		protected virtual void OnBeforeClose() { }
		
        protected void StartHide()
        {
			OnHideStart();

            SetInteractiveState(false);

			Model.StartHide();

			OnHidePlaySound();
			
			Destroy();
        }

		protected void Destroy()
        {
			OnHideEnd();
        }
		
		public void SetOnClose(Action onClose)
		{
			Model.SetOnCloseCallback(onClose);
		}
	}
}
