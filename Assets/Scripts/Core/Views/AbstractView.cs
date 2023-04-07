using Model;
using UnityEngine;

namespace Views
{
	public abstract class AbstractView : MonoBehaviour, IView
	{
		public IModel Model { get; protected set; }

		private void Awake()
		{
			AfterAwake();
		}

		public void BindModel(IModel model)
		{
			RemoveListeners();
			
			Model = model;
		
			AddListeners();

			OnBindComplete();
		}
		
		public void SetParent(Transform parent, bool worldPositionStays = true)
		{
			transform.SetParent(parent, worldPositionStays);
		}

		protected virtual void AfterAwake() {}
		protected virtual void AddListeners()
		{
			if (Model == null)
				return;

			AddChildListeners();
		}

		protected virtual void RemoveListeners()
		{
			if (Model == null)
				return;
			
			RemoveChildListeners();
		}
		
		private void OnDestroy()
		{
			RemoveListeners();
			
			Model = null;
		}
		
		protected virtual void OnBindComplete()
		{
			SyncModel();
		}
		
		protected abstract void AddChildListeners();
		protected abstract void RemoveChildListeners();
		
		protected abstract void SyncModel();
	}
}