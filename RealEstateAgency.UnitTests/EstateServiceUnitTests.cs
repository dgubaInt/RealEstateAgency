using RealEstateAgency.Core.Entities;
using RealEstateAgency.Core.Interfaces;
using RealEstateAgency.Core.Models;
using RealEstateAgency.Service.EstateService;
using System.Linq.Expressions;

namespace RealEstateAgency.UnitTests
{
    public class EstateServiceUnitTests
    {
        private IEstateService _estateService;
        private Mock<IGenericRepository<Estate>> _estateRepository;
        private Mock<IGenericRepository<EstateOption>> _estateOptionRepository;
        private Mock<IGenericRepository<Photo>> _imageRepository;
        private List<Estate> estates = new List<Estate>();

        [SetUp]
        public void Setup()
        {
            _estateRepository = new Mock<IGenericRepository<Estate>>();
            _estateOptionRepository = new Mock<IGenericRepository<EstateOption>>();
            _imageRepository = new Mock<IGenericRepository<Photo>>();
            _estateService = new EstateService(_estateRepository.Object, _estateOptionRepository.Object, _imageRepository.Object);
            estates = new List<Estate>
            {
                new Estate { Id = Guid.Parse("DD1CB006-BA04-4A2A-BEAB-8E97BD7F461A"), EstateName = "estate1"},
                new Estate { Id = Guid.Parse("F33C76DF-B629-4CE4-AAD1-610F27724385"), EstateName = "estate2"},
                new Estate { Id = Guid.Parse("B5688410-59A7-423A-9B62-C14F4DF002DC"), EstateName = "estate3"},
                new Estate { Id = Guid.Parse("DE688410-59A7-423A-9B62-C14F4DF002DC"), EstateName = "to delete"},
                new Estate { Id = Guid.Parse("DE3C76DF-B629-4CE4-AAD1-610F27724385"), EstateName = "to delete"},
                new Estate { Id = Guid.Parse("DE1CB006-BA04-4A2A-BEAB-8E97BD7F461A"), EstateName = "to delete"}
            };
        }

        [TestCase("DD1CB006-BA04-4A2A-BEAB-8E97BD7F461A")]
        [TestCase("F33C76DF-B629-4CE4-AAD1-610F27724385")]
        [TestCase("B5688410-59A7-423A-9B62-C14F4DF002DC")]
        public async Task ShouldSucceedToGetEstateById(string id)
        {
            //Arrange
            _estateRepository.Setup(e => e.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<Expression<Func<Estate, object>>[]>()))
                .ReturnsAsync(() => estates.Where(e => e.Id == Guid.Parse(id)).FirstOrDefault());

            //Act
            var result = await _estateService.GetByIdAsync(Guid.Parse(id));

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(Guid.Parse(id), Is.EqualTo(result.Id));
        }

        [TestCase("FA1CB006-BA04-4A2A-BEAB-8E97BD7F461A")]
        [TestCase("FA3C76DF-B629-4CE4-AAD1-610F27724385")]
        [TestCase("FA688410-59A7-423A-9B62-C14F4DF002DC")]
        public async Task ShouldFailToGetEstateById(string id)
        {
            //Arrange
            _estateRepository.Setup(e => e.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<Expression<Func<Estate, object>>[]>()))
                .ReturnsAsync(() => estates.Where(e => e.Id == Guid.Parse(id)).FirstOrDefault());

            //Act
            var result = await _estateService.GetByIdAsync(Guid.Parse(id));

