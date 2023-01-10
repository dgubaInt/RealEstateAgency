using RealEstateAgency.Core.Entities;
using RealEstateAgency.Core.Interfaces;
using RealEstateAgency.Service.RoleService;

namespace RealEstateAgency.UnitTests
{
    public class RoleServiceUnitTests
    {
        private IRoleService _roleService;
        private Mock<IGenericRepository<Role>> _roleRepository;
        private Mock<IGenericRepository<UserRole>> _userRoleRepository;
        List<Role> roles;
        List<UserRole> userRoles;

        [SetUp]
        public void Setup()
        {
            _roleRepository = new Mock<IGenericRepository<Role>>();
            _userRoleRepository = new Mock<IGenericRepository<UserRole>>();
            _roleService = new RoleService(_roleRepository.Object, _userRoleRepository.Object);
            roles = new List<Role>
            {
                new Role { Id = Guid.Parse("DD1CB006-BA04-4A2A-BEAB-8E97BD7F461A"), Name = "role1"},
                new Role { Id = Guid.Parse("F33C76DF-B629-4CE4-AAD1-610F27724385"), Name = "role2"},
                new Role { Id = Guid.Parse("B5688410-59A7-423A-9B62-C14F4DF002DC"), Name = "role3"},
                new Role { Id = Guid.Parse("DE688410-59A7-423A-9B62-C14F4DF002DC"), Name = "to delete"},
                new Role { Id = Guid.Parse("DE3C76DF-B629-4CE4-AAD1-610F27724385"), Name = "to delete"},
                new Role { Id = Guid.Parse("DE1CB006-BA04-4A2A-BEAB-8E97BD7F461A"), Name = "to delete"}
            };
            userRoles = new List<UserRole>
            {
                new UserRole { RoleId = Guid.Parse("111CB006-BA04-4A2A-BEAB-8E97BD7F461A"), UserId = Guid.Parse("11188410-59A7-423A-9B62-C14F4DF002DC") },
                new UserRole { RoleId = Guid.Parse("222CB006-BA04-4A2A-BEAB-8E97BD7F461A"), UserId = Guid.Parse("22288410-59A7-423A-9B62-C14F4DF002DC") },
                new UserRole { RoleId = Guid.Parse("333CB006-BA04-4A2A-BEAB-8E97BD7F461A"), UserId = Guid.Parse("33388410-59A7-423A-9B62-C14F4DF002DC") }

            };

        }

        [TestCase("DD1CB006-BA04-4A2A-BEAB-8E97BD7F461A")]
        [TestCase("F33C76DF-B629-4CE4-AAD1-610F27724385")]
        [TestCase("B5688410-59A7-423A-9B62-C14F4DF002DC")]
        public async Task ShouldSucceedToGetRoleById(string id)
        {
            //Arrange
            _roleRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(() => roles.Where(r => r.Id == Guid.Parse(id)).FirstOrDefault());

            //Act
            var result = await _roleService.GetByIdAsync(Guid.Parse(id));

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(Guid.Parse(id), Is.EqualTo(result.Id));
        }

        [TestCase("DD1CB006-BA04-4A2A-BEAB-8E97BD7F461A")]
        [TestCase("F33C76DF-B629-4CE4-AAD1-610F27724385")]
        [TestCase("B5688410-59A7-423A-9B62-C14F4DF002DC")]
        public async Task ShouldFailToGetRoleById(string id)
        {
            //Arrange
            _roleRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(() => default);

            //Act
            var result = await _roleService.GetByIdAsync(Guid.Parse(id));

            //Assert
            _roleRepository.Verify(r => r.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
            Assert.That(result, Is.Null);
        }

        [TestCase(true)]
        [TestCase(true)]
        [TestCase(true)]
        public async Task ShouldSucceedToGetAllRoles(bool value)
        {
            // Arrange
            _roleRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(() => roles);

            //Act
            var result = await _roleService.GetAllAsync();

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<List<Role>>());
            Assert.That(result.Count, Is.EqualTo(roles.Count()));
            Assert.That(result, Is.EquivalentTo(roles));
        }

