using Random = UnityEngine.Random;
using UnityEngine;

public enum ReturnType
{
	Text,
	Number,
	RandomMix,
}

public class InputTesting
{
	public static object ReturnTest(ReturnType _returnType)
	{
		return _returnType switch
		{
			ReturnType.Number    => Random.Range(0, 100),
			ReturnType.Text      => "you returned text",
			ReturnType.RandomMix => "text and " + Random.Range(0, 100),
			_                    => "Undefined"
		};
	}

	public static void SpawnTest(float _offset)
	{
		MonoBehaviour.Instantiate(Resources.Load<GameObject>("Cube"), new Vector3(0,_offset,0), Quaternion.identity);
	}
}