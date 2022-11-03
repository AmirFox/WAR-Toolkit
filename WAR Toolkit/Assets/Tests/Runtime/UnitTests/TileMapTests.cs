using NUnit.Framework;
using WarToolkit.ObjectData;
using WarToolkit.Utilities;
using UnityEngine;

namespace WarToolkit.PlayTests
{
    public class TileMapTests
    {
        private IMapData<TestTile> _mapData;

        [SetUp]
        public void UnitySetUp()
        {
            INoiseGenerator noiseGenerator = new SimpleNoiseGenerator("tile map test");
            _mapData = new TestMapData(10, 10, 5);
        }

        [TearDown]
        public void UnityTearDown()
        {
            _mapData = null;
        }

        [Test]
        public void CanGenerateMap()
        {
            //ARRANGE
            ITileMap<TestTile> sut = new TileMap<TestTile>(_mapData);

            //ACT
            sut.GenerateMap();

            //ASSERT
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    Vector2 position = new Vector2(x, y);
                    ITile tile = sut.GetTile(position);
                    Assert.NotNull(tile);
                }
            }
        }

        [Test]
        [TestCase(5, 5)]
        public void CanFindExistingTile(int x, int y)
        {
            //ARRANGE
            ITileMap<TestTile> sut = new TileMap<TestTile>(_mapData);

            //ACT
            sut.GenerateMap();

            //ASSERT
            Vector2 position = new Vector2(x, y);
            ITile tile = sut.GetTile(position);
            Assert.NotNull(tile);
        }

        [Test]
        [TestCase(11, 11)]
        public void CannotFindTileOutOfRange(int x, int y)
        {
            //ARRANGE
            ITileMap<TestTile> sut = new TileMap<TestTile>(_mapData);

            //ACT
            sut.GenerateMap();

            //ASSERT
            Vector2 position = new Vector2(x, y);
            ITile tile = sut.GetTile(position);
            Assert.IsNull(tile);
        }

        [Test]
        [TestCase(0, 0)]
        public void CannotFindTileBeforeGeneratingMap(int x, int y)
        {
            //ARRANGE
            ITileMap<TestTile> sut = new TileMap<TestTile>(_mapData);

            //ASSERT
            Vector2 position = new Vector2(x, y);
            ITile tile = sut.GetTile(position);
            Assert.IsNull(tile);
        }
    }
}