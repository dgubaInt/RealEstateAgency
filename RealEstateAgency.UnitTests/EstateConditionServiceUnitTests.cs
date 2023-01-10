using RealEstateAgency.Core.DTOs.EstateCondition;
using RealEstateAgency.Core.Entities;
using RealEstateAgency.Core.Interfaces;
using RealEstateAgency.Service.EstateConditionService;

namespace RealEstateAgency.UnitTests
{
    public class EstateConditionServiceUnitTests
    {
        private IEstateConditionService _estateConditionService;
        private Mock<IGenericRepository<EstateCondition>> _estateConditionRepository;
        List<EstateCondition> estateConditions;

        [SetUp]
        public void Setup()
        {
            _estateConditionRepository = new Mock<IGenericRepository<EstateCondition>>();
            _estateConditionService = new EstateConditionService(_estateConditionRepository.Object);
            estateConditions = new List<EstateCondition>
            {
                new EstateCondition { Id = Guid.Parse("DD1CB006-BA04-4A2A-BEAB-8E97BD7F461A"), EstateConditionName = "estateCondition1"},
                new EstateCondition { Id = Guid.Parse("F33C76DF-B629-4CE4-AAD1-610F27724385"), EstateConditionName = "estateCondition2"},
                new EstateCondition { Id = Guid.Parse("B5688410-59A7-423A-9B62-C14F4DF002DC"), EstateConditionName = "estateCondition3"},
                new EstateCondition { Id = Guid.Parse("DE688410-59A7-423A-9B62-C14F4DF002DC"), EstateConditionName = "to delete"},
                new EstateCondition { Id = Guid.Parse("DE3C76DF-B629-4CE4-AAD1-610F27724385"), EstateConditionName = "to delete"},
                new EstateCondition { Id = Guid.Parse("DE1CB006-BA04-4A2A-BEAB-8E97BD7F461A"), EstateConditionName = "to delete"}
            };

        }

