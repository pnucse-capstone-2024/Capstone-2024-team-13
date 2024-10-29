using System.Collections;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.XR.Interaction.Toolkit;

namespace SojaExiles

{
	public class opencloseStallDoor : MonoBehaviour
	{

		public Animator openandclose;
		public bool open;
		public Transform Player;
		private NetworkObject netObj;

		public ActionBasedController leftController;
		public InputHelpers.Button interactionButton = InputHelpers.Button.Trigger; // Button to use for interaction
		private float activationThreshold = 0.1f;

		void Start()
		{
			StartCoroutine(WaitForNetworkObject());
			open = false;
		}

		private IEnumerator WaitForNetworkObject()
		{
			while (NetworkObjectManager.Instance.GetNetworkObject() == null)
			{
				Debug.Log("Waiting for network object...");
				yield return new WaitForSeconds(0.1f);
			}

			netObj = NetworkObjectManager.Instance.GetNetworkObject();

			if (netObj != null)
			{
				Player = netObj.transform.Find("LeftHand");
				if(Player != null)
                {
					StartCoroutine(InteractionRoutine());
                }
			}
			else
			{
				Debug.LogError("NetworkObject is null.");
			}
		}

		private IEnumerator InteractionRoutine()
		{
			while (true)
			{
				if (leftController != null)
				{
					float dist = Vector3.Distance(Player.position, transform.position);
					if (dist < 15)
					{
						if (leftController.selectAction.action.ReadValue<float>() > activationThreshold)
						{
							if (!open)
							{
								StartCoroutine(opening());
							}
							else
							{
								StartCoroutine(closing());
							}
						}
					}
				}
				yield return null;
			}
		}


		IEnumerator opening()
		{
			print("you are opening the door");
			openandclose.Play("OpeningStall");
			open = true;
			yield return new WaitForSeconds(.5f);
		}

		IEnumerator closing()
		{
			print("you are closing the door");
			openandclose.Play("ClosingStall");
			open = false;
			yield return new WaitForSeconds(.5f);
		}


	}
}