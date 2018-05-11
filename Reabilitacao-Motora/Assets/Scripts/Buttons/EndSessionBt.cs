using UnityEngine;
using UnityEngine.UI;

public class EndSessionBt : MonoBehaviour 
{
	[SerializeField]
	protected Button nextPage;

	void Awake ()
	{
		nextPage.onClick.AddListener(delegate{Flow.StaticEndSession();});
	}
}