using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WT1.Controllers;
using WebLabsV05.DAL.Data;
using WebLabsV05.DAL.Entities;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace WT1.Tests
{
    public class ProductControllerTests
    {
        DbContextOptions<ApplicationDbContext> _options;
        public ProductControllerTests()
        {
            _options =
            new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "testDb")
            .Options;
        }

        [Theory]
        [MemberData(nameof(TestData.Params), MemberType = typeof(TestData))]
        public void ControllerGetsProperPage(int page, int qty, int id)
        {
            // Arrange
            // Контекст контроллера
            var controllerContext = new ControllerContext();
            // Макет HttpContext
            var moqHttpContext = new Mock<HttpContext>();
            moqHttpContext.Setup(c => c.Request.Headers)
            .Returns(new HeaderDictionary());
            controllerContext.HttpContext = moqHttpContext.Object;
            //заполнить DB данными
            using (var context = new ApplicationDbContext(_options))
            {
                TestData.FillContext(context);
            }
            using (var context = new ApplicationDbContext(_options))
            {
                // создать объект класса контроллера
                var controller = new ProductController(context)
                { ControllerContext = controllerContext };
                // Act
                var result = controller.Index(pageNo: page, group: null) as ViewResult;
                var model = result?.Model as List<PCPart>;
                // Assert
                Assert.NotNull(model);
                Assert.Equal(qty, model.Count);
                Assert.Equal(id, model[0].PCPartId);
            }
            // удалить базу данных из памяти
            using (var context = new ApplicationDbContext(_options))
            {
                context.Database.EnsureDeleted();
            }
        }

            [Fact]
            public void ControllerSelectsGroup()
            {
                // arrange
                // Контекст контроллера
                var controllerContext = new ControllerContext();
                // Макет HttpContext
                var moqHttpContext = new Mock<HttpContext>();
                moqHttpContext.Setup(c => c.Request.Headers)
                .Returns(new HeaderDictionary());
                controllerContext.HttpContext = moqHttpContext.Object;
                //заполнить DB данными
                using (var context = new ApplicationDbContext(_options))
                {
                    TestData.FillContext(context);
                }
                using (var context = new ApplicationDbContext(_options))
                {
                    var controller = new ProductController(context)
                    { ControllerContext = controllerContext };
                    var comparer = Comparer<PCPart>.GetComparer((p1, p2) =>
                    p1.PCPartId.Equals(p2.PCPartId));
                    // act
                    var result = controller.Index(2) as ViewResult;
                    var model = result.Model as List<PCPart>;
                    // assert
                    Assert.Equal(2, model.Count);
                    Assert.Equal(context.PCParts
                    .ToArrayAsync()
                    .GetAwaiter()
                    .GetResult()[2], model[0], comparer);
                }
            }
    }
}

