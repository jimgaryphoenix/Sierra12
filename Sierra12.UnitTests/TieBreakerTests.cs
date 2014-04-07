using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProgressTen.Domain.Entities;
using ProgressTen.Services;

namespace ProgressTen.UnitTests
{
	/// <summary>
	/// Summary description for UnitTest1
	/// </summary>
	[TestClass]
	public class TieBreakerTests
	{
		private ResultService _resultService;

		public TieBreakerTests()
		{
			_resultService = new ResultService();
		}

		private TestContext testContextInstance;

		/// <summary>
		///Gets or sets the test context which provides
		///information about and functionality for the current test run.
		///</summary>
		public TestContext TestContext
		{
			get
			{
				return testContextInstance;
			}
			set
			{
				testContextInstance = value;
			}
		}

		#region Additional test attributes
		//
		// You can use the following additional attributes as you write your tests:
		//
		// Use ClassInitialize to run code before running the first test in the class
		// [ClassInitialize()]
		// public static void MyClassInitialize(TestContext testContext) { }
		//
		// Use ClassCleanup to run code after all tests in a class have run
		// [ClassCleanup()]
		// public static void MyClassCleanup() { }
		//
		// Use TestInitialize to run code before running each test 
		// [TestInitialize()]
		// public void MyTestInitialize() { }
		//
		// Use TestCleanup to run code after each test has run
		// [TestCleanup()]
		// public void MyTestCleanup() { }
		//
		#endregion

		[TestMethod]
		public void There_Are_Five_Results()
		{
			var results = CreateResultSet(5, 3);

			Assert.IsTrue(results.Count == 5);
		}

		[TestMethod]
		public void One_Result_No_Ties()
		{
			var results = CreateResultSet(1, 3);

			var score1_1 = new Score { CourseNumber = 1, CourseScore = 1 };
			results[0].Scores.Add(score1_1);
			var score1_2 = new Score { CourseNumber = 2, CourseScore = 1 };
			results[0].Scores.Add(score1_2);
			var score1_3 = new Score { CourseNumber = 3, CourseScore = 1 };
			results[0].Scores.Add(score1_3);

			var idList = _resultService.BreakTies(results);

			Assert.IsTrue(idList.Count == 1);

			Assert.IsTrue(idList[0].Scores.Count == 3);

			Assert.IsTrue(idList[0].ResultId == 10);
		}

		[TestMethod]
		public void Two_Results_No_Ties()
		{
			var results = CreateResultSet(2, 3);

			var score1_1 = new Score { CourseNumber = 1, CourseScore = 1 };
			results[0].Scores.Add(score1_1);
			var score1_2 = new Score { CourseNumber = 2, CourseScore = 1 };
			results[0].Scores.Add(score1_2);
			var score1_3 = new Score { CourseNumber = 3, CourseScore = 1 };
			results[0].Scores.Add(score1_3);

			var score2_1 = new Score { CourseNumber = 1, CourseScore = 2 };
			results[1].Scores.Add(score2_1);
			var score2_2 = new Score { CourseNumber = 2, CourseScore = 2 };
			results[1].Scores.Add(score2_2);
			var score2_3 = new Score { CourseNumber = 3, CourseScore = 2 };
			results[1].Scores.Add(score2_3);

			var idList = _resultService.BreakTies(results);

			Assert.IsTrue(idList.Count == 2);

			Assert.IsTrue(idList[0].Scores.Count == 3);

			Assert.IsTrue(idList[0].ResultId == 10);
			Assert.IsTrue(idList[1].ResultId == 11);
		}

