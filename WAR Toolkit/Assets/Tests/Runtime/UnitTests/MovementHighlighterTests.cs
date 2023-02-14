using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using WarToolkit.Core;
using WarToolkit.ObjectData;
using UnityEngine;
using WarToolkit.Pathfinding;

namespace WarToolkit.PlayTests
{
    public class MovementHighlighterTests
    {
        private IMapData<TestTile> _mapData;
        private ITileMap<TestTile> _tileMap;
        private Vector2[] _smallHighlightArea = { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 0), new Vector2(-1, 0), new Vector2(0, -1) };
        private Vector2[] _mediumHighlightArea = { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 0), new Vector2(-1, 0), new Vector2(0, -1) };
        private Vector2[] _largeHighlightArea = { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 0), new Vector2(-1, 0), new Vector2(0, -1) };

        [SetUp]
        public void SetUp()
        {
            _mapData = new TestMapData(10, 10);
            _tileMap = new TileMap<TestTile>(_mapData);
        }

        public void TearDown()
        {
            _tileMap = null;
            _mapData = null;
        }

        [Test]
        [TestCase(5, 5, 1)]
        [TestCase(5, 5, 2)]
        [TestCase(5, 5, 3)]
        public void CheckAreaAroundOriginIsHighlighted(int x, int y, int points)
        {
            //ARRANGE
            _tileMap.GenerateMap();
            ITileQuery<TestTile> sut = new MovementHighlighter<TestTile>(_tileMap);

            //ACT
            TestTile origin = _tileMap.GetTile(new Vector2(x, y));
            List<TestTile> highlightArea = sut.QueryRadius(origin, points);

            //ASSERT
            Assert.IsNotEmpty(highlightArea);

            int expectedAreaSize = CalcBaseHighlightAreaSize(points);
            Assert.AreEqual(expectedAreaSize, highlightArea.Count); //CHECK HIGHLIGHTED AREA IS OF EXPECTED SIZE

            Vector2 left = new Vector2(x + points, y);
            Vector2 right = new Vector2(x - points, y);
            Vector2 up = new Vector2(x, y + points);
            Vector2 down = new Vector2(x, y - points);

            List<Vector2> highlightedPositions = highlightArea.Select(x => x.Coordinates).ToList();
            Assert.Contains(left, highlightedPositions);    //CHECK LEFT-MOST POINT
            Assert.Contains(right, highlightedPositions);   //CHECK RIGHT-MOST POINT
            Assert.Contains(up, highlightedPositions);      //CHECK UPPER-MOST POINT
            Assert.Contains(down, highlightedPositions);    //CHECK LOWER-MOST POINT
        }

        [Test]
        public void CheckInaccessibleTilesNotHighlighted()
        {
            //ARRANGE
            _tileMap.GenerateMap();
            ITileQuery<TestTile> sut = new MovementHighlighter<TestTile>(_tileMap);
            int points = 5;
            Vector2 originPos = new Vector2(5, 5);

            //ACT
            TestTile origin = _tileMap.GetTile(originPos);
            _tileMap.GetTile(new Vector2(6, 6)).IsAccesible = false;
            List<TestTile> highlightArea = sut.QueryRadius(origin, points);

            //ASSERT
            Assert.IsNotEmpty(highlightArea);

            int expectedAreaSize = CalcBaseHighlightAreaSize(points) - 2;
            Assert.AreEqual(expectedAreaSize, highlightArea.Count); //CHECK HIGHLIGHTED AREA IS OF EXPECTED SIZE

            List<Vector2> highlightedPositions = highlightArea.Select(x => x.Coordinates).ToList();
            bool actual = highlightedPositions.Contains(new Vector2(6, 6));
            Assert.IsFalse(actual);
        }

        private int CalcBaseHighlightAreaSize(int points)
        {
            int size = 0;
            int areaGrowth = 4;
            for (int i = 1; i <= points; i++)
            {
                size += areaGrowth;
                areaGrowth += 4;
            }

            return size;
        }
    }
}
