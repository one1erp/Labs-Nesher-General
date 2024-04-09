using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Logic
{
    class TestLogic:BaseLogic
    {
        internal static Test GetTestByName(Entities context, string testName)
        {
            return (from item in context.Tests1 where item.NAME == testName select item).FirstOrDefault();

        }

        internal static Test GetTestById(Entities context, long testId)
        {
            return (from item in context.Tests1 where item.TEST_ID == testId select item).FirstOrDefault();

        }
    }
}
