using Model;
using UnityEngine;

namespace Views
{
	public abstract class AbstractView : MonoBehaviour, IView
	{
		public IModel Model { get; protected set; }
		
		public void BindModel(IModel model)
		{
			RemoveListeners();
			
			Model = model;
		
			AddListeners();
		}

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
		
		protected abstract void AddChildListeners();
		protected abstract void RemoveChildListeners();
	}
}