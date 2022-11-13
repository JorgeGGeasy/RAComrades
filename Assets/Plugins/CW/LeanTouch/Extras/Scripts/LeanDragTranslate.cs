using UnityEngine;
using CW.Common;

namespace Lean.Touch
{
	/// <summary>This component allows you to translate the current GameObject relative to the camera using the finger drag gesture.</summary>
	[HelpURL(LeanTouch.HelpUrlPrefix + "LeanDragTranslate")]
	[AddComponentMenu(LeanTouch.ComponentPathPrefix + "Drag Translate")]
	public class LeanDragTranslate : MonoBehaviour
	{
		/// <summary>The method used to find fingers to use with this component. See LeanFingerFilter documentation for more information.</summary>
		public LeanFingerFilter Use = new LeanFingerFilter(true);

		/// <summary>The camera the translation will be calculated using.
		/// None/null = MainCamera.</summary>
		public Camera Camera { set { _camera = value; } get { return _camera; } }
		[SerializeField] private Camera _camera;

		/// <summary>The movement speed will be multiplied by this.
		/// -1 = Inverted Controls.</summary>
		public float Sensitivity { set { sensitivity = value; } get { return sensitivity; } }
		[SerializeField] private float sensitivity = 1.0f;

		/// <summary>If you want this component to change smoothly over time, then this allows you to control how quick the changes reach their target value.
		/// -1 = Instantly change.
		/// 1 = Slowly change.
		/// 10 = Quickly change.</summary>
		public float Damping { set { damping = value; } get { return damping; } }
		[SerializeField] private float damping = -1.0f;

		/// <summary>This allows you to control how much momentum is retained when the dragging fingers are all released.
		/// NOTE: This requires <b>Dampening</b> to be above 0.</summary>
		public float Inertia { set { inertia = value; } get { return inertia; } }
		[SerializeField] [Range(0.0f, 1.0f)] private float inertia;

		[SerializeField]
		private Vector3 remainingTranslation;

		/// <summary>If you've set Use to ManuallyAddedFingers, then you can call this method to manually add a finger.</summary>
		public void AddFinger(LeanFinger finger)
		{
			Use.AddFinger(finger);
		}

		/// <summary>If you've set Use to ManuallyAddedFingers, then you can call this method to manually remove a finger.</summary>
		public void RemoveFinger(LeanFinger finger)
		{
			Use.RemoveFinger(finger);
		}

		/// <summary>If you've set Use to ManuallyAddedFingers, then you can call this method to manually remove all fingers.</summary>
		public void RemoveAllFingers()
		{
			Use.RemoveAllFingers();
		}

#if UNITY_EDITOR
		protected virtual void Reset()
		{
			Use.UpdateRequiredSelectable(gameObject);
		}
#endif

		protected virtual void Awake()
		{
			Use.UpdateRequiredSelectable(gameObject);
		}

		protected virtual void Update()
		{
			// Store
			var oldPosition = transform.localPosition;

			// Get the fingers we want to use
			var fingers = Use.UpdateAndGetFingers();

			// Calculate the screenDelta value based on these fingers
			var screenDelta = LeanGesture.GetScreenDelta(fingers);

			if (screenDelta != Vector2.zero)
			{
				Debug.Log("Entra en screenDelta");
				// Perform the translation
				if (transform is RectTransform)
				{
					TranslateUI(screenDelta);
				}
				else
				{
                    GameObject objetoUtilidades = GameObject.FindGameObjectWithTag("utilidades");
                    Utilidades utilidades = objetoUtilidades.GetComponent<Utilidades>();
					int numeroDedos;

					var fingers1 = Lean.Touch.LeanTouch.Fingers;

					if (fingers1.Count == 1)
					{
						numeroDedos = 0;
					}
					else
					{
						numeroDedos = 1;
					}

					// Si estamos en android es fingers 0 y si estamos en pc es fingers 1
					Translate(screenDelta, fingers1[numeroDedos].ScreenPosition, utilidades);
				}
			}

			// Increment
			remainingTranslation += transform.localPosition - oldPosition;

			// Get t value
			var factor = CwHelper.DampenFactor(Damping, Time.deltaTime);

			// Dampen remainingDelta
			var newRemainingTranslation = Vector3.Lerp(remainingTranslation, Vector3.zero, factor);

			// Shift this transform by the change in delta
			transform.localPosition = oldPosition + remainingTranslation - newRemainingTranslation;

			if (fingers.Count == 0 && Inertia > 0.0f && Damping > 0.0f)
			{
				newRemainingTranslation = Vector3.Lerp(newRemainingTranslation, remainingTranslation, Inertia);
			}

			// Update remainingDelta with the dampened value
			remainingTranslation = newRemainingTranslation;
		}

