using RealEstateAgency.Core.DTOs.Zone;
using RealEstateAgency.Core.Entities;
using RealEstateAgency.Core.Interfaces;
using RealEstateAgency.Service.ZoneService;

namespace RealEstateAgency.UnitTests
{
    public class ZoneServiceUnitTests
    {
        private IZoneService _zoneService;
        private Mock<IGenericRepository<Zone>> _zoneRepository;
        List<Zone> zones;

        [SetUp]
        public void Setup()
        {
            _zoneRepository = new Mock<IGenericRepository<Zone>>();
            _zoneService = new ZoneService(_zoneRepository.Object);
            zones = new List<Zone>
            {
                new Zone { Id = Guid.Parse("DD1CB006-BA04-4A2A-BEAB-8E97BD7F461A"), ZoneName = "zone1"},
                new Zone { Id = Guid.Parse("F33C76DF-B629-4CE4-AAD1-610F27724385"), ZoneName = "zone2"},
                new Zone { Id = Guid.Parse("B5688410-59A7-423A-9B62-C14F4DF002DC"), ZoneName = "zone3"},
                new Zone { Id = Guid.Parse("DE688410-59A7-423A-9B62-C14F4DF002DC"), ZoneName = "to delete"},
                new Zone { Id = Guid.Parse("DE3C76DF-B629-4CE4-AAD1-610F27724385"), ZoneName = "to delete"},
                new Zone { Id = Guid.Parse("DE1CB006-BA04-4A2A-BEAB-8E97BD7F461A"), ZoneName = "to delete"}
            };

        }

        [TestCase("DD1CB006-BA04-4A2A-BEAB-8E97BD7F461A")]
        [TestCase("F33C76DF-B629-4CE4-AAD1-610F27724385")]
        [TestCase("B5688410-59A7-423A-9B62-C14F4DF002DC")]
        public async Task ShouldSucceedToGetZoneById(string id)
        {
            //Arrange
            _zoneRepository.Setup(z => z.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(() => zones.Where(z => z.Id == Guid.Parse(id)).FirstOrDefault());

            //Act
            var result = await _zoneService.GetByIdAsync(Guid.Parse(id));

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(Guid.Parse(id), Is.EqualTo(result.Id));
        }

        [TestCase("DD1CB006-BA04-4A2A-BEAB-8E97BD7F461A")]
        [TestCase("F33C76DF-B629-4CE4-AAD1-610F27724385")]
        [TestCase("B5688410-59A7-423A-9B62-C14F4DF002DC")]
        public async Task ShouldFailToGetZoneById(string id)
        {
            //Arrange
            _zoneRepository.Setup(z => z.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(() => default);

            //Act
            var result = await _zoneService.GetByIdAsync(Guid.Parse(id));

            //Assert
            _zoneRepository.Verify(z => z.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
            Assert.That(result, Is.Null);
        }

        [TestCase(true)]
        [TestCase(true)]
        [TestCase(true)]
        public async Task ShouldSucceedToGetAllZones(bool value)
        {
            // Arrange
            _zoneRepository.Setup(z => z.GetAllAsync()).ReturnsAsync(() => zones);

            //Act
            var result = await _zoneService.GetAllAsync();

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<List<Zone>>());
            Assert.That(result.Count, Is.EqualTo(zones.Count()));
            Assert.That(result, Is.EquivalentTo(zones));
        }

        [TestCase(false)]
        [TestCase(false)]
        [TestCase(false)]
        public async Task ShouldFailToGetAllZones(bool value)
        {
            // Arrange
            _zoneRepository.Setup(z => z.GetAllAsync()).ReturnsAsync(() => default);

            //Act
            var result = await _zoneService.GetAllAsync();

            //Assert
            _zoneRepository.Verify(z => z.GetAllAsync(), Times.Once);
            Assert.That(result, Is.Null);
        }

        [TestCase("zone4")]
        [TestCase("zone5")]
        [TestCase("zone6")]
        public async Task ShouldSucceedToAddZone(string name)
        {
            //Arrange
            _zoneRepository.Setup(z => z.AddAsync(It.IsAny<Zone>()))
                .Callback((Zone zone) =>
                {
                    zones.Add(zone);
                })
                .ReturnsAsync(() => true);

            //Act
            var result = await _zoneService.AddAsync(new CreateZoneDTO
            {
                ZoneName = name
            });

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(zones.Where(z => z.ZoneName == name).FirstOrDefault(), Is.Not.Null);
            Assert.That(result.ZoneName, Is.EqualTo(name));
        }

        [TestCase("zone4")]
        [TestCase("zone5")]
        [TestCase("zone6")]
        public async Task ShouldFailToAddZone(string name)
        {
            //Arrange
            _zoneRepository.Setup(z => z.AddAsync(It.IsAny<Zone>())).ReturnsAsync(() => false);

            //Act
            var result = await _zoneService.AddAsync(new CreateZoneDTO
            {
                ZoneName = name
            });

            //Assert
            _zoneRepository.Verify(z => z.AddAsync(It.IsAny<Zone>()), Times.Once);
            Assert.That(result, Is.Null);
        }

        [TestCase("DD1CB006-BA04-4A2A-BEAB-8E97BD7F461A")]
        [TestCase("F33C76DF-B629-4CE4-AAD1-610F27724385")]
        [TestCase("B5688410-59A7-423A-9B62-C14F4DF002DC")]
        public async Task ShouldSucceedToUpdateZone(string id)
        {
            //Arrange
            _zoneRepository.Setup(z => z.UpdateAsync(It.IsAny<Zone>()))
                .Callback((Zone zone) =>
                {
                    zones = zones
                        .Where(z => z.Id == Guid.Parse(id))
                        .Select(z => { z.ZoneName = "zone edit"; return z; })
                        .ToList();
                })
                .ReturnsAsync(() => true);

            //Act
            var result = await _zoneService.UpdateAsync(zones.Where(z => z.Id == Guid.Parse(id)).FirstOrDefault());

            //Assert
            Assert.That(zones
                            .Where(z => z.ZoneName == "zone edit" && z.Id == Guid.Parse(id))
                            .FirstOrDefault(), Is.Not.Null);
            Assert.That(result, Is.True);
        }

        [TestCase("123CB006-BA04-4A2A-BEAB-8E97BD7F461A")]
        [TestCase("123C76DF-B629-4CE4-AAD1-610F27724385")]
        [TestCase("12388410-59A7-423A-9B62-C14F4DF002DC")]
        public async Task ShouldFailToUpdateZone(string id)
        {

            //Arrange
            _zoneRepository.Setup(z => z.UpdateAsync(It.IsAny<Zone>()))
                .Callback((Zone zone) =>
                {
                    zones = zones
                        .Where(z => z.Id == Guid.Parse(id))
                        .Select(z => { z.ZoneName = "zone edit"; return z; })
                        .ToList();
                })
                .ReturnsAsync(() => false);

            //Act
            var result = await _zoneService.UpdateAsync(zones.Where(z => z.Id == Guid.Parse(id)).FirstOrDefault());

            //Assert
            Assert.That(zones
                            .Where(z => z.ZoneName == "zone edit" && z.Id == Guid.Parse(id))
                            .FirstOrDefault(), Is.Null);
            Assert.That(result, Is.False);
        }

        [TestCase("DE688410-59A7-423A-9B62-C14F4DF002DC")]
        [TestCase("DE3C76DF-B629-4CE4-AAD1-610F27724385")]
        [TestCase("DE1CB006-BA04-4A2A-BEAB-8E97BD7F461A")]
        public async Task ShouldSucceedToDeleteZone(string id)
        {
            //Arrange
            _zoneRepository.Setup(z => z.DeleteAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) =>
                {
                    var zone = zones.FirstOrDefault(z => z.Id == id);
                    if (zone is not null)
                    {
                        zones.Remove(zone);
                        return true;
                    }
                    return false;
                });

            //Act
            var result = await _zoneService.DeleteAsync(Guid.Parse(id));

            //Assert
            Assert.That(result, Is.True);
            Assert.That(zones.FirstOrDefault(z => z.Id == Guid.Parse(id)), Is.Null);
        }

        [TestCase("FA688410-59A7-423A-9B62-C14F4DF002DC")]
        [TestCase("FA3C76DF-B629-4CE4-AAD1-610F27724385")]
        [TestCase("FA1CB006-BA04-4A2A-BEAB-8E97BD7F461A")]
        public async Task ShouldFailToDeleteZone(string id)
        {
            //Arrange
            _zoneRepository.Setup(z => z.DeleteAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) =>
                {
                    var zone = zones.FirstOrDefault(z => z.Id == id);
                    if (zone is not null)
                    {
                        zones.Remove(zone);
                        return true;
                    }
                    return false;
                });

            //Act
            var result = await _zoneService.DeleteAsync(Guid.Parse(id));

            //Assert
            _zoneRepository.Verify(z => z.DeleteAsync(It.IsAny<Guid>()), Times.Once);
            Assert.That(result, Is.False);
            Assert.That(zones.FirstOrDefault(z => z.Id == Guid.Parse(id)), Is.Null);
        }
    }
}