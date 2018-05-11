using UnityEngine;
using UnityEngine.UI;

public class SessionBt : MonoBehaviour 
{
	[SerializeField]
	protected Button nextPage;

	void Awake ()
	{
		nextPage.onClick.AddListener(delegate{Flow.StaticSession();});
	}
}