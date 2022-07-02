using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TransactionMicroservice.Controllers;
using TransactionMicroservice.Models;
using TransactionMicroservice.Repo;

namespace TransactionMicroserviceTests
{
    [TestClass]
    public class UnitTest1
    {
        public Mock<ITransactionService> mockRepo = new Mock<ITransactionService>();
        [TestMethod]
        public void Deposit_validaccountId_TransactionStatusSuccess()
        {
            //Arrange
            int AccId = 12;
            float amount = 5000;
            var result = new TransactionStatus
            {
                SourceBalance = 0,
                DestinationBalance = 9000,
                Message = "success"
            };
            var T = new TransactionController(mockRepo.Object);
            string msg = "success";

            mockRepo.Setup(p => p.deposit(AccId, amount)).Returns(result);

            OkObjectResult actionresult = (OkObjectResult)T.Deposit(AccId, amount);
            var okResult = actionresult as OkObjectResult;
            var status = okResult.Value;

            Assert.AreEqual(status, result);
        }

        


        [TestMethod]
        public void Withdraw_validWithdrawlamountandAccId_TransactionstatusSuccess()
        {
            //Arrange
            int AccId = 10;
            float amount = 2000;
            var result = new TransactionStatus
            {
                SourceBalance = 5000,
                DestinationBalance =0 ,
                Message = "success"
            };
            var T = new TransactionController(mockRepo.Object);
           

            mockRepo.Setup(p => p.withdraw(AccId, amount)).Returns(result);

            OkObjectResult actionresult = (OkObjectResult)T.WithDraw(AccId, amount);
            var okResult = actionresult as OkObjectResult;
            var status = okResult.Value;

            Assert.AreEqual(status, result);
        }



        [TestMethod]
        public void Withdraw_InvalidWithdrawlAmount_TransactionStatusFail()
        {
            //Arrange
            int AccId = 17;
            float amount = 5000;
            var result = new TransactionStatus
            {
                SourceBalance = 5000,
                DestinationBalance = 0,
                Message = "fail"
            };
            var T = new TransactionController(mockRepo.Object);


            mockRepo.Setup(p => p.withdraw(AccId, amount)).Returns(result);

            OkObjectResult actionresult = (OkObjectResult)T.WithDraw(AccId, amount);
            var okResult = actionresult as OkObjectResult;
            var status = okResult.Value;

            Assert.AreEqual(status, result);
        }


    }
}
