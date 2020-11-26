using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FreeSpeechEventSender<T> : MonoBehaviour
{
	public delegate void OnSendEventAction(T t);
	public static event OnSendEventAction OnSendEvent = delegate { };

	protected void SendEvent(T _t)
	{
		OnSendEvent(_t);
	}
}
