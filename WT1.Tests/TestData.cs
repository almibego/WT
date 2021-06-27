
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WT1.Controllers;
using WebLabsV05.DAL.Entities;
using Xunit;

namespace WT1.Tests
{
    public class TestData
    {
        public static List<PCPart> GetPCPartsList()
        {
            return new List<PCPart>
            {
                new PCPart{ PCPartId=1, PCPartGroupId=1 },
                new PCPart{ PCPartId=2, PCPartGroupId=1 },
                new PCPart{ PCPartId=3, PCPartGroupId=2 },
                new PCPart{ PCPartId=4, PCPartGroupId=2 },
                new PCPart{ PCPartId=5, PCPartGroupId=3 }
            };
        }
        public static IEnumerable<object[]> Params()
        {
            // 1-я страница, кол. объектов 3, id первого объекта 1
            yield return new object[] { 1, 3, 1 };
            // 2-я страница, кол. объектов 2, id первого объекта 4
            yield return new object[] { 2, 2, 4 };
        }
    }
}
