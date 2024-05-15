using Microsoft.AspNetCore.Mvc;
using Moq;
using Transactions.Controllers;
using Transactions.Models;
using Transactions.Repositories;

namespace Transactions.UnitTests
{
    public class AdvertismentControllerTests
    {
        private AdvertismentController advertismentController;
        [SetUp]
        public void Setup()
        {
            var inMemoryData = new List<Advertisment>
            {
                new Advertisment { Id = 1, Name = "Test1", ImageURL = "TestUrl1"},
                new Advertisment { Id = 2, Name = "Test2", ImageURL = "TestUrl2"},
                new Advertisment { Id = 3, Name = "Test3", ImageURL = "TestUrl3"}
            };

            var repository = new Mock<IAdvertismentRepository>();

            repository.Setup(x => x.Get()).Returns(() => Task.FromResult(inMemoryData));
            repository.Setup(x => x.Get(It.IsAny<int>())).Returns((int i) => Task.FromResult(inMemoryData.FirstOrDefault(bo => bo.Id == i)));
            repository.Setup(x => x.Create(It.IsAny<Advertisment>())).Callback((Advertisment adv) => inMemoryData.Add(adv));
            repository.Setup(x => x.Update(It.IsAny<Advertisment>())).Callback((Advertisment adv) => {
                var advert = inMemoryData.FirstOrDefault(x => x.Id == adv.Id);
                if (advert is null)
                    return;
                advert.Name = adv.Name;
                advert.ImageURL = adv.ImageURL;
            });
            repository.Setup(x => x.Delete(It.IsAny<int>())).Callback((int id) => {
                var advert = inMemoryData.FirstOrDefault(z => z.Id == id);
                if (advert is null) 
                    return;
                inMemoryData.Remove(advert);
            });

            advertismentController = new AdvertismentController(repository.Object);
        }

        [Test]
        public void GetAllAdvertisments_Should_Return_200_Status_Code()
        {
            var result = advertismentController.GetAllAdvertisments().Result as OkObjectResult;
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public void GetAdvertisment_Should_Return_200_Status_Code()
        {
            var result = advertismentController.GetAdvertisment(1).Result as OkObjectResult;
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public void AddAdvertisment_Should_Return_200_Status_Code()
        {
            var result = advertismentController.AddAdvertisment(new Advertisment { Id = 4, Name = "Test4", ImageURL = "TestUrl4"}).Result as OkObjectResult;
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public void DeleteAdvertisment_Should_Return_200_Status_Code()
        {
            var result = advertismentController.DeleteAdvertisment(1).Result as OkObjectResult;
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public void UpdateAdvertisment_Should_Return_200_Status_Code()
        {
            var result = advertismentController.UpdateAdvertisment(new Advertisment { Id = 1, Name = "Test1Changed", ImageURL = "TestUrl1Changed" }).Result as OkObjectResult;
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(200));
        }
    }
}