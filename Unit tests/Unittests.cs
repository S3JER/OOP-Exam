using OOP_Eksamen;
using System;
using System.Collections.Generic;
using Xunit;

namespace Unit_tests
{
    public class Unittests
    {

        private IStregsystem CreateStregsystem()
        {
            return new Stregsystem();
        }

        [Theory]
        [InlineData("sadasdas.sdada-sad@sdad.oad.-in.sdad.dk")]
        [InlineData("sadasdas.sdada-sad@dasd...oad.-in.sdad.dk")]
        [InlineData("eksempel(2)@-mit_domain.dk")]
        public void Email_Throws_If_Invalid_Test(string testEmail)
        {
            //Arrange+Act+Assert
            Assert.Throws<EmailNotLegalException>(() => new User("test", "test", "test", testEmail, 0, 0));
        }

        [Theory]
        [InlineData("sadasdas.sdada-sad@domaintest.sdad.dk")]
        [InlineData("eksempel@domain.dk")]
        public void User_Exist_If_Email_Is_Valid_Test(string testEmail)
        {
            //Arrange+Act+Assert
            Assert.IsType<User>(new User("test0", "te", "te", testEmail, 0, 1));
        }

        [Fact]
        public void Check_If_Transaction_Works()
        {
            //Arrange
            IStregsystem stregsytem = CreateStregsystem();
            User user = new User("test1", "te", "te", "eksempel@domain.dk", 50, 1);
            Product product = new Product(1, "Test", 10, true, true);
            decimal userBlance = user.Balance;

            //Act
            stregsytem.BuyProduct(user, product);

            //Assert
            Assert.True(user.Balance < userBlance);
        }

        [Fact]
        public void Check_GetProductByID()
        {
            //Arrange
            IStregsystem stregsytem = CreateStregsystem();
            Product product = new Product(1, "Test", 10, true, true);
            stregsytem.AddProductToList(product);

            //Act
            Product returnedProduct = stregsytem.GetProductByID(1);

            //Assert
            Assert.Equal(product, returnedProduct);
        }

        [Fact]
        public void Check_GetUserByUsername()
        {
            //Arrange
            IStregsystem stregsytem = CreateStregsystem();
            User user = new User("test2", "test", "test", "sadasdas.sdada-sad@domaintest.sdad.dk", 50, 100);
            stregsytem.AddUserToList(user);
            
            //Act
            User returnedUser = stregsytem.GetUserByUsername("test2");

            //Assert
            Assert.Equal(user.Username, returnedUser.Username);
        }
        
