using System.Collections.Generic;
using Generators.Entities;
using Generators.Enums;
using Generators.Utils;
using UnityEngine;

namespace Generators
{
    public class MazeGenerator : AbstractLabyrinthGenerator
    {
        private long _filledCount;
        private long _turns = 0;
        private readonly List<(int, int)> _notVisitedPoints = new();
        private DirectionsEnum[,] _directionsEnums;

        public override Room[,] DoGeneration(Room[,] rooms, float difficulty)
        {
            _notVisitedPoints.Clear();
            _filledCount = 0;
            _turns = 0;
            _directionsEnums = new DirectionsEnum[rooms.GetLength(0), rooms.GetLength(1)];
            for (int i = 0; i < rooms.GetLength(0); i++)
            {
                for (int j = 0; j < rooms.GetLength(1); j++)
                {
                    _notVisitedPoints.Add((i, j));
                }
            }

            rooms = LowFillGeneration(rooms);
            _notVisitedPoints.Shuffle();

            while (_filledCount < rooms.GetLength(0) * rooms.GetLength(1))
            {
                rooms = HighFillGeneration(rooms);
            }

            return rooms;
        }

        /**<summary>
     *Данный метод использует алгоритм алгоритм Альдуса-Бродера для генерации одной ветки лабиринта
     * </summary>
     * <seealso href="https://weblog.jamisbuck.org/2011/1/17/maze-generation-aldous-broder-algorithm">Подробнее</seealso>
     */
        private Room?[,] LowFillGeneration(Room?[,] previousState)
        {
            var startPoint = RandomPoint(previousState.GetLength(0), previousState.GetLength(1));
            var previousPoint = startPoint;
            previousState[startPoint.Item1, startPoint.Item2] = new();
            _filledCount++;
            _notVisitedPoints.Remove(startPoint);
            while (previousState.GetLength(0) * previousState.GetLength(1) / (1.0 * _filledCount) > 2.0)
            {
                DirectionsEnum move;
                do
                {
                    move = RandomMove();
                    startPoint = move.MovePoint(previousPoint);
                } while (startPoint.Item1 < 0
                         || startPoint.Item1 >= previousState.GetLength(0)
                         || startPoint.Item2 < 0
                         || startPoint.Item2 >= previousState.GetLength(1));

                if (previousState[startPoint.Item1, startPoint.Item2] == null)
                {
                    previousState[startPoint.Item1, startPoint.Item2] = new();
                    previousState[previousPoint.Item1, previousPoint.Item2]!.AddExit(move);
                    previousState[startPoint.Item1, startPoint.Item2]!.AddExit(move.Opposite);
                    _filledCount++;
                    _turns = 0;
                    _notVisitedPoints.Remove(startPoint);
                }

                previousPoint = startPoint;
                _turns++;
                if (_turns == 20)
                {
                    Debug.LogWarning("Possible cycle detected. Proceeding further generation");
                }
            }

            return previousState;
        }

        /**<summary>
     *Данный метод использует алгоритм алгоритм Вильмона для генерации одной ветки лабиринта
     * </summary>
     * <seealso href="https://weblog.jamisbuck.org/2011/1/20/maze-generation-wilson-s-algorithm">Подробнее</seealso>
     */
        private Room?[,] HighFillGeneration(Room?[,] previousState)
        {
            var startPoint = _notVisitedPoints[0];
            var movePoint = startPoint;
            while (previousState[movePoint.Item1, movePoint.Item2] == null)
            {
                var oldPoint = movePoint;
                DirectionsEnum move;
                do
                {
                    move = RandomMove();
                    movePoint = move.MovePoint(oldPoint);
                } while (movePoint.Item1 < 0
                         || movePoint.Item1 >= previousState.GetLength(0)
                         || movePoint.Item2 < 0
                         || movePoint.Item2 >= previousState.GetLength(1));

                _directionsEnums[oldPoint.Item1, oldPoint.Item2] = move;
            }

            var previousPoint = startPoint;
            _filledCount++;
            _notVisitedPoints.Remove(startPoint);
            previousState[startPoint.Item1, startPoint.Item2] = new();
            while (startPoint != movePoint)
            {
                startPoint = _directionsEnums[startPoint.Item1, startPoint.Item2].MovePoint(startPoint);
                previousState[previousPoint.Item1, previousPoint.Item2]
                    .AddExit(_directionsEnums[previousPoint.Item1, previousPoint.Item2]);
                if (previousState[startPoint.Item1, startPoint.Item2] == null)
                {
                    previousState[startPoint.Item1, startPoint.Item2] = new Room();
                    _filledCount++;
                    _turns = 0;
                    _notVisitedPoints.Remove(startPoint);
                }

                previousState[startPoint.Item1, startPoint.Item2]
                    .AddExit(_directionsEnums[previousPoint.Item1, previousPoint.Item2].Opposite);
                previousPoint = startPoint;

                _turns++;
                if (_turns == 20)
                {
                    Debug.LogWarning("Possible cycle detected. Proceeding further generation");
                }
            }

            return previousState;
        }
    }
}