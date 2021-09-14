using Microsoft.VisualStudio.TestTools.UnitTesting;
using Notes.Db.Implementation;
using Notes.Db.Model;
using System.Threading.Tasks;

namespace Notes.Db.Test
{
    [TestClass]
    public class UserDaoTest
    {
        private const string DbName = "TestDb";

        private User CreateLocalUser(string suffix)
        {
            var user = new Model.User()
            {
                FirstName = $"Test{suffix}",
                LastName = $"User{suffix}",
                ExternalUserId = $"Test{suffix}.User@Ttest.com",
                Email = $"Test{suffix}.User@Ttest.com",
                IsAdmin = true,
                Active = true
            };

            return user;
        }

        [TestMethod]
        public async Task CanCreateAndGetUser()
        {
            var provider = new InMemoiyDbProvider(DbName);
                
            int id = 0;

            //Create User 
            using (var context = new NotesDbContext(provider))
            {
                var dao = new UserDao();
                dao.Context = context;
                id = await dao.CreateUser(CreateLocalUser("First"));
            }
            Assert.IsTrue(id > 0);
            User user;
            using (var context = new NotesDbContext(provider))
            {
                var dao = new UserDao();
                dao.Context = context;
                user = await dao.GetUser(id);
            }
            Assert.IsNotNull(user);
            Assert.AreEqual("TestFirst", user.FirstName);
            // Clean Up Code
            using (var context = new NotesDbContext(provider))
            {
                await context.ClearDb();
            }
              
            
            
        }

        [TestMethod]
        public async Task CanGetUserPaged()
        {

            var provider = new InMemoiyDbProvider(DbName);


            int id = 0;

            //Create User data
            using (var context = new NotesDbContext(provider))
            {
                var dao = new UserDao();
                dao.Context = context;
                for (int i = 0; i < 16; i++)
                {
                    id = await dao.CreateUser(CreateLocalUser((i + 1).ToString()));
                }

            }
            PagedModel<User> pagedResult;
            using (var context = new NotesDbContext(provider))
            {

                var dao = new UserDao();
                dao.Context = context;
                pagedResult = await dao.GetAllUsers(2, 10);

            }

            Assert.IsNotNull(pagedResult);
            Assert.AreEqual(pagedResult.TotalRecords, 16);
            Assert.AreEqual(pagedResult.TotalPages, 2);
            Assert.AreEqual(pagedResult.PageNumber, 2);
            Assert.AreEqual(pagedResult.Result.Count, 6);

            // Clean Up Code
            using (var context = new NotesDbContext(provider))
            {
                await context.ClearDb();
            }
        }


        [TestMethod]
        public async Task ReturnFirstPageIfPageNumberIsMoreThanTotalPages()
        {

            var provider = new InMemoiyDbProvider(DbName);


            int id = 0;

            //Create User data
            using (var context = new NotesDbContext(provider))
            {
                var dao = new UserDao();
                dao.Context = context;
                for (int i = 0; i < 16; i++)
                {
                    id = await dao.CreateUser(CreateLocalUser((i + 1).ToString()));
                }

            }
            PagedModel<User> pagedResult;
            using (var context = new NotesDbContext(provider))
            {

                var dao = new UserDao();
                dao.Context = context;
                pagedResult = await dao.GetAllUsers(7, 10);

            }

            Assert.IsNotNull(pagedResult);
            Assert.AreEqual(pagedResult.TotalRecords, 16);
            Assert.AreEqual(pagedResult.TotalPages, 2);
            Assert.AreEqual(pagedResult.PageNumber, 1);
            Assert.AreEqual(pagedResult.Result.Count, 10);

            // Clean Up Code
            using (var context = new NotesDbContext(provider))
            {
                await context.ClearDb();
            }
        }



        [TestMethod]
        public async Task ReturnFirstPageIfPageNumberIfPageNumberIsZero()
        {

            var provider = new InMemoiyDbProvider(DbName);


            int id = 0;

            //Create User data
            using (var context = new NotesDbContext(provider))
            {
                var dao = new UserDao();
                dao.Context = context;
                for (int i = 0; i < 16; i++)
                {
                    id = await dao.CreateUser(CreateLocalUser((i + 1).ToString()));
                }

            }
            PagedModel<User> pagedResult;
            using (var context = new NotesDbContext(provider))
            {

                var dao = new UserDao();
                dao.Context = context;
                pagedResult = await dao.GetAllUsers(0, 10);

            }

            Assert.IsNotNull(pagedResult);
            Assert.AreEqual(pagedResult.TotalRecords, 16);
            Assert.AreEqual(pagedResult.TotalPages, 2);
            Assert.AreEqual(pagedResult.PageNumber, 1);
            Assert.AreEqual(pagedResult.Result.Count, 10);

            // Clean Up Code
            using (var context = new NotesDbContext(provider))
            {
                await context.ClearDb();
            }
        }

        [TestMethod]
        public  async Task  CanQueryUserBasedUponUserEmail()
        {
            var localUser = CreateLocalUser("External");
            var provider = new InMemoiyDbProvider(DbName);
            var userEmail = localUser.Email;
            //Create User 
            using (var context = new NotesDbContext(provider))
            {
                var dao = new UserDao();
                dao.Context = context;
                var id = await dao.CreateUser(localUser);
            }

            User userInfo;

            using (var context = new NotesDbContext(provider))
            {
                var dao = new UserDao();
                dao.Context = context;
                userInfo = await dao.QueryUser(user=>user.Email == userEmail);
            }

            Assert.IsNotNull(userInfo);
            Assert.AreEqual(userEmail, userInfo.Email);

            // Clean Up Code
            using (var context = new NotesDbContext(provider))
            {
                await context.ClearDb();
            }
        }
    }
}
