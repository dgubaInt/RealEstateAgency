using RealEstateAgency.Core.DTOs.EstateOption;
using RealEstateAgency.Core.Entities;
using RealEstateAgency.Core.Interfaces;
using RealEstateAgency.Service.EstateOptionService;

namespace RealEstateAgency.UnitTests
{
    public class EstateOptionServiceUnitTests
    {
        private IEstateOptionService _estateOptionService;
        private Mock<IGenericRepository<EstateOption>> _estateOptionRepository;
        List<EstateOption> estateOptions;

        [SetUp]
        public void Setup()
        {
            _estateOptionRepository = new Mock<IGenericRepository<EstateOption>>();
            _estateOptionService = new EstateOptionService(_estateOptionRepository.Object);
            estateOptions = new List<EstateOption>
            {
                new EstateOption { Id = Guid.Parse("DD1CB006-BA04-4A2A-BEAB-8E97BD7F461A"), EstateOptionName = "estateOption1"},
                new EstateOption { Id = Guid.Parse("F33C76DF-B629-4CE4-AAD1-610F27724385"), EstateOptionName = "estateOption2"},
                new EstateOption { Id = Guid.Parse("B5688410-59A7-423A-9B62-C14F4DF002DC"), EstateOptionName = "estateOption3"},
                new EstateOption { Id = Guid.Parse("DE688410-59A7-423A-9B62-C14F4DF002DC"), EstateOptionName = "to delete"},
                new EstateOption { Id = Guid.Parse("DE3C76DF-B629-4CE4-AAD1-610F27724385"), EstateOptionName = "to delete"},
                new EstateOption { Id = Guid.Parse("DE1CB006-BA04-4A2A-BEAB-8E97BD7F461A"), EstateOptionName = "to delete"}
            };

        }

