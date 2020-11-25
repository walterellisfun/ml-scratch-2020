using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeMany : MonoBehaviour
{
	[SerializeField] private GameObject prefabToMakeMany;
	[SerializeField] private Transform parent;

	[SerializeField] private Vector2Int instances;
	//[SerializeField] private Vector2 gaps;
	[SerializeField] private Vector2Int separation;
	
	private Vector3 position;

	private void Start()
	{
		Bounds bounds = prefabToMakeMany.transform.GetChild(0).GetComponent<Collider>().bounds;
		//print(prefabToMakeMany.transform.GetChild(0).GetComponent<Collider>());
		//Vector3 gaps3 = new Vector3(gaps.x, 0, gaps.y);

		for (int i = 0; i < instances.x; i++)
		{
			for (int j = 0; j < instances.y; j++)
			{
				position = new Vector3(separation.x * i, 0, separation.y * j);
				Instantiate(prefabToMakeMany, position, Quaternion.identity, parent);
				//position = new Vector3(bounds.size.x + gaps.x * i, 0, bounds.size.z + gaps.y * j);
			}
		}
	}
}
