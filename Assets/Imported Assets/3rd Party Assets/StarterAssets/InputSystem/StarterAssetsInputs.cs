using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;
		public bool shoot;
		public bool zoom;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

		GameManager gameManager;

        void Start()
        {	
			gameManager = FindFirstObjectByType<GameManager>();
			SetCursorState(true);
        }

#if ENABLE_INPUT_SYSTEM
        public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}

		public void OnShoot(InputValue value)
		{
			ShootInput(value.isPressed);
		}

		public void OnZoom(InputValue value)
		{
			ZoomInput(value.isPressed);
		}
#endif


		public void MoveInput(Vector2 newMoveDirection)
		{
			if (gameManager.gameStarted)
			{
				move = newMoveDirection;
			}
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			if (gameManager.gameStarted)
			{
				look = newLookDirection;
			}
		}

		public void JumpInput(bool newJumpState)
		{
			if (gameManager.gameStarted)
			{
				jump = newJumpState;
			}
		}

		public void SprintInput(bool newSprintState)
		{
			if (gameManager.gameStarted)
			{
				sprint = newSprintState;
			}
		}

		public void ShootInput(bool newShootState)
		{
			if (gameManager.gameStarted)
			{
				shoot = newShootState;
			}
		}

		public void ZoomInput(bool newZoomState)
		{
			if (gameManager.gameStarted)
			{
				zoom = newZoomState;
			}
		}
		
		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		public void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
	
}