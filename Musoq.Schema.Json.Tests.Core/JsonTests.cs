﻿using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Musoq.Converter;
using Musoq.Evaluator;
using Musoq.Plugins;
using Newtonsoft.Json.Linq;

namespace Musoq.Schema.Json.Tests.Core
{
    [TestClass]
    public class JsonTests
    {
        [TestMethod]
        public void SimpleSelectTest()
        {
            var query =
                @"select Name, Age from #json.file('./JsonTestFile_First.json', './JsonTestFile_First.schema.json', ' ')";

            var vm = CreateAndRunVirtualMachine(query);
            var table = vm.Run();

            Assert.AreEqual(2, table.Columns.Count());
            Assert.AreEqual("Name", table.Columns.ElementAt(0).Name);
            Assert.AreEqual(typeof(string), table.Columns.ElementAt(0).ColumnType);
            Assert.AreEqual("Age", table.Columns.ElementAt(1).Name);
            Assert.AreEqual(typeof(int), table.Columns.ElementAt(1).ColumnType);

            Assert.AreEqual(3, table.Count);
            Assert.AreEqual("Aleksander", table[0].Values[0]);
            Assert.AreEqual(24, table[0].Values[1]);
            Assert.AreEqual("Mikolaj", table[1].Values[0]);
            Assert.AreEqual(11, table[1].Values[1]);
            Assert.AreEqual("Marek", table[2].Values[0]);
            Assert.AreEqual(45, table[2].Values[1]);
        }

        [TestMethod]
        public void SelectArrayTest()
        {
            var query =
                @"select Array from #json.file('./JsonTestFile_MakeFlatArray_Arr.json', './JsonTestFile_MakeFlatArray_Arr.schema.json', ' ')";

            var vm = CreateAndRunVirtualMachine(query);
            var table = vm.Run();

            Assert.AreEqual(1, table.Columns.Count());
            Assert.AreEqual("Array", table.Columns.ElementAt(0).Name);
            Assert.AreEqual(typeof(JArray), table.Columns.ElementAt(0).ColumnType);

            Assert.AreEqual(2, table.Count);
            Assert.AreEqual(3, ((JArray) table[0].Values[0]).Count);
            Assert.AreEqual(0, ((JArray) table[1].Values[0]).Count);
        }

        [TestMethod]
        public void SelectWithArrayLengthTest()
        {
            var query =
                @"select Name, Length(Books) from #json.file('./JsonTestFile_First.json', './JsonTestFile_First.schema.json', ' ')";

            var vm = CreateAndRunVirtualMachine(query);
            var table = vm.Run();

            Assert.AreEqual(2, table.Columns.Count());
            Assert.AreEqual("Name", table.Columns.ElementAt(0).Name);
            Assert.AreEqual(typeof(string), table.Columns.ElementAt(0).ColumnType);
            Assert.AreEqual("Length(Books)", table.Columns.ElementAt(1).Name);
            Assert.AreEqual(typeof(int), table.Columns.ElementAt(1).ColumnType);

            Assert.AreEqual(3, table.Count);
            Assert.AreEqual("Aleksander", table[0].Values[0]);
            Assert.AreEqual(2, table[0].Values[1]);
            Assert.AreEqual("Mikolaj", table[1].Values[0]);
            Assert.AreEqual(0, table[1].Values[1]);
            Assert.AreEqual("Marek", table[2].Values[0]);
            Assert.AreEqual(0, table[2].Values[1]);
        }

        [TestMethod]
        public void MakeFlatArrayTest()
        {
            var query =
                @"select MakeFlat(Array) from #json.file('./JsonTestFile_MakeFlatArray.json', './JsonTestFile_MakeFlatArray.schema.json', ' ')";

            var vm = CreateAndRunVirtualMachine(query);
            var table = vm.Run();

            Assert.AreEqual(1, table.Columns.Count());
            Assert.AreEqual("MakeFlat(Array)", table.Columns.ElementAt(0).Name);
            Assert.AreEqual(typeof(string), table.Columns.ElementAt(0).ColumnType);

            Assert.AreEqual(2, table.Count);
            Assert.AreEqual("1, 2, 3", table[0].Values[0]);
            Assert.AreEqual(string.Empty, table[1].Values[0]);
        }

        private CompiledQuery CreateAndRunVirtualMachine(string script)
        {
            return InstanceCreator.CompileForExecution(script, new JsonSchemaProvider());
        }

        static JsonTests()
        {
            new Environment().SetValue(Constants.NetStandardDllEnvironmentName, EnvironmentUtils.GetOrCreateEnvironmentVariable());
        }
    }
}