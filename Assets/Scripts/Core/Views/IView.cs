using UnityEngine;

namespace Views
{
	public interface IView
	{
		public void SetParent(Transform parent, bool worldPositionStays = true);
	}
}