using Model;
using UnityEngine;

namespace Views
{
	public interface IView
	{
		public void BindModel(IModel model);
		public void SetParent(Transform parent);
	}
}