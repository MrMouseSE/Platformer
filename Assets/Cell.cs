using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    // Start is called before the first frame update
    public GameManager gameManager;
    public Board board;
    public Coord coord;
    public List<Cell> NeighbourList { get; set; }
    public Vector2 Position { get; set; }
    public CellType Type { get; set; }
    public Vector2 Size { get; set; }


    private GameObject parent;
    private List<Cell> neighbourList = new List<Cell>();
    private Vector2 size;
  //  private CellType type;
    private Vector2 position;
    private Vector2 coords;


    public void Initialize(int type, Vector2 pos, Vector2 size, GameManager gm)
    {
        coord = new Coord((int)pos.x, (int)pos.y);
        board = gm.board;
        this.Type = (CellType)type;
        position = pos;
        this.size = size;
        gameManager = gm;
        gameObject.name = "Cell_" + position.x + "_" + position.y;
        RectTransform transform = gameObject.GetComponent<RectTransform>();
        transform.anchorMin = new Vector2(0, 0);
        transform.anchorMax = new Vector2(0, 0);
        float multiply = board.GetMultiply();
        float indent = board.GetIndent();
                transform.anchoredPosition = coords = new Vector2(115 * position.x * multiply + indent, 115 * position.y * multiply + indent);
        transform.sizeDelta = new Vector2(100 * multiply, 100 * multiply);
        if ((pos.x == board.SizeX - 2 && pos.y == 0) || (pos.x == board.SizeX - 1 && pos.y == 0) || (pos.x == board.SizeX - 1 && pos.y == 1))
        {
            gameObject.SetActive(true);
            if(pos.x == 4 && pos.y == 0)
                gameObject.GetComponent<Button>().interactable = false;
        }
        else gameObject.SetActive(false);
    }
        
    void Start()
    {
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(ActivateCell);
    }

    void ActivateCell()
    {
        foreach(var neighbour in NeighbourList)
        {
            neighbour.gameObject.SetActive(true);
            if(neighbour.Type != CellType.Impassable)
                neighbour.gameObject.GetComponent<Button>().interactable = true;
            foreach (var n in neighbour.NeighbourList)
            {
                if (n != this)
                    n.gameObject.SetActive(false);
            }
        }
        gameManager.player.GetComponent<RectTransform>().anchoredPosition = coords;
        gameObject.GetComponent<Button>().interactable = false;
    }

    public void ChangeCell(CellType type)
    {
        this.Type = type;
        switch (type)
        {
            case CellType.Passeble:
                gameObject.GetComponent<Image>().sprite = gameManager.passebleCellSprite;
                break;
            case CellType.Impassable:
                gameObject.GetComponent<Image>().sprite = gameManager.impassebleCellSprite;
                break;
            case CellType.Win:
                gameObject.GetComponent<Image>().sprite = gameManager.winCellSprite;
                break;
            case CellType.Lose:
                gameObject.GetComponent<Image>().sprite = gameManager.loseCellSprite;
                break;
            case CellType.Map:
                gameObject.GetComponent<Image>().sprite = gameManager.mapCellSprite;
                break;
            case CellType.Path:
                gameObject.GetComponent<Image>().sprite = gameManager.pathCellSprite;
                break;
        }
    }


    public List<Cell> GetPassableNeighbours(List<Cell> excludeThis, int step = 1)
    {
        List<Cell> neighbours = new List<Cell>();
        List<Coord> neighbourCoords = coord.GetNeighbourCoodrs(board.SizeX, board.SizeY, step);
        //neighbourCoords берёшь не списком координат, а прям ячейками
        //neighbourCoords.Except(excludeThis).ToList();
        foreach (var c in neighbourCoords)
        {
            if(board.GetCell(c).Type != CellType.Impassable)
                neighbours.Add(board.GetCell(c));
        }
        return neighbours;
    }

    public List<Cell> GetNeighbours(int step = 1)
    {
        List<Cell> neighbours = new List<Cell>();
        List<Coord> neighbourCoords = coord.GetNeighbourCoodrs(board.SizeX, board.SizeY, step);
        foreach(var c in neighbourCoords)
        {
            neighbours.Add(board.GetCell(c));
        }
        return neighbours;
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
    // Update is called once per frame
}
