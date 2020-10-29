using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SystemUI : SingletonMonoBehaviour<SystemUI>
{
	static readonly string YES_NO_DIALOG_PATH = "Prefabs/SystemUI/YesNoDialog";

	private SystemDialog m_Dialog;

	private void CreateDialog(string prefabPath)
	{
		GameObject originalObj = (GameObject)Resources.Load(prefabPath);
		GameObject dialogObj = Instantiate(originalObj, Vector3.zero, Quaternion.identity, this.transform);
		dialogObj.transform.localPosition = Vector3.zero;

		m_Dialog = dialogObj.GetComponent<SystemDialog>();
		m_Dialog.Setup(CloseDialog);
	}
	

	public void OpenYesNoDialog(string title, string body, UnityAction yesCallback, UnityAction noCallback)
	{
		CreateDialog(YES_NO_DIALOG_PATH);

		m_Dialog.OpenYesNoDialog(title, body, yesCallback, noCallback);
	}

	public void CloseDialog()
	{
		if (m_Dialog == null)
		{
			return;
		}

		Destroy(m_Dialog.gameObject);
	}
}
