using RealEstateAgency.Core.DTOs.BuildingPlan;
using RealEstateAgency.Core.Entities;
using RealEstateAgency.Core.Interfaces;
using RealEstateAgency.Service.BuildingPlanService;

namespace RealEstateAgency.UnitTests
{
    public class BuildingPlanServiceUnitTests
    {
        private IBuildingPlanService _buildingPlanService;
        private Mock<IGenericRepository<BuildingPlan>> _buildingPlanRepository;
        List<BuildingPlan> buildingPlans;

        [SetUp]
        public void Setup()
        {
            _buildingPlanRepository = new Mock<IGenericRepository<BuildingPlan>>();
            _buildingPlanService = new BuildingPlanService(_buildingPlanRepository.Object);
            buildingPlans = new List<BuildingPlan>
            {
                new BuildingPlan { Id = Guid.Parse("DD1CB006-BA04-4A2A-BEAB-8E97BD7F461A"), BuildingPlanName = "buildingPlan1"},
                new BuildingPlan { Id = Guid.Parse("F33C76DF-B629-4CE4-AAD1-610F27724385"), BuildingPlanName = "buildingPlan2"},
                new BuildingPlan { Id = Guid.Parse("B5688410-59A7-423A-9B62-C14F4DF002DC"), BuildingPlanName = "buildingPlan3"},
                new BuildingPlan { Id = Guid.Parse("DE688410-59A7-423A-9B62-C14F4DF002DC"), BuildingPlanName = "to delete"},
                new BuildingPlan { Id = Guid.Parse("DE3C76DF-B629-4CE4-AAD1-610F27724385"), BuildingPlanName = "to delete"},
                new BuildingPlan { Id = Guid.Parse("DE1CB006-BA04-4A2A-BEAB-8E97BD7F461A"), BuildingPlanName = "to delete"}
            };

        }

