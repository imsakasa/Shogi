using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using EventHandlers = SystemUI.EventHandlers;

public class SettingsDialog : MonoBehaviour
{
	static readonly Color BUTTON_ACTIVE_COLOR = new Color(125f/255f, 125f/255f, 125f/255f);
	static readonly Color BUTTON_INACTIVE_COLOR = new Color(1f, 1f, 1f);


	[SerializeField] private Button m_ResetButton;

	[SerializeField] private Button m_EasyButton;
	[SerializeField] private Button m_NormalButton;
	[SerializeField] private Button m_HardButton;

	[SerializeField] private Button m_CloseButton;

	private IReadOnlyDictionary<EnemyAI.Difficulty, Button> m_DifficultyButtonDic;

	void Start()
	{
		m_DifficultyButtonDic = new Dictionary<EnemyAI.Difficulty, Button>
		{
			{EnemyAI.Difficulty.Easy, m_EasyButton},
			{EnemyAI.Difficulty.Normal, m_NormalButton},
			{EnemyAI.Difficulty.Hard, m_HardButton},
		};

		SetEnemyAIDifficulty(EnemyAI.Difficulty.Easy);
	}

	public void RegisterEvent(EventHandlers eventHandlers)
	{
		m_ResetButton.onClick.AddListener(() => SystemUI.I.OpenYesNoDialog("Reset Game", "Reset the game?", eventHandlers.OnReset, null));
		m_EasyButton.onClick.AddListener(() => OnPressedDifficultyButton(eventHandlers, EnemyAI.Difficulty.Easy));
		m_NormalButton.onClick.AddListener(() => OnPressedDifficultyButton(eventHandlers, EnemyAI.Difficulty.Normal));
		m_HardButton.onClick.AddListener(() => OnPressedDifficultyButton(eventHandlers, EnemyAI.Difficulty.Hard));
		m_CloseButton.onClick.AddListener(() => this.gameObject.SetActive(false));
	}

	private void OnPressedDifficultyButton(EventHandlers eventHandlers, EnemyAI.Difficulty difficulty)
	{
		SetEnemyAIDifficulty(difficulty);
		eventHandlers.OnChangeDifficulty.Invoke(difficulty);
	}

	public void SetEnemyAIDifficulty(EnemyAI.Difficulty difficulty)
	{
		foreach (var item in m_DifficultyButtonDic)
		{
			var buttonColor = (item.Key == difficulty) ? BUTTON_ACTIVE_COLOR : BUTTON_INACTIVE_COLOR;
			item.Value.GetComponent<Image>().color = buttonColor;
		}
	}
}