        [TestCase(false)]
        [TestCase(false)]
        [TestCase(false)]
        public async Task ShouldFailToGetAllRoles(bool value)
        {
            // Arrange
            _roleRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(() => default);

            //Act
            var result = await _roleService.GetAllAsync();

            //Assert
            _roleRepository.Verify(r => r.GetAllAsync(), Times.Once);
            Assert.That(result, Is.Null);
        }

        [TestCase("role4")]
        [TestCase("role5")]
        [TestCase("role6")]
        public async Task ShouldSucceedToAddRole(string name)
        {
            //Arrange
            _roleRepository.Setup(r => r.AddAsync(It.IsAny<Role>()))
                .Callback((Role role) =>
                {
                    roles.Add(role);
                })
                .ReturnsAsync(() => true);

            //Act
            var initialListCount = roles.Count;
            var result = await _roleService.AddAsync(new Role
            {
                Id = Guid.NewGuid(),
                Name = name
            });

            //Assert
            Assert.That(result, Is.True);
            Assert.That(roles.Count, Is.EqualTo(initialListCount + 1));
        }

        [TestCase("role4")]
        [TestCase("role5")]
        [TestCase("role6")]
        public async Task ShouldFailToAddRole(string name)
        {
            //Arrange
            _roleRepository.Setup(r => r.AddAsync(It.IsAny<Role>()))
                .ReturnsAsync(() => false);

            //Act
            var initialListCount = roles.Count;
            var result = await _roleService.AddAsync(new Role
            {
                Id = Guid.NewGuid(),
                Name = name
            });

            //Assert
            Assert.That(result, Is.False);
            Assert.That(roles.Count, Is.Not.EqualTo(initialListCount + 1));
        }

        [TestCase("DD1CB006-BA04-4A2A-BEAB-8E97BD7F461A")]
        [TestCase("F33C76DF-B629-4CE4-AAD1-610F27724385")]
        [TestCase("B5688410-59A7-423A-9B62-C14F4DF002DC")]
        public async Task ShouldSucceedToUpdateRole(string id)
        {
            //Arrange
            _roleRepository.Setup(r => r.UpdateAsync(It.IsAny<Role>()))
                .Callback((Role role) =>
                {
                    roles = roles
                        .Where(r => r.Id == Guid.Parse(id))
                        .Select(r => { r.Name = "role edit"; return r; })
                        .ToList();
                })
                .ReturnsAsync(() => true);

            //Act
            var result = await _roleService.UpdateAsync(roles.Where(r => r.Id == Guid.Parse(id)).FirstOrDefault());

            //Assert
            Assert.That(roles
                            .Where(r => r.Name == "role edit" && r.Id == Guid.Parse(id))
                            .FirstOrDefault(), Is.Not.Null);
            Assert.That(result, Is.True);
        }

        [TestCase("123CB006-BA04-4A2A-BEAB-8E97BD7F461A")]
        [TestCase("123C76DF-B629-4CE4-AAD1-610F27724385")]
        [TestCase("12388410-59A7-423A-9B62-C14F4DF002DC")]
        public async Task ShouldFailToUpdateRole(string id)
        {

            //Arrange
            _roleRepository.Setup(r => r.UpdateAsync(It.IsAny<Role>()))
                .Callback((Role role) =>
                {
                    roles = roles
                        .Where(r => r.Id == Guid.Parse(id))
                        .Select(r => { r.Name = "role edit"; return r; })
                        .ToList();
                })
                .ReturnsAsync(() => false);

            //Act
            var result = await _roleService.UpdateAsync(roles.Where(r => r.Id == Guid.Parse(id)).FirstOrDefault());

            //Assert
            Assert.That(roles
                            .Where(r => r.Name == "role edit" && r.Id == Guid.Parse(id))
                            .FirstOrDefault(), Is.Null);
            Assert.That(result, Is.False);
        }

