using WebLabsV05.DAL.Entities;
using WT1.Models;
using Xunit;

namespace WT1.Tests
{
    public class ListViewModelTests
    {
        [Fact]
        public void ListViewModelCountsPages()
        {
            // Act
            var model = ListViewModel<PCPart>
            .GetModel(TestData.GetPCPartsList(), 1, 3);
            // Assert
            Assert.Equal(2, model.TotalPages);
        }
        
        [Theory]
        [MemberData(memberName: nameof(TestData.Params),
        MemberType = typeof(TestData))]
        public void ListViewModelSelectsCorrectQty(int page, int qty, int id)
        {
            // Act
            var model = ListViewModel<PCPart>
            .GetModel(TestData.GetPCPartsList(), page, 3);
            // Assert
            Assert.Equal(qty, model.Count);
        }

        [Theory]
        [MemberData(memberName: nameof(TestData.Params), MemberType = typeof(TestData))]
        public void ListViewModelHasCorrectData(int page, int qty, int id)
        {
            // Act
            var model = ListViewModel<PCPart>
            .GetModel(TestData.GetPCPartsList(), page, 3);
            // Assert
            Assert.Equal(id, model[0].PCPartId);
        }
    }
}