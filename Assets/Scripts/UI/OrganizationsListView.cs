using System;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class OrganizationsListView : MonoBehaviour
{
    [SerializeField] private ScrollRect scrollView;
    [SerializeField] private Transform content;
    [SerializeField] private OrganizationView rowPrefab;

    [SerializeField] private GameObject paginationPanel;
    [SerializeField] private Button previousPageButton;
    [SerializeField] private Button nextPageButton;
    [SerializeField] private TMP_Text pageNumberText;
    [SerializeField] private TMP_Text totalPagesText;

    private readonly List<OrganizationView> pool = new List<OrganizationView>();

    private const int PageSize = 5;
    private const int PooledRows = 5;
    private int currentPage = 1;
    private int totalPages = 1;

    private IReadOnlyList<OrganizationData> items = Array.Empty<OrganizationData>();
    private string logosDirectory;

    public void Initialize(string persistentLogosDirectory)
    {
        logosDirectory = persistentLogosDirectory;
        PreparePool();

        if (previousPageButton != null)
        {
            previousPageButton.onClick.AddListener(GoPrevious);
        }

        if (nextPageButton != null)
        {
            nextPageButton.onClick.AddListener(GoNext);
        }
    }

    private void OnDestroy()
    {
        if (previousPageButton != null)
        {
            previousPageButton.onClick.RemoveListener(GoPrevious);
        }

        if (nextPageButton != null)
        {
            nextPageButton.onClick.RemoveListener(GoNext);
        }
    }

    private void PreparePool()
    {
        foreach (Transform c in content)
        {
            Destroy(c.gameObject);
        }
        pool.Clear();

        for (int i = 0; i < PooledRows; i++)
        {
            var row = Instantiate(rowPrefab, content);
            row.gameObject.SetActive(false);
            pool.Add(row);
        }
    }

    public void Refresh(IReadOnlyList<OrganizationData> data)
    {
        SetItem(data);
    }

    private void SetItem(IReadOnlyList<OrganizationData> data)
    {
        items = data ?? Array.Empty<OrganizationData>();
        currentPage = 1;
        totalPages = Mathf.Max(1, Mathf.CeilToInt(items.Count / (float)PageSize));
        UpdatePaginationVisibility();
        DrawPage();
    }

    private void UpdatePaginationVisibility()
    {
        bool showPagination = items.Count > PageSize;
        if (paginationPanel != null)
        {
            paginationPanel.SetActive(showPagination);
        }
    }

    private void DrawPage()
    {
        int count = items.Count;
        if (count == 0)
        {
            for (int i = 0; i < pool.Count; i++)
            {
                pool[i].gameObject.SetActive(false);
            }

            UpdateFooter(0, 0, 0);
            scrollView.verticalNormalizedPosition = 1f;
            return;
        }

        currentPage = Mathf.Clamp(currentPage, 1, totalPages);

        int startIndex = (currentPage - 1) * PageSize;
        int endIndex = Mathf.Min(startIndex + PageSize, count);

        int visibleCount = endIndex - startIndex;
        for (int i = 0; i < pool.Count; i++)
        {
            if (i < visibleCount)
            {
                var data = items[startIndex + i];
                pool[i].Bind(data, logosDirectory);
                pool[i].gameObject.SetActive(true);
            }
            else
            {
                pool[i].gameObject.SetActive(false);
            }
        }

        UpdateFooter(startIndex, endIndex, count);

        scrollView.verticalNormalizedPosition = 1f;

        if (previousPageButton != null)
        {
            previousPageButton.interactable = currentPage > 1;
        }

        if (nextPageButton != null)
        {
            nextPageButton.interactable = currentPage < totalPages;
        }
    }

    private void UpdateFooter(int startIndex, int endIndex, int totalCount)
    {
        if (pageNumberText != null)
        {
            pageNumberText.text = totalCount > PageSize ? currentPage.ToString() : string.Empty;
        }

        if (totalPagesText != null)
        {
            if (totalCount == 0)
            {
                totalPagesText.text = string.Empty;
            }
            else
            {
                int a = startIndex + 1;
                int b = endIndex;
                totalPagesText.text = $"{a} - {b} of {totalCount}";
            }
        }
    }

    private void GoPrevious()
    {
        if (currentPage <= 1)
        {
            return;
        }
        currentPage--;
        DrawPage();
    }

    private void GoNext()
    {
        if (currentPage >= totalPages)
        {
            return;
        }
        currentPage++;
        DrawPage();
    }
}