        [TestCase("ADDCB006-BA04-4A2A-BEAB-8E97BD7F461A", "11188410-59A7-423A-9B62-C14F4DF002DC")]
        [TestCase("ADDCB006-BA04-4A2A-BEAB-8E97BD7F461A", "22288410-59A7-423A-9B62-C14F4DF002DC")]
        [TestCase("ADDCB006-BA04-4A2A-BEAB-8E97BD7F461A", "33388410-59A7-423A-9B62-C14F4DF002DC")]
        public async Task ShouldSucceedToAddRoleToUser(string roleId, string userId)
        {
            //Arrange
            _userRoleRepository.Setup(ur => ur.AddAsync(It.IsAny<UserRole>()))
                .ReturnsAsync(() =>
                {
                    if (userRoles.FirstOrDefault(ur => ur.RoleId == Guid.Parse(roleId) && ur.UserId == Guid.Parse(userId)) is null)
                    {
                        userRoles.Add(new UserRole { RoleId = Guid.Parse(roleId), UserId = Guid.Parse(userId) });
                        return true;
                    }
                    return false;
                });

            //Act
            var result = await _roleService.AddRoleAsync(Guid.Parse(roleId), Guid.Parse(userId));

            //Assert
            Assert.That(result, Is.True);
            Assert.That(userRoles.FirstOrDefault(ur => ur.RoleId == Guid.Parse(roleId) && ur.UserId == Guid.Parse(userId)), Is.Not.Null);
        }

        [TestCase("111CB006-BA04-4A2A-BEAB-8E97BD7F461A", "11188410-59A7-423A-9B62-C14F4DF002DC")]
        [TestCase("222CB006-BA04-4A2A-BEAB-8E97BD7F461A", "22288410-59A7-423A-9B62-C14F4DF002DC")]
        [TestCase("333CB006-BA04-4A2A-BEAB-8E97BD7F461A", "33388410-59A7-423A-9B62-C14F4DF002DC")]
        public async Task ShouldFailToAddRoleToUser(string roleId, string userId)
        {
            //Arrange
            _userRoleRepository.Setup(ur => ur.AddAsync(It.IsAny<UserRole>()))
                .ReturnsAsync(() =>
                {
                    if (userRoles.FirstOrDefault(ur => ur.RoleId == Guid.Parse(roleId) && ur.UserId == Guid.Parse(userId)) is null)
                    {
                        userRoles.Add(new UserRole { RoleId = Guid.Parse(roleId), UserId = Guid.Parse(userId) });
                        return true;
                    }
                    return false;
                });

            //Act
            var result = await _roleService.AddRoleAsync(Guid.Parse(roleId), Guid.Parse(userId));

            //Assert
            _userRoleRepository.Verify(ur => ur.AddAsync(It.IsAny<UserRole>()), Times.Once);
            Assert.That(result, Is.False);
            Assert.That(userRoles.Where(ur => ur.RoleId == Guid.Parse(roleId) && ur.UserId == Guid.Parse(userId)).Count() <= 1, Is.True);
        }

        [TestCase("111CB006-BA04-4A2A-BEAB-8E97BD7F461A", "11188410-59A7-423A-9B62-C14F4DF002DC")]
        [TestCase("222CB006-BA04-4A2A-BEAB-8E97BD7F461A", "22288410-59A7-423A-9B62-C14F4DF002DC")]
        [TestCase("333CB006-BA04-4A2A-BEAB-8E97BD7F461A", "33388410-59A7-423A-9B62-C14F4DF002DC")]
        public async Task ShouldSucceedToRemoveRoleFromUser(string roleId, string userId)
        {
            //Arrange
            _userRoleRepository.Setup(ur => ur.DeleteAsync(It.IsAny<UserRole>()))
                .ReturnsAsync((UserRole userRole) =>
                {
                    var existingUserRole = userRoles.FirstOrDefault(ur => ur.RoleId == userRole.RoleId && ur.UserId == userRole.UserId);
                    if (existingUserRole is not null)
                    {
                        userRoles.Remove(existingUserRole);
                        return true;
                    }
                    return false;
                });

            //Act
            var result = await _roleService.RemoveRoleAsync(new UserRole { RoleId = Guid.Parse(roleId), UserId = Guid.Parse(userId) });

            //Assert
            Assert.That(result, Is.True);
            Assert.That(userRoles.FirstOrDefault(ur => ur.RoleId == Guid.Parse(roleId) && ur.UserId == Guid.Parse(userId)), Is.Null);
        }