            //Assert
            _estateRepository.Verify(e => e.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<Expression<Func<Estate, object>>[]>()), Times.Once);
            Assert.That(result, Is.Null);
            Assert.That(estates.Find(e => e.Id == Guid.Parse(id)), Is.Null);
        }

        [TestCase(true)]
        [TestCase(true)]
        [TestCase(true)]
        public async Task ShouldSucceedToGetAllEstates(bool value)
        {
            // Arrange
            _estateRepository.Setup(u => u.GetAllAsync(It.IsAny<Expression<Func<Estate, object>>[]>())).ReturnsAsync(() => estates);

            //Act
            var result = await _estateService.GetAllAsync();

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<List<Estate>>());
            Assert.That(result.Count, Is.EqualTo(estates.Count));
            Assert.That(result, Is.EquivalentTo(estates));
        }

        [TestCase(false)]
        [TestCase(false)]
        [TestCase(false)]
        public async Task ShouldFailToGetAllEstates(bool value)
        {
            // Arrange
            _estateRepository.Setup(u => u.GetAllAsync(It.IsAny<Expression<Func<Estate, object>>[]>())).ReturnsAsync(() => default);

            //Act
            var result = await _estateService.GetAllAsync();

            //Assert
            _estateRepository.Verify(u => u.GetAllAsync(It.IsAny<Expression<Func<Estate, object>>[]>()), Times.Once);
            Assert.That(result, Is.Null);
        }

        [TestCase(true)]
        [TestCase(true)]
        [TestCase(true)]
        public async Task ShouldSucceedToGetAllFilteredEstates(bool value)
        {
            // Arrange
            _estateRepository.
                Setup(u => u.GetAllAsync(It.IsAny<Expression<Func<Estate, bool>>>(), It.IsAny<Expression<Func<Estate, object>>[]>()))
                .ReturnsAsync(() => estates.Where(e => e.EstateName.Contains("estate")).ToList());

            //Act
            var result = await _estateService.GetAllAsync(e => e.EstateName.Contains("estate"));

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<List<Estate>>());
            Assert.That(result.Count, Is.EqualTo(estates.Where(e => e.EstateName.Contains("estate")).Count()));
            Assert.That(result, Is.EquivalentTo(estates.Where(e => e.EstateName.Contains("estate")).ToList()));
        }

        [TestCase(false)]
        [TestCase(false)]
        [TestCase(false)]
        public async Task ShouldFailToGetAllFilteredEstates(bool value)
        {
            // Arrange
            _estateRepository
                .Setup(u => u.GetAllAsync(It.IsAny<Expression<Func<Estate, bool>>>(), It.IsAny<Expression<Func<Estate, object>>[]>()))
                .ReturnsAsync(() => default);

            //Act
            var result = await _estateService.GetAllAsync(e => e.EstateName.Contains("test"));

            //Assert
            _estateRepository
                .Verify(u => u.GetAllAsync(It.IsAny<Expression<Func<Estate, bool>>>(), It.IsAny<Expression<Func<Estate, object>>[]>()), Times.Once);
            Assert.That(result, Is.Null);
        }

        [TestCase("estate4")]
        [TestCase("estate5")]
        [TestCase("estate6")]
        public async Task ShouldSucceedToAddEstate(string name)
        {
            //Arrange
            _estateRepository.Setup(u => u.AddAsync(It.IsAny<Estate>()))
                .Callback((Estate estate) =>
                {
                    estates.Add(estate);
                })
                .ReturnsAsync(() => true);

            //Act
            var initialListCount = estates.Count;
            var result = await _estateService.AddAsync(new Estate
            {
                Id = Guid.NewGuid(),
                EstateName = name
            });

            //Assert
            Assert.That(result, Is.True);
            Assert.That(estates.Count, Is.EqualTo(initialListCount + 1));
        }

        [TestCase("estate4")]
        [TestCase("estate5")]
        [TestCase("estate6")]
        public async Task ShouldFailToAddEstate(string name)
        {
            //Arrange
            _estateRepository.Setup(u => u.AddAsync(It.IsAny<Estate>()))
                .ReturnsAsync(() => false);

            //Act
            var initialListCount = estates.Count;
            var result = await _estateService.AddAsync(new Estate
            {
                Id = Guid.NewGuid(),
                EstateName = name
            });

            //Assert
            Assert.That(result, Is.False);
            Assert.That(estates.Count, Is.Not.EqualTo(initialListCount + 1));
        }

        [TestCase("DE688410-59A7-423A-9B62-C14F4DF002DC")]
        [TestCase("DE3C76DF-B629-4CE4-AAD1-610F27724385")]
        [TestCase("DE1CB006-BA04-4A2A-BEAB-8E97BD7F461A")]
        public async Task ShouldSucceedToDeleteEstate(string id)
        {
            //Arrange
            _estateRepository.Setup(e => e.DeleteAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) =>
                {
                    var estate = estates.FirstOrDefault(e => e.Id == id);
                    if (estate is not null)
                    {
                        estates.Remove(estate);
                        return true;
                    }
                    return false;
                });

            //Act
            var result = await _estateService.DeleteAsync(Guid.Parse(id));

            //Assert
            Assert.That(result, Is.True);
            Assert.That(estates.FirstOrDefault(e => e.Id == Guid.Parse(id)), Is.Null);
        }

        [TestCase("FA688410-59A7-423A-9B62-C14F4DF002DC")]
        [TestCase("FA3C76DF-B629-4CE4-AAD1-610F27724385")]
        [TestCase("FA1CB006-BA04-4A2A-BEAB-8E97BD7F461A")]
        public async Task ShouldFailToDeleteEstate(string id)
        {
            //Arrange
            _estateRepository.Setup(e => e.DeleteAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) =>
                {
                    var estate = estates.FirstOrDefault(e => e.Id == id);
                    if (estate is not null)
                    {
                        estates.Remove(estate);
                        return true;
                    }
                    return false;
                });

            //Act
            var result = await _estateService.DeleteAsync(Guid.Parse(id));

            //Assert
            Assert.That(result, Is.False);
            Assert.That(estates.FirstOrDefault(e => e.Id == Guid.Parse(id)), Is.Null);
        }

        [TestCase("DD1CB006-BA04-4A2A-BEAB-8E97BD7F461A")]
        [TestCase("F33C76DF-B629-4CE4-AAD1-610F27724385")]
        [TestCase("B5688410-59A7-423A-9B62-C14F4DF002DC")]
        public async Task ShouldSucceedToUpdateEstate(string id)
        {
            //Arrange
            _estateRepository.Setup(e => e.UpdateAsync(It.IsAny<Estate>()))
                .Callback((Estate estate) =>
                {
                    estates = estates
                        .Where(e => e.Id == Guid.Parse(id))
                        .Select(e => { e.EstateName = "estate edit"; return e; })
                        .ToList();
                })
                .ReturnsAsync(() => true);
            _estateRepository.Setup(e => e.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<Expression<Func<Estate, object>>[]>()))
                .ReturnsAsync(() => estates.Find(e => e.Id == Guid.Parse(id)));
            _estateOptionRepository.Setup(eo => eo.GetAllAsync(It.IsAny<Expression<Func<EstateOption, bool>>>()))
                .ReturnsAsync(() => new List<EstateOption> { new EstateOption { Id = Guid.NewGuid(), EstateOptionName = "test" } });
            _imageRepository.Setup(eo => eo.GetAllAsync(It.IsAny<Expression<Func<Photo, bool>>>()))
                .ReturnsAsync(() => new List<Photo> { new Photo { Id = Guid.NewGuid(), FileTitle = "test" } });

            //Act
            var editEstateViewModel = new EditEstateViewModel
            {
                Id = Guid.Parse(id),
                EstateName = "estate edit",
                Photos = new List<string>(),
                EstateOptionViewModels = new List<EstateOptionViewModel>()
            };
            var result = await _estateService.UpdateAsync(editEstateViewModel);

            //Assert
            Assert.That(estates
                            .Where(e => e.EstateName == "estate edit" && e.Id == Guid.Parse(id))
                            .FirstOrDefault(), Is.Not.Null);
            Assert.That(result, Is.True);
        }

        [TestCase("123CB006-BA04-4A2A-BEAB-8E97BD7F461A")]
        [TestCase("123C76DF-B629-4CE4-AAD1-610F27724385")]
        [TestCase("12388410-59A7-423A-9B62-C14F4DF002DC")]
        public async Task ShouldFailToUpdateEstate(string id)
        {
            //Arrange
            _estateRepository.Setup(e => e.UpdateAsync(It.IsAny<Estate>()))
                .Callback((Estate estate) =>
                {
                    estates = estates
                        .Where(e => e.Id == Guid.Parse(id))
                        .Select(e => { e.EstateName = "estate edit"; return e; })
                        .ToList();
                })
                .ReturnsAsync(() => false);
            _estateRepository.Setup(e => e.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<Expression<Func<Estate, object>>[]>()))
               .ReturnsAsync(() => new Estate { Id = Guid.Parse(id) });
            _estateOptionRepository.Setup(eo => eo.GetAllAsync(It.IsAny<Expression<Func<EstateOption, bool>>>()))
                .ReturnsAsync(() => new List<EstateOption> { new EstateOption { Id = Guid.NewGuid(), EstateOptionName = "test" } });
            _imageRepository.Setup(eo => eo.GetAllAsync(It.IsAny<Expression<Func<Photo, bool>>>()))
                .ReturnsAsync(() => new List<Photo> { new Photo { Id = Guid.NewGuid(), FileTitle = "test" } });

            //Act
            var result = await _estateService.UpdateAsync(new EditEstateViewModel { Id = Guid.Parse(id), EstateName = "estate edit" });

            //Assert
            Assert.That(estates
                            .Where(e => e.EstateName == "estate edit" && e.Id == Guid.Parse(id)).Count() == 0, Is.True);
            Assert.That(result, Is.False);
        }
    }
}
