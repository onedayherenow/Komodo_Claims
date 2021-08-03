using KomodoClaims_Repo;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace KomodoClaims_Repo
{

	[TestClass]
	public class ClaimUI
	{

		[TestMethod]

		//public void DisplayClaims()
		//{
		// Console.WriteLine("{0,5} {1,10} {2,-10}", "Claim ID", "Claim Type", "Claim Description");
		//}

		//ProgramUI testClaim = new ProgramUI();
		//testClaim.Run();





	}



	[TestClass]
	public class ClaimRepository
	{

		private ClaimRepository _repo;  //intitalizing
		private Claim _claim; //initiallizing

		[TestInitialize] //this will run before each test
		public void Arrange()
		{
			_repo = new ClaimRepository();  //new instance of field with access to all methods
			_claim = new Claim(1735, "Car was in collision", 2250, DateTime.Parse("05/15/2021"), DateTime.Parse("05/21/2021"), true, ClaimType.Car);  //new object of streaming content

			_repo.AddClaimToList(_claim);  //added content to repo, now accessable

		}

		//add method
		[TestMethod]
		public void AddToList_ShouldGetNotNull()
		{

			//AAA paradigm, Arrange, Act, Assert

			//Arrange --> setting up playing field
			Claim content = new Claim();   //new object to stream
			content.ClaimID = "Toy Story";  //made the title toy story
			ClaimRepository repository = new ClaimRepository();   //new repo object, blank constructor already exists.
																  //^^^ now we have access to the repo methods

			//Act  --> run code we want to test
			repository.AddContentToList(content);   //we added the streaming object to our repo
			Claim contentFromDirectory = repository.GetContentByTitle("Toy Story");

			//Assert -->  Use the assert to verify expected outcome
			Assert.IsNotNull(contentFromDirectory);  //if title does not exist in directory and has not been added, test will fail
		}

		//update
		//we dont have to do every single thing to setup every single time
		[TestMethod]
		public void UpdateExistingContent_ShouldReturnTrue()
		{
			// Arrange
			//Test initialize
			Claim newContent = new Claim("Rubber", "A car tire comes to life", "R", 4.3, false, ClaimType.RomCom);

			//act
			bool updateResult = _repo.UpdateExistingContent("Rubber", newContent);

			//Assert
			Assert.IsTrue(updateResult);
		}


		[DataTestMethod]   //tests your data
		[DataRow("Rubber", true)]
		[DataRow("Toy Story", false)]
		public void UpdateExistingContent_ShouldMatchGivenBool(string originalTitle, bool shouldUpdate) //will change title
		{
			// Arrange
			//Test initialize
			Claim newContent = new Claim("Rubber", "A car tire comes to life", "R", 4.3, false, ClaimType.RomCom);

			//act
			bool updateResult = _repo.UpdateExistingContent(originalTitle, newContent);

			//Assert
			Assert.AreEqual(shouldUpdate, updateResult);

		}

		[TestMethod]
		public void DeleteContent_ShouldReturnTrue()
		{
			//arrange
			//test initializer

			//act
			bool deleteResult = _repo.RemoveContentByList(_content.ClaimID);

			//assert
			Assert.IsTrue(deleteResult);
		}

	}
}
