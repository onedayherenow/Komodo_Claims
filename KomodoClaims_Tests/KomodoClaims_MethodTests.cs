using KomodoClaims_Repo;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace KomodoClaims_Repo
{
	[TestClass]
	public class KomodoClaims_MethodTests
	{
			private ClaimRepository _repo;  //intitalizing
			private Claim _claim; //initiallizing

			[TestInitialize] //this will run before each test
			public void Arrange()
			{
				_repo = new ClaimRepository();  //new instance of field with access to all methods
				_claim = new Claim(1735, "Car was in collision", 2250, DateTime.Parse("05/15/2021"), DateTime.Parse("05/21/2021"), true, ClaimType.Car);  //new claim object

				_repo.AddClaimToList(_claim);  //added content to repo, now accessable

			}

			//create method
			[TestMethod]
			public void AddToList_ShouldGetNotNull()
			{
				//AAA paradigm, Arrange, Act, Assert

				//Arrange --> setting up playing field
				Claim creatorClaim = new Claim();   //new claim to add
				creatorClaim.ClaimID = 9;  //made the claim id my favorite number
				ClaimRepository repository = new ClaimRepository();   //new repo object for repo methods
																	

				//Act  --> run code we want to test
				repository.AddClaimToList(creatorClaim);   //we added the claim object to our repo
				Claim addedToRepo = repository.GetClaimByID(9);  //get the claim and assign it as a copy to empty claim object

				//Assert -->  Use the assert to verify expected outcome
				Assert.IsNotNull(addedToRepo);  //if object (by ID 9) and has not been added, test will fail
			}

			//read method
			[TestMethod]
			public void GetClaimsList_ShouldNotGetNull()
			{	//arrange has been done in test initializer
				//both my 1. act and 2. assert is nested within another
			Assert.IsNotNull(_repo.GetClaimsList());  //act & assert
			}
			
			//update
			[TestMethod]
			public void UpdateExistingClaim_ShouldReturnTrue()
			{
				// Arrange & Test initialize
				Claim updatedClaim = new Claim(182, "Cracked window on passenger side", 3500, DateTime.Parse("05/19/2021"), DateTime.Parse("05/28/2021"), true, ClaimType.Car);

				//act
				bool updateResult = _repo.UpdateExistingClaim(9, updatedClaim);   //we update the claim by id number 9, with the new updatedClaim

				//Assert
				Assert.IsTrue(updateResult);
			}

			//delete
			[TestMethod]
			public void RemoveClaimFromList_ShouldReturnTrue()
			{	//everything has been arranged already in the test initializer

				//act  , implement our delete of the initialied claim, by their ID
				bool deleteResult = _repo.RemoveClaimFromList(_claim.ClaimID);

				//assert
				Assert.IsTrue(deleteResult);
			}
	}
}