		private void TranslateUI(Vector2 screenDelta)
		{
			var camera = this._camera;

			if (camera == null)
			{
				var canvas = transform.GetComponentInParent<Canvas>();

				if (canvas != null && canvas.renderMode != RenderMode.ScreenSpaceOverlay)
				{
					camera = canvas.worldCamera;
				}
			}

			// Screen position of the transform
			var screenPoint = RectTransformUtility.WorldToScreenPoint(camera, transform.position);

			// Add the deltaPosition
			screenPoint += screenDelta * Sensitivity;

			// Convert back to world space
			var worldPoint = default(Vector3);

			if (RectTransformUtility.ScreenPointToWorldPointInRectangle(transform.parent as RectTransform, screenPoint, camera, out worldPoint) == true)
			{
				transform.position = worldPoint;
			}
		}
		private void Translate(Vector2 screenDelta, Vector2 fingerScreenPosition, Utilidades utilidades)
		{
			Debug.Log("Entra en Translate");
			// Make sure the camera exists
			var camera = CwHelper.GetCamera(this._camera, gameObject);

			Debug.Log("Intentando mover");

			if (camera != null)
			{
				// Screen position of the transform
				var screenPoint = camera.WorldToScreenPoint(transform.position);

				Debug.Log("RayCast");
				Ray m_ray = Camera.main.ScreenPointToRay(fingerScreenPosition);
				RaycastHit m_hit;
				if (Physics.Raycast(m_ray, out m_hit, 1000))
				{
					if (m_hit.collider.gameObject.tag == "objetoMovible")
					{
						// Add the deltaPosition
						screenPoint += (Vector3)screenDelta * Sensitivity;
						// Convert back to world space
						transform.position = camera.ScreenToWorldPoint(screenPoint);
					}
				}
				else
				{

					//Debug.Log("Completamente fake este movimento" + camera.WorldToScreenPoint(transform.position) + " " + fingerScreenPosition);

					float posX = camera.WorldToScreenPoint(transform.position).x - fingerScreenPosition.x;
					float posY = camera.WorldToScreenPoint(transform.position).y - fingerScreenPosition.y;

					float valor = utilidades.dameValor();
					float valorNegativo = valor * -1;
					if (posX < valor && posY < valor)
					{
						if (posX > valorNegativo && posY > valorNegativo)
						{
							Debug.Log("Completamente X menor que 10 y mayor que -10 " + posX);

							// Add the deltaPosition
							screenPoint += (Vector3)screenDelta * Sensitivity;
							// Convert back to world space
							transform.position = camera.ScreenToWorldPoint(screenPoint);
						}
					}

				}
			}
			else
			{
				Debug.LogError("Failed to find camera. Either tag your camera as MainCamera, or set one in this component.", this);
			}

		}
	}
}

#if UNITY_EDITOR
namespace Lean.Touch.Editor
{
	using UnityEditor;
	using TARGET = LeanDragTranslate;

	[CanEditMultipleObjects]
	[CustomEditor(typeof(TARGET), true)]
	public class LeanDragTranslate_Editor : CwEditor
	{
		protected override void OnInspector()
		{
			TARGET tgt; TARGET[] tgts; GetTargets(out tgt, out tgts);

			Draw("Use");
			Draw("_camera", "The camera the translation will be calculated using.\n\nNone/null = MainCamera.");
			Draw("sensitivity", "The movement speed will be multiplied by this.\n\n-1 = Inverted Controls.");
			Draw("damping", "If you want this component to change smoothly over time, then this allows you to control how quick the changes reach their target value.\n\n-1 = Instantly change.\n\n1 = Slowly change.\n\n10 = Quickly change.");
			Draw("inertia", "This allows you to control how much momentum is retained when the dragging fingers are all released.\n\nNOTE: This requires <b>Damping</b> to be above 0.");
		}
	}
}
#endif