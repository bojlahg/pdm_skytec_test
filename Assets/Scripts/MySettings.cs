﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySettings : MonoBehaviour
{
    public bool m_MusicEnabled = true;
    public float m_Volume = 1.0f;
    public int m_WinCount = 0, m_LossCount = 0, m_GameModeIndex = 0;
    public string m_Username = "Username";

    private void Awake()
    {
        RestoreData();
    }

    private void RestoreData()
    {
        m_Volume = PlayerPrefs.GetFloat("Volume", 1.0f);
        m_MusicEnabled = PlayerPrefs.GetInt("MusicEnabled", 1) == 1 ? true : false;
        m_WinCount = PlayerPrefs.GetInt("WinCount", 0);
        m_LossCount = PlayerPrefs.GetInt("LossCount", 0);
        m_GameModeIndex = PlayerPrefs.GetInt("GameModeIndex", 0);
        m_Username = PlayerPrefs.GetString("Username", "Username");
    }

    public void StoreData()
    {
        PlayerPrefs.SetFloat("Volume", m_Volume);
        PlayerPrefs.SetInt("MusicEnabled", m_MusicEnabled ? 1 : 0);
        PlayerPrefs.SetInt("WinCount", m_WinCount);
        PlayerPrefs.SetInt("LossCount", m_LossCount);
        PlayerPrefs.SetInt("GameModeIndex", m_GameModeIndex);
        PlayerPrefs.SetString("Username", m_Username);
        PlayerPrefs.Save();
    }
}