		[TestMethod]
		public void Two_Results_One_Tie_Course3_Breaks_Doesnt_Change_Order()
		{
			var results = CreateResultSet(2, 3);

			var score1_1 = new Score { CourseNumber = 1, CourseScore = 2 };
			results[0].Scores.Add(score1_1);
			var score1_2 = new Score { CourseNumber = 2, CourseScore = 3 };
			results[0].Scores.Add(score1_2);
			var score1_3 = new Score { CourseNumber = 3, CourseScore = 1 };
			results[0].Scores.Add(score1_3);

			var score2_1 = new Score { CourseNumber = 1, CourseScore = 2 };
			results[1].Scores.Add(score2_1);
			var score2_2 = new Score { CourseNumber = 2, CourseScore = 2 };
			results[1].Scores.Add(score2_2);
			var score2_3 = new Score { CourseNumber = 3, CourseScore = 2 };
			results[1].Scores.Add(score2_3);

			var idList = _resultService.BreakTies(results);

			Assert.IsTrue(idList.Count == 2);

			Assert.IsTrue(idList[0].Scores.Count == 3);

			Assert.IsTrue(idList[0].ResultId == 10);
			Assert.IsTrue(idList[1].ResultId == 11);
		}

		[TestMethod]
		public void Two_Results_One_Tie_Course3_Breaks_Changes_Order()
		{
			var results = CreateResultSet(2, 3);

			var score1_1 = new Score { CourseNumber = 1, CourseScore = 2 };
			results[0].Scores.Add(score1_1);
			var score1_2 = new Score { CourseNumber = 2, CourseScore = 2 };
			results[0].Scores.Add(score1_2);
			var score1_3 = new Score { CourseNumber = 3, CourseScore = 2 };
			results[0].Scores.Add(score1_3);

			var score2_1 = new Score { CourseNumber = 1, CourseScore = 2 };
			results[1].Scores.Add(score2_1);
			var score2_2 = new Score { CourseNumber = 2, CourseScore = 3 };
			results[1].Scores.Add(score2_2);
			var score2_3 = new Score { CourseNumber = 3, CourseScore = 1 };
			results[1].Scores.Add(score2_3);

			var idList = _resultService.BreakTies(results);

			Assert.IsTrue(idList.Count == 2);

			Assert.IsTrue(idList[0].Scores.Count == 3);

			Assert.IsTrue(idList[0].ResultId == 11);
			Assert.IsTrue(idList[1].ResultId == 10);
		}

		[TestMethod]
		public void Five_Results_No_Ties_Correct_Order()
		{
			var results = CreateResultSet(5, 3);

			var score1_1 = new Score { CourseNumber = 1, CourseScore = 1 };
			results[0].Scores.Add(score1_1);
			var score1_2 = new Score { CourseNumber = 2, CourseScore = 1 };
			results[0].Scores.Add(score1_2);
			var score1_3 = new Score { CourseNumber = 3, CourseScore = 1 };
			results[0].Scores.Add(score1_3);

			var score2_1 = new Score { CourseNumber = 1, CourseScore = 3 };
			results[1].Scores.Add(score2_1);
			var score2_2 = new Score { CourseNumber = 2, CourseScore = 3 };
			results[1].Scores.Add(score2_2);
			var score2_3 = new Score { CourseNumber = 3, CourseScore = 3 };
			results[1].Scores.Add(score2_3);

			var score3_1 = new Score { CourseNumber = 1, CourseScore = 2 };
			results[2].Scores.Add(score3_1);
			var score3_2 = new Score { CourseNumber = 2, CourseScore = 2 };
			results[2].Scores.Add(score3_2);
			var score3_3 = new Score { CourseNumber = 3, CourseScore = 2 };
			results[2].Scores.Add(score3_3);

			var score4_1 = new Score { CourseNumber = 1, CourseScore = -2 };
			results[3].Scores.Add(score4_1);
			var score4_2 = new Score { CourseNumber = 2, CourseScore = -2 };
			results[3].Scores.Add(score4_2);
			var score4_3 = new Score { CourseNumber = 3, CourseScore = -2 };
			results[3].Scores.Add(score4_3);

			var score5_1 = new Score { CourseNumber = 1, CourseScore = 10 };
			results[4].Scores.Add(score5_1);
			var score5_2 = new Score { CourseNumber = 2, CourseScore = 10 };
			results[4].Scores.Add(score5_2);
			var score5_3 = new Score { CourseNumber = 3, CourseScore = 10 };
			results[4].Scores.Add(score5_3);

			var idList = _resultService.BreakTies(results.OrderBy(r => r.TotalScore).ToList());

			Assert.IsTrue(idList.Count == 5);

			Assert.IsTrue(idList[0].Scores.Count == 3);

			Assert.IsTrue(idList[0].ResultId == 13);
			Assert.IsTrue(idList[1].ResultId == 10);
			Assert.IsTrue(idList[2].ResultId == 12);
			Assert.IsTrue(idList[3].ResultId == 11);
			Assert.IsTrue(idList[4].ResultId == 14);
		}

