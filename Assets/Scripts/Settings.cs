using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public enum SettingType
    {
        String,
        Bool,
        Int,
        Float
    }

    [System.Serializable]
    public class Setting
    {
        public string m_Name;
        public SettingType m_SettingType;
        public Object m_Default;
    }

    internal class SettingInternal<T>
    {
        public T value
        {
            set { m_Value = value; } get { return m_Value; }
        }

        private T m_Value;
    }

    public static Settings instance { get { return m_Instance; } }
    private static Settings m_Instance;

    public delegate void OnSettingsChanged();
    public OnSettingsChanged onSettingsChanged;

    public bool m_MusicEnabled = true;
    public float m_SoundVolume = 1.0f;
    public int m_ScoreCount = 0, m_GameModeIndex = 0;
    public string m_Username = "Username";

    public Setting[] m_Settings;
    private Dictionary<string, Setting> m_SettingsDict = new Dictionary<string, Setting>();

    private void Awake()
    {
        m_Instance = this;

        for(int i = 0; i < m_Settings.Length; ++i)
        {
            if(!m_SettingsDict.ContainsKey(m_Settings[i].m_Name))
            {
                m_SettingsDict.Add(m_Settings[i].m_Name, m_Settings[i]);
            }
            else
            {
                Debug.LogErrorFormat("Duplicate setting name: {0}", m_Settings[0].m_Name);
            }
        }
    }

    public void RestoreData()
    {
        m_SoundVolume = PlayerPrefs.GetFloat("Volume", 1.0f);
        m_MusicEnabled = PlayerPrefs.GetInt("MusicEnabled", 1) == 1 ? true : false;
        m_ScoreCount = PlayerPrefs.GetInt("ScoreCount", 0);
        m_GameModeIndex = PlayerPrefs.GetInt("GameModeIndex", 0);
        m_Username = PlayerPrefs.GetString("Username", "Username");

        if(onSettingsChanged != null)
        {
            onSettingsChanged.Invoke();
        }
    }

    public void StoreData()
    {
        PlayerPrefs.SetFloat("Volume", m_SoundVolume);
        PlayerPrefs.SetInt("MusicEnabled", m_MusicEnabled ? 1 : 0);
        PlayerPrefs.SetInt("ScoreCount", m_ScoreCount);
        PlayerPrefs.SetInt("GameModeIndex", m_GameModeIndex);
        PlayerPrefs.SetString("Username", m_Username);
        PlayerPrefs.Save();

        if (onSettingsChanged != null)
        {
            onSettingsChanged.Invoke();
        }
    }
}
