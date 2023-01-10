using Microsoft.AspNetCore.Mvc;
using RealEstateAgency.Core.DTOs.BuildingPlan;
using RealEstateAgency.Core.Entities;
using RealEstateAgency.Core.Interfaces;
using RealEstateAgencyMVC.Areas.Admin.Controllers;

namespace RealEstateAgency.UnitTests
{
    public class BuildingPlansControllerUnitTests
    {
        private Mock<IBuildingPlanService> _buildingPlanService;
        private BuildingPlansController _buildingPlansController;
        private List<BuildingPlan> _buildingPlans;

        [SetUp]
        public void SepUp()
        {
            _buildingPlanService = new Mock<IBuildingPlanService>();
            _buildingPlansController = new BuildingPlansController(_buildingPlanService.Object);
            _buildingPlans = new List<BuildingPlan>
            {
                new BuildingPlan { Id = Guid.Parse("DD1CB006-BA04-4A2A-BEAB-8E97BD7F461A"), BuildingPlanName = "buildingPlan1"},
                new BuildingPlan { Id = Guid.Parse("F33C76DF-B629-4CE4-AAD1-610F27724385"), BuildingPlanName = "buildingPlan2"},
                new BuildingPlan { Id = Guid.Parse("B5688410-59A7-423A-9B62-C14F4DF002DC"), BuildingPlanName = "buildingPlan3"},
                new BuildingPlan { Id = Guid.Parse("DE688410-59A7-423A-9B62-C14F4DF002DC"), BuildingPlanName = "to delete"},
                new BuildingPlan { Id = Guid.Parse("DE3C76DF-B629-4CE4-AAD1-610F27724385"), BuildingPlanName = "to delete"},
                new BuildingPlan { Id = Guid.Parse("DE1CB006-BA04-4A2A-BEAB-8E97BD7F461A"), BuildingPlanName = "to delete"}
            };
        }

        [Test]
        public async Task BuildingPlansController_ShouldSuccedToGetBuildingPlans()
        {
            //Arrange
            _buildingPlanService.Setup(bp => bp.GetAllAsync()).ReturnsAsync(() => _buildingPlans);

            //Act
            var result = await _buildingPlansController.GetBuildingPlans();

            //Assert
            dynamic value = result.Value;
            Assert.That(result, Is.TypeOf<JsonResult>());
            Assert.That(value.Result, Is.EqualTo("OK"));
            Assert.That(_buildingPlans.Count, Is.EqualTo(value.TotalRecordCount));
            Assert.That(value.Records, Is.TypeOf<List<BuildingPlanDTO>>());
        }

        [Test]
        public async Task BuildingPlansController_ShouldFailToGetBuildingPlans()
        {
            //Arrange
            _buildingPlanService.Setup(bp => bp.GetAllAsync()).ReturnsAsync(() => default);

            //Act
            var result = await _buildingPlansController.GetBuildingPlans();

            //Assert
            dynamic value = result.Value;
            _buildingPlanService.Verify(bp => bp.GetAllAsync(), Times.Once);
            Assert.That(result, Is.TypeOf<JsonResult>());
            Assert.That(value.Result, Is.EqualTo("ERROR"));
        }

        [TestCase("buildingPlan4")]
        [TestCase("buildingPlan5")]
        [TestCase("buildingPlan6")]
        public async Task BuildingPlansController_ShouldSuccedToCreateBuildingPlan(string name)
        {
            //Arrange
            _buildingPlanService.Setup(bp => bp.AddAsync(It.IsAny<CreateBuildingPlanDTO>()))
                .ReturnsAsync((CreateBuildingPlanDTO createBuildingPlanDTO) =>
                {
                    return new BuildingPlan { Id = Guid.NewGuid(), BuildingPlanName = createBuildingPlanDTO.BuildingPlanName };
                });

            //Act
            var result = await _buildingPlansController.PostBuildingPlan(new CreateBuildingPlanDTO { BuildingPlanName = name });

            //Assert
            dynamic value = result.Value;
            Assert.That(result, Is.TypeOf<JsonResult>());
            Assert.That(value.Result, Is.EqualTo("OK"));
            Assert.That(value.Message, Is.EqualTo("OK"));
        }

        [TestCase("buildingPlan4")]
        [TestCase("buildingPlan5")]
        [TestCase("buildingPlan6")]
        public async Task BuildingPlansController_ShouldFailToCreateBuildingPlan(string name)
        {
            //Arrange
            _buildingPlanService.Setup(bp => bp.AddAsync(It.IsAny<CreateBuildingPlanDTO>()))
                .ReturnsAsync((CreateBuildingPlanDTO createBuildingPlanDTO) =>
                {
                    return default;
                });

            //Act
            var result = await _buildingPlansController.PostBuildingPlan(new CreateBuildingPlanDTO { BuildingPlanName = name });

            //Assert
            dynamic value = result.Value;
            Assert.That(result, Is.TypeOf<JsonResult>());
            Assert.That(value.Result, Is.EqualTo("ERROR"));
            Assert.That(value.Message, Is.Not.EqualTo("OK"));
        }