		[TestMethod]
		public void Five_Results_One_Tie_Course_3_Breaks_Does_Not_Change_Order()
		{
			var results = CreateResultSet(5, 3);

			var score1_1 = new Score { CourseNumber = 1, CourseScore = 1 }; // 3    2
			results[0].Scores.Add(score1_1);
			var score1_2 = new Score { CourseNumber = 2, CourseScore = 1 };
			results[0].Scores.Add(score1_2);
			var score1_3 = new Score { CourseNumber = 3, CourseScore = 1 };
			results[0].Scores.Add(score1_3);

			var score2_1 = new Score { CourseNumber = 1, CourseScore = 3 }; // 8    3
			results[1].Scores.Add(score2_1);
			var score2_2 = new Score { CourseNumber = 2, CourseScore = 3 };
			results[1].Scores.Add(score2_2);
			var score2_3 = new Score { CourseNumber = 3, CourseScore = 2 }; // WINNER
			results[1].Scores.Add(score2_3);

			var score3_1 = new Score { CourseNumber = 1, CourseScore = 3 }; // 8    4
			results[2].Scores.Add(score3_1);
			var score3_2 = new Score { CourseNumber = 2, CourseScore = 2 };
			results[2].Scores.Add(score3_2);
			var score3_3 = new Score { CourseNumber = 3, CourseScore = 3 };
			results[2].Scores.Add(score3_3);

			var score4_1 = new Score { CourseNumber = 1, CourseScore = -2 }; // -6    1
			results[3].Scores.Add(score4_1);
			var score4_2 = new Score { CourseNumber = 2, CourseScore = -2 };
			results[3].Scores.Add(score4_2);
			var score4_3 = new Score { CourseNumber = 3, CourseScore = -2 };
			results[3].Scores.Add(score4_3);

			var score5_1 = new Score { CourseNumber = 1, CourseScore = 10 }; // 30    5
			results[4].Scores.Add(score5_1);
			var score5_2 = new Score { CourseNumber = 2, CourseScore = 10 };
			results[4].Scores.Add(score5_2);
			var score5_3 = new Score { CourseNumber = 3, CourseScore = 10 };
			results[4].Scores.Add(score5_3);

			var idList = _resultService.BreakTies(results.OrderBy(r => r.TotalScore).ToList());

			Assert.IsTrue(idList.Count == 5);

			Assert.IsTrue(idList[0].Scores.Count == 3);

			Assert.IsTrue(idList[0].ResultId == 13);
			Assert.IsTrue(idList[1].ResultId == 10);
			Assert.IsTrue(idList[2].ResultId == 11);
			Assert.IsTrue(idList[3].ResultId == 12);
			Assert.IsTrue(idList[4].ResultId == 14);
		}

