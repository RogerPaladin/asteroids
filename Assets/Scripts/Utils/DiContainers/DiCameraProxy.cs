using UnityEngine;

namespace Utils.DiContainers
{
	public class DiCameraProxy : IDiContainerChild
	{
		public readonly Camera Camera;

		public DiCameraProxy(Camera camera) { Camera = camera; }
	}
}