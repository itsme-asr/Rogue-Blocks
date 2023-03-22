using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{
    public GameObject squareShapeImg;

    [HideInInspector]
    public ShapeData currentShapeData;

    private List<GameObject> _currentShape = new List<GameObject>();
    void Start()
    {
        RequestNewShape(currentShapeData);
    }

    public void RequestNewShape(ShapeData shapeData)
    {
        createShape(shapeData);
    }

    public void createShape(ShapeData shapeData)
    {
        currentShapeData = shapeData;
        var totalSqNum = getNumberofSq(shapeData);

        while (_currentShape.Count <= totalSqNum)
        {
            _currentShape.Add(Instantiate(squareShapeImg, transform) as GameObject);
        }

        foreach (var square in _currentShape)
        {
            square.gameObject.transform.position = Vector3.zero;
            square.gameObject.SetActive(false);

        }

        var squareRect = squareShapeImg.GetComponent<RectTransform>();
        var movDistance = new Vector2(squareRect.rect.width * squareRect.localScale.x,
        squareRect.rect.height * squareRect.localScale.y);

        int currentIndexInList = 0;

        for (var row = 0; row < shapeData.rows; row++)
        {
            for (var column = 0; column < shapeData.columns; column++)
            {
                if (shapeData.board[row].column[column])
                {
                    _currentShape[currentIndexInList].SetActive(true);
                    _currentShape[currentIndexInList].GetComponent<RectTransform>().localPosition = new
                    Vector2(getXPosForShapeSq(shapeData, column, movDistance), getYPosForShapeSq(shapeData, row, movDistance));
                    currentIndexInList++;
                }

            }
        }

    }

    private float getYPosForShapeSq(ShapeData shapeData, int row, Vector2 movDistance)
    {
        float shiftOnY = 0f;

        if (shapeData.rows > 1)
        {
            if (shapeData.rows % 2 != 0)
            {
                var middleSqIndex = (shapeData.rows - 1) / 2;
                var multiplier = (shapeData.rows - 1) / 2;
                if (row < middleSqIndex)
                {
                    shiftOnY = movDistance.x * -1;
                    shiftOnY *= multiplier;
                }
                else if (row > middleSqIndex)
                {
                    shiftOnY = movDistance.x * 1;
                    shiftOnY *= multiplier;
                }
            }
            else
            {
                var middleSqIndex2 = (shapeData.rows == 2) ? 1 : (shapeData.rows / 2);
                var middleSqIndex1 = (shapeData.rows == 2) ? 0 : (shapeData.rows - 1);
                var multiplier = shapeData.rows / 2;

                if (row == middleSqIndex1 || row == middleSqIndex2)
                {
                    if (row == middleSqIndex2)
                    {
                        shiftOnY = movDistance.x / 2;
                    }
                    if (row == middleSqIndex1)
                    {
                        shiftOnY = (movDistance.x / 2) * -1;
                    }
                }
                if (row < middleSqIndex1 && row < middleSqIndex2)
                {
                    shiftOnY = movDistance.x * -1;
                    shiftOnY *= multiplier;
                }
                else if (row > middleSqIndex1 && row > middleSqIndex2)
                {
                    shiftOnY = movDistance.x * 1;
                    shiftOnY *= multiplier;
                }
            }
        }
        return shiftOnY;
    }
    private float getXPosForShapeSq(ShapeData shapeData, int column, Vector2 movDistance)
    {

        float shiftOnX = 0f;

        if (shapeData.columns > 1)
        {
            if (shapeData.columns % 2 != 0)
            {
                var middleSqIndex = (shapeData.columns - 1) / 2;
                var multiplier = (shapeData.columns - 1) / 2;
                if (column < middleSqIndex)
                {
                    shiftOnX = movDistance.x * -1;
                    shiftOnX *= multiplier;
                }
                else if (column > middleSqIndex)
                {
                    shiftOnX = movDistance.x * 1;
                    shiftOnX *= multiplier;
                }
            }
            else
            {
                var middleSqIndex2 = (shapeData.columns == 2) ? 1 : (shapeData.columns / 2);
                var middleSqIndex1 = (shapeData.columns == 2) ? 0 : (shapeData.columns - 1);
                var multiplier = shapeData.columns / 2;

                if (column == middleSqIndex1 || column == middleSqIndex2)
                {
                    if (column == middleSqIndex2)
                    {
                        shiftOnX = movDistance.x / 2;
                    }
                    if (column == middleSqIndex1)
                    {
                        shiftOnX = (movDistance.x / 2) * -1;
                    }
                }
                if (column < middleSqIndex1 && column < middleSqIndex2)
                {
                    shiftOnX = movDistance.x * -1;
                    shiftOnX *= multiplier;
                }
                else if (column > middleSqIndex1 && column > middleSqIndex2)
                {
                    shiftOnX = movDistance.x * 1;
                    shiftOnX *= multiplier;
                }
            }
        }
        return shiftOnX;
    }
    private int getNumberofSq(ShapeData shapeData)
    {
        int num = 0;
        foreach (var rowData in shapeData.board)
        {
            foreach (var active in rowData.column)
            {
                if (active)
                {
                    num++;
                }
            }
        }
        return num;
    }
}

