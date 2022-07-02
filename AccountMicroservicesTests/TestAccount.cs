using AccountMicroservice.Controllers;
using AccountMicroservice.Models;
using AccountMicroservice.Repo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace AccountMicroservicesTests
{
    [TestClass]
    public class TestAccount
    {
        public Mock<IAccountService> mockRepo =new Mock<IAccountService>();
        [TestMethod]
        public void getCustomerAccounts_validcustomerId_returnsallAccountsofcustomer()
        {
            //Arrange
            IEnumerable<Account> list = new List<Account>
            {
                new Account { AccountID=1,AccountBalance=1000, CustomerID=1},
                new Account { AccountID=2,AccountBalance=1700, CustomerID=1}
            };

            //Act
            mockRepo.Setup(p => p.getCustomerAccounts(1)).Returns(list);
            AccountController acc = new AccountController(mockRepo.Object);
            OkObjectResult actionresult = (OkObjectResult) acc.GetCustomerAccounts(1);
            var okResult = actionresult as OkObjectResult;
            var status = okResult.Value;


            //Assert
            Assert.AreEqual(list,status);
        }

        
  
        [TestMethod]
        public void GetAllAccounts_validAccId_returnsAccounts()
        {
            List<Account> list = new List<Account>
            {
                new Account { AccountID=1,AccountBalance=1000, CustomerID=1},
                new Account { AccountID=2,AccountBalance=1700, CustomerID=1},
                new Account { AccountID=3,AccountBalance=1900,  CustomerID=2},
                new Account { AccountID=4,AccountBalance=1700,  CustomerID=3}
            };
            //Act
            mockRepo.Setup(p => p.GetAllAccounts()).Returns(list);
            AccountController acc = new AccountController(mockRepo.Object);
            OkObjectResult actionresult = (OkObjectResult)acc.GetAllAccounts();
            var okResult = actionresult as OkObjectResult;
            var status = okResult.Value;

            //Assert
            Assert.AreEqual(list,status);
        }

        [TestMethod]
        public void GetBalance_validAccId_returnbalance()
        {
            var acc = new Account
            {
                AccountID = 1,
                AccountBalance = 1400,
                CustomerID = 1,

            };

            //Act
            mockRepo.Setup(p => p.GetBalance(acc.AccountID)).Returns(acc.AccountBalance);
            AccountController account = new AccountController(mockRepo.Object);
            OkObjectResult actionresult = (OkObjectResult)account.GetBalance(acc.AccountID);
            var actualconfiguration = (float)actionresult.Value;

            //Assert
            Assert.AreEqual(actualconfiguration , acc.AccountBalance);

            

            
        }
        
        [TestMethod]
        public void GetBalanceTestFailure()
        {
            var result = new Account
            {
                AccountID = 1,
                AccountBalance = 1400,
                CustomerID = 1,
               
            };
            mockRepo.Setup(p => p.GetBalance(result.AccountID)).Returns(-1);
            var acc = new AccountController(mockRepo.Object);
            OkObjectResult actionresult = (OkObjectResult)acc.GetBalance(result.AccountID);
            var actualconfiguration = (float)actionresult.Value;

            Assert.AreNotEqual(actualconfiguration ,result.AccountBalance);
        }


        [TestMethod]
        public void CreateAccountTestSuccess()
        {
            int CustomerId = 1;
            AccountCreationStatus result = new AccountCreationStatus()
            {
                AccountId=1,
                Message="success"
            };
            var acc = new AccountController(mockRepo.Object);
            mockRepo.Setup(p => p.createAccount(CustomerId)).Returns(result);
            var actionresult = acc.CreateAccount(CustomerId);
            var okResult = actionresult as OkObjectResult;

            var status = okResult.Value;

            Assert.AreEqual(status, result);
        }

        [TestMethod]
        public void DepositeTestSuccess()
        {
            var acc = new Account
            {
                AccountID = 1,
                AccountBalance = 2000,
                CustomerID = 1,

            };
            var result = new TransactionStatus
            {
               SourceBalance=0,
               DestinationBalance=4000,
               Message="success"
            };
            var account = new AccountController(mockRepo.Object);
            string msg = "success";

            mockRepo.Setup(p => p.Deposite(acc.AccountID,2000)).Returns(result);

            OkObjectResult actionresult = (OkObjectResult)account.Deposit(acc.AccountID, 2000);
            var okResult = actionresult as OkObjectResult;
            var status = okResult.Value;

            Assert.AreEqual(status, result);
        }

        
        [TestMethod]
        public void WithdrawTestSuccess()
        {

            var acc = new Account
            {
                AccountID = 1,
                AccountBalance = 2500,
                CustomerID = 1,

            };
            var result = new TransactionStatus
            {
                SourceBalance = 2000,
                DestinationBalance = 0,
                Message = "success"
            };
            var account = new AccountController(mockRepo.Object);
            string msg = "success";

            mockRepo.Setup(p => p.Withdraw(acc.AccountID, 500)).Returns(result);

            OkObjectResult actionresult = (OkObjectResult)account.Withdraw(acc.AccountID, 500);
            var okResult = actionresult as OkObjectResult;
            var status = okResult.Value;

            Assert.AreEqual(status, result);
        }

       

        [TestMethod]
        public void WithdrawTestFailure()
        {
            var acc = new Account
            {
                AccountID = 1,
                AccountBalance = 2000,
                CustomerID = 1,

            };
            var result = new TransactionStatus
            {
                SourceBalance = 2000,
                DestinationBalance = 0,
                Message = "failed"
            };
            var account = new AccountController(mockRepo.Object);
           // string msg = "failed";

            mockRepo.Setup(p => p.Withdraw(acc.AccountID, 2000)).Returns(result);

            OkObjectResult actionresult = (OkObjectResult)account.Withdraw(acc.AccountID, 2000);
            var okResult = actionresult as OkObjectResult;
            var status = okResult.Value;

            Assert.AreEqual(status, result);
        }

        
    }
}
