using Microsoft.VisualStudio.TestTools.UnitTesting;
using ADHDPlanner_Backend.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfaceLayer.Interfaces;
using ADHDPlanner_BackendTests.Mock;

namespace ADHDPlanner_Backend.Controllers.Tests
{
    [TestClass()]
    public class TasksControllerTests
    {
        private TasksController _controller;
        private MockTaskDatabase _mockDatabase;

        [TestInitialize()]
        public void Initialize()
        {
            _mockDatabase = new MockTaskDatabase();
            _controller = new TasksController(null, _mockDatabase);
        }

        [TestMethod()]
        public void GetTaskTest()
        {
            Assert.Fail();
        }
    }
}