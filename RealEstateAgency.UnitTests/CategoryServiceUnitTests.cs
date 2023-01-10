using RealEstateAgency.Core.DTOs.Category;
using RealEstateAgency.Core.Entities;
using RealEstateAgency.Core.Interfaces;
using RealEstateAgency.Service.CategoryService;

namespace RealEstateAgency.UnitTests
{
    public class CategoryServiceUnitTests
    {
        private ICategoryService _categoryService;
        private Mock<IGenericRepository<Category>> _categoryRepository;
        List<Category> categorys;

        [SetUp]
        public void Setup()
        {
            _categoryRepository = new Mock<IGenericRepository<Category>>();
            _categoryService = new CategoryService(_categoryRepository.Object);
            categorys = new List<Category>
            {
                new Category { Id = Guid.Parse("DD1CB006-BA04-4A2A-BEAB-8E97BD7F461A"), CategoryName = "category1"},
                new Category { Id = Guid.Parse("F33C76DF-B629-4CE4-AAD1-610F27724385"), CategoryName = "category2"},
                new Category { Id = Guid.Parse("B5688410-59A7-423A-9B62-C14F4DF002DC"), CategoryName = "category3"},
                new Category { Id = Guid.Parse("DE688410-59A7-423A-9B62-C14F4DF002DC"), CategoryName = "to delete"},
                new Category { Id = Guid.Parse("DE3C76DF-B629-4CE4-AAD1-610F27724385"), CategoryName = "to delete"},
                new Category { Id = Guid.Parse("DE1CB006-BA04-4A2A-BEAB-8E97BD7F461A"), CategoryName = "to delete"}
            };

        }