		[TestMethod]
		public void Five_Results_One_Tie_Course_2_Breaks_Changes_Order()
		{
			var results = CreateResultSet(5, 3);

			var score1_1 = new Score { CourseNumber = 1, CourseScore = 1 }; // 3    2
			results[0].Scores.Add(score1_1);
			var score1_2 = new Score { CourseNumber = 2, CourseScore = 1 };
			results[0].Scores.Add(score1_2);
			var score1_3 = new Score { CourseNumber = 3, CourseScore = 1 };
			results[0].Scores.Add(score1_3);

			var score2_1 = new Score { CourseNumber = 1, CourseScore = 2 }; // 8    4
			results[1].Scores.Add(score2_1);
			var score2_2 = new Score { CourseNumber = 2, CourseScore = 3 };
			results[1].Scores.Add(score2_2);
			var score2_3 = new Score { CourseNumber = 3, CourseScore = 3 };
			results[1].Scores.Add(score2_3);

			var score3_1 = new Score { CourseNumber = 1, CourseScore = 4 }; // 8    3
			results[2].Scores.Add(score3_1);
			var score3_2 = new Score { CourseNumber = 2, CourseScore = 1 };
			results[2].Scores.Add(score3_2);
			var score3_3 = new Score { CourseNumber = 3, CourseScore = 3 }; // WINNER
			results[2].Scores.Add(score3_3);

			var score4_1 = new Score { CourseNumber = 1, CourseScore = -2 }; // -6    1
			results[3].Scores.Add(score4_1);
			var score4_2 = new Score { CourseNumber = 2, CourseScore = -2 };
			results[3].Scores.Add(score4_2);
			var score4_3 = new Score { CourseNumber = 3, CourseScore = -2 };
			results[3].Scores.Add(score4_3);

			var score5_1 = new Score { CourseNumber = 1, CourseScore = 10 }; // 30    5
			results[4].Scores.Add(score5_1);
			var score5_2 = new Score { CourseNumber = 2, CourseScore = 10 };
			results[4].Scores.Add(score5_2);
			var score5_3 = new Score { CourseNumber = 3, CourseScore = 10 };
			results[4].Scores.Add(score5_3);

			var idList = _resultService.BreakTies(results.OrderBy(r => r.TotalScore).ToList());

			Assert.IsTrue(idList.Count == 5);

			Assert.IsTrue(idList[0].Scores.Count == 3);

			Assert.IsTrue(idList[0].ResultId == 13);
			Assert.IsTrue(idList[1].ResultId == 10);
			Assert.IsTrue(idList[2].ResultId == 12);
			Assert.IsTrue(idList[3].ResultId == 11);
			Assert.IsTrue(idList[4].ResultId == 14);
		}

		[TestMethod]
		public void Five_Results_One_Tie_Course_2_Breaks_Changes_Order_2()
		{
			var results = CreateResultSet(5, 3);

			var score1_1 = new Score { CourseNumber = 1, CourseScore = -18 }; // -20    2
			results[0].Scores.Add(score1_1);
			var score1_2 = new Score { CourseNumber = 2, CourseScore = -10 };
			results[0].Scores.Add(score1_2);
			var score1_3 = new Score { CourseNumber = 3, CourseScore = 8 };
			results[0].Scores.Add(score1_3);

			var score2_1 = new Score { CourseNumber = 1, CourseScore = -10 }; // -20    1
			results[1].Scores.Add(score2_1);
			var score2_2 = new Score { CourseNumber = 2, CourseScore = -18 };
			results[1].Scores.Add(score2_2);
			var score2_3 = new Score { CourseNumber = 3, CourseScore = 8 }; // WINNER
			results[1].Scores.Add(score2_3);

			var score3_1 = new Score { CourseNumber = 1, CourseScore = 4 }; // 8    4
			results[2].Scores.Add(score3_1);
			var score3_2 = new Score { CourseNumber = 2, CourseScore = 1 };
			results[2].Scores.Add(score3_2);
			var score3_3 = new Score { CourseNumber = 3, CourseScore = 3 };
			results[2].Scores.Add(score3_3);

			var score4_1 = new Score { CourseNumber = 1, CourseScore = -2 }; // -6    3
			results[3].Scores.Add(score4_1);
			var score4_2 = new Score { CourseNumber = 2, CourseScore = -2 };
			results[3].Scores.Add(score4_2);
			var score4_3 = new Score { CourseNumber = 3, CourseScore = -2 };
			results[3].Scores.Add(score4_3);

			var score5_1 = new Score { CourseNumber = 1, CourseScore = 10 }; // 30    5
			results[4].Scores.Add(score5_1);
			var score5_2 = new Score { CourseNumber = 2, CourseScore = 10 };
			results[4].Scores.Add(score5_2);
			var score5_3 = new Score { CourseNumber = 3, CourseScore = 10 };
			results[4].Scores.Add(score5_3);

			var idList = _resultService.BreakTies(results.OrderBy(r => r.TotalScore).ToList());

			Assert.IsTrue(idList.Count == 5);

			Assert.IsTrue(idList[0].Scores.Count == 3);

			Assert.IsTrue(idList[0].ResultId == 11);
			Assert.IsTrue(idList[1].ResultId == 10);
			Assert.IsTrue(idList[2].ResultId == 13);
			Assert.IsTrue(idList[3].ResultId == 12);
			Assert.IsTrue(idList[4].ResultId == 14);
		}

