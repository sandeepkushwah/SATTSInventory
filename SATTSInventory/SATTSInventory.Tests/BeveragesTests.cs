using Moq;
using NUnit.Framework;
using SATTSInventory.Controllers;
using SATTSInventory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Results;

namespace SATTSInventory.Tests
{
    [TestFixture]
    public class BeveragesTests
    {
        Mock<IBeverageRepository> mockBeverageRepository;
        BeveragesController beverageController;
        List<Beverage> expectedBeverages;

        [SetUp]
        public void InitializeTestData()
        {
            expectedBeverages = GetExpectedBeverages();
            mockBeverageRepository = new Mock<IBeverageRepository>();
            beverageController = new BeveragesController(mockBeverageRepository.Object);

            mockBeverageRepository.Setup(m => m.GetBeverages()).ReturnsAsync(expectedBeverages);


            mockBeverageRepository.Setup(m => m.AddBeverage(It.IsAny<Beverage>())).ReturnsAsync(
                (Beverage target) =>
                {
                    expectedBeverages.Add(target);
                    return target.Id;
                });

            mockBeverageRepository.Setup(m => m.UpdateBeverage(It.IsAny<Beverage>())).ReturnsAsync(
                (Beverage target) =>
                {
                    Beverage beverageToUpdate = expectedBeverages.FirstOrDefault(eB => eB.Id == target.Id);

                    if (beverageToUpdate != null)
                    {
                        beverageToUpdate.Name = target.Name;
                        beverageToUpdate.Description = target.Description;
                        beverageToUpdate.Price = target.Price;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                });

            mockBeverageRepository.Setup(m => m.DeleteBeverage(It.IsAny<int>())).ReturnsAsync(
                (int id) =>
                {
                    Beverage beverageToDelete = expectedBeverages.Find(eB => eB.Id == id);
                    if (beverageToDelete != null)
                    {
                        expectedBeverages.Remove(beverageToDelete);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                });

        }

        [TearDown]
        public void CleanUpTestData()
        {
            expectedBeverages = null;
            mockBeverageRepository = null;
            beverageController = null;
        }

        [TestCase]
        public void GetBeveragesHttpActionResult_Should_Return_HttpResult_With_All_Beverages()
        {
            var actualBevs = beverageController.GetBeverages().Result as OkNegotiatedContentResult<IEnumerable<Beverage>>;
            Assert.AreSame(expectedBeverages, actualBevs.Content);
        }

        [TestCase(1)]
        [TestCase(2)]
        public void GetBeverageHttpActionResult_Should_Return_HttpResult_With_Specific_Beverage(int id)
        {
            var actualBevs = beverageController.GetBeverage(id).Result as OkNegotiatedContentResult<Beverage>;
            Assert.AreSame(expectedBeverages.First(x => x.Id == id), actualBevs.Content);
        }

        [TestCase(3)]
        [TestCase(-1)]
        public void GetBeverageHttpActionResult_Should_Return_HttpResult_With_Beverage_NotFound(int id)
        {
            var actualBevs = beverageController.GetBeverage(id).Result;
            Assert.IsInstanceOf<NotFoundResult>(actualBevs);
        }


        [TestCase("Ginger", "Ginger For immunity", 12)]
        public void Add_Beverage(string name, string description, decimal price)
        {
            var bevList = beverageController.GetBeverages().Result as OkNegotiatedContentResult<IEnumerable<Beverage>>;

            int currentBevCount = bevList.Content.Count();

            Beverage newBeverage = GetNewBeverage(name, description, price);
            
            _ = beverageController.PostBeverage(newBeverage).Result;

            bevList = beverageController.GetBeverages().Result as OkNegotiatedContentResult<IEnumerable<Beverage>>;
            
            Assert.AreEqual(currentBevCount + 1, bevList.Content.Count());
        }
        [TestCase]
        public void Update_Beverage()
        {
            Beverage beverage = new Beverage()
            {
                Id = 2,
                Name = "Iranee",//Changed Name
                Description = "Ginger Tea for better immunity",
                Price = 22
            };

            _ = beverageController.PutBeverage(beverage.Id, beverage).Result;

            var bevList = beverageController.GetBeverages().Result as OkNegotiatedContentResult<IEnumerable<Beverage>>;

            Assert.AreEqual("Ginger", bevList.Content.FirstOrDefault(b => b.Id == beverage.Id).Name);
        }
        [TestCase(1)]
        public void Delete_Beverage(int id)
        {
            var bevList = beverageController.GetBeverages().Result as OkNegotiatedContentResult<IEnumerable<Beverage>>;
            int previousBevCount = bevList.Content.Count();

            _ = beverageController.DeleteBeverage(id);

            bevList = beverageController.GetBeverages().Result as OkNegotiatedContentResult<IEnumerable<Beverage>>;

            Assert.AreEqual(previousBevCount - 1, bevList.Content.Count());
        }

        #region HelperMethods
        private static List<Beverage> GetExpectedBeverages()
        {
            return new List<Beverage>()
            {
                new Beverage()
                {
                    Id = 1,
                    Name = "Masala",
                    Description = "Masala Tea for authentic taste of masala",
                    Price = 11
                },
                new Beverage()
                {
                    Id = 2,
                    Name = "Irani",
                    Description = "Irani Tea for Hyderabadi Folks!",
                    Price = 22
                }
            };
        }

        private static Beverage GetNewBeverage(string name, string description, decimal price)
        {
            return new Beverage()
            {
                Name = name,
                Description = description,
                Price = price
            };
        }
        #endregion
    }
}
