public class FoodPool : ObjectPool<Food> 
{
	private void OnEnable()
	{
		FreeSpeechEventSender<Food>.OnSendEvent += ReturnToPool;
	}
	private void OnDisable()
	{
		FreeSpeechEventSender<Food>.OnSendEvent -= ReturnToPool;
	}
}
