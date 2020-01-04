using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MLBoxing
{
	public class RevolveCamera : MonoBehaviour
	{

		public Transform target;

		// Use this for initialization
		void Start()
		{

		}

		// Update is called once per frame
		void Update()
		{
			transform.LookAt(target);
			transform.Translate(Vector3.right * Time.deltaTime * 2f);
		}
	}
}

