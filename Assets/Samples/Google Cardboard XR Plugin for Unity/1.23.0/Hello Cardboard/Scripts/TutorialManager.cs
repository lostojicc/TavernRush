using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class TutorialManager : Singleton<TutorialManager>
{
	[Header("UI")]
	[SerializeField] private GameObject tutorialPanel;

	[Header("Settings")]
	[SerializeField] private float tutorialStepDuration = 5f;

	[Header("Steps")]
	[SerializeField] private TutorialStep[] steps;

	private int currentStepIndex = 0;
	private bool waitingForAction = false;
	public bool IsTutorialActive => currentStepIndex < steps.Length;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	private void Start()
	{
		tutorialPanel.SetActive(true);
		StartCoroutine(RunTutorial());
	}

	private IEnumerator RunTutorial() {
		while (currentStepIndex < steps.Length) {
			TutorialStep step = steps[currentStepIndex];
			tutorialPanel.GetComponentInChildren<TMP_Text>().text = step.text;
			if (step.stepType == TutorialStepType.Auto) 
				yield return new WaitForSecondsRealtime(tutorialStepDuration);
			else {
				waitingForAction = true;
				yield return new WaitUntil(() => waitingForAction == false);
				yield return new WaitForSecondsRealtime(tutorialStepDuration);
			}

			currentStepIndex++;
		}
		tutorialPanel.SetActive(false);
	}

	public void NotifyStepComplete(string actionId) {
		if (!waitingForAction) return;
		
		if (steps[currentStepIndex].actionID == actionId) 
			waitingForAction = false;
	}
}
