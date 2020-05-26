using BusinessLayer.Services.Students;
using BusinessLayer.Services.Students.Contracts;
using DataAccess.Contracts;
using DataAccess.Entities;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tests
{
    public class StudentsBlServiceTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCaseSource("FilterTestData")]
        public async Task GetStudents_FilterTest(GetStudentsBlContract contract, int[] idCheckList, int total)
        {
            var iStudentDbService = new Mock<IStudentDbService>();

            //Func<IQueryable<Student>, IQueryable<Student>> returnedFilter = null;

            iStudentDbService
                .Setup(x => x.GetStudentCount(It.IsAny<Func<IQueryable<Student>, IQueryable<Student>>>()))
                //.Callback<Func<IQueryable<Student>, IQueryable<Student>>>(filter => { returnedFilter = filter; })
                .Returns<Func<IQueryable<Student>, IQueryable<Student>>>(filter => Task.FromResult(filter(TestData.AsQueryable()).Count()));

            iStudentDbService
                .Setup(x => x.GetStudents(It.IsAny<Func<IQueryable<Student>, IQueryable<Student>>>()))
                //.Callback<Func<IQueryable<Student>, IQueryable<Student>>, int, int>((filter, skip, take) => { returnedFilter = filter; })
                .Returns<Func<IQueryable<Student>, IQueryable<Student>>>((filter) => Task.FromResult(filter(TestData.AsQueryable()).AsEnumerable()));


            var result = await new StudentsBlService(iStudentDbService.Object).GetStudents(contract);


            Assert.IsNotNull(result, "Result");
            Assert.AreEqual(contract.PageNumber, result.PageNumber, "PageNumber");
            Assert.AreEqual(contract.PageSize, result.PageSize, "PageSize");
            Assert.AreEqual(total, result.Total, "Total");

            Assert.IsNotNull(result.Students, "StudentList");
            Assert.AreEqual(idCheckList.Length, result.Students.Count, "StudentListCount");

            var i = 0;
            foreach(var student in result.Students)
            {
                var stringNumber = idCheckList[i].ToString("D2");
                Assert.AreEqual(student.Id, new Guid($"{{00000000-0000-0000-0000-0000000000{stringNumber}}}"), "Wrong order");
                i++;
            }

        }

        //TODO: Generate more test cases for different situations
        private static IEnumerable<TestCaseData> FilterTestData
        {
            get
            {
                yield return new TestCaseData(new GetStudentsBlContract
                {
                    Desc = true,
                    SortByField = "LastName",
                    PageNumber = 2,
                    PageSize = 3,
                    Sex = null,
                    FirstName = null,
                    LastName = null,
                    MiddleName = null,
                }, new int[] { 4, 5, 6 }, 18);

                yield return new TestCaseData(new GetStudentsBlContract
                {
                    Desc = false,
                    SortByField = "LastName",
                    PageNumber = 2,
                    PageSize = 3,
                    Sex = null,
                    FirstName = null,
                    LastName = null,
                    MiddleName = null,
                }, new int[] { 16, 14, 13 }, 18);

                yield return new TestCaseData(new GetStudentsBlContract
                {
                    Desc = false,
                    SortByField = "FirstName",
                    PageNumber = 2,
                    PageSize = 3,
                    Sex = null,
                    FirstName = null,
                    LastName = null,
                    MiddleName = null,
                }, new int[] { 9, 8, 7 }, 18);

                yield return new TestCaseData(new GetStudentsBlContract
                {
                    Desc = false,
                    SortByField = "FirstName",
                    PageNumber = 1,
                    PageSize = 10,
                    Sex = null,
                    FirstName = "a",
                    LastName = null,
                    MiddleName = null,
                }, new int[] { 3, 12, 1 }, 3);

                yield return new TestCaseData(new GetStudentsBlContract
                {
                    Desc = false,
                    SortByField = "MiddleName",
                    PageNumber = 1,
                    PageSize = 10,
                    Sex = null,
                    FirstName = null,
                    LastName = "f",
                    MiddleName = null,
                }, new int[] { 13, 6 }, 2);

                yield return new TestCaseData(new GetStudentsBlContract
                {
                    Desc = false,
                    SortByField = "Sex",
                    PageNumber = 1,
                    PageSize = 10,
                    Sex = null,
                    FirstName = null,
                    LastName = null,
                    MiddleName = "m",
                }, new int[] { 13, 14 }, 2);
            }
        }

        private readonly static List<Student> TestData = new List<Student>
        {
            new Student{Deleted = false, FirstName = "taaaFn", LastName="saaaLn", MiddleName = "raaaMn", Id = new Guid("{00000000-0000-0000-0000-000000000001}"), Sex = SexEnum.Female, Uid = "2111111" },
            new Student{Deleted = false, FirstName = "sbbbFn", LastName="abbbLn", MiddleName = "qbbbMn", Id = new Guid("{00000000-0000-0000-0000-000000000002}"), Sex = SexEnum.Male, Uid = "5222222" },
            new Student{Deleted = false, FirstName = "acccFn", LastName="qcccLn", MiddleName = "scccMn", Id = new Guid("{00000000-0000-0000-0000-000000000003}"), Sex = SexEnum.Male, Uid = "4333333" },
            new Student{Deleted = false, FirstName = "qdddFn", LastName="paaaLn", MiddleName = "pdddMn", Id = new Guid("{00000000-0000-0000-0000-000000000004}"), Sex = SexEnum.Female, Uid = "3444444" },
            new Student{Deleted = false, FirstName = "peeeFn", LastName="oeeeLn", MiddleName = "oeeeMn", Id = new Guid("{00000000-0000-0000-0000-000000000005}"), Sex = SexEnum.Female, Uid = "1555555" },
            new Student{Deleted = false, FirstName = "offfFn", LastName="nfffLn", MiddleName = "tfffMn", Id = new Guid("{00000000-0000-0000-0000-000000000006}"), Sex = SexEnum.Male, Uid = "6666666" },
            new Student{Deleted = false, FirstName = "fgggFn", LastName="mgggLn", MiddleName = "agggMn", Id = new Guid("{00000000-0000-0000-0000-000000000007}"), Sex = SexEnum.Female, Uid = "8777777" },
            new Student{Deleted = false, FirstName = "ehhhFn", LastName="khhhLn", MiddleName = "bhhhMn", Id = new Guid("{00000000-0000-0000-0000-000000000008}"), Sex = SexEnum.Female, Uid = "9888888" },
            new Student{Deleted = false, FirstName = "diiiFn", LastName="jiiiLn", MiddleName = "ciiiMn", Id = new Guid("{00000000-0000-0000-0000-000000000009}"), Sex = SexEnum.Male, Uid = "7999999" },
            new Student{Deleted = true, FirstName = "cjjjFn", LastName="ijjjLn", MiddleName = "djjjMn", Id = new Guid("{00000000-0000-0000-0000-000000000010}"), Sex = SexEnum.Female, Uid = null },
            new Student{Deleted = false, FirstName = "bkkkFn", LastName="hkkkLn", MiddleName = "ekkkMn", Id = new Guid("{00000000-0000-0000-0000-000000000011}"), Sex = SexEnum.Female, Uid = null },
            new Student{Deleted = false, FirstName = "alllFn", LastName="glllLn", MiddleName = "nlllMn", Id = new Guid("{00000000-0000-0000-0000-000000000012}"), Sex = SexEnum.Female, Uid = null },
            new Student{Deleted = false, FirstName = "kmmmFn", LastName="fmmmLn", MiddleName = "fmmmMn", Id = new Guid("{00000000-0000-0000-0000-000000000013}"), Sex = SexEnum.Male, Uid = null },
            new Student{Deleted = false, FirstName = "jnnnFn", LastName="ennnLn", MiddleName = "mnnnMn", Id = new Guid("{00000000-0000-0000-0000-000000000014}"), Sex = SexEnum.Female, Uid = null },
            new Student{Deleted = true, FirstName = "ioooFn", LastName="doooLn", MiddleName = "goooMn", Id = new Guid("{00000000-0000-0000-0000-000000000015}"), Sex = SexEnum.Female, Uid = null },
            new Student{Deleted = false, FirstName = "hpppFn", LastName="cpppLn", MiddleName = "hpppMn", Id = new Guid("{00000000-0000-0000-0000-000000000016}"), Sex = SexEnum.Male, Uid = null },
            new Student{Deleted = false, FirstName = "gqqqFn", LastName="tqqqLn", MiddleName = "lqqqMn", Id = new Guid("{00000000-0000-0000-0000-000000000017}"), Sex = SexEnum.Female, Uid = null },
            new Student{Deleted = false, FirstName = "nrrrFn", LastName="brrrLn", MiddleName = "irrrMn", Id = new Guid("{00000000-0000-0000-0000-000000000018}"), Sex = SexEnum.Female, Uid = null },
            new Student{Deleted = false, FirstName = "msssFn", LastName="asssLn", MiddleName = "ksssMn", Id = new Guid("{00000000-0000-0000-0000-000000000019}"), Sex = SexEnum.Female, Uid = null },
            new Student{Deleted = false, FirstName = "ltttFn", LastName="ltttLn", MiddleName = "jtttMn", Id = new Guid("{00000000-0000-0000-0000-000000000020}"), Sex = SexEnum.Male, Uid = null },
        };
    }
}