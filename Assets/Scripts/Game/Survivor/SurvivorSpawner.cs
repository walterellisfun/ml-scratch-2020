public class SurvivorSpawner : ObjectSpawner<SurvivorPool, Survivor>
{
	//debug
	private void Start()
	{
		for (int i = 0; i < 10; i++)
		{
			SpawnObject();
		}
	}
}