        [TestCase("DD1CB006-BA04-4A2A-BEAB-8E97BD7F461A")]
        [TestCase("F33C76DF-B629-4CE4-AAD1-610F27724385")]
        [TestCase("B5688410-59A7-423A-9B62-C14F4DF002DC")]
        public async Task BuildingPlansController_ShouldSuccedToUpdateBuildingPlan(string id)
        {
            //Arrange
            _buildingPlanService.Setup(bp => bp.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) =>
                {
                    return _buildingPlans.Find(bp => bp.Id == id);
                });
            _buildingPlanService.Setup(bp => bp.UpdateAsync(It.IsAny<BuildingPlan>()))
                .Callback((BuildingPlan buildingPlan) =>
                {
                    _buildingPlans = _buildingPlans
                        .Where(bp => bp.Id == buildingPlan.Id)
                        .Select(bp => { bp.BuildingPlanName = "buildingPlan edit"; return bp; })
                        .ToList();
                })
                .ReturnsAsync(() => true);

            //Act

            var result = await _buildingPlansController.PutBuildingPlan(new BuildingPlanDTO { Id = Guid.Parse(id) });

            //Assert
            dynamic value = result.Value;
            Assert.That(result, Is.TypeOf<JsonResult>());
            Assert.That(value.Result, Is.EqualTo("OK"));
            Assert.That(_buildingPlans.Find(bp => bp.Id == Guid.Parse(id)).BuildingPlanName, Is.EqualTo("buildingPlan edit"));
        }

        [TestCase("123CB006-BA04-4A2A-BEAB-8E97BD7F461A")]
        [TestCase("123C76DF-B629-4CE4-AAD1-610F27724385")]
        [TestCase("12388410-59A7-423A-9B62-C14F4DF002DC")]
        public async Task BuildingPlansController_ShouldFailToUpdateBuildingPlan(string id)
        {
            //Arrange
            _buildingPlanService.Setup(bp => bp.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) =>
                {
                    return default;
                });
            _buildingPlanService.Setup(bp => bp.UpdateAsync(It.IsAny<BuildingPlan>()))
                .Callback((BuildingPlan buildingPlan) =>
                {
                    _buildingPlans = _buildingPlans
                        .Where(bp => bp.Id == buildingPlan.Id)
                        .Select(bp => { bp.BuildingPlanName = "buildingPlan edit"; return bp; })
                        .ToList();
                })
                .ReturnsAsync(() => false);

            //Act

            var result = await _buildingPlansController.PutBuildingPlan(new BuildingPlanDTO { Id = Guid.Parse(id) });

            //Assert
            dynamic value = result.Value;
            Assert.That(result, Is.TypeOf<JsonResult>());
            Assert.That(value.Result, Is.EqualTo("ERROR"));
            Assert.That(_buildingPlans.Select(bp => bp.Id).Contains(Guid.Parse(id)), Is.False);
        }

        [TestCase("DD1CB006-BA04-4A2A-BEAB-8E97BD7F461A")]
        [TestCase("F33C76DF-B629-4CE4-AAD1-610F27724385")]
        [TestCase("B5688410-59A7-423A-9B62-C14F4DF002DC")]
        public async Task BuildingPlansController_ShouldSuccedToDeleteBuildingPlan(string id)
        {
            //Arrange
            _buildingPlanService.Setup(bp => bp.DeleteAsync(It.IsAny<Guid>()))
                .Callback((Guid id) =>
                {
                    _buildingPlans.Remove(_buildingPlans.Find(bp => bp.Id == id));
                })
                .ReturnsAsync(() => true);
            //Act

            var result = await _buildingPlansController.DeleteBuildingPlan(Guid.Parse(id));

            //Assert
            dynamic value = result.Value;
            Assert.That(result, Is.TypeOf<JsonResult>());
            Assert.That(value.Result, Is.EqualTo("OK"));
            Assert.That(_buildingPlans.Select(bp => bp.Id).Contains(Guid.Parse(id)), Is.False);
        }

        [TestCase("123CB006-BA04-4A2A-BEAB-8E97BD7F461A")]
        [TestCase("123C76DF-B629-4CE4-AAD1-610F27724385")]
        [TestCase("12388410-59A7-423A-9B62-C14F4DF002DC")]
        public async Task BuildingPlansController_ShouldFailToDeleteBuildingPlan(string id)
        {
            //Arrange
            _buildingPlanService.Setup(bp => bp.DeleteAsync(It.IsAny<Guid>()))
                .Callback((Guid id) =>
                {
                    _buildingPlans.Remove(_buildingPlans.Find(bp => bp.Id == id));
                })
                .ReturnsAsync(() => false);
            //Act

            var result = await _buildingPlansController.DeleteBuildingPlan(Guid.Parse(id));

            //Assert
            dynamic value = result.Value;
            Assert.That(result, Is.TypeOf<JsonResult>());
            Assert.That(value.Result, Is.EqualTo("ERROR"));
            Assert.That(_buildingPlans.Select(bp => bp.Id).Contains(Guid.Parse(id)), Is.False);
        }

    }
}
