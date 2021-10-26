using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public delegate void OnSettingsCommited();

    public OnSettingsCommited onSettingsCommited;

    public enum SettingType
    {
        Unknown,
        String,
        Bool,
        Int,
        Float
    }

    public class Setting<T>
    {
        internal Setting(SettingInternal<T> si)
        {
            m_SettingInternal = si;
        }

        public T value
        {
            get { return m_SettingInternal.valueProp; }
            set { m_SettingInternal.valueProp = value; }
        }

        private SettingInternal<T> m_SettingInternal;
    }

    internal class SettingInternalBase
    {
        internal string m_Name;
        internal SettingType m_SettingType;

        internal virtual void Commit()
        {

        }

        internal virtual void Rollback()
        {

        }
    }

    internal class SettingInternal<T> : SettingInternalBase
    {
        internal T m_Value, m_Edited, m_Default;

        internal T valueProp
        {
            get { return m_Value; }
            set { m_Edited = value; }
        }

        internal SettingInternal(string n, SettingType t, T d)
        {
            m_Name = n;
            m_SettingType = t;
            m_Value = default(T);
            m_Default = d;
        }

        internal override void Commit()
        {
            m_Value = m_Edited;
        }

        internal override void Rollback()
        {
            m_Edited = m_Value;
        }
    }

    public static Settings instance { get { return m_Instance; } }
    private static Settings m_Instance;

    public delegate void OnSettingsChanged();
    public OnSettingsChanged onSettingsChanged;

    private Dictionary<string, SettingInternalBase> m_SettingsDict = new Dictionary<string, SettingInternalBase>();

    private void Awake()
    {
        m_Instance = this;
    }

    public void AddSetting<T>(string n, T dv)
    {
        if (!m_SettingsDict.ContainsKey(n))
        {
            SettingType st = SettingType.Unknown;
            if(typeof(T) == typeof(string))
            {
                st = SettingType.String;
            }
            else if (typeof(T) == typeof(bool))
            {
                st = SettingType.Bool;
            }
            else if (typeof(T) == typeof(int))
            {
                st = SettingType.Int;
            }
            else if (typeof(T) == typeof(float))
            {
                st = SettingType.Float;
            }
            else
            {
                Debug.LogErrorFormat("Unsupported type: {0}", typeof(T).Name);
            }
            if (st != SettingType.Unknown)
            {
                m_SettingsDict.Add(n, new SettingInternal<T>(n, st, dv));
            }
        }
        else
        {
            Debug.LogErrorFormat("Duplicate setting name: {0}", n);
        }
    }

    public Setting<T> GetSetting<T>(string n)
    {
        SettingInternalBase sib;
        m_SettingsDict.TryGetValue(n, out sib);

        return new Setting<T>((SettingInternal<T>)sib);
    }

    public void Commit()
    {
        foreach (KeyValuePair<string, SettingInternalBase> kvp in m_SettingsDict)
        {
            kvp.Value.Commit();
        }

        StoreData();

        if(onSettingsCommited != null)
        {
            onSettingsCommited.Invoke();
        }
    }

    public void Rollback()
    {
        foreach (KeyValuePair<string, SettingInternalBase> kvp in m_SettingsDict)
        {
            kvp.Value.Rollback();
        }
    }

    public void RestoreData()
    {
        foreach(KeyValuePair<string, SettingInternalBase> kvp in m_SettingsDict)
        {
            switch (kvp.Value.m_SettingType)
            {
                case SettingType.String:
                    SettingInternal<string> sis = (SettingInternal<string>)kvp.Value;
                    sis.m_Value = PlayerPrefs.GetString(sis.m_Name, sis.m_Default);
                    sis.m_Edited = sis.m_Value;
                    break;
                case SettingType.Bool:
                    SettingInternal<bool> sib = (SettingInternal<bool>)kvp.Value;
                    sib.m_Value = PlayerPrefs.GetInt(sib.m_Name, sib.m_Default ? 1 : 0 ) == 0 ? false : true;
                    sib.m_Edited = sib.m_Value;
                    break;
                case SettingType.Int:
                    SettingInternal<int> sii = (SettingInternal<int>)kvp.Value;
                    sii.m_Value = PlayerPrefs.GetInt(sii.m_Name, sii.m_Default);
                    sii.m_Edited = sii.m_Value;
                    break;
                case SettingType.Float:
                    SettingInternal<float> sif = (SettingInternal<float>)kvp.Value;
                    sif.m_Value = PlayerPrefs.GetFloat(sif.m_Name, sif.m_Default);
                    sif.m_Edited = sif.m_Value;
                    break;
            }
        }

        if(onSettingsChanged != null)
        {
            onSettingsChanged.Invoke();
        }
    }

    private void StoreData()
    {
        foreach (KeyValuePair<string, SettingInternalBase> kvp in m_SettingsDict)
        {
            switch (kvp.Value.m_SettingType)
            {
                case SettingType.String:
                    SettingInternal<string> sis = (SettingInternal<string>)kvp.Value;
                    PlayerPrefs.SetString(sis.m_Name, sis.m_Value);
                    break;
                case SettingType.Bool:
                    SettingInternal<bool> sib = (SettingInternal<bool>)kvp.Value;
                    PlayerPrefs.SetInt(sib.m_Name, sib.m_Value ? 1 : 0);
                    break;
                case SettingType.Int:
                    SettingInternal<int> sii = (SettingInternal<int>)kvp.Value;
                    PlayerPrefs.SetInt(sii.m_Name, sii.m_Value);
                    break;
                case SettingType.Float:
                    SettingInternal<float> sif = (SettingInternal<float>)kvp.Value;
                    PlayerPrefs.SetFloat(sif.m_Name, sif.m_Value);
                    break;
            }
        }

        PlayerPrefs.Save();

        if (onSettingsChanged != null)
        {
            onSettingsChanged.Invoke();
        }
    }
}
