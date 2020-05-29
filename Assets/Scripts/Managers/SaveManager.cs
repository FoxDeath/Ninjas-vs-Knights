using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System;
using UnityEngine.InputSystem;

public class SaveManager : MonoBehaviour
{
    private static SaveManager instance;
    [SerializeField] InputActionAsset inputActions;
    private AudioManager audioManager;
    private MouseLook mouseLook;
    private CrossBow crossBow;

    //Returns the singleton instance of the class.
    public static SaveManager GetInstance()
    {
        return instance;
    }

    //Makes sure there is only one instance at a time.
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    //Loops through the inputs and saves the changes with the help of a binary formatter.
    public void SaveConfig()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/config.ismathrelatedtoscience";
        FileStream stream = new FileStream(path, FileMode.Create);
        var config = new Dictionary<Guid, string>();

        foreach (var map in inputActions.actionMaps)
        {
            foreach (var binding in map.bindings)
            {
                if (!string.IsNullOrEmpty(binding.overridePath))
                {
                    config[binding.id] = binding.overridePath;
                }
            }
        }

        formatter.Serialize(stream, config);
        stream.Close();
    }

    //Loops through the inputs and loads the ones that changed with the help of a binary formatter.
    public void LoadConfig()
    {
        string path = Application.persistentDataPath + "/config.ismathrelatedtoscience";

        if (File.Exists(path) && File.ReadAllBytes(path).Length != 0)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            var config = formatter.Deserialize(stream) as Dictionary<Guid, string>;
            stream.Close();

            foreach (var map in inputActions.actionMaps)
            {
                var bindings = map.bindings;

                for (var i = 0; i < bindings.Count; ++i)
                {
                    if (config.TryGetValue(bindings[i].id, out var overridePath))
                    {
                        map.ApplyBindingOverride(i, new InputBinding { overridePath = overridePath });
                    }
                }
            }
        }
    }

    //Saves the options data file.
    public void SaveOptions()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/options.ismathrelatedtoscience";
        FileStream stream = new FileStream(path, FileMode.Create);
        OptionsData options = new OptionsData(GameObject.Find("UI").transform.Find("OptionsMenu").GetComponent<OptionsMenu>());

        formatter.Serialize(stream, options);
        stream.Close();
    }

    //Loads the option data file and sets the options accordingly.
    public void LoadOptions()
    {
        string path = Application.persistentDataPath + "/options.ismathrelatedtoscience";

        if (File.Exists(path) && File.ReadAllBytes(path).Length != 0)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            var options = formatter.Deserialize(stream) as OptionsData;
            stream.Close();
            
            if(GameObject.Find("UI").transform.Find("OptionsMenu").gameObject.activeSelf)
            {
                GameObject.Find("UI").transform.Find("OptionsMenu").GetComponent<OptionsMenu>().SetVolume(options.volume);
                GameObject.Find("UI").transform.Find("OptionsMenu").GetComponent<OptionsMenu>().SetLookSensitivity(options.lookSens);
                GameObject.Find("UI").transform.Find("OptionsMenu").GetComponent<OptionsMenu>().SetScopedSensitivity(options.scopedSens);
            }
        
            if(FindObjectOfType<AudioManager>() != null)
            {
                FindObjectOfType<AudioManager>().SetMasterVolume(options.volume);
            }
            
            AudioListener.volume = options.volume;
            
            if(FindObjectOfType<MouseLook>() != null)
            {
                FindObjectOfType<MouseLook>().SetSensitivity(options.lookSens);
            }

            if(FindObjectOfType<CrossBow>() != null)
            {
                FindObjectOfType<CrossBow>().SetScopedSensitivity(options.scopedSens);
            }
        }
    }

    //The data class that helps us gather the data for saving the options
    [Serializable]
    private class OptionsData
    {
        public float volume;
        public float lookSens;
        public float scopedSens;
        
        public OptionsData(OptionsMenu optionsMenu)
        {
            volume = optionsMenu.GetVolume();
            lookSens = optionsMenu.GetLookSensitivity();
            scopedSens = optionsMenu.GetScopedSensitivity();
        }
    }
}