        [TestCase("DD1CB006-BA04-4A2A-BEAB-8E97BD7F461A")]
        [TestCase("F33C76DF-B629-4CE4-AAD1-610F27724385")]
        [TestCase("B5688410-59A7-423A-9B62-C14F4DF002DC")]
        public async Task ShouldSucceedToGetEstateOptionById(string id)
        {
            //Arrange
            _estateOptionRepository.Setup(eo => eo.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(() => estateOptions.Where(eo => eo.Id == Guid.Parse(id)).FirstOrDefault());

            //Act
            var result = await _estateOptionService.GetByIdAsync(Guid.Parse(id));

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(Guid.Parse(id), Is.EqualTo(result.Id));
        }

        [TestCase("DD1CB006-BA04-4A2A-BEAB-8E97BD7F461A")]
        [TestCase("F33C76DF-B629-4CE4-AAD1-610F27724385")]
        [TestCase("B5688410-59A7-423A-9B62-C14F4DF002DC")]
        public async Task ShouldFailToGetEstateOptionById(string id)
        {
            //Arrange
            _estateOptionRepository.Setup(eo => eo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(() => default);

            //Act
            var result = await _estateOptionService.GetByIdAsync(Guid.Parse(id));

            //Assert
            _estateOptionRepository.Verify(eo => eo.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
            Assert.That(result, Is.Null);
        }

        [TestCase(true)]
        [TestCase(true)]
        [TestCase(true)]
        public async Task ShouldSucceedToGetAllEstateOptions(bool value)
        {
            // Arrange
            _estateOptionRepository.Setup(eo => eo.GetAllAsync()).ReturnsAsync(() => estateOptions);

            //Act
            var result = await _estateOptionService.GetAllAsync();

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<List<EstateOption>>());
            Assert.That(result.Count, Is.EqualTo(estateOptions.Count()));
            Assert.That(result, Is.EquivalentTo(estateOptions));
        }

        [TestCase(false)]
        [TestCase(false)]
        [TestCase(false)]
        public async Task ShouldFailToGetAllEstateOptions(bool value)
        {
            // Arrange
            _estateOptionRepository.Setup(eo => eo.GetAllAsync()).ReturnsAsync(() => default);

            //Act
            var result = await _estateOptionService.GetAllAsync();

            //Assert
            _estateOptionRepository.Verify(eo => eo.GetAllAsync(), Times.Once);
            Assert.That(result, Is.Null);
        }

        [TestCase("estateOption4")]
        [TestCase("estateOption5")]
        [TestCase("estateOption6")]
        public async Task ShouldSucceedToAddEstateOption(string name)
        {
            //Arrange
            _estateOptionRepository.Setup(eo => eo.AddAsync(It.IsAny<EstateOption>()))
                .Callback((EstateOption estateOption) =>
                {
                    estateOptions.Add(estateOption);
                })
                .ReturnsAsync(() => true);

            //Act
            var result = await _estateOptionService.AddAsync(new CreateEstateOptionDTO
            {
                EstateOptionName = name
            });

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(estateOptions.Where(eo => eo.EstateOptionName == name).FirstOrDefault(), Is.Not.Null);
            Assert.That(result.EstateOptionName, Is.EqualTo(name));
        }

        [TestCase("estateOption4")]
        [TestCase("estateOption5")]
        [TestCase("estateOption6")]
        public async Task ShouldFailToAddEstateOption(string name)
        {
            //Arrange
            _estateOptionRepository.Setup(eo => eo.AddAsync(It.IsAny<EstateOption>())).ReturnsAsync(() => false);

            //Act
            var result = await _estateOptionService.AddAsync(new CreateEstateOptionDTO
            {
                EstateOptionName = name
            });

            //Assert
            _estateOptionRepository.Verify(eo => eo.AddAsync(It.IsAny<EstateOption>()), Times.Once);
            Assert.That(result, Is.Null);
        }

        [TestCase("DD1CB006-BA04-4A2A-BEAB-8E97BD7F461A")]
        [TestCase("F33C76DF-B629-4CE4-AAD1-610F27724385")]
        [TestCase("B5688410-59A7-423A-9B62-C14F4DF002DC")]
        public async Task ShouldSucceedToUpdateEstateOption(string id)
        {
            //Arrange
            _estateOptionRepository.Setup(eo => eo.UpdateAsync(It.IsAny<EstateOption>()))
                .Callback((EstateOption estateOption) =>
                {
                    estateOptions = estateOptions
                        .Where(eo => eo.Id == Guid.Parse(id))
                        .Select(eo => { eo.EstateOptionName = "estateOption edit"; return eo; })
                        .ToList();
                })
                .ReturnsAsync(() => true);

            //Act
            var result = await _estateOptionService.UpdateAsync(estateOptions.Where(eo => eo.Id == Guid.Parse(id)).FirstOrDefault());

            //Assert
            Assert.That(estateOptions
                            .Where(eo => eo.EstateOptionName == "estateOption edit" && eo.Id == Guid.Parse(id))
                            .FirstOrDefault(), Is.Not.Null);
            Assert.That(result, Is.True);
        }

        [TestCase("123CB006-BA04-4A2A-BEAB-8E97BD7F461A")]
        [TestCase("123C76DF-B629-4CE4-AAD1-610F27724385")]
        [TestCase("12388410-59A7-423A-9B62-C14F4DF002DC")]
        public async Task ShouldFailToUpdateEstateOption(string id)
        {

            //Arrange
            _estateOptionRepository.Setup(eo => eo.UpdateAsync(It.IsAny<EstateOption>()))
                .Callback((EstateOption estateOption) =>
                {
                    estateOptions = estateOptions
                        .Where(eo => eo.Id == Guid.Parse(id))
                        .Select(eo => { eo.EstateOptionName = "estateOption edit"; return eo; })
                        .ToList();
                })
                .ReturnsAsync(() => false);

            //Act
            var result = await _estateOptionService.UpdateAsync(estateOptions.Where(eo => eo.Id == Guid.Parse(id)).FirstOrDefault());

            //Assert
            _estateOptionRepository.Verify(eo => eo.UpdateAsync(It.IsAny<EstateOption>()), Times.Once);
            Assert.That(estateOptions
                            .Where(eo => eo.EstateOptionName == "estateOption edit" && eo.Id == Guid.Parse(id))
                            .FirstOrDefault(), Is.Null);
            Assert.That(result, Is.False);
        }

        [TestCase("DE688410-59A7-423A-9B62-C14F4DF002DC")]
        [TestCase("DE3C76DF-B629-4CE4-AAD1-610F27724385")]
        [TestCase("DE1CB006-BA04-4A2A-BEAB-8E97BD7F461A")]
        public async Task ShouldSucceedToDeleteEstateOption(string id)
        {
            //Arrange
            _estateOptionRepository.Setup(eo => eo.DeleteAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) =>
                {
                    var estateOption = estateOptions.FirstOrDefault(eo => eo.Id == id);
                    if (estateOption is not null)
                    {
                        estateOptions.Remove(estateOption);
                        return true;
                    }
                    return false;
                });

            //Act
            var result = await _estateOptionService.DeleteAsync(Guid.Parse(id));

            //Assert
            Assert.That(result, Is.True);
            Assert.That(estateOptions.FirstOrDefault(eo => eo.Id == Guid.Parse(id)), Is.Null);
        }

        [TestCase("FA688410-59A7-423A-9B62-C14F4DF002DC")]
        [TestCase("FA3C76DF-B629-4CE4-AAD1-610F27724385")]
        [TestCase("FA1CB006-BA04-4A2A-BEAB-8E97BD7F461A")]
        public async Task ShouldFailToDeleteEstateOption(string id)
        {
            //Arrange
            _estateOptionRepository.Setup(eo => eo.DeleteAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) =>
                {
                    var estateOption = estateOptions.FirstOrDefault(eo => eo.Id == id);
                    if (estateOption is not null)
                    {
                        estateOptions.Remove(estateOption);
                        return true;
                    }
                    return false;
                });

            //Act
            var result = await _estateOptionService.DeleteAsync(Guid.Parse(id));

            //Assert
            _estateOptionRepository.Verify(eo => eo.DeleteAsync(It.IsAny<Guid>()), Times.Once);
            Assert.That(result, Is.False);
            Assert.That(estateOptions.FirstOrDefault(eo => eo.Id == Guid.Parse(id)), Is.Null);
        }
    }
}