		[TestMethod]
		public void Five_Results_One_3_Way_Tie_Course_3_Breaks()
		{
			var results = CreateResultSet(5, 3);

			var score1_1 = new Score { CourseNumber = 1, CourseScore = -18 }; // -20    2
			results[0].Scores.Add(score1_1);
			var score1_2 = new Score { CourseNumber = 2, CourseScore = -10 };
			results[0].Scores.Add(score1_2);
			var score1_3 = new Score { CourseNumber = 3, CourseScore = 8 };
			results[0].Scores.Add(score1_3);

			var score2_1 = new Score { CourseNumber = 1, CourseScore = -10 }; // -20    1
			results[1].Scores.Add(score2_1);
			var score2_2 = new Score { CourseNumber = 2, CourseScore = -18 };
			results[1].Scores.Add(score2_2);
			var score2_3 = new Score { CourseNumber = 3, CourseScore = 8 }; // WINNER
			results[1].Scores.Add(score2_3);

			var score3_1 = new Score { CourseNumber = 1, CourseScore = -20 }; // -20    3
			results[2].Scores.Add(score3_1);
			var score3_2 = new Score { CourseNumber = 2, CourseScore = -10 };
			results[2].Scores.Add(score3_2);
			var score3_3 = new Score { CourseNumber = 3, CourseScore = 10 };
			results[2].Scores.Add(score3_3);

			var score4_1 = new Score { CourseNumber = 1, CourseScore = 0 }; // 58    5
			results[3].Scores.Add(score4_1);
			var score4_2 = new Score { CourseNumber = 2, CourseScore = 22 };
			results[3].Scores.Add(score4_2);
			var score4_3 = new Score { CourseNumber = 3, CourseScore = 36 };
			results[3].Scores.Add(score4_3);

			var score5_1 = new Score { CourseNumber = 1, CourseScore = 10 }; // 0    4
			results[4].Scores.Add(score5_1);
			var score5_2 = new Score { CourseNumber = 2, CourseScore = 0 };
			results[4].Scores.Add(score5_2);
			var score5_3 = new Score { CourseNumber = 3, CourseScore = -10 };
			results[4].Scores.Add(score5_3);

			var idList = _resultService.BreakTies(results.OrderBy(r => r.TotalScore).ToList());

			Assert.IsTrue(idList.Count == 5);

			Assert.IsTrue(idList[0].Scores.Count == 3);

			Assert.IsTrue(idList[0].ResultId == 11);
			Assert.IsTrue(idList[1].ResultId == 10);
			Assert.IsTrue(idList[2].ResultId == 12);
			Assert.IsTrue(idList[3].ResultId == 14);
			Assert.IsTrue(idList[4].ResultId == 13);
		}

