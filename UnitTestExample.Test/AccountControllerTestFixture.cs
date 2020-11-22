using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace UnitTestExample.Test
{
    public class AccountControllerTestFixture
    {
        [
           Test,
           TestCase("irf@uni-corvinus.hu", "Abcd1234"),
           TestCase("irf@uni-corvinus.hu", "Abcd1234567"),
        ]
        public void TestValidateEmail(string email, bool expectedResult)
        {
            // Arrange
            var accountController = new AccountController();

            // Act
            var actualResult = accountController.ValidateEmail(email);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
        public void TestRegisterHappyPath(string email, string password)
        {
            // Arrange
            var accountController = new AccountController();

            // Act
            var actualResult = accountController.Register(email, password);

            // Assert
            Assert.AreEqual(email, actualResult.Email);
            Assert.AreEqual(password, actualResult.Password);
            Assert.AreNotEqual(Guid.Empty, actualResult.ID);
        }
    }
}