        [TestCase("DD1CB006-BA04-4A2A-BEAB-8E97BD7F461A")]
        [TestCase("F33C76DF-B629-4CE4-AAD1-610F27724385")]
        [TestCase("B5688410-59A7-423A-9B62-C14F4DF002DC")]
        public async Task ShouldSucceedToGetBuildingPlanById(string id)
        {
            //Arrange
            _buildingPlanRepository.Setup(bp => bp.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(() => buildingPlans.Where(bp => bp.Id == Guid.Parse(id)).FirstOrDefault());

            //Act
            var result = await _buildingPlanService.GetByIdAsync(Guid.Parse(id));

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(Guid.Parse(id), Is.EqualTo(result.Id));
        }

        [TestCase("DD1CB006-BA04-4A2A-BEAB-8E97BD7F461A")]
        [TestCase("F33C76DF-B629-4CE4-AAD1-610F27724385")]
        [TestCase("B5688410-59A7-423A-9B62-C14F4DF002DC")]
        public async Task ShouldFailToGetBuildingPlanById(string id)
        {
            //Arrange
            _buildingPlanRepository.Setup(bp => bp.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(() => default);

            //Act
            var result = await _buildingPlanService.GetByIdAsync(Guid.Parse(id));

            //Assert
            _buildingPlanRepository.Verify(bp => bp.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
            Assert.That(result, Is.Null);
        }

        [TestCase(true)]
        [TestCase(true)]
        [TestCase(true)]
        public async Task ShouldSucceedToGetAllBuildingPlans(bool value)
        {
            // Arrange
            _buildingPlanRepository.Setup(bp => bp.GetAllAsync()).ReturnsAsync(() => buildingPlans);

            //Act
            var result = await _buildingPlanService.GetAllAsync();

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<List<BuildingPlan>>());
            Assert.That(result.Count, Is.EqualTo(buildingPlans.Count()));
            Assert.That(result, Is.EquivalentTo(buildingPlans));
        }

        [TestCase(false)]
        [TestCase(false)]
        [TestCase(false)]
        public async Task ShouldFailToGetAllBuildingPlans(bool value)
        {
            // Arrange
            _buildingPlanRepository.Setup(bp => bp.GetAllAsync()).ReturnsAsync(() => default);

            //Act
            var result = await _buildingPlanService.GetAllAsync();

            //Assert
            _buildingPlanRepository.Verify(bp => bp.GetAllAsync());
            Assert.That(result, Is.Null);
        }

        [TestCase("buildingPlan4")]
        [TestCase("buildingPlan5")]
        [TestCase("buildingPlan6")]
        public async Task ShouldSucceedToAddBuildingPlan(string name)
        {
            //Arrange
            _buildingPlanRepository.Setup(bp => bp.AddAsync(It.IsAny<BuildingPlan>()))
                .Callback((BuildingPlan buildingPlan) =>
                {
                    buildingPlans.Add(buildingPlan);
                })
                .ReturnsAsync(() => true);

            //Act
            var result = await _buildingPlanService.AddAsync(new CreateBuildingPlanDTO
            {
                BuildingPlanName = name
            });

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(buildingPlans.Where(bp => bp.BuildingPlanName == name).FirstOrDefault(), Is.Not.Null);
            Assert.That(result.BuildingPlanName, Is.EqualTo(name));
        }

        [TestCase("buildingPlan4")]
        [TestCase("buildingPlan5")]
        [TestCase("buildingPlan6")]
        public async Task ShouldFailToAddBuildingPlan(string name)
        {
            //Arrange
            var result = _buildingPlanRepository.Setup(bp => bp.AddAsync(It.IsAny<BuildingPlan>())).ReturnsAsync(() => false);

            //Act
            var buildingPlan = await _buildingPlanService.AddAsync(new CreateBuildingPlanDTO
            {
                BuildingPlanName = name
            });

            //Assert
            _buildingPlanRepository.Verify(bp => bp.AddAsync(It.IsAny<BuildingPlan>()), Times.Once);
            Assert.That(buildingPlan, Is.Null);
        }

        [TestCase("DD1CB006-BA04-4A2A-BEAB-8E97BD7F461A")]
        [TestCase("F33C76DF-B629-4CE4-AAD1-610F27724385")]
        [TestCase("B5688410-59A7-423A-9B62-C14F4DF002DC")]
        public async Task ShouldSucceedToUpdateBuildingPlan(string id)
        {
            //Arrange
            _buildingPlanRepository.Setup(bp => bp.UpdateAsync(It.IsAny<BuildingPlan>()))
                .Callback((BuildingPlan buildingPlan) =>
                {
                    buildingPlans = buildingPlans
                        .Where(bp => bp.Id == Guid.Parse(id))
                        .Select(bp => { bp.BuildingPlanName = "buildingPlan edit"; return bp; })
                        .ToList();
                })
                .ReturnsAsync(() => true);

            //Act
            var result = await _buildingPlanService.UpdateAsync(buildingPlans.Where(bp => bp.Id == Guid.Parse(id)).FirstOrDefault());

            //Assert
            Assert.That(buildingPlans
                            .Where(bp => bp.BuildingPlanName == "buildingPlan edit" && bp.Id == Guid.Parse(id))
                            .FirstOrDefault(), Is.Not.Null);
            Assert.That(result, Is.True);
        }

        [TestCase("123CB006-BA04-4A2A-BEAB-8E97BD7F461A")]
        [TestCase("123C76DF-B629-4CE4-AAD1-610F27724385")]
        [TestCase("12388410-59A7-423A-9B62-C14F4DF002DC")]
        public async Task ShouldFailToUpdateBuildingPlan(string id)
        {

            //Arrange
            _buildingPlanRepository.Setup(bp => bp.UpdateAsync(It.IsAny<BuildingPlan>()))
                .Callback((BuildingPlan buildingPlan) =>
                {
                    buildingPlans = buildingPlans
                        .Where(bp => bp.Id == Guid.Parse(id))
                        .Select(bp => { bp.BuildingPlanName = "buildingPlan edit"; return bp; })
                        .ToList();
                })
                .ReturnsAsync(() => false);

            //Act
            var result = await _buildingPlanService.UpdateAsync(buildingPlans.Where(bp => bp.Id == Guid.Parse(id)).FirstOrDefault());

            //Assert
            Assert.That(buildingPlans
                            .Where(bp => bp.BuildingPlanName == "buildingPlan edit" && bp.Id == Guid.Parse(id)).Count() == 0, Is.True);
            Assert.That(result, Is.False);
        }

        [TestCase("DE688410-59A7-423A-9B62-C14F4DF002DC")]
        [TestCase("DE3C76DF-B629-4CE4-AAD1-610F27724385")]
        [TestCase("DE1CB006-BA04-4A2A-BEAB-8E97BD7F461A")]
        public async Task ShouldSucceedToDeleteBuildingPlan(string id)
        {
            //Arrange
            _buildingPlanRepository.Setup(bp => bp.DeleteAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) =>
                {
                    var buildingPlan = buildingPlans.FirstOrDefault(bp => bp.Id == id);
                    if (buildingPlan is not null)
                    {
                        buildingPlans.Remove(buildingPlan);
                        return true;
                    }
                    return false;
                });

            //Act
            var result = await _buildingPlanService.DeleteAsync(Guid.Parse(id));

            //Assert
            Assert.That(result, Is.True);
            Assert.That(buildingPlans.FirstOrDefault(bp => bp.Id == Guid.Parse(id)), Is.Null);
        }

        [TestCase("FA688410-59A7-423A-9B62-C14F4DF002DC")]
        [TestCase("FA3C76DF-B629-4CE4-AAD1-610F27724385")]
        [TestCase("FA1CB006-BA04-4A2A-BEAB-8E97BD7F461A")]
        public async Task ShouldFailToDeleteBuildingPlan(string id)
        {
            //Arrange
            _buildingPlanRepository.Setup(bp => bp.DeleteAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) =>
                {
                    var buildingPlan = buildingPlans.FirstOrDefault(bp => bp.Id == id);
                    if (buildingPlan is not null)
                    {
                        buildingPlans.Remove(buildingPlan);
                        return true;
                    }
                    return false;
                });

            //Act
            var result = await _buildingPlanService.DeleteAsync(Guid.Parse(id));

            //Assert
            Assert.That(result, Is.False);
            Assert.That(buildingPlans.FirstOrDefault(bp => bp.Id == Guid.Parse(id)), Is.Null);
        }
    }
}