        [TestCase("DD1CB006-BA04-4A2A-BEAB-8E97BD7F461A")]
        [TestCase("F33C76DF-B629-4CE4-AAD1-610F27724385")]
        [TestCase("B5688410-59A7-423A-9B62-C14F4DF002DC")]
        public async Task ShouldSucceedToGetEstateConditionById(string id)
        {
            //Arrange
            _estateConditionRepository.Setup(ec => ec.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(() => estateConditions.Where(ec => ec.Id == Guid.Parse(id)).FirstOrDefault());

            //Act
            var result = await _estateConditionService.GetByIdAsync(Guid.Parse(id));

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(Guid.Parse(id), Is.EqualTo(result.Id));
        }

        [TestCase("DD1CB006-BA04-4A2A-BEAB-8E97BD7F461A")]
        [TestCase("F33C76DF-B629-4CE4-AAD1-610F27724385")]
        [TestCase("B5688410-59A7-423A-9B62-C14F4DF002DC")]
        public async Task ShouldFailToGetEstateConditionById(string id)
        {
            //Arrange
            _estateConditionRepository.Setup(ec => ec.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(() => default);

            //Act
            var result = await _estateConditionService.GetByIdAsync(Guid.Parse(id));

            //Assert
            _estateConditionRepository.Verify(ec => ec.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
            Assert.That(result, Is.Null);
        }

        [TestCase(true)]
        [TestCase(true)]
        [TestCase(true)]
        public async Task ShouldSucceedToGetAllEstateConditions(bool value)
        {
            // Arrange
            _estateConditionRepository.Setup(ec => ec.GetAllAsync()).ReturnsAsync(() => estateConditions);

            //Act
            var result = await _estateConditionService.GetAllAsync();

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<List<EstateCondition>>());
            Assert.That(result.Count, Is.EqualTo(estateConditions.Count()));
            Assert.That(result, Is.EquivalentTo(estateConditions));
        }

        [TestCase(false)]
        [TestCase(false)]
        [TestCase(false)]
        public async Task ShouldFailToGetAllEstateConditions(bool value)
        {
            // Arrange
            _estateConditionRepository.Setup(ec => ec.GetAllAsync()).ReturnsAsync(() => default);

            //Act
            var result = await _estateConditionService.GetAllAsync();

            //Assert
            _estateConditionRepository.Verify(ec => ec.GetAllAsync(), Times.Once);
            Assert.That(result, Is.Null);
        }

        [TestCase("estateCondition4")]
        [TestCase("estateCondition5")]
        [TestCase("estateCondition6")]
        public async Task ShouldSucceedToAddEstateCondition(string name)
        {
            //Arrange
            _estateConditionRepository.Setup(ec => ec.AddAsync(It.IsAny<EstateCondition>()))
                .Callback((EstateCondition estateCondition) =>
                {
                    estateConditions.Add(estateCondition);
                })
                .ReturnsAsync(() => true);

            //Act
            var result = await _estateConditionService.AddAsync(new CreateEstateConditionDTO
            {
                EstateConditionName = name
            });

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(estateConditions.Where(ec => ec.EstateConditionName == name).FirstOrDefault(), Is.Not.Null);
            Assert.That(result.EstateConditionName, Is.EqualTo(name));
        }

        [TestCase("estateCondition4")]
        [TestCase("estateCondition5")]
        [TestCase("estateCondition6")]
        public async Task ShouldFailToAddEstateCondition(string name)
        {
            //Arrange
            _estateConditionRepository.Setup(ec => ec.AddAsync(It.IsAny<EstateCondition>())).ReturnsAsync(() => false);

            //Act
            var result = await _estateConditionService.AddAsync(new CreateEstateConditionDTO
            {
                EstateConditionName = name
            });

            //Assert
            _estateConditionRepository.Verify(ec => ec.AddAsync(It.IsAny<EstateCondition>()), Times.Once);
            Assert.That(result, Is.Null);
        }

        [TestCase("DD1CB006-BA04-4A2A-BEAB-8E97BD7F461A")]
        [TestCase("F33C76DF-B629-4CE4-AAD1-610F27724385")]
        [TestCase("B5688410-59A7-423A-9B62-C14F4DF002DC")]
        public async Task ShouldSucceedToUpdateEstateCondition(string id)
        {
            //Arrange
            _estateConditionRepository.Setup(ec => ec.UpdateAsync(It.IsAny<EstateCondition>()))
                .Callback((EstateCondition estateCondition) =>
                {
                    estateConditions = estateConditions
                        .Where(ec => ec.Id == Guid.Parse(id))
                        .Select(ec => { ec.EstateConditionName = "estateCondition edit"; return ec; })
                        .ToList();
                })
                .ReturnsAsync(() => true);

            //Act
            var result = await _estateConditionService.UpdateAsync(estateConditions.Where(ec => ec.Id == Guid.Parse(id)).FirstOrDefault());

            //Assert
            Assert.That(estateConditions
                            .Where(ec => ec.EstateConditionName == "estateCondition edit" && ec.Id == Guid.Parse(id))
                            .FirstOrDefault(), Is.Not.Null);
            Assert.That(result, Is.True);
        }

        [TestCase("123CB006-BA04-4A2A-BEAB-8E97BD7F461A")]
        [TestCase("123C76DF-B629-4CE4-AAD1-610F27724385")]
        [TestCase("12388410-59A7-423A-9B62-C14F4DF002DC")]
        public async Task ShouldFailToUpdateEstateCondition(string id)
        {

            //Arrange
            _estateConditionRepository.Setup(ec => ec.UpdateAsync(It.IsAny<EstateCondition>()))
                .Callback((EstateCondition estateCondition) =>
                {
                    estateConditions = estateConditions
                        .Where(ec => ec.Id == Guid.Parse(id))
                        .Select(ec => { ec.EstateConditionName = "estateCondition edit"; return ec; })
                        .ToList();
                })
                .ReturnsAsync(() => false);

            //Act
            var result = await _estateConditionService.UpdateAsync(estateConditions.Where(ec => ec.Id == Guid.Parse(id)).FirstOrDefault());

            //Assert
            _estateConditionRepository.Verify(ec => ec.UpdateAsync(It.IsAny<EstateCondition>()), Times.Once);
            Assert.That(estateConditions
                            .Where(ec => ec.EstateConditionName == "estateCondition edit" && ec.Id == Guid.Parse(id))
                            .Count() == 0, Is.True);
            Assert.That(result, Is.False);
        }

        [TestCase("DE688410-59A7-423A-9B62-C14F4DF002DC")]
        [TestCase("DE3C76DF-B629-4CE4-AAD1-610F27724385")]
        [TestCase("DE1CB006-BA04-4A2A-BEAB-8E97BD7F461A")]
        public async Task ShouldSucceedToDeleteEstateCondition(string id)
        {
            //Arrange
            _estateConditionRepository.Setup(ec => ec.DeleteAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) =>
                {
                    var estateCondition = estateConditions.FirstOrDefault(ec => ec.Id == id);
                    if (estateCondition is not null)
                    {
                        estateConditions.Remove(estateCondition);
                        return true;
                    }
                    return false;
                });

            //Act
            var result = await _estateConditionService.DeleteAsync(Guid.Parse(id));

            //Assert
            Assert.That(result, Is.True);
            Assert.That(estateConditions.FirstOrDefault(ec => ec.Id == Guid.Parse(id)), Is.Null);
        }

        [TestCase("FA688410-59A7-423A-9B62-C14F4DF002DC")]
        [TestCase("FA3C76DF-B629-4CE4-AAD1-610F27724385")]
        [TestCase("FA1CB006-BA04-4A2A-BEAB-8E97BD7F461A")]
        public async Task ShouldFailToDeleteEstateCondition(string id)
        {
            //Arrange
            _estateConditionRepository.Setup(ec => ec.DeleteAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) =>
                {
                    var estateCondition = estateConditions.FirstOrDefault(ec => ec.Id == id);
                    if (estateCondition is not null)
                    {
                        estateConditions.Remove(estateCondition);
                        return true;
                    }
                    return false;
                });

            //Act
            var result = await _estateConditionService.DeleteAsync(Guid.Parse(id));

            //Assert
            _estateConditionRepository.Verify(ec => ec.DeleteAsync(It.IsAny<Guid>()), Times.Once);
            Assert.That(result, Is.False);
            Assert.That(estateConditions.FirstOrDefault(ec => ec.Id == Guid.Parse(id)), Is.Null);
        }
    }
}