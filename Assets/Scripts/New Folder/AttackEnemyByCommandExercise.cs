using LLMUnity;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
 
public class AttackEnemyByCommandExercise : MonoBehaviour
{
    public LLMCharacter llmCharacter;
    [SerializeField] GameObject mainCamera;
    [SerializeField] GameObject faceCamera;
    [SerializeField] GameCommandProcessor gameCommand;
    VSTextToSpeechControl TTS;
    [SerializeField] private TMP_Text text;
    void Start()
    {
        TTS = FindAnyObjectByType<VSTextToSpeechControl>();
    }

    string[] GetFunctionNames<T>()
    {
        List<string> functionNames = new List<string>();
        foreach (var function in typeof(T).GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly))
            functionNames.Add(function.Name);
        return functionNames.ToArray();
    }

    string ConstructEnemyPrompt(string message)
    {
        string prompt = "Which of the following enemies should be attacked?\n\n";
        prompt += "Input: " + message + "\n\n";
        prompt += "Choices:\n";
        foreach (string functionName in GetFunctionNames<NPCsNameFunctions>()) prompt += $"- {functionName}\n";
        prompt += "\nAnswer directly with the choice.";
        return prompt;
    }

    string ConstructAttackPrompt(string message)
    {
        string prompt = "Which attack action matches best with the input?\n\n";
        prompt += "Input: " + message + "\n\n";
        prompt += "Choices:\n";
        foreach (string functionName in GetFunctionNames<AttackFunctions>()) prompt += $"- {functionName}\n";
        prompt += "\nAnswer directly with the choice.";
        return prompt;
    }

    public async void onInputFieldSubmit(string message)
    {
        Debug.Log("User input: " + message);

        bool isCommand = message.ToLower().Contains("attack") || message.ToLower().Contains("kick") || message.ToLower().Contains("shoot");

        if (isCommand)
        {
            Debug.Log("Processing command...");

            string enemyFunctionName = await llmCharacter.Chat(ConstructEnemyPrompt(message));
            string enemyName = (string)typeof(NPCsNameFunctions).GetMethod(enemyFunctionName).Invoke(null, null);

            string attackFunctionName = await llmCharacter.Chat(ConstructAttackPrompt(message));

            if (!string.IsNullOrEmpty(enemyName))
            {
                mainCamera.SetActive(true);
                faceCamera.SetActive(false);
                string attackName = (string)typeof(AttackFunctions).GetMethod(attackFunctionName).Invoke(null, null);
                gameCommand.ProcessCommand(attackName);
                Debug.Log($"Executed attack on: {enemyName}");
            }
            else
            {
                TTS.SpeakMyText("No enemy mentioned in the command");
                Debug.Log("No enemy mentioned in the command.");
            }
        }
        else
        {
            Debug.Log("Processing normal conversation...");
            mainCamera.SetActive(false);
            faceCamera.SetActive(true);
            string response = await llmCharacter.Chat(message);
            TTS.SpeakMyText(response);
            Debug.Log("AI Response: " + response);
        }
        text.text = "Press E to Record ";
    }


   


    public void CancelRequests()
    {
        llmCharacter.CancelRequests();
    }

    public void ExitGame()
    {
        Debug.Log("Exit button clicked");
        Application.Quit();
    }

    bool onValidateWarning = true;


    void OnValidate()
    {
        if (onValidateWarning && !llmCharacter.remote && llmCharacter.llm != null && llmCharacter.llm.model == "")
        {
            Debug.LogWarning($"Please select a model in the {llmCharacter.llm.gameObject.name} GameObject!");
            onValidateWarning = false;
        }
    }
}