		[TestMethod]
		public void Five_Results_One_5_Way_Tie()
		{
			var results = CreateResultSet(5, 3);

			var score1_1 = new Score { CourseNumber = 1, CourseScore = -18 }; // -20    4
			results[0].Scores.Add(score1_1);
			var score1_2 = new Score { CourseNumber = 2, CourseScore = -10 };
			results[0].Scores.Add(score1_2);
			var score1_3 = new Score { CourseNumber = 3, CourseScore = 8 };
			results[0].Scores.Add(score1_3);

			var score2_1 = new Score { CourseNumber = 1, CourseScore = -10 }; // -20    3
			results[1].Scores.Add(score2_1);
			var score2_2 = new Score { CourseNumber = 2, CourseScore = -18 };
			results[1].Scores.Add(score2_2);
			var score2_3 = new Score { CourseNumber = 3, CourseScore = 8 };
			results[1].Scores.Add(score2_3);

			var score3_1 = new Score { CourseNumber = 1, CourseScore = -20 }; // -20    5
			results[2].Scores.Add(score3_1);
			var score3_2 = new Score { CourseNumber = 2, CourseScore = -10 };
			results[2].Scores.Add(score3_2);
			var score3_3 = new Score { CourseNumber = 3, CourseScore = 10 };
			results[2].Scores.Add(score3_3);

			var score4_1 = new Score { CourseNumber = 1, CourseScore = 0 }; // -20    2
			results[3].Scores.Add(score4_1);
			var score4_2 = new Score { CourseNumber = 2, CourseScore = -18 };
			results[3].Scores.Add(score4_2);
			var score4_3 = new Score { CourseNumber = 3, CourseScore = -2 };
			results[3].Scores.Add(score4_3);

			var score5_1 = new Score { CourseNumber = 1, CourseScore = 10 }; // -20    1
			results[4].Scores.Add(score5_1);
			var score5_2 = new Score { CourseNumber = 2, CourseScore = -20 };
			results[4].Scores.Add(score5_2);
			var score5_3 = new Score { CourseNumber = 3, CourseScore = -10 }; // WINNER
			results[4].Scores.Add(score5_3);

			var idList = _resultService.BreakTies(results.OrderBy(r => r.TotalScore).ToList());

			Assert.IsTrue(idList.Count == 5);

			Assert.IsTrue(idList[0].Scores.Count == 3);

			Assert.IsTrue(idList[0].ResultId == 14);
			Assert.IsTrue(idList[1].ResultId == 13);
			Assert.IsTrue(idList[2].ResultId == 11);
			Assert.IsTrue(idList[3].ResultId == 10);
			Assert.IsTrue(idList[4].ResultId == 12);
		}

		[TestMethod]
		public void Five_Results_Two_Ties_One_Unbreakable()
		{
			var results = CreateResultSet(5, 3);

			var score1_1 = new Score { CourseNumber = 1, CourseScore = 10 }; // 0    3
			results[0].Scores.Add(score1_1);
			var score1_2 = new Score { CourseNumber = 2, CourseScore = -10 };
			results[0].Scores.Add(score1_2);
			var score1_3 = new Score { CourseNumber = 3, CourseScore = 0 };
			results[0].Scores.Add(score1_3);

			var score2_1 = new Score { CourseNumber = 1, CourseScore = -10 }; // -20    2
			results[1].Scores.Add(score2_1);
			var score2_2 = new Score { CourseNumber = 2, CourseScore = -18 };
			results[1].Scores.Add(score2_2);
			var score2_3 = new Score { CourseNumber = 3, CourseScore = 8 };
			results[1].Scores.Add(score2_3);

			var score3_1 = new Score { CourseNumber = 1, CourseScore = 10 }; // 0    4
			results[2].Scores.Add(score3_1);
			var score3_2 = new Score { CourseNumber = 2, CourseScore = -10 };
			results[2].Scores.Add(score3_2);
			var score3_3 = new Score { CourseNumber = 3, CourseScore = 0 };
			results[2].Scores.Add(score3_3);

			var score4_1 = new Score { CourseNumber = 1, CourseScore = 28 }; // 20    5
			results[3].Scores.Add(score4_1);
			var score4_2 = new Score { CourseNumber = 2, CourseScore = -10 };
			results[3].Scores.Add(score4_2);
			var score4_3 = new Score { CourseNumber = 3, CourseScore = 0 };
			results[3].Scores.Add(score4_3);

			var score5_1 = new Score { CourseNumber = 1, CourseScore = 10 }; // -20    1
			results[4].Scores.Add(score5_1);
			var score5_2 = new Score { CourseNumber = 2, CourseScore = -20 };
			results[4].Scores.Add(score5_2);
			var score5_3 = new Score { CourseNumber = 3, CourseScore = -10 }; // WINNER
			results[4].Scores.Add(score5_3);

			var idList = _resultService.BreakTies(results.OrderBy(r => r.TotalScore).ToList());

			Assert.IsTrue(idList.Count == 5);

			Assert.IsTrue(idList[0].Scores.Count == 3);

			Assert.IsTrue(idList[0].ResultId == 14);
			Assert.IsTrue(idList[1].ResultId == 11);
			Assert.IsTrue(idList[2].ResultId == 10);
			Assert.IsTrue(idList[3].ResultId == 12);
			Assert.IsTrue(idList[4].ResultId == 13);
		}

