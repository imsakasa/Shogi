using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class SystemDialog : MonoBehaviour
{
	static readonly string YES_NO_DIALOG_PATH = "Prefabs/SystemUI/YesNoDialog";

	[SerializeField] private TextMeshProUGUI m_Title;
	[SerializeField] private TextMeshProUGUI m_Body;
	[SerializeField] private Button m_YesButton;
	[SerializeField] private Button m_NoButton;

	public void Setup(UnityAction closeCallback)
	{
		m_YesButton.onClick.AddListener(closeCallback);
		m_NoButton.onClick.AddListener(closeCallback);
	}

	public void OpenYesNoDialog(string title, string body, UnityAction yesCallback, UnityAction noCallback)
	{
		m_Title.text = title;
		m_Body.text = body;

		if (yesCallback != null)
		{
			m_YesButton.onClick.AddListener(yesCallback);
		}

		if (noCallback != null)
		{
			m_NoButton.onClick.AddListener(noCallback);
		}
	}
}
