using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grid : MonoBehaviour
{
    public int columns = 0;
    public int rows = 0;
    public float squaresGap = .1f;
    public GameObject gridSquare;
    public Vector2 startPosition = new Vector2(0.0f, 0.0f);
    public float squareScale = 0.5f;
    public float everySquareOffset = 0.0f;

    private Vector2 _offset = new Vector2(0.0f, 0.0f);
    private List<GameObject> _gridSquare = new List<GameObject>();


    void Start()
    {
        createGrid();
    }
    private void createGrid()
    {
        spwanGridSquares();
        setGridSquaresPosition();
    }
    private void spwanGridSquares()
    {
        int square_index = 0;
        for (var row = 0; row < rows; ++row)
        {
            for (var col = 0; col < columns; ++col)
            {
                _gridSquare.Add(Instantiate(gridSquare) as GameObject);
                _gridSquare[_gridSquare.Count - 1].transform.SetParent(this.transform);
                _gridSquare[_gridSquare.Count - 1].transform.localScale = new Vector3(squareScale, squareScale, squareScale);
                _gridSquare[_gridSquare.Count - 1].GetComponent<GridSquare>().setImg(square_index % 2 == 0);
                square_index++;
            }
        }

    }

    private void setGridSquaresPosition()
    {
        int col_num = 0;
        int row_num = 0;
        Vector2 square_gap_num = new Vector2(0.0f, 0.0f);
        bool row_moved = false;

        var sq_rect = _gridSquare[0].GetComponent<RectTransform>();
        _offset.x = sq_rect.rect.width * sq_rect.transform.localScale.x + everySquareOffset;
        _offset.y = sq_rect.rect.height * sq_rect.transform.localScale.y + everySquareOffset;

        foreach (GameObject square in _gridSquare)
        {
            if (col_num + 1 > columns)
            {
                square_gap_num.x = 0;
                col_num = 0;
                row_num++;
                row_moved = true;
            }

            var pos_x_offset = _offset.x * col_num + (square_gap_num.x * squaresGap);
            var pos_y_offset = _offset.y * row_num + (square_gap_num.y * squaresGap);

            if (col_num > 0 && col_num % 3 == 0)
            {
                square_gap_num.x++;
                pos_x_offset += squaresGap;

            }

            if (row_num > 0 && row_num % 3 == 0 && row_moved == false)
            {
                row_moved = true;
                square_gap_num.y++;
                pos_y_offset += squaresGap;
            }

            square.GetComponent<RectTransform>().anchoredPosition = new Vector2(startPosition.x + pos_x_offset, startPosition.y - pos_y_offset);
            square.GetComponent<RectTransform>().localPosition = new Vector3(startPosition.x + pos_x_offset, startPosition.y - pos_y_offset, .0f);
            col_num++;
        }

    }
}