		[TestMethod]
		public void WPaC_Case_ThreeWay_Two_Unbreakable()
		{
			var results = CreateResultSet(6, 2);

			var score1_1 = new Score { CourseNumber = 1, CourseScore = 28 };
			results[0].Scores.Add(score1_1);
			var score1_2 = new Score { CourseNumber = 2, CourseScore = 28 };
			results[0].Scores.Add(score1_2);

			var score2_1 = new Score { CourseNumber = 1, CourseScore = 36 };
			results[1].Scores.Add(score2_1);
			var score2_2 = new Score { CourseNumber = 2, CourseScore = 30 };
			results[1].Scores.Add(score2_2);

			var score3_1 = new Score { CourseNumber = 1, CourseScore = 38 };
			results[2].Scores.Add(score3_1);
			var score3_2 = new Score { CourseNumber = 2, CourseScore = 28 };
			results[2].Scores.Add(score3_2);

			var score4_1 = new Score { CourseNumber = 1, CourseScore = 38 };
			results[3].Scores.Add(score4_1);
			var score4_2 = new Score { CourseNumber = 2, CourseScore = 28 };
			results[3].Scores.Add(score4_2);

			var score5_1 = new Score { CourseNumber = 1, CourseScore = 38 };
			results[4].Scores.Add(score5_1);
			var score5_2 = new Score { CourseNumber = 2, CourseScore = 30 };
			results[4].Scores.Add(score5_2);

			var score6_1 = new Score { CourseNumber = 1, CourseScore = 32 };
			results[5].Scores.Add(score6_1);
			var score6_2 = new Score { CourseNumber = 2, CourseScore = 38 };
			results[5].Scores.Add(score6_2);

			var idList = _resultService.BreakTies(results.OrderBy(r => r.TotalScore).ToList());

			Assert.IsTrue(idList.Count == 6);

			Assert.IsTrue(idList[0].Scores.Count == 2);

			Assert.IsTrue(idList[0].ResultId == 10);
			Assert.IsTrue(idList[1].ResultId == 12);
			Assert.IsTrue(idList[2].ResultId == 13);
			Assert.IsTrue(idList[3].ResultId == 11);
			Assert.IsTrue(idList[4].ResultId == 14);
			Assert.IsTrue(idList[5].ResultId == 15);
		}

		/* WHAT IT IS ON WPaC PAGE
			1	 Mike King (StIcK kInG)			28	28	 56	
			2	 Chris Lengyel (DrCrawlGood)	36	30	 66
			3	 Carey Baird (Eviltwin v2)		38	28	 66
			4	 Mike Young (onespeeder)		38	28	 66
			5	 Bill Goff (ovalman)			38	30	 68
			6	 Del Anderson (DRCMan)			32	38	 70	
			
			WHAT IT SHOULD BE
			1	 Mike King (StIcK kInG)			28	28	 56	
			2	 Carey Baird (Eviltwin v2)		38	28	 66
			3	 Mike Young (onespeeder)		38	28	 66
			4	 Chris Lengyel (DrCrawlGood)	36	30	 66
			5	 Bill Goff (ovalman)			38	30	 68
			6	 Del Anderson (DRCMan)			32	38	 70	
		*/

		private IList<Result> CreateResultSet(int numberResults, int numberOfCourses)
		{
			var results = new List<Result>();

			for (int i = 0; i < numberResults; i++)
			{
				var comp = new Event
				           	{
				           		NumberOfCourses = numberOfCourses
				           	};

				var result = new Result
				{
					ResultId = (i + 10), 
					ClassId = 2,
					EventId = 3,
					DriverId = (i + 1), 
					Event = comp
				};

				results.Add(result);
			}

			return results;
		}
	}
}