        [TestCase("DD1CB006-BA04-4A2A-BEAB-8E97BD7F461A")]
        [TestCase("F33C76DF-B629-4CE4-AAD1-610F27724385")]
        [TestCase("B5688410-59A7-423A-9B62-C14F4DF002DC")]
        public async Task ShouldSucceedToGetCategoryById(string id)
        {
            //Arrange
            _categoryRepository.Setup(c => c.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(() => categorys.Where(c => c.Id == Guid.Parse(id)).FirstOrDefault());

            //Act
            var result = await _categoryService.GetByIdAsync(Guid.Parse(id));

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(Guid.Parse(id), Is.EqualTo(result.Id));
        }

        [TestCase("DD1CB006-BA04-4A2A-BEAB-8E97BD7F461A")]
        [TestCase("F33C76DF-B629-4CE4-AAD1-610F27724385")]
        [TestCase("B5688410-59A7-423A-9B62-C14F4DF002DC")]
        public async Task ShouldFailToGetCategoryById(string id)
        {
            //Arrange
            _categoryRepository.Setup(c => c.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(() => default);

            //Act
            var result = await _categoryService.GetByIdAsync(Guid.Parse(id));

            //Assert
            _categoryRepository.Verify(c => c.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
            Assert.That(result, Is.Null);
        }

        [TestCase(true)]
        [TestCase(true)]
        [TestCase(true)]
        public async Task ShouldSucceedToGetAllCategories(bool value)
        {
            // Arrange
            _categoryRepository.Setup(c => c.GetAllAsync()).ReturnsAsync(() => categorys);

            //Act
            var result = await _categoryService.GetAllAsync();

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<List<Category>>());
            Assert.That(result.Count, Is.EqualTo(categorys.Count()));
            Assert.That(result, Is.EquivalentTo(categorys));
        }

        [TestCase(false)]
        [TestCase(false)]
        [TestCase(false)]
        public async Task ShouldFailToGetAllCategories(bool value)
        {
            // Arrange
            _categoryRepository.Setup(c => c.GetAllAsync()).ReturnsAsync(() => default);

            //Act
            var result = await _categoryService.GetAllAsync();

            //Assert
            _categoryRepository.Verify(c => c.GetAllAsync(), Times.Once);
            Assert.That(result, Is.Null);
        }

        [TestCase("category4")]
        [TestCase("category5")]
        [TestCase("category6")]
        public async Task ShouldSucceedToAddCategory(string name)
        {
            //Arrange
            _categoryRepository.Setup(c => c.AddAsync(It.IsAny<Category>()))
                .Callback((Category category) =>
                {
                    categorys.Add(category);
                })
                .ReturnsAsync(() => true);

            //Act
            var result = await _categoryService.AddAsync(new CreateCategoryDTO
            {
                CategoryName = name
            });

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(categorys.Where(c => c.CategoryName == name).FirstOrDefault(), Is.Not.Null);
            Assert.That(result.CategoryName, Is.EqualTo(name));
        }

        [TestCase("category4")]
        [TestCase("category5")]
        [TestCase("category6")]
        public async Task ShouldFailToAddCategory(string name)
        {
            //Arrange
            _categoryRepository.Setup(c => c.AddAsync(It.IsAny<Category>())).ReturnsAsync(() => false);

            //Act
            var result = await _categoryService.AddAsync(new CreateCategoryDTO
            {
                CategoryName = name
            });

            //Assert
            _categoryRepository.Verify(c => c.AddAsync(It.IsAny<Category>()), Times.Once);
            Assert.That(result, Is.Null);
        }

        [TestCase("DD1CB006-BA04-4A2A-BEAB-8E97BD7F461A")]
        [TestCase("F33C76DF-B629-4CE4-AAD1-610F27724385")]
        [TestCase("B5688410-59A7-423A-9B62-C14F4DF002DC")]
        public async Task ShouldSucceedToUpdateCategory(string id)
        {
            //Arrange
            _categoryRepository.Setup(c => c.UpdateAsync(It.IsAny<Category>()))
                .Callback((Category category) =>
                {
                    categorys = categorys
                        .Where(c => c.Id == Guid.Parse(id))
                        .Select(c => { c.CategoryName = "category edit"; return c; })
                        .ToList();
                })
                .ReturnsAsync(() => true);

            //Act
            var result = await _categoryService.UpdateAsync(categorys.Where(c => c.Id == Guid.Parse(id)).FirstOrDefault());

            //Assert
            Assert.That(categorys
                            .Where(c => c.CategoryName == "category edit" && c.Id == Guid.Parse(id))
                            .FirstOrDefault(), Is.Not.Null);
            Assert.That(result, Is.True);
        }

        [TestCase("123CB006-BA04-4A2A-BEAB-8E97BD7F461A")]
        [TestCase("123C76DF-B629-4CE4-AAD1-610F27724385")]
        [TestCase("12388410-59A7-423A-9B62-C14F4DF002DC")]
        public async Task ShouldFailToUpdateCategory(string id)
        {

            //Arrange
            _categoryRepository.Setup(c => c.UpdateAsync(It.IsAny<Category>()))
                .Callback((Category category) =>
                {
                    categorys = categorys
                        .Where(c => c.Id == Guid.Parse(id))
                        .Select(c => { c.CategoryName = "category edit"; return c; })
                        .ToList();
                })
                .ReturnsAsync(() => false);

            //Act
            var result = await _categoryService.UpdateAsync(categorys.Where(c => c.Id == Guid.Parse(id)).FirstOrDefault());

            //Assert
            Assert.That(categorys
                            .Where(c => c.CategoryName == "category edit" && c.Id == Guid.Parse(id))
                            .FirstOrDefault(), Is.Null);
            Assert.That(result, Is.False);
        }

        [TestCase("DE688410-59A7-423A-9B62-C14F4DF002DC")]
        [TestCase("DE3C76DF-B629-4CE4-AAD1-610F27724385")]
        [TestCase("DE1CB006-BA04-4A2A-BEAB-8E97BD7F461A")]
        public async Task ShouldSucceedToDeleteCategory(string id)
        {
            //Arrange
            _categoryRepository.Setup(c => c.DeleteAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) =>
                {
                    var category = categorys.FirstOrDefault(c => c.Id == id);
                    if (category is not null)
                    {
                        categorys.Remove(category);
                        return true;
                    }
                    return false;
                });

            //Act
            var result = await _categoryService.DeleteAsync(Guid.Parse(id));

            //Assert
            Assert.That(result, Is.True);
            Assert.That(categorys.FirstOrDefault(c => c.Id == Guid.Parse(id)), Is.Null);
        }

        [TestCase("FA688410-59A7-423A-9B62-C14F4DF002DC")]
        [TestCase("FA3C76DF-B629-4CE4-AAD1-610F27724385")]
        [TestCase("FA1CB006-BA04-4A2A-BEAB-8E97BD7F461A")]
        public async Task ShouldFailToDeleteCategory(string id)
        {
            //Arrange
            _categoryRepository.Setup(c => c.DeleteAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) =>
                {
                    var category = categorys.FirstOrDefault(c => c.Id == id);
                    if (category is not null)
                    {
                        categorys.Remove(category);
                        return true;
                    }
                    return false;
                });

            //Act
            var result = await _categoryService.DeleteAsync(Guid.Parse(id));

            //Assert
            Assert.That(result, Is.False);
            Assert.That(categorys.FirstOrDefault(c => c.Id == Guid.Parse(id)), Is.Null);
        }
    }
}