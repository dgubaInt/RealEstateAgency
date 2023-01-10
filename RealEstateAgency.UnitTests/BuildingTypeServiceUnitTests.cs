using RealEstateAgency.Core.DTOs.BuildingType;
using RealEstateAgency.Core.Entities;
using RealEstateAgency.Core.Interfaces;
using RealEstateAgency.Service.BuildingTypeService;

namespace RealEstateAgency.UnitTests
{
    public class BuildingTypeServiceUnitTests
    {
        private IBuildingTypeService _buildingTypeService;
        private Mock<IGenericRepository<BuildingType>> _buildingTypeRepository;
        List<BuildingType> buildingTypes;

        [SetUp]
        public void Setup()
        {
            _buildingTypeRepository = new Mock<IGenericRepository<BuildingType>>();
            _buildingTypeService = new BuildingTypeService(_buildingTypeRepository.Object);
            buildingTypes = new List<BuildingType>
            {
                new BuildingType { Id = Guid.Parse("DD1CB006-BA04-4A2A-BEAB-8E97BD7F461A"), BuildingTypeName = "buildingType1"},
                new BuildingType { Id = Guid.Parse("F33C76DF-B629-4CE4-AAD1-610F27724385"), BuildingTypeName = "buildingType2"},
                new BuildingType { Id = Guid.Parse("B5688410-59A7-423A-9B62-C14F4DF002DC"), BuildingTypeName = "buildingType3"},
                new BuildingType { Id = Guid.Parse("DE688410-59A7-423A-9B62-C14F4DF002DC"), BuildingTypeName = "to delete"},
                new BuildingType { Id = Guid.Parse("DE3C76DF-B629-4CE4-AAD1-610F27724385"), BuildingTypeName = "to delete"},
                new BuildingType { Id = Guid.Parse("DE1CB006-BA04-4A2A-BEAB-8E97BD7F461A"), BuildingTypeName = "to delete"}
            };

        }

