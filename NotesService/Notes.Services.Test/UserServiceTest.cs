using Microsoft.VisualStudio.TestTools.UnitTesting;
using Notes.Services.Implementation;
using Notes.Services.Model;
using System.Threading.Tasks;

namespace Notes.Services.Test
{
    [TestClass]
    public class UserServiceTest
    {
       

        [TestMethod]
        public async Task  ValidateUserEmail1()
        {
            var user = new User() { Email = "" };
            var userService = new UserService(null, null, null);
            string message = "";
            string expectedMessage = string.Format(NotesServiceMessages.REQUIRED, nameof(user.Email));
            try
            {
                await userService.CreateUser(user);
            }
            catch (ValidationException ex)
            {
                message = ex.Message;
            }

            Assert.AreEqual(expectedMessage, message);
        }

        [TestMethod]
        public async Task ValidateUserEmail2()
        {
            var user = new User() { Email = "abcde" };
            var userService = new UserService(null, null, null);
            string message = "";
            string expectedMessage = string.Format(NotesServiceMessages.INVALID, nameof(user.Email));
            try
            {
                await userService.CreateUser(user);
            }
            catch (ValidationException ex)
            {
                message = ex.Message;
            }

            Assert.AreEqual(expectedMessage, message);
        }

        [TestMethod]
        public async Task ValidateUserPassword1()
        {
            var user = new User() { Email = "admin@admin.com" ,Password="" };
            var userService = new UserService(null, null, null);
            string message = "";
            string expectedMessage = string.Format(NotesServiceMessages.REQUIRED, nameof(user.Password));
            try
            {
                await userService.CreateUser(user);
            }
            catch(ValidationException ex)
            {
                message = ex.Message;
            }

            Assert.AreEqual(expectedMessage, message);
        }

        [TestMethod]
        public async Task ValidateUserPassword2()
        {
            var user = new User() { Email = "admin@admin.com", Password = "pqr" };
            var userService = new UserService(null, null, null);
            string message = "";
            string expectedMessage = string.Format(NotesServiceMessages.MIN_LENGTH, nameof(user.Password),8);
            try
            {
                await userService.CreateUser(user);
            }
            catch (ValidationException ex)
            {
                message = ex.Message;
            }

            Assert.AreEqual(expectedMessage, message);
        }



        [TestMethod]
        public async Task ValidateConfirmPassword1()
        {
            var user = new User() { Email = "admin@admin.com", Password = "pqrpqr123"  };
            var userService = new UserService(null, null, null);
            string message = "";
            string expectedMessage = string.Format(NotesServiceMessages.REQUIRED, nameof(user.ConfirmPassword));
            try
            {
                await userService.CreateUser(user);
            }
            catch (ValidationException ex)
            {
                message = ex.Message;
            }

            Assert.AreEqual(expectedMessage, message);
        }

        [TestMethod]
        public async Task ValidatePasswordConfirmPassword1()
        {
            var user = new User() { Email = "admin@admin.com", Password = "pqrpqr123" ,ConfirmPassword="pqrpqr456" };
            var userService = new UserService(null, null, null);
            string message = "";
            string expectedMessage = string.Format(NotesServiceMessages.MATCH,nameof(user.Password), nameof(user.ConfirmPassword));
            try
            {
                await userService.CreateUser(user);
            }
            catch (ValidationException ex)
            {
                message = ex.Message;
            }

            Assert.AreEqual(expectedMessage, message);
        }

        //[TestMethod] Currently disabled as Moq setup not working for throwing specific exception
        //public async Task CanCreateOrUpdateUserThatIsAlreadyPresentInIdpButNotPresentInDb()
        //{
        //    var user = new User() { Email = "admin@admin.com", Password = "pqrpqr123", ConfirmPassword = "pqrpqr123", FirstName = "User1", LastName = "LastName1", IsAdmin = false,Active=true };

        //    var userDao = new UserDao();
        //    var provider = new InMemoiyDbProvider("notes");
        //    bool validationExceptionThrown = false;
        //    // Mocking Idp Service
        //    var idpService = new Mock<IIdpService>();
        //    idpService.Setup(x => x.CreateUser(It.IsAny<User>()).Result).Throws<ValidationException>();   
        //    var userService = new UserService(userDao,idpService.Object, provider);
        //    string message = "";
        //    string expectedMessage = NotesServiceMessages.USER_ALREADY_EXISTS;
        //    try
        //    {
        //        await userService.CreateUser(user);
        //    }
        //    catch (ValidationException ex)
        //    {
        //        validationExceptionThrown = true;
        //    }
        //    // Check If User is created or updated in Db
        //    using (var context = new NotesDbContext(provider))
        //    {
        //        var dao = new UserDao();
        //        dao.Context = context;
        //        var userInfo = await dao.Query(user => user.Email == user.Email);

        //        Assert.AreEqual(user.Email, userInfo.Email);
        //    }

        //    Assert.IsTrue(validationExceptionThrown);


        //    // Clean Up Code
        //    using (var context = new NotesDbContext(provider))
        //    {
        //        await context.ClearDb();
        //    }
        //}
    }
}
