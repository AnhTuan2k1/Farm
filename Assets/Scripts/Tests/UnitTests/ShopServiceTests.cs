//using Xunit;
//using Moq;
//using System.Collections.Generic;

public class ShopServiceTests
{
    //private readonly Mock<Farm> _mockFarm;
    //private readonly Mock<Dictionary<string, FarmEntityConfig>> _mockEntityConfigs;
    //private readonly ShopService _shopService;

    //public ShopServiceTests()
    //{
    //    _mockFarm = new Mock<Farm>(1000, 1, 1, new GameConfig());
    //    _mockEntityConfigs = new Mock<Dictionary<string, FarmEntityConfig>>();
    //    _shopService = new ShopService(_mockFarm.Object, _mockEntityConfigs.Object);
    //}

    //[Fact]
    //public void BuySeed_ShouldReturnTrue_WhenEnoughGold()
    //{
    //    // Arrange
    //    var entityName = "Tomato";
    //    var amount = 5;
    //    var config = new FarmEntityConfig { SeedPrice = 30 };
    //    _mockEntityConfigs.Setup(x => x.TryGetValue(entityName, out config)).Returns(true);

    //    // Act
    //    var result = _shopService.BuySeed(entityName, amount);

    //    // Assert
    //    Assert.True(result);
    //    _mockFarm.Verify(f => f.SpendGold(It.Is<int>(x => x == 30 * amount)), Times.Once);
    //}

    //[Fact]
    //public void SellProduct_ShouldReturnTrue_WhenEnoughProducts()
    //{
    //    // Arrange
    //    var entityName = "Tomato";
    //    var amount = 5;
    //    var config = new FarmEntityConfig { ProductValue = 5 };
    //    _mockEntityConfigs.Setup(x => x.TryGetValue(entityName, out config)).Returns(true);
    //    _mockFarm.Setup(f => f.Inventory.GetProductCount(entityName)).Returns(10);

    //    // Act
    //    var result = _shopService.SellProduct(entityName, amount);

    //    // Assert
    //    Assert.True(result);
    //    _mockFarm.Verify(f => f.AddGold(It.Is<int>(x => x == 5 * amount)), Times.Once);
    //}
}
