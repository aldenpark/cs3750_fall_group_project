using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Assert.AreEqual(true, true);
        }
        
        [TestMethod]
        public void TestAddUser()
        {
            Group_Project.Models.User user = new Group_Project.Models.User();
            user.Email = "erck@email.com";
            user.Passwrd = "password";
            user.FirstName = "Aric";
            user.LastName = "Campbell";
            user.Birthdate = DateTime.Now;

            using (var db = new Group_Project.Data.ApplicationDbContext(Group_Project.Data.Utility.TestDbContextOptions()))
            {
                Group_Project.Controllers.UsersController usersController = new Group_Project.Controllers.UsersController(db);
                Group_Project.Data.Repository.UnitOfWork unitOfWork = new Group_Project.Data.Repository.UnitOfWork(db);

                var result = usersController.PostUser(user);
                var userId = result.Id;

                var obj = unitOfWork.User.GetFirstorDefault(u => u.ID == userId);

                Assert.IsTrue(obj != null);
                Assert.IsTrue(obj.ID == userId);
                user.ID = userId;
                Assert.IsTrue(obj == user);

            }
        }

        [TestMethod]
        public async Task TestDeleteUser()
        {
            Group_Project.Models.User user = new Group_Project.Models.User();
            user.Email = "erck@email.com";
            user.Passwrd = "password";
            user.FirstName = "Aric";
            user.LastName = "Campbell";
            user.Birthdate = DateTime.Now;

            using (var db = new Group_Project.Data.ApplicationDbContext(Group_Project.Data.Utility.TestDbContextOptions()))
            {
                Group_Project.Controllers.UsersController usersController = new Group_Project.Controllers.UsersController(db);
                Group_Project.Data.Repository.UnitOfWork unitOfWork = new Group_Project.Data.Repository.UnitOfWork(db);

                var result = usersController.PostUser(user);
                var userId = result.Id;

                var obj = unitOfWork.User.GetFirstorDefault(u => u.ID == userId);

                Assert.IsNotNull(obj);
                Assert.IsTrue(obj.ID == userId);
                user.ID = userId;
                Assert.IsTrue(obj == user);

                await usersController.DeleteUser(userId);

                var obj2 = unitOfWork.User.GetFirstorDefault(u => u.ID == userId);

                Assert.IsNull(obj2);

            }
        }






    }
}
