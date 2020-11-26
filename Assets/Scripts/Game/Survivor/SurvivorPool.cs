public class SurvivorPool : ObjectPool<Survivor> 
{
	private void OnEnable()
	{
		FreeSpeechEventSender<Survivor>.OnSendEvent += ReturnToPool;
	}
	private void OnDisable()
	{
		FreeSpeechEventSender<Survivor>.OnSendEvent -= ReturnToPool;
	}
}
