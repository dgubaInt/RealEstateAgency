using RealEstateAgency.Core.Entities;
using RealEstateAgency.Core.Interfaces;
using RealEstateAgency.Service.ImageService;
namespace RealEstateAgency.UnitTests
{
    public class ImageServiceUnitTests
    {
        private IImageService _imageService;
        private Mock<IGenericRepository<Photo>> _photoRepository;
        private List<Photo> _photos;

        [SetUp]
        public void Setup()
        {
            _photoRepository = new Mock<IGenericRepository<Photo>>();
            _imageService = new ImageService(_photoRepository.Object);
            _photos = new List<Photo>
            {
                new Photo{Id = Guid.NewGuid(), FileTitle = "image1"},
                new Photo{Id = Guid.NewGuid(), FileTitle = "image2"},
                new Photo{Id = Guid.NewGuid(), FileTitle = "image3"}
            };
        }

        [Test]
        public async Task ShouldSucceedToAddPhoto()
        {
            //Arrange
            _photoRepository.Setup(p => p.AddAsync(It.IsAny<Photo>())).ReturnsAsync(() => true);

            //Act
            var result = await _imageService.AddAsync(new Photo { Id = Guid.NewGuid(), FileTitle = "image" });

            //Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public async Task ShouldFailToAddPhoto()
        {
            //Arrange
            _photoRepository.Setup(p => p.AddAsync(It.IsAny<Photo>())).ReturnsAsync(() => false);

            //Act
            var result = await _imageService.AddAsync(new Photo { Id = Guid.NewGuid(), FileTitle = "image" });

            //Assert
            _photoRepository.Verify(p => p.AddAsync(It.IsAny<Photo>()), Times.Once);
            Assert.That(result, Is.False);
        }

        [TestCase(true)]
        [TestCase(true)]
        [TestCase(true)]
        public async Task ShouldSucceedToGetAllImages(bool value)
        {
            // Arrange
            _photoRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(() => _photos);

            //Act
            var result = await _imageService.GetAllAsync();

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<List<Photo>>());
            Assert.That(result.Count, Is.EqualTo(_photos.Count()));
            Assert.That(result, Is.EquivalentTo(_photos));
        }

        [TestCase(false)]
        [TestCase(false)]
        [TestCase(false)]
        public async Task ShouldFailToGetAllImages(bool value)
        {
            // Arrange
            _photoRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(() => default);

            //Act
            var result = await _imageService.GetAllAsync();

            //Assert
            _photoRepository.Verify(r => r.GetAllAsync(), Times.Once);
            Assert.That(result, Is.Null);
        }
    }
}