        [TestCase("DD1CB006-BA04-4A2A-BEAB-8E97BD7F461A")]
        [TestCase("F33C76DF-B629-4CE4-AAD1-610F27724385")]
        [TestCase("B5688410-59A7-423A-9B62-C14F4DF002DC")]
        public async Task ShouldSucceedToGetBuildingTypeById(string id)
        {
            //Arrange
            _buildingTypeRepository.Setup(bt => bt.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(() => buildingTypes.Where(bt => bt.Id == Guid.Parse(id)).FirstOrDefault());

            //Act
            var result = await _buildingTypeService.GetByIdAsync(Guid.Parse(id));

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(Guid.Parse(id), Is.EqualTo(result.Id));
        }

        [TestCase("DD1CB006-BA04-4A2A-BEAB-8E97BD7F461A")]
        [TestCase("F33C76DF-B629-4CE4-AAD1-610F27724385")]
        [TestCase("B5688410-59A7-423A-9B62-C14F4DF002DC")]
        public async Task ShouldFailToGetBuildingTypeById(string id)
        {
            //Arrange
            _buildingTypeRepository.Setup(bt => bt.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(() => default);

            //Act
            var result = await _buildingTypeService.GetByIdAsync(Guid.Parse(id));

            //Assert
            _buildingTypeRepository.Verify(bt => bt.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
            Assert.That(result, Is.Null);
        }

        [TestCase(true)]
        [TestCase(true)]
        [TestCase(true)]
        public async Task ShouldSucceedToGetAllBuildingTypes(bool value)
        {
            // Arrange
            _buildingTypeRepository.Setup(bt => bt.GetAllAsync()).ReturnsAsync(() => buildingTypes);

            //Act
            var result = await _buildingTypeService.GetAllAsync();

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<List<BuildingType>>());
            Assert.That(result.Count, Is.EqualTo(buildingTypes.Count()));
            Assert.That(result, Is.EquivalentTo(buildingTypes));
        }

        [TestCase(false)]
        [TestCase(false)]
        [TestCase(false)]
        public async Task ShouldFailToGetAllBuildingTypes(bool value)
        {
            // Arrange
            _buildingTypeRepository.Setup(bt => bt.GetAllAsync()).ReturnsAsync(() => default);

            //Act
            var result = await _buildingTypeService.GetAllAsync();

            //Assert
            _buildingTypeRepository.Verify(bt => bt.GetAllAsync());
            Assert.That(result, Is.Null);
        }

        [TestCase("buildingType4")]
        [TestCase("buildingType5")]
        [TestCase("buildingType6")]
        public async Task ShouldSucceedToAddBuildingType(string name)
        {
            //Arrange
            _buildingTypeRepository.Setup(bt => bt.AddAsync(It.IsAny<BuildingType>()))
                .Callback((BuildingType buildingType) =>
                {
                    buildingTypes.Add(buildingType);
                })
                .ReturnsAsync(() => true);

            //Act
            var result = await _buildingTypeService.AddAsync(new CreateBuildingTypeDTO
            {
                BuildingTypeName = name
            });

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(buildingTypes.Where(bt => bt.BuildingTypeName == name).FirstOrDefault(), Is.Not.Null);
            Assert.That(result.BuildingTypeName, Is.EqualTo(name));
        }

        [TestCase("buildingType4")]
        [TestCase("buildingType5")]
        [TestCase("buildingType6")]
        public async Task ShouldFailToAddBuildingType(string name)
        {
            //Arrange
            _buildingTypeRepository.Setup(bt => bt.AddAsync(It.IsAny<BuildingType>())).ReturnsAsync(() => false);

            //Act
            var result = await _buildingTypeService.AddAsync(new CreateBuildingTypeDTO
            {
                BuildingTypeName = name
            });

            //Assert
            _buildingTypeRepository.Verify(bt => bt.AddAsync(It.IsAny<BuildingType>()), Times.Once);
            Assert.That(result, Is.Null);
        }

        [TestCase("DD1CB006-BA04-4A2A-BEAB-8E97BD7F461A")]
        [TestCase("F33C76DF-B629-4CE4-AAD1-610F27724385")]
        [TestCase("B5688410-59A7-423A-9B62-C14F4DF002DC")]
        public async Task ShouldSucceedToUpdateBuildingType(string id)
        {
            //Arrange
            _buildingTypeRepository.Setup(bt => bt.UpdateAsync(It.IsAny<BuildingType>()))
                .Callback((BuildingType buildingType) =>
                {
                    buildingTypes = buildingTypes
                        .Where(bt => bt.Id == Guid.Parse(id))
                        .Select(bt => { bt.BuildingTypeName = "buildingType edit"; return bt; })
                        .ToList();
                })
                .ReturnsAsync(() => true);

            //Act
            var result = await _buildingTypeService.UpdateAsync(buildingTypes.Where(bt => bt.Id == Guid.Parse(id)).FirstOrDefault());

            //Assert
            Assert.That(buildingTypes
                            .Where(bt => bt.BuildingTypeName == "buildingType edit" && bt.Id == Guid.Parse(id))
                            .FirstOrDefault(), Is.Not.Null);
            Assert.That(result, Is.True);
        }

        [TestCase("123CB006-BA04-4A2A-BEAB-8E97BD7F461A")]
        [TestCase("123C76DF-B629-4CE4-AAD1-610F27724385")]
        [TestCase("12388410-59A7-423A-9B62-C14F4DF002DC")]
        public async Task ShouldFailToUpdateBuildingType(string id)
        {

            //Arrange
            _buildingTypeRepository.Setup(bt => bt.UpdateAsync(It.IsAny<BuildingType>()))
                .Callback((BuildingType buildingType) =>
                {
                    buildingTypes = buildingTypes
                        .Where(bt => bt.Id == Guid.Parse(id))
                        .Select(bt => { bt.BuildingTypeName = "buildingType edit"; return bt; })
                        .ToList();
                })
                .ReturnsAsync(() => false);

            //Act
            var result = await _buildingTypeService.UpdateAsync(buildingTypes.Where(bt => bt.Id == Guid.Parse(id)).FirstOrDefault());

            //Assert
            Assert.That(buildingTypes
                            .Where(bt => bt.BuildingTypeName == "buildingType edit" && bt.Id == Guid.Parse(id))
                            .Count() == 0, Is.True);
            Assert.That(result, Is.False);
        }

        [TestCase("DE688410-59A7-423A-9B62-C14F4DF002DC")]
        [TestCase("DE3C76DF-B629-4CE4-AAD1-610F27724385")]
        [TestCase("DE1CB006-BA04-4A2A-BEAB-8E97BD7F461A")]
        public async Task ShouldSucceedToDeleteBuildingType(string id)
        {
            //Arrange
            _buildingTypeRepository.Setup(bt => bt.DeleteAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) =>
                {
                    var buildingType = buildingTypes.FirstOrDefault(bt => bt.Id == id);
                    if (buildingType is not null)
                    {
                        buildingTypes.Remove(buildingType);
                        return true;
                    }
                    return false;
                });

            //Act
            var result = await _buildingTypeService.DeleteAsync(Guid.Parse(id));

            //Assert
            Assert.That(result, Is.True);
            Assert.That(buildingTypes.FirstOrDefault(bt => bt.Id == Guid.Parse(id)), Is.Null);
        }

        [TestCase("FA688410-59A7-423A-9B62-C14F4DF002DC")]
        [TestCase("FA3C76DF-B629-4CE4-AAD1-610F27724385")]
        [TestCase("FA1CB006-BA04-4A2A-BEAB-8E97BD7F461A")]
        public async Task ShouldFailToDeleteBuildingType(string id)
        {
            //Arrange
            _buildingTypeRepository.Setup(bt => bt.DeleteAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) =>
                {
                    var buildingType = buildingTypes.FirstOrDefault(bt => bt.Id == id);
                    if (buildingType is not null)
                    {
                        buildingTypes.Remove(buildingType);
                        return true;
                    }
                    return false;
                });

            //Act
            var result = await _buildingTypeService.DeleteAsync(Guid.Parse(id));

            //Assert
            Assert.That(result, Is.False);
            Assert.That(buildingTypes.FirstOrDefault(bt => bt.Id == Guid.Parse(id)), Is.Null);
        }
    }
}