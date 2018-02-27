using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Sistemover.TouchGamePad
{
	public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
	{
		public enum AxisOption
		{
			// Options for which axes to use
			Both, // Use both
			OnlyHorizontal, // Only horizontal
			OnlyVertical // Only vertical
		}

		public float MovementRange = 100;
		public AxisOption axesToUse = AxisOption.Both; // The options for the axes that the still will use
		public string horizontalAxisName = "Horizontal"; // The name given to the horizontal axis for the cross platform input
		public string verticalAxisName = "Vertical"; // The name given to the vertical axis for the cross platform input

		Vector3 m_StartPos;
		bool m_UseX; // Toggle for using the x axis
		bool m_UseY; // Toggle for using the Y axis
		CrossPlatformInputManager.VirtualAxis m_HorizontalVirtualAxis; // Reference to the joystick in the cross platform input
		CrossPlatformInputManager.VirtualAxis m_VerticalVirtualAxis; // Reference to the joystick in the cross platform input

		public Canvas c;
		//public Text t;
		int abs_movementRange;
		float p_movementRange;

		void OnEnable()
		{
			CreateVirtualAxes();
		}

        void Start()
        {
			p_movementRange = MovementRange*(c.GetComponent<Transform>().transform.localScale.x);
			//abs_movementRange = Convert.ToInt32 (MovementRange*(c.GetComponent<Transform>().transform.localScale.x));
			//t.text = Convert.ToString(p_movementRange);
            m_StartPos = transform.position;
        }

		void UpdateVirtualAxes(Vector3 value)
		{
			var delta = m_StartPos - value;
			delta.y = -delta.y;
			delta /= p_movementRange;
			if (m_UseX)
			{
				m_HorizontalVirtualAxis.Update(-delta.x);
			}

			if (m_UseY)
			{
				m_VerticalVirtualAxis.Update(delta.y);
			}
		}

		void CreateVirtualAxes()
		{
			// set axes to use
			m_UseX = (axesToUse == AxisOption.Both || axesToUse == AxisOption.OnlyHorizontal);
			m_UseY = (axesToUse == AxisOption.Both || axesToUse == AxisOption.OnlyVertical);

			// create new axes based on axes to use
			if (m_UseX)
			{
				m_HorizontalVirtualAxis = new CrossPlatformInputManager.VirtualAxis(horizontalAxisName);
				CrossPlatformInputManager.RegisterVirtualAxis(m_HorizontalVirtualAxis);
			}
			if (m_UseY)
			{
				m_VerticalVirtualAxis = new CrossPlatformInputManager.VirtualAxis(verticalAxisName);
				CrossPlatformInputManager.RegisterVirtualAxis(m_VerticalVirtualAxis);
			}
		}

		public void OnDrag(PointerEventData data)
		{
			Vector2 newPos = Vector2.zero;
			Vector2 delPos = Vector2.zero;

			if (m_UseX)
				delPos.x = (data.position.x - m_StartPos.x);

			if(m_UseY)
				delPos.y = (data.position.y - m_StartPos.y);
			
			float h = Mathf.Sqrt ((delPos.x*delPos.x)+(delPos.y*delPos.y));
			if (h <= p_movementRange) 
				newPos = delPos;
			else
			{					
				delPos.x = Mathf.Round((delPos.x * p_movementRange) / h);
				delPos.y = Mathf.Round((delPos.y * p_movementRange) / h);
				newPos = delPos;
			}

			transform.position = new Vector2 (m_StartPos.x + newPos.x, m_StartPos.y + newPos.y);
			UpdateVirtualAxes(transform.position);
		}


		public void OnPointerUp(PointerEventData data)
		{
			transform.position = m_StartPos;
			UpdateVirtualAxes(m_StartPos);
		}


		public void OnPointerDown(PointerEventData data) { }

		void OnDisable()
		{
			// remove the joysticks from the cross platform input
			if (m_UseX)
			{
				m_HorizontalVirtualAxis.Remove();
			}
			if (m_UseY)
			{
				m_VerticalVirtualAxis.Remove();
			}
		}
	}
}