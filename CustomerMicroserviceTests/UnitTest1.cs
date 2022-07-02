using CustomerMicroservice.Controllers;
using CustomerMicroservice.Models;
using CustomerMicroservice.Repo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CustomerMicroserviceTests
{
    [TestClass]
    public class UnitTest1
    {
        Mock<ICustomerService> mockRepo = new Mock<ICustomerService>();
        [TestMethod]
        public void GetCustomer_validcustId_returnCustDetails()
        {
            //Arrange
            var cust = new Customer()
            {
                CustId=1,
                Name="ss",
                Address="Satara",
                PANno=1234567
            };

            //Act
            mockRepo.Setup(p => p.GetCustomerDetails(1)).Returns(cust);
            CustomerController customer = new CustomerController(mockRepo.Object);
            OkObjectResult actionresult = (OkObjectResult)customer.GetCustomerById(1);
            var okResult = actionresult as OkObjectResult;
            var status = okResult.Value;


            //Assert
            Assert.AreEqual(cust, status);
        }


        [TestMethod]
        public void GetCustomer_InvalidcustId_returnNULL()
        {
            //Arrange
            Customer cust = null;

            //Act
            mockRepo.Setup(p => p.GetCustomerDetails(1000)).Returns(cust);
            CustomerController customer = new CustomerController(mockRepo.Object);
            NotFoundResult actionresult = (NotFoundResult)customer.GetCustomerById(1000);
            var notfoundResult = actionresult as NotFoundResult;
            var status = notfoundResult.StatusCode;


            //Assert
            Assert.AreEqual(404, status);
        }

        [TestMethod]
        public void CreateCustomer_validCustomerobject_returnSuccessCustcreationstatus()
        {
            //Arrange
            var cust = new Customer()
            {
                CustId = 10,
                Name = "ss",
                Address = "Satara",
                PANno = 1234567
            };
            var res = new CustomerCreationStatus()
            {
                CustomerId = 10,
                Message = "success"
            };

            //Act
            mockRepo.Setup(p => p.AddCustomer(cust)).Returns(res);
            CustomerController customer = new CustomerController(mockRepo.Object);
            OkObjectResult actionresult = (OkObjectResult)customer.AddCustomer(cust);
            var okResult = actionresult as OkObjectResult;
            var status = okResult.Value;


            //Assert
            Assert.AreEqual(res, status);
        }
    }
}
