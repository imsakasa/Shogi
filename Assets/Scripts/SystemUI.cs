using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;
using TMPro;

public class SystemUI : SingletonMonoBehaviour<SystemUI>
{
	static readonly string YES_NO_DIALOG_PATH = "Prefabs/SystemUI/YesNoDialog";
	static readonly float TELOP_MOVE_Y = 1500f;

	private SystemDialog m_Dialog;

	[SerializeField]
	private TextMeshProUGUI m_Telop;

	[SerializeField]
	private Button m_SettingsButton;

	[SerializeField]
	private SettingsDialog m_SettingsDialog;

	public class EventHandlers
	{
		public UnityAction OnReset;
		public UnityAction<EnemyAI.Difficulty> OnChangeDifficulty;
	}

	void Start()
	{
		m_SettingsDialog.gameObject.SetActive(false);
		m_SettingsButton.onClick.AddListener(() => m_SettingsDialog.gameObject.SetActive(true));
	}

	public void RegisterEvent(EventHandlers eventHandlers)
	{
		m_SettingsDialog.RegisterEvent(eventHandlers);
	}

	private void CreateDialog(string prefabPath)
	{
		GameObject originalObj = (GameObject)Resources.Load(prefabPath);
		GameObject dialogObj = Instantiate(originalObj, Vector3.zero, Quaternion.identity, this.transform);
		dialogObj.transform.localPosition = Vector3.zero;

		m_Dialog = dialogObj.GetComponent<SystemDialog>();
		m_Dialog.Setup(CloseDialog);
	}
	

	public void OpenYesNoDialog(string title, string body, UnityAction yesCallback = null, UnityAction noCallback = null)
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

	public void PlayTelop(string text)
	{
		m_Telop.text = text;
		m_Telop.transform.localPosition = new Vector3(TELOP_MOVE_Y, 0f, 0f); // 初期位置をセット

		var sequence = DOTween.Sequence(); 
		sequence.Append(m_Telop.transform.DOLocalMoveX(0f, 0.4f));
		sequence.Append(m_Telop.transform.DOLocalMoveX(-TELOP_MOVE_Y, 0.4f).SetDelay(1.2f));
	}
}
