using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGame : MonoBehaviour, IGame
{
    internal class Map
    {
        public Map(int size)
        {
            m_Size = size;
            m_Map = new int[m_Size, m_Size];
        }

        public int this[int x, int y]
        {
            get
            {
                if (x >= 0 && x < m_Size && y >= 0 && y < m_Size)
                {
                    return m_Map[x, y];
                }
                else
                {
                    return -1;
                }
            }

            set
            {
                m_Map[x, y] = value;
            }
        }

        private int[,] m_Map;
        private int m_Size;
    };

    public Camera m_Camera;
    public GameObject m_TilePrefab, m_XPrefab, m_OPrefab;


    private GameObject[,] m_TileField;
    private GameObject[,] m_MovesField;
    private Map m_Map;
    private int m_Size = 0, m_Turn = 0, m_TurnsMade = 0;
    private bool m_GameStarted = false, m_GamePaused = false;
    private float m_Timer = 0;

    private Settings.Setting<int> m_GameModeIndexSetting, m_ScoreCountSetting;

    private void Start()
    {
        m_GameModeIndexSetting = Settings.instance.GetSetting<int>("GameModeIndex");
        m_ScoreCountSetting = Settings.instance.GetSetting<int>("ScoreCount");
    }

    public void StartGame()
    {
        m_GameStarted = true;
        m_Size = 3 + m_GameModeIndexSetting.value;

        CreateField();

        m_Timer = 0;
        m_TurnsMade = 0;
        m_Turn = Random.Range(1, 3);
        if (m_Turn == 2)
        {
            CPUTurn();
        }

        m_Camera.orthographicSize = 0.5f * (m_Size + 1.0f) / m_Camera.aspect;
    }

    public void AbortGame()
    {
        m_GameStarted = false;
        m_GamePaused = false;
        DestroyField();
    }

    public void PauseGame()
    {
        m_GamePaused = true;
    }

    public void UnpauseGame()
    {
        m_GamePaused = false;
    }

    private void GameFinished(int winner)
    {
        MyApp.instance.m_MyResultsMenu.m_ScoreAnimFrom = m_ScoreCountSetting.value;
        if (winner == 1)
        {
            m_ScoreCountSetting.value += 100;
            SoundManager.instance.PlayOnce("Win");
        }
        else if (winner == 2)
        {
            m_ScoreCountSetting.value -= 100;
            SoundManager.instance.PlayOnce("Loose");
        }
        MyApp.instance.m_Settings.Commit();
        m_GameStarted = false;

        DestroyField();
        

        MyApp.instance.m_MyResultsMenu.m_Result = winner;
        MyApp.instance.m_MyResultsMenu.m_ScoreAnimTo = m_ScoreCountSetting.value;

        GUIManager.instance.ReplaceTop(MyApp.instance.m_MyResultsMenu);
    }

    private Vector2 FromIntCoord(Vector2Int pos)
    {
        return new Vector2(-(float)m_Size * 0.5f + 0.5f + pos.x, -(float)m_Size * 0.5f + 0.5f + pos.y);
    }

    private Vector2Int ToIntCoord(Vector2 pos)
    {
        return new Vector2Int(Mathf.FloorToInt(pos.x + (float)m_Size * 0.5f), Mathf.FloorToInt(pos.y + (float)m_Size * 0.5f));
    }

    private void CreateField()
    {
        m_Map = new Map(m_Size);
        m_TileField = new GameObject[m_Size, m_Size];
        m_MovesField = new GameObject[m_Size, m_Size];

        for (int y = 0; y < m_Size; ++y)
        {
            for (int x = 0; x < m_Size; ++x)
            {
                m_Map[x, y] = 0;
                m_TileField[x, y] = GameObject.Instantiate(m_TilePrefab, FromIntCoord(new Vector2Int(x, y)), Quaternion.identity, transform);
            }
        }
    }

    private void DestroyField()
    {
        for (int y = 0; y < m_Size; ++y)
        {
            for (int x = 0; x < m_Size; ++x)
            {
                GameObject.Destroy(m_TileField[x, y]);
                if (m_MovesField[x, y] != null)
                {
                    GameObject.Destroy(m_MovesField[x, y]);
                }
            }
        }
        m_Map = null;
        m_TileField = null;
        m_MovesField = null;
    }

    private void MadeMove(Vector2Int pos, int p)
    {
        GameObject newgo;
        if (p == 1)
        {
            newgo = GameObject.Instantiate(m_XPrefab, FromIntCoord(pos), Quaternion.identity, transform);
        }
        else
        {
            newgo = GameObject.Instantiate(m_OPrefab, FromIntCoord(pos), Quaternion.identity, transform);
        }
        m_MovesField[pos.x, pos.y] = newgo;
        StartCoroutine(AnimateMove(newgo));
    }

    private IEnumerator AnimateMove(GameObject go)
    {
        int nextturn = m_Turn + 1;
        if (nextturn == 3)
        {
            nextturn = 1;
        }
        m_Turn = 0; // блокировка

        SoundManager.instance.PlayOnce("Move");
        float timer = 0;
        while (timer < 0.5f)
        {
            float t = timer * 2;
            go.transform.localScale = Vector3.one * EaseOutBack(t);
            yield return null;
            timer += Time.deltaTime;
        }
        go.transform.localScale = Vector3.one;

        m_Turn = nextturn;

        ++m_TurnsMade;

        CheckWinner();
    }

    private static float EaseOutBack(float x)
    {
        float c1 = 1.70158f;
        float c3 = c1 + 1;
        float xm1 = x - 1;
        return 1 + c3 * xm1 * xm1 * xm1 + c1 * xm1 * xm1;
    }

    private void CPUTurn()
    {
        Vector2Int pos;

        List<Vector2Int> moves = new List<Vector2Int>();

        // Рандомный ход
        List<Vector2Int> randomMoves = new List<Vector2Int>();
        for (int y = 0; y < m_Size; ++y)
        {
            for (int x = 0; x < m_Size; ++x)
            {
                if (m_Map[x, y] == 0)
                {
                    randomMoves.Add(new Vector2Int(x, y));
                }
            }
        }
        // Ищем ходы - заканчивающие игру победой
        List<Vector2Int> winMoves = new List<Vector2Int>();
        for (int y = 0; y < m_Size; ++y)
        {
            for (int x = 0; x < m_Size; ++x)
            {
                // Диагональ /
                if (m_Map[x + 0, y + 0] == 0 && m_Map[x + 1, y + 1] == 2 && m_Map[x + 2, y + 2] == 2)
                {
                    winMoves.Add(new Vector2Int(x + 0, y + 0));
                }
                else if (m_Map[x + 0, y + 0] == 2 && m_Map[x + 1, y + 1] == 0 && m_Map[x + 2, y + 2] == 2)
                {
                    winMoves.Add(new Vector2Int(x + 1, y + 1));
                }
                else if (m_Map[x + 0, y + 0] == 2 && m_Map[x + 1, y + 1] == 2 && m_Map[x + 2, y + 2] == 0)
                {
                    winMoves.Add(new Vector2Int(x + 2, y + 2));
                }
                // Диагональ \
                if (m_Map[x + 0, y + 2] == 0 && m_Map[x + 1, y + 1] == 2 && m_Map[x + 2, y + 0] == 2)
                {
                    winMoves.Add(new Vector2Int(x + 0, y + 2));
                }
                else if (m_Map[x + 0, y + 2] == 2 && m_Map[x + 1, y + 1] == 0 && m_Map[x + 2, y + 0] == 2)
                {
                    winMoves.Add(new Vector2Int(x + 1, y + 1));
                }
                else if (m_Map[x + 0, y + 2] == 2 && m_Map[x + 1, y + 1] == 2 && m_Map[x + 2, y + 0] == 0)
                {
                    winMoves.Add(new Vector2Int(x + 2, y + 0));
                }
                // Горизонталь -
                if (m_Map[x + 0, y + 0] == 0 && m_Map[x + 1, y + 0] == 2 && m_Map[x + 2, y + 0] == 2)
                {
                    winMoves.Add(new Vector2Int(x + 0, y + 0));
                }
                else if (m_Map[x + 0, y + 0] == 2 && m_Map[x + 1, y + 0] == 0 && m_Map[x + 2, y + 0] == 2)
                {
                    winMoves.Add(new Vector2Int(x + 1, y + 0));
                }
                else if (m_Map[x + 0, y + 0] == 2 && m_Map[x + 1, y + 0] == 2 && m_Map[x + 2, y + 0] == 0)
                {
                    winMoves.Add(new Vector2Int(x + 2, y + 0));
                }
                // Вертикаль |
                if (m_Map[x + 0, y + 0] == 0 && m_Map[x + 0, y + 1] == 2 && m_Map[x + 0, y + 2] == 2)
                {
                    winMoves.Add(new Vector2Int(x + 0, y + 0));
                }
                else if (m_Map[x + 0, y + 0] == 2 && m_Map[x + 0, y + 1] == 0 && m_Map[x + 0, y + 2] == 2)
                {
                    winMoves.Add(new Vector2Int(x + 0, y + 1));
                }
                else if (m_Map[x + 0, y + 0] == 2 && m_Map[x + 0, y + 1] == 2 && m_Map[x + 0, y + 2] == 0)
                {
                    winMoves.Add(new Vector2Int(x + 0, y + 2));
                }
            }
        }
        // Ищем ходы - для блокировки противника
        List<Vector2Int> blockMoves = new List<Vector2Int>();
        for (int y = 0; y < m_Size; ++y)
        {
            for (int x = 0; x < m_Size; ++x)
            {
                // Диагональ /
                if (m_Map[x + 0, y + 0] == 0 && m_Map[x + 1, y + 1] == 1 && m_Map[x + 2, y + 2] == 1)
                {
                    blockMoves.Add(new Vector2Int(x + 0, y + 0));
                }
                else if (m_Map[x + 0, y + 0] == 1 && m_Map[x + 1, y + 1] == 0 && m_Map[x + 2, y + 2] == 1)
                {
                    blockMoves.Add(new Vector2Int(x + 1, y + 1));
                }
                else if (m_Map[x + 0, y + 0] == 1 && m_Map[x + 1, y + 1] == 1 && m_Map[x + 2, y + 2] == 0)
                {
                    blockMoves.Add(new Vector2Int(x + 2, y + 2));
                }
                // Диагональ \
                if (m_Map[x + 0, y + 2] == 0 && m_Map[x + 1, y + 1] == 1 && m_Map[x + 2, y + 0] == 1)
                {
                    blockMoves.Add(new Vector2Int(x + 0, y + 2));
                }
                else if (m_Map[x + 0, y + 2] == 1 && m_Map[x + 1, y + 1] == 0 && m_Map[x + 2, y + 0] == 1)
                {
                    blockMoves.Add(new Vector2Int(x + 1, y + 1));
                }
                else if (m_Map[x + 0, y + 2] == 1 && m_Map[x + 1, y + 1] == 1 && m_Map[x + 2, y + 0] == 0)
                {
                    blockMoves.Add(new Vector2Int(x + 2, y + 0));
                }
                // Горизонталь -
                if (m_Map[x + 0, y + 0] == 0 && m_Map[x + 1, y + 0] == 1 && m_Map[x + 2, y + 0] == 1)
                {
                    blockMoves.Add(new Vector2Int(x + 0, y + 0));
                }
                else if (m_Map[x + 0, y + 0] == 1 && m_Map[x + 1, y + 0] == 0 && m_Map[x + 2, y + 0] == 1)
                {
                    blockMoves.Add(new Vector2Int(x + 1, y + 0));
                }
                else if (m_Map[x + 0, y + 0] == 1 && m_Map[x + 1, y + 0] == 1 && m_Map[x + 2, y + 0] == 0)
                {
                    blockMoves.Add(new Vector2Int(x + 2, y + 0));
                }
                // Вертикаль |
                if (m_Map[x + 0, y + 0] == 0 && m_Map[x + 0, y + 1] == 1 && m_Map[x + 0, y + 2] == 1)
                {
                    blockMoves.Add(new Vector2Int(x + 0, y + 0));
                }
                else if (m_Map[x + 0, y + 0] == 1 && m_Map[x + 0, y + 1] == 0 && m_Map[x + 0, y + 2] == 1)
                {
                    blockMoves.Add(new Vector2Int(x + 0, y + 1));
                }
                else if (m_Map[x + 0, y + 0] == 1 && m_Map[x + 0, y + 1] == 1 && m_Map[x + 0, y + 2] == 0)
                {
                    blockMoves.Add(new Vector2Int(x + 0, y + 2));
                }
            }
        }

        if (randomMoves.Count > 0)
        {
            moves.Add(randomMoves[Random.Range(0, randomMoves.Count)]);
        }
        if (winMoves.Count > 0)
        {
            moves.Add(winMoves[Random.Range(0, winMoves.Count)]);
        }
        if (blockMoves.Count > 0)
        {
            moves.Add(blockMoves[Random.Range(0, blockMoves.Count)]);
        }

        // Выберем рандомный ход
        pos = moves[Random.Range(0, moves.Count)];

        m_Map[pos.x, pos.y] = m_Turn;

        MadeMove(pos, 2);
    }

    private void CheckWinner()
    {
        // 0 - ничья, 1 - пользователь, 2 - компьютер, 3 - не закончена
        int winner = 3;
        for (int p = 1; p < 3; ++p)
        {
            for (int y = 0; y < m_Size; ++y)
            {
                for (int x = 0; x < m_Size; ++x)
                {
                    // Диагональ /
                    if (m_Map[x + 0, y + 0] == p && m_Map[x + 1, y + 1] == p && m_Map[x + 2, y + 2] == p)
                    {
                        winner = p;
                    }
                    else if (m_Map[x + 0, y + 0] == p && m_Map[x + 1, y + 1] == p && m_Map[x + 2, y + 2] == p)
                    {
                        winner = p;
                    }
                    else if (m_Map[x + 0, y + 0] == p && m_Map[x + 1, y + 1] == p && m_Map[x + 2, y + 2] == p)
                    {
                        winner = p;
                    }
                    // Диагональ \
                    if (m_Map[x + 0, y + 2] == p && m_Map[x + 1, y + 1] == p && m_Map[x + 2, y + 0] == p)
                    {
                        winner = p;
                    }
                    else if (m_Map[x + 0, y + 2] == p && m_Map[x + 1, y + 1] == p && m_Map[x + 2, y + 0] == p)
                    {
                        winner = p;
                    }
                    else if (m_Map[x + 0, y + 2] == p && m_Map[x + 1, y + 1] == p && m_Map[x + 2, y + 0] == p)
                    {
                        winner = p;
                    }
                    // Горизонталь -
                    if (m_Map[x + 0, y + 0] == p && m_Map[x + 1, y + 0] == p && m_Map[x + 2, y + 0] == p)
                    {
                        winner = p;
                    }
                    else if (m_Map[x + 0, y + 0] == p && m_Map[x + 1, y + 0] == p && m_Map[x + 2, y + 0] == p)
                    {
                        winner = p;
                    }
                    else if (m_Map[x + 0, y + 0] == p && m_Map[x + 1, y + 0] == p && m_Map[x + 2, y + 0] == p)
                    {
                        winner = p;
                    }
                    // Вертикаль |
                    if (m_Map[x + 0, y + 0] == p && m_Map[x + 0, y + 1] == p && m_Map[x + 0, y + 2] == p)
                    {
                        winner = p;
                    }
                    else if (m_Map[x + 0, y + 0] == p && m_Map[x + 0, y + 1] == p && m_Map[x + 0, y + 2] == p)
                    {
                        winner = p;
                    }
                    else if (m_Map[x + 0, y + 0] == p && m_Map[x + 0, y + 1] == p && m_Map[x + 0, y + 2] == p)
                    {
                        winner = p;
                    }
                }
            }
        }

        if (winner == 3 && m_TurnsMade == m_Size * m_Size)
        {
            winner = 0;
        }

        if (winner != 3)
        {
            GameFinished(winner);
        }
        else
        {
            if (m_Turn == 2)
            {
                CPUTurn();
            }
        }
    }

    private void Update()
    {
        if (m_GameStarted && !m_GamePaused)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Click(m_Camera.ScreenToWorldPoint(Input.mousePosition));
            }

            if (Input.touchCount == 1 && Input.touches[0].phase == TouchPhase.Began)
            {
                Click(m_Camera.ScreenToWorldPoint(Input.touches[0].position));
            }

            m_Timer += Time.unscaledDeltaTime;
            MyApp.instance.m_MyGameMenu.SetTimer(m_Timer);
        }
    }

    private void Click(Vector2 pos)
    {
        Vector2Int posi = ToIntCoord(pos);

        if (posi.x >= 0 && posi.x < m_Size && posi.y >= 0 && posi.y < m_Size && m_Turn == 1)
        {
            if (m_Map[posi.x, posi.y] == 0)
            {
                m_Map[posi.x, posi.y] = m_Turn;
                MadeMove(posi, 1);
            }
        }
    }
}
