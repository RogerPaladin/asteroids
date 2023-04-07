using Core.Controllers.Input;
using UnityEngine;

namespace Views.Input
{
	public class InputView : MonoBehaviour
	{
		private InputController _controller;
		
		public void SetData(InputController controller)
		{
			_controller = controller;
		}
		
		private void Update()
		{
			CheckRotation();
			CheckThrust();
			CheckWeapons();
			CheckAnyKeyPressed();
		}

		private void CheckRotation()
		{
			var val = 0;
			
			if (UnityEngine.Input.GetKey(KeyCode.LeftArrow))
				val = -1;
				
			if (UnityEngine.Input.GetKey(KeyCode.RightArrow))
				val = 1;
				
			_controller.SetRotation(val);
		}

		private void CheckThrust()
		{
			var val = 0;
			
			if (UnityEngine.Input.GetKey(KeyCode.UpArrow))
				val = 1;
				
			_controller.SetThrust(val);
		}
		
		private void CheckWeapons()
		{
			var needShootFirstWeapon = UnityEngine.Input.GetKeyDown(KeyCode.LeftControl);
			var needShootSecondWeapon = UnityEngine.Input.GetKeyDown(KeyCode.Space);
			
			_controller.SetNeedShootFirstWeapon(needShootFirstWeapon);
			_controller.SetNeedShootSecondWeapon(needShootSecondWeapon);
		}

		private void CheckAnyKeyPressed()
		{
			var anyKeyDown = UnityEngine.Input.anyKeyDown;
			
			_controller.SetIsAnyKeyPressed(anyKeyDown);
		}
	}
}