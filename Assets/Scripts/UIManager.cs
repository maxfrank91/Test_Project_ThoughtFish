using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public GameObject buttonPrefab;
	public GameObject menuButtonPrefab;
	public GameObject UIMain;
	public GameObject UIMenu;

	public bool record = false;

	string fileName;
	
	struct RecordData
	{
		GameObject go;
		Event usedEvent;
		float time;
	};

	List<GameObject> buttons = new List<GameObject>();
	int buttoncount = 3;

	List<RecordData> recData = new List<RecordData>();
	List<GameObject> menuButtons = new List<GameObject>();
	
	// Use this for initialization
	void Start () 
	{
		for (int i = 0; i < buttoncount; i++)
		{
			GameObject button = Instantiate(buttonPrefab);
			button.transform.position = new Vector3(250*(i+1), -200, 0);
			button.GetComponent<RectTransform>().sizeDelta = new Vector2(Random.Range(100,200), Random.Range(50,150));
			button.transform.parent = UIMain.transform;
			buttons.Add(button);
		}
		readJSON();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void readJSON()
	{
		string jsonString = Resources.Load<TextAsset>("menu").text;
		JSONObject j = new JSONObject(jsonString);
		accessData(j);

	}
	void accessData(JSONObject obj)
	{
		switch(obj.type)
		{
		case JSONObject.Type.OBJECT:
			for(int i = 0; i < obj.list.Count; i++){
				string key = (string)obj.keys[i];
				if (key == "button")
				{
					GameObject mbutton = Instantiate(menuButtonPrefab);
					menuButtons.Add(mbutton);
				}
				JSONObject j = (JSONObject)obj.list[i];
				Debug.Log(key);
				accessData(j);
			}
			break;
		case JSONObject.Type.STRING:
			Debug.Log(obj.str);
			break;
		}
	}
}