        [Fact]
        public void Get_User_Test()
        {
            //Arrange
            User user = new User("test1", "test", "test", "test@domain.test.cool.dk", 100, 1);
            User user2 = new User("test2", "test", "test", "test@doma.intest.com", 50, 2);
            IStregsystem stregsytem = CreateStregsystem();

            //Act
            stregsytem.AddUserToList(user);
            stregsytem.AddUserToList(user2);
            stregsytem.InputUsersFromFile();
            List<User> users = stregsytem.GetUsers((user) => user.Balance < 100);

            //Assert
            Assert.Contains(user2, users);
            Assert.DoesNotContain(user, users);
        }
        [Fact]
        public void Compare_User_Return_False_Test()
        {
            //Arrange
            User user = new User("test1", "test", "test", "test@domaintest.cool.dk", 100, 1);
            User user2 = new User("test2", "test", "test", "test.test@domaintest.com", 50, 2);
            Product product = new Product(1, "Test", 10, true, true);

            //Act+Assert
            Assert.False(user.Equals(user2));
            Assert.False(user.Equals(product));
        }
        [Fact]
        public void Compare_User_Return_True_Test()
        {
            //Arrange
            User user = new User("test1", "test", "test", "test@domaintest.cool.dk", 100, 1);

            //Act+Assert
            Assert.True(user.Equals(user));
        }
        [Fact]
        public void Check_BuyTransaction_Constructor_Test()
        {
            //Arrange
            BuyTransaction buyTransaction = new BuyTransaction(1, new User("test1", "test", "test", "test@domaintest.cool.dk", 100, 1), 1000, new Product(1, "Test", 10, true, true), "2021/12/13 19.26.03");

            //Assert
            Assert.IsType<BuyTransaction>(buyTransaction);
        }
        [Fact]
        public void Check_BuyTransaction_Execution_Test()
        {
            //Arrange
            User user = new User("test", "test", "test", "test@domaintest.cool.dk", 100, 1);
            User user2 = new User("test1", "test", "test", "test@domaintest.cool.dk", 100, 2);
            User user3 = new User("test2", "test", "test", "test@domaintest.cool.dk", 100, 3);
            decimal balance = user.Balance;
            decimal balance3 = user3.Balance;
            BuyTransaction buyTransaction = new BuyTransaction(1, user, 1000, new Product(1, "Test", 10, true, true), "2021/12/13 19.26.03");
            BuyTransaction buyTransaction2 = new BuyTransaction(1, user2, 1000, new Product(1, "Test", 1000, true, false), "2021/12/13 19.26.03");
            BuyTransaction buyTransaction3 = new BuyTransaction(1, user3, 1000, new Product(1, "Test", 1000, true, true), "2021/12/13 19.26.03");
            //Act
            buyTransaction.Execute();
            buyTransaction3.Execute();

            //Assert
            Assert.True(user.Balance < balance);
            Assert.Throws<InsufficientCreditsException>(() => buyTransaction2.Execute());
            Assert.True(user3.Balance < balance3);

        }
        [Fact]
        public void Check_InsertCashTransaction_Constructor_Test()
        {
            //Arrange
            InsertCashTransaction insertCashTransaction = new InsertCashTransaction(1, new User("test1", "test", "test", "test@domaintest.cool.dk", 100, 1), 1000, "2021/12/13 19.26.03");

            //Assert
            Assert.IsType<InsertCashTransaction>(insertCashTransaction);
        }
        [Fact]
        public void Check_InsertCashTransaction_Execution_Test()
        {
            //Arrange
            User user = new User("test1", "test", "test", "test@domaintest.cool.dk", 100, 1);

            decimal balance = user.Balance;
            InsertCashTransaction insertCashTransaction = new InsertCashTransaction(1, user, 1000, "2021/12/13 19.26.03");

            //Act
            insertCashTransaction.Execute();

            //Assert
            Assert.True(user.Balance > balance);
        }
        [Fact]
        public void Userinfo_Exceptions()
        {
            //Arrange+Act+Assert
            Assert.Throws<UsernameNotLegalException>(() => new User(null, "test", "test", "test@domaintest.cool.dk", 0, 1));
            Assert.Throws<ArgumentNullException>(() => new User("test", null, "test", "test@domaintest.cool.dk", 0, 2));
            Assert.Throws<ArgumentNullException>(() => new User("test", "test", null, "test@domaintest.cool.dk", 0, 3));
            Assert.Throws<IdException>(() => new User("test", "test", "test", "test@domaintest.cool.dk", 0, 0)); 
        }

        [Fact]
        public void AddCreditsToAccount_Test()
        {
            //Arrange
            User user = new User("test1", "test", "test", "test@domaintest.cool.dk", 100, 1);
            decimal balance = user.Balance;
            IStregsystem stregsystem = CreateStregsystem();
            decimal amount = 100;

            //Act
            stregsystem.AddCreditsToAccount(user, amount);

            //Assert
            Assert.Equal(balance + amount, user.Balance);
            Assert.IsType<InsertCashTransaction>(stregsystem.AddCreditsToAccount(user, amount));
        }
        [Fact]
        public void ExecuteTransaction_Test()
        {
            //Arrange
            User user = new User("test1", "test", "test", "test@domaintest.cool.dk", 100, 1);
            Product product = new Product(1, "Test", 100, true, true);
            decimal balance = user.Balance;
            IStregsystem stregsystem = CreateStregsystem();
            BuyTransaction transaction = new BuyTransaction(1, user, 100, product, "2021/12/13 19.26.03");

            //Act
            stregsystem.ExecuteTransaction(transaction);
            
            //Assert
            Assert.Equal(0, user.Balance);
        }
        [Fact]
        public void If_Produc_Id_Is_0_Throw_Test()
        {
            //Arrange+Act+Assert
            Assert.Throws<IdException>(() => new Product(0, "test", 10, true, true));
            Assert.Throws<ArgumentNullException>(() => new Product(1, null, 10, true, true));
        }
    }
}
