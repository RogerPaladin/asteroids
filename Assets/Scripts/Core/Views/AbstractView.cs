using Model;
using UnityEngine;

namespace Views
{
	public abstract class AbstractView<T> : MonoBehaviour, IView where T : IModel
	{
		protected T Model { get; private set; }

		private void Awake()
		{
			AfterAwake();
		}

		public void BindModel(T model)
		{
			RemoveListeners();
			
			Model = model;
		
			AddListeners();

			SyncModel();
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
			
			Model = default;
		}
		
		protected abstract void AddChildListeners();
		protected abstract void RemoveChildListeners();
		
		protected abstract void SyncModel();
	}
}