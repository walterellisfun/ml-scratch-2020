using UnityEngine;

public class Food : MonoBehaviour
{
	private float nutritionalValue = 0.2f;

	public float Consume()
	{
		Destroy(gameObject);
		return nutritionalValue;
	}
}
