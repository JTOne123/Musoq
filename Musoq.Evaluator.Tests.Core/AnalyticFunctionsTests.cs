using Microsoft.VisualStudio.TestTools.UnitTesting;
using Musoq.Evaluator.Tests.Core.Schema;
using System;
using System.Collections.Generic;
using System.Text;

namespace Musoq.Evaluator.Tests.Core
{
    [TestClass]
    public class AnalyticFunctionsTests : TestBase
    {
        [TestMethod]
        public void SimpleAnalyticTest()
        {
            var query = "select Rank() over (partition by Country) from #A.entities()";

            var sources = new Dictionary<string, IEnumerable<BasicEntity>>
            {
                {
                    "#A", new[]
                    {
                        new BasicEntity("WARSAW", "POLAND", 500),
                        new BasicEntity("CZESTOCHOWA", "POLAND", 400),
                        new BasicEntity("KATOWICE", "POLAND", 250),
                        new BasicEntity("BERLIN", "GERMANY", 250),
                        new BasicEntity("MUNICH", "GERMANY", 350)
                    }
                }
            };

            var vm = CreateAndRunVirtualMachine(query, sources);
            var table = vm.Run();
        }
    }
}
