﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WT1.Controllers;
using WebLabsV05.DAL.Entities;
using Xunit;

namespace WT1.Tests
{
    public class ProductControllerTests
    {
        [Theory]
        [MemberData(nameof(TestData.Params), MemberType = typeof(TestData))]
        public void ControllerGetsProperPage(int page, int qty, int id)
        {
            // Arrange
            var controller = new ProductController();
            controller._pcParts = new List<PCPart>
            {
                new PCPart{ PCPartId=1 },
                new PCPart{ PCPartId=2 },
                new PCPart{ PCPartId=3 },
                new PCPart{ PCPartId=4 },
                new PCPart{ PCPartId=5 }
            };
            // Act
            var result = controller.Index(pageNo: page, group: null) as ViewResult;
            var model = result?.Model as List<PCPart>;
            // Assert
            Assert.NotNull(model);
            Assert.Equal(qty, model.Count);
            Assert.Equal(id, model[0].PCPartId);
        }

        [Fact]
        public void ControllerSelectsGroup()
        {
            // arrange
            var controller = new ProductController();
            var data = TestData.GetPCPartsList();
            controller._pcParts = data;
            var comparer = Comparer<PCPart>
            .GetComparer((p1, p2) => p1.PCPartId.Equals(p2.PCPartId));
            // act
            var result = controller.Index(2) as ViewResult;
            var model = result.Model as List<PCPart>;
            // assert
            Assert.Equal(2, model.Count);
            Assert.Equal(data[2], model[0], comparer);
        }
    }
}