        [TestCase("FA1CB006-BA04-4A2A-BEAB-8E97BD7F461A", "11188410-59A7-423A-9B62-C14F4DF002DC")]
        [TestCase("FA2CB006-BA04-4A2A-BEAB-8E97BD7F461A", "22288410-59A7-423A-9B62-C14F4DF002DC")]
        [TestCase("FA3CB006-BA04-4A2A-BEAB-8E97BD7F461A", "33388410-59A7-423A-9B62-C14F4DF002DC")]
        public async Task ShouldFailToRemoveRoleFromUser(string roleId, string userId)
        {
            //Arrange
            _userRoleRepository.Setup(ur => ur.DeleteAsync(It.IsAny<UserRole>()))
                .ReturnsAsync((UserRole userRole) =>
                {
                    var existingUserRole = userRoles.FirstOrDefault(ur => ur.RoleId == userRole.RoleId && ur.UserId == userRole.UserId);
                    if (existingUserRole is not null)
                    {
                        userRoles.Remove(existingUserRole);
                        return true;
                    }
                    return false;
                });

            //Act
            var result = await _roleService.RemoveRoleAsync(new UserRole { RoleId = Guid.Parse(roleId), UserId = Guid.Parse(userId) });

            //Assert
            Assert.That(result, Is.False);
            Assert.That(userRoles.FirstOrDefault(ur => ur.RoleId == Guid.Parse(roleId) && ur.UserId == Guid.Parse(userId)), Is.Null);
        }

        [TestCase("DD1CB006-BA04-4A2A-BEAB-8E97BD7F461A", true)]
        [TestCase("F33C76DF-B629-4CE4-AAD1-610F27724385", true)]
        [TestCase("B5688410-59A7-423A-9B62-C14F4DF002DC", false)]
        public async Task ShouldSucceedToSetRoles(string roleId, bool toSet)
        {
            //Arrange
            var user = new AgentUser { Id = Guid.Parse("BBB88410-59A7-423A-9B62-C14F4DF00BBB"), UserName = "userTest" };
            var rolesToSet = new Dictionary<Guid, bool>();
            rolesToSet.Add(Guid.Parse(roleId), toSet);

            _userRoleRepository.Setup(ur => ur.AddAsync(It.IsAny<UserRole>()))
                .Callback((UserRole userRole) =>
                {
                    var existingRole = roles.FirstOrDefault(r => r.Id == Guid.Parse(roleId));
                    if (existingRole is not null && toSet)
                    {
                        userRoles.Add(new UserRole { RoleId = existingRole.Id, UserId = user.Id });
                    }
                });

            //Act
            await _roleService.SetRolesAsync(user, rolesToSet);

            //Assert
            Assert.That(roles.Select(r => r.Id).ToList().Contains(Guid.Parse(roleId)), Is.True);
            if (toSet)
                Assert.That(userRoles.FirstOrDefault(ur => ur.UserId == user.Id && ur.RoleId == Guid.Parse(roleId)), Is.Not.Null);
            else
                Assert.That(userRoles.FirstOrDefault(ur => ur.UserId == user.Id && ur.RoleId == Guid.Parse(roleId)), Is.Null);
        }

        [TestCase("DDDCB006-BA04-4A2A-BEAB-8E97BD7F461A", true)]
        [TestCase("DDDC76DF-B629-4CE4-AAD1-610F27724385", true)]
        [TestCase("DDD88410-59A7-423A-9B62-C14F4DF002DC", false)]
        public async Task ShouldFailToSetRoles(string roleId, bool toSet)
        {
            //Arrange
            var user = new AgentUser { Id = Guid.Parse("BBB88410-59A7-423A-9B62-C14F4DF00BBB"), UserName = "userTest" };
            var rolesToSet = new Dictionary<Guid, bool>();
            rolesToSet.Add(Guid.Parse(roleId), toSet);

            _userRoleRepository.Setup(ur => ur.AddAsync(It.IsAny<UserRole>()))
                .Callback((UserRole userRole) =>
                {
                    var existingRole = roles.FirstOrDefault(r => r.Id == Guid.Parse(roleId));
                    if (existingRole is not null && toSet)
                    {
                        userRoles.Add(new UserRole { RoleId = existingRole.Id, UserId = user.Id });
                    }
                });

            //Act
            await _roleService.SetRolesAsync(user, rolesToSet);

            //Assert
            Assert.That(roles.Select(r => r.Id).ToList().Contains(Guid.Parse(roleId)), Is.False);
            Assert.That(userRoles.FirstOrDefault(ur => ur.UserId == user.Id && ur.RoleId == Guid.Parse(roleId)), Is.Null);
        }
    }
}