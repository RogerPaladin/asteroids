
using Utils.DiContainers;
using UnityEngine;

namespace Utils.Input
{
	public class InputController : IDiContainerChild
	{
		public float Rotation
		{
			get
			{
				if (UnityEngine.Input.GetKey(KeyCode.LeftArrow))
					return -1;
				
				if (UnityEngine.Input.GetKey(KeyCode.RightArrow))
					return 1;
				
				return 0;
			}
		}
		
		public float Thrust 
		{
			get
			{
				if (UnityEngine.Input.GetKey(KeyCode.UpArrow))
					return 1;
				
				return 0;
			}
		}

		public bool NeedShootFirstWeapon => UnityEngine.Input.GetKeyDown(KeyCode.LeftControl);
		public bool NeedShootSecondWeapon => UnityEngine.Input.GetKeyDown(KeyCode.Space);
		public bool IsAnyKeyPressed => UnityEngine.Input.anyKeyDown;
	}
}