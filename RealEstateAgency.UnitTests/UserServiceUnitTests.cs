using RealEstateAgency.Core.Entities;
using RealEstateAgency.Core.Interfaces;
using RealEstateAgency.Service.UserService;

namespace RealEstateAgency.UnitTests
{
    public class UserServiceUnitTests
    {
        private IUserService _userService;
        private Mock<IGenericRepository<AgentUser>> _userRepository;
        List<AgentUser> users;

        [SetUp]
        public void Setup()
        {
            _userRepository = new Mock<IGenericRepository<AgentUser>>();
            _userService = new UserService(_userRepository.Object);
            users = new List<AgentUser>
            {
                new AgentUser { Id = Guid.Parse("DD1CB006-BA04-4A2A-BEAB-8E97BD7F461A"), UserName = "user1", LockoutEnd = null, LockoutEnabled = false },
                new AgentUser { Id = Guid.Parse("F33C76DF-B629-4CE4-AAD1-610F27724385"), UserName = "user2", LockoutEnd = null, LockoutEnabled = false },
                new AgentUser { Id = Guid.Parse("B5688410-59A7-423A-9B62-C14F4DF002DC"), UserName = "user3", LockoutEnd = null, LockoutEnabled = false },
                new AgentUser { Id = Guid.Parse("DE688410-59A7-423A-9B62-C14F4DF002DC"), UserName = "to lockout", LockoutEnd = null, LockoutEnabled = false },
                new AgentUser { Id = Guid.Parse("DE3C76DF-B629-4CE4-AAD1-610F27724385"), UserName = "to lockout", LockoutEnd = null, LockoutEnabled = false },
                new AgentUser { Id = Guid.Parse("DE1CB006-BA04-4A2A-BEAB-8E97BD7F461A"), UserName = "to lockout", LockoutEnd = null, LockoutEnabled = false }
            };
        }

        [TestCase("DD1CB006-BA04-4A2A-BEAB-8E97BD7F461A")]
        [TestCase("F33C76DF-B629-4CE4-AAD1-610F27724385")]
        [TestCase("B5688410-59A7-423A-9B62-C14F4DF002DC")]
        public async Task ShouldSucceedToGetAgentUserById(string id)
        {
            //Arrange
            _userRepository.Setup(u => u.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(() => users.Where(u => u.Id == Guid.Parse(id)).FirstOrDefault());

            //Act
            var result = await _userService.GetByIdAsync(Guid.Parse(id));

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(Guid.Parse(id), Is.EqualTo(result.Id));
        }

        [TestCase("DD1CB006-BA04-4A2A-BEAB-8E97BD7F461A")]
        [TestCase("F33C76DF-B629-4CE4-AAD1-610F27724385")]
        [TestCase("B5688410-59A7-423A-9B62-C14F4DF002DC")]
        public async Task ShouldFailToGetAgentUserById(string id)
        {
            //Arrange
            _userRepository.Setup(u => u.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(() => default);

            //Act
            var result = await _userService.GetByIdAsync(Guid.Parse(id));

            //Assert
            _userRepository.Verify(u => u.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
            Assert.That(result, Is.Null);
        }

        [TestCase(true)]
        [TestCase(true)]
        [TestCase(true)]
        public async Task ShouldSucceedToGetAllAgentUsers(bool value)
        {
            // Arrange
            _userRepository.Setup(u => u.GetAllAsync()).ReturnsAsync(() => users);

            //Act
            var result = await _userService.GetAllAsync();

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<List<AgentUser>>());
            Assert.That(result.Count, Is.EqualTo(users.Count()));
            Assert.That(result, Is.EquivalentTo(users));
        }

        [TestCase(false)]
        [TestCase(false)]
        [TestCase(false)]
        public async Task ShouldFailToGetAllAgentUsers(bool value)
        {
            // Arrange
            _userRepository.Setup(u => u.GetAllAsync()).ReturnsAsync(() => default);

            //Act
            var result = await _userService.GetAllAsync();

            //Assert
            _userRepository.Verify(u => u.GetAllAsync(), Times.Once);
            Assert.That(result, Is.Null);
        }

        [TestCase("user4")]
        [TestCase("user5")]
        [TestCase("user6")]
        public async Task ShouldSucceedToAddAgentUser(string name)
        {
            //Arrange
            _userRepository.Setup(u => u.AddAsync(It.IsAny<AgentUser>()))
                .Callback((AgentUser user) =>
                {
                    users.Add(user);
                })
                .ReturnsAsync(() => true);

            //Act
            var initialListCount = users.Count;
            var result = await _userService.AddAsync(new AgentUser
            {
                Id = Guid.NewGuid(),
                UserName = name
            });

            //Assert
            Assert.That(result, Is.True);
            Assert.That(users.Count, Is.EqualTo(initialListCount + 1));
        }

        [TestCase("user4")]
        [TestCase("user5")]
        [TestCase("user6")]
        public async Task ShouldFailToAddAgentUser(string name)
        {
            //Arrange
            _userRepository.Setup(u => u.AddAsync(It.IsAny<AgentUser>()))
                .ReturnsAsync(() => false);

            //Act
            var initialListCount = users.Count;
            var result = await _userService.AddAsync(new AgentUser
            {
                Id = Guid.NewGuid(),
                UserName = name
            });

            //Assert
            Assert.That(result, Is.False);
            Assert.That(users.Count, Is.Not.EqualTo(initialListCount + 1));
        }

        [TestCase("DD1CB006-BA04-4A2A-BEAB-8E97BD7F461A")]
        [TestCase("F33C76DF-B629-4CE4-AAD1-610F27724385")]
        [TestCase("B5688410-59A7-423A-9B62-C14F4DF002DC")]
        public async Task ShouldSucceedToUpdateAgentUser(string id)
        {
            //Arrange
            _userRepository.Setup(u => u.UpdateAsync(It.IsAny<AgentUser>()))
                .Callback((AgentUser user) =>
                {
                    users = users
                        .Where(u => u.Id == Guid.Parse(id))
                        .Select(u => { u.UserName = "user edit"; return u; })
                        .ToList();
                })
                .ReturnsAsync(() => true);

            //Act
            var result = await _userService.UpdateAsync(users.Where(r => r.Id == Guid.Parse(id)).FirstOrDefault());

            //Assert
            Assert.That(users
                            .Where(u => u.UserName == "user edit" && u.Id == Guid.Parse(id))
                            .FirstOrDefault(), Is.Not.Null);
            Assert.That(result, Is.True);
        }

        [TestCase("123CB006-BA04-4A2A-BEAB-8E97BD7F461A")]
        [TestCase("123C76DF-B629-4CE4-AAD1-610F27724385")]
        [TestCase("12388410-59A7-423A-9B62-C14F4DF002DC")]
        public async Task ShouldFailToUpdateAgentUser(string id)
        {

            //Arrange
            _userRepository.Setup(u => u.UpdateAsync(It.IsAny<AgentUser>()))
                .Callback((AgentUser user) =>
                {
                    users = users
                        .Where(u => u.Id == Guid.Parse(id))
                        .Select(u => { u.UserName = "user edit"; return u; })
                        .ToList();
                })
                .ReturnsAsync(() => false);

            //Act
            var result = await _userService.UpdateAsync(users.Where(r => r.Id == Guid.Parse(id)).FirstOrDefault());

            //Assert
            Assert.That(users
                            .Where(u => u.UserName == "user edit" && u.Id == Guid.Parse(id))
                            .FirstOrDefault(), Is.Null);
            Assert.That(result, Is.False);
        }

        [TestCase("DE688410-59A7-423A-9B62-C14F4DF002DC")]
        [TestCase("DE3C76DF-B629-4CE4-AAD1-610F27724385")]
        [TestCase("DE1CB006-BA04-4A2A-BEAB-8E97BD7F461A")]
        public async Task ShouldSucceedToLockoutAgentUser(string id)
        {
            //Arrange
            var user = users.Find(u => u.Id == Guid.Parse(id));
            _userRepository.Setup(u => u.UpdateAsync(It.IsAny<AgentUser>()))
                .ReturnsAsync(() =>
                {
                    if (user is not null)
                    {
                        users = users
                        .Where(u => u.Id == Guid.Parse(id))
                        .Select(u => { u.LockoutEnabled = true; u.LockoutEnd = DateTime.Now.AddYears(20); return u; })
                        .ToList();
                        return true;
                    }
                    return false;
                });

            //Act
            var result = await _userService.UpdateAsync(user);

            //Assert
            Assert.That(result, Is.True);
            Assert.That(users.Find(u => u.Id == user.Id)?.LockoutEnabled, Is.True);
            Assert.That(users.Find(u => u.Id == user.Id)?.LockoutEnd, Is.Not.Null);
        }

        [TestCase("FA688410-59A7-423A-9B62-C14F4DF002DC")]
        [TestCase("FA3C76DF-B629-4CE4-AAD1-610F27724385")]
        [TestCase("FA1CB006-BA04-4A2A-BEAB-8E97BD7F461A")]
        public async Task ShouldFailToLockoutAgentUser(string id)
        {
            //Arrange
            var user = users.Find(u => u.Id == Guid.Parse(id));
            _userRepository.Setup(u => u.UpdateAsync(It.IsAny<AgentUser>()))
                .ReturnsAsync(() =>
                {
                    if (user is not null)
                    {
                        users = users
                        .Where(u => u.Id == Guid.Parse(id))
                        .Select(u => { u.LockoutEnabled = true; u.LockoutEnd = DateTime.Now.AddYears(20); return u; })
                        .ToList();
                        return true;
                    }
                    return false;
                });

            //Act
            var result = await _userService.UpdateAsync(user);

            //Assert
            _userRepository.Verify(u => u.UpdateAsync(It.IsAny<AgentUser>()), Times.Once);
            Assert.That(result, Is.False);
            Assert.That(user, Is.Null);
        }

        [TestCase("DE688410-59A7-423A-9B62-C14F4DF002DC")]
        [TestCase("DE3C76DF-B629-4CE4-AAD1-610F27724385")]
        [TestCase("DE1CB006-BA04-4A2A-BEAB-8E97BD7F461A")]
        public async Task ShouldSucceedToRemoveLockoutAgentUser(string id)
        {
            //Arrange
            var user = users.Find(u => u.Id == Guid.Parse(id));
            _userRepository.Setup(u => u.UpdateAsync(It.IsAny<AgentUser>()))
                .ReturnsAsync(() =>
                {
                    if (user is not null)
                    {
                        users = users
                        .Where(u => u.Id == Guid.Parse(id))
                        .Select(u => { u.LockoutEnabled = false; u.LockoutEnd = null; return u; })
                        .ToList();
                        return true;
                    }
                    return false;
                });

            //Act
            var result = await _userService.UpdateAsync(user);

            //Assert
            Assert.That(result, Is.True);
            Assert.That(users.Find(u => u.Id == user.Id)?.LockoutEnabled, Is.False);
            Assert.That(users.Find(u => u.Id == user.Id)?.LockoutEnd, Is.Null);
        }

        [TestCase("FA688410-59A7-423A-9B62-C14F4DF002DC")]
        [TestCase("FA3C76DF-B629-4CE4-AAD1-610F27724385")]
        [TestCase("FA1CB006-BA04-4A2A-BEAB-8E97BD7F461A")]
        public async Task ShouldFailToRemoveLockoutAgentUser(string id)
        {
            //Arrange
            var user = users.Find(u => u.Id == Guid.Parse(id));
            _userRepository.Setup(u => u.UpdateAsync(It.IsAny<AgentUser>()))
                .ReturnsAsync(() =>
                {
                    if (user is not null)
                    {
                        users = users
                        .Where(u => u.Id == Guid.Parse(id))
                        .Select(u => { u.LockoutEnabled = false; u.LockoutEnd = null; return u; })
                        .ToList();
                        return true;
                    }
                    return false;
                });

            //Act
            var result = await _userService.UpdateAsync(user);

            //Assert
            _userRepository.Verify(u => u.UpdateAsync(It.IsAny<AgentUser>()), Times.Once);
            Assert.That(result, Is.False);
            Assert.That(user, Is.Null);
        }

    }
}
