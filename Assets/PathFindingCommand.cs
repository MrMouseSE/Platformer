using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathFindingCommand
{
    public List<Cell> GetPath(Cell currentCell, Cell targetCell, int radius = 0) //или GetCalculatedCellsList или GetValidCellsPath или тому подобное
    {
        List<Cell> path = new List<Cell>();
        if (radius == 0) path = CalculateRandomBeautifulPath(currentCell, targetCell);

        return path;
    }
    
    List<Cell> GetUnvisitedNeighbour(List<Cell> cellNeighbours, List<Cell> visitedCells)
    {
        return cellNeighbours.Except(visitedCells).ToList();
    }

    private List<Cell> CalculateRandomBeautifulPath(Cell startCell, Cell targetCell)
    {
        List<Cell> randomBeautifulPath = new List<Cell>{targetCell};
        var currentCell = startCell;
        List<Cell> visitedCells = new List<Cell>();

        while (currentCell != targetCell)
        {
            visitedCells.Add(currentCell);
            var currentNeighbours = startCell.GetPassableNeighbours(visitedCells);
            if (currentNeighbours == null)
            {
                var previousCell = visitedCells.FindLast(cell => cell.GetType() == typeof(Cell));
                visitedCells.Remove(previousCell);
                visitedCells.Add(currentCell);
                currentCell = previousCell;
                continue;
            }
            var nextStep = currentNeighbours[Random.Range(0,currentNeighbours.Count)];
            randomBeautifulPath.Add(nextStep);
            visitedCells.Add(currentCell);
            currentCell = nextStep;
        }
        
        return randomBeautifulPath;
    }

    private List<Cell> CalculatePath(Cell startCell, Cell targetCell, int radius)
    {
        var finishCell = targetCell;
        List<Cell> pathList = new List<Cell>();
        Stack<Cell> path = new Stack<Cell>();
        List<Cell> unvisitedCells = new List<Cell>();
        List<Cell> visitedCells = new List<Cell>();
        Cell currentCell = startCell;
        Cell nextCell;
        int steps = 0;
        do
        {
            List<Cell> neighbours = GetUnvisitedNeighbour(currentCell.GetPassableNeighbours(), visitedCells);
            if (neighbours.Count != 0 && steps <= radius && currentCell != finishCell)
            {
                int rand = Random.Range(0, 100) % (neighbours.Count);
                nextCell = neighbours[rand];
                foreach (var c in neighbours)
                {
                    if (c != nextCell && !unvisitedCells.Contains(c))
                        unvisitedCells.Add(c);
                }
                path.Push(currentCell);
                currentCell = nextCell;
                unvisitedCells.Remove(currentCell);
                visitedCells.Add(currentCell);
                steps++;
            }
            else if (path.Count > 0)
            {
                currentCell = path.Pop();
                steps--;
            }
            else
            {
                int rand = Random.Range(0, unvisitedCells.Count - 1);
                currentCell = unvisitedCells[rand];
            }
        }
        while (unvisitedCells.Count > 0) ;

        if (finishCell != null)
            while (path.Count != 0)
            {
                pathList.Add(path.Pop());
            }
        else pathList = visitedCells;
        return pathList;
    }
}