using UnityEngine;

namespace Views.GamePlay.Background
{
	public interface IBackgroundView
	{
		void OnSizeChange(Vector2 size);
		void OnChangeParent(Transform parent);
	}
}