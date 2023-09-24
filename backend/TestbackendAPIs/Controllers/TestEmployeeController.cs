using backend.Controllers;
using backend.Models.Request;
using backend.Models.Response;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Globalization;

namespace TestbackendAPIs.Controllers
{
    public class TestEmployeeController
    {
        private EmployeeController _employeeController;
        private Mock<IEmployeeService> _mockEmployeeService;

        private List<EmployeeResponse> employeeResponseList;
        private EmployeeResponse employeeResponse;
        
        [SetUp]
        public void Setup()
        {
            _mockEmployeeService = new Mock<IEmployeeService>();
            _employeeController = new EmployeeController(_mockEmployeeService.Object);

            employeeResponseList = new List<EmployeeResponse>() {
            new EmployeeResponse
            {
                EmployeeId = "EMP-1234",
                EmployeeName = "Test",
                Designation = "admin",
                Department = "IT",
                Gender = "F",
                DateOfBirth = DateTime.Now.Date,
                DateOfJoining = DateTime.Now.Date
            },

            new EmployeeResponse
            {
                EmployeeId = "EMP-1235",
                EmployeeName = "Test1",
                Designation = "employee",
                Department = "IT",
                Gender = "F",
                DateOfBirth = DateTime.Now.Date,
                DateOfJoining = DateTime.Now.Date
            }
        };
            employeeResponse = new EmployeeResponse
            {
                EmployeeId = "EMP-1234",
                EmployeeName = "Test",
                Designation = "admin",
                Department = "IT",
                Gender = "F",
                DateOfBirth = DateTime.Now.Date,
                DateOfJoining = DateTime.Now.Date
            };
        }

        [Test]
        public void TestGetEmployees()
        {
            _mockEmployeeService.Setup(service => service.GetAllEmployees()).Returns(employeeResponseList);

            var result = _employeeController.GetEmployees();

            Assert.That(result, Is.InstanceOf<Task<ActionResult<IEnumerable<EmployeeResponse>>>>());
        }

        [TestCase("EMP-1234")]
        public void TestGetEmployeeById(string id)
        {
            _mockEmployeeService.Setup(service => service.GetEmployeeById(id)).Returns(employeeResponse);

            var result = _employeeController.GetEmployeeById(id);

            Assert.That(result, Is.InstanceOf<Task<ActionResult<EmployeeResponse>>>());
        }

        [TestCase("EMP-1234")]
        public void TestUpdateEmployee(string id)
        {
            var updateEmployeeRequest = new UpdateEmployeeRequest
            {
                EmployeeId = "EMP-2134",
                EmployeeName = "Test",
                Designation = "admin",
                Department = "IT",
                Gender = "F",
                DateOfBirth = DateTime.Now.Date,
                DateOfJoining = DateTime.Now.Date
            };
            _mockEmployeeService.Setup(service => service.UpdateEmployee( updateEmployeeRequest)).Returns(true);

            var result =   _employeeController.UpdateEmployee(id, updateEmployeeRequest);

            Assert.IsInstanceOf<Task<ActionResult>>(result);
        }

        [TestCase("EMP-1234")]
        public void TestDeleteEmployee(string id)
        {
            
            _mockEmployeeService.Setup(service => service.DeleteEmployee(id)).Returns(employeeResponse);

            var result = _employeeController.DeleteEmployee(id);

            Assert.IsInstanceOf<Task<ActionResult>>(result);
        }


    }
}
