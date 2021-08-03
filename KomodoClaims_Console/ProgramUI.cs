using KomodoClaims_Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomodoClaims_Repo
{
	public class ProgramUI
	{
		//persister field, always existing for whole instance of this UI object
		private ClaimRepository _claimRepo = new ClaimRepository();

		//method that runs/starts the UI part of application
		public void Run() //public so it can be called through class to method
		{
			SeedContentList();  //fires off right before the menu runs to seed it before anything
			Menu();
		}

		//menu  ---we want these private
		private void Menu()
		{
			bool keepRunning = true;
			while (keepRunning)
			{
				//1. display options to the user
				//We want to be able to build a UI that matches each of our repository methods
				Console.WriteLine("Select a menu option:\n" +
				"1. Enter a New Claim \n" +
				"2. View All Claims \n" +
				"3. Take Care of a Claim \n" +
				"4. Exit");

				//2. get the user's input
				string input = Console.ReadLine();

				//3. evaluate user's input and act accordingly
				switch (input)
				{
					case "1":
						//creates a new claim
						CreateNewClaim();
						break;
					case "2":
						//view all content
						DisplayAllClaims();
						break;
					//case "3":
					// //view content by title
					// DisplayContentByTitle();
					// break;
					//case "4":
					// //Update existing content
					// UpdateExistingContent();
					// break;
					case "3":
						//Delete existing content
						TakeCareOfClaim();
						break;
					case "4":
						// exit
						Console.WriteLine("Goodbye");
						keepRunning = false; //breaks the while loop and exits the application
						break;
					default:
						Console.WriteLine("Please enter a valid number");
						break;
				}
				Console.WriteLine("Please press any key to continue...");
				Console.ReadLine();
				Console.Clear();
			}
		}



		//methods that we want to do something but not return anything from this method to menu (void)
		//private so that they can be used inside this class by another method but not from outside this class
		//create new streaming content
		private void CreateNewClaim()
		{
			Claim newClaim = new Claim(); //we declare it first so that we can then use the property of object for user input

			//ID
			Console.WriteLine("Enter the Claim ID for the new claim");
			string idAsString = Console.ReadLine();
			newClaim.ClaimID = int.Parse(idAsString);

			//claim type
			Console.WriteLine("Enter the claim type's corresponding number:\n" +
			"1. Car\n" +
			"2. Home\n" +
			"3. Theft\n");

			string claimAsString = Console.ReadLine();
			int claimAsInt = int.Parse(claimAsString);
			newClaim.TypeOfClaim = (ClaimType)claimAsInt;    //casting integer into enum by the enum's numerical value

			//description
			Console.WriteLine("Enter the description for the content");
			newClaim.Description = Console.ReadLine();

			//claim amount
			Console.WriteLine("Enter the claim ammount: (1100.00, 3350.00, 6800.50, etc)");
			string amountAsString = Console.ReadLine();
			newClaim.ClaimAmount = double.Parse(amountAsString);

			//date of incident
			Console.WriteLine("Hello, please enter the date of the incident with MM/dd/yyyy format: (example: 09/23/1996)");

			bool isTrue = true;

			while (isTrue)
			{
				Console.WriteLine("Please use the MM/dd/yyyy format. (example: 09/23/1996)");
				DateTime incidentDate = DateTime.Parse(Console.ReadLine());

				if (incidentDate != null)
				{
					newClaim.DateOfIncident = incidentDate;
					isTrue = false;
				}
				else
				{
					isTrue = true;
				}
			}


			//date of claim
			Console.WriteLine("Hello, please enter the date of the claim with MM/dd/yyyy format: (example: 09/23/1996)");

			bool currentlyTrue = true;

			while (currentlyTrue)
			{
				Console.WriteLine("Please use the MM/dd/yyyy format. (example: 09/23/1996)");
				DateTime claimDate = DateTime.Parse(Console.ReadLine());

				if (claimDate != null)
				{
					newClaim.DateOfClaim = claimDate;
					currentlyTrue = false;
				}
				else
				{
					currentlyTrue = true;
				}
			}


			//is valid
			void ReturnerOfValidation()
			{

				int numDaysAfter = NumberOfDaysBetweeenIncidentAndClaim(newClaim.DateOfIncident, newClaim.DateOfClaim);

				bool Valid = IsValid(numDaysAfter);

				if (Valid == true)
				{
					newClaim.IsValid = true;
				}
				else
				{
					newClaim.IsValid = false;
				}

				if (newClaim.IsValid == true)
				{
					Console.WriteLine("This claim is currently valid.");
				}
				else
				{
					Console.WriteLine("This claim is currently not valid.");
				}
			}
			ReturnerOfValidation();



			_claimRepo.AddClaimToList(newClaim);

		}


		//view current list of claims
		private void DisplayAllClaims()
		{
			Console.Clear(); //clears menu before we see all content

			// we set listOfContent (a list of Claim objects) equal to the persistant repository _claimRepo,
			// which is equal to out list inside our repository, which has access to CRUD methods
			List<Claim> listOfContent = _claimRepo.GetClaimsList();

			//foreach (Claim content in listOfContent)
			//{
			// Console.WriteLine($"Title: {content.ClaimID}\n" +
			// $"Desc: { content.Description}");
			//}

			Console.WriteLine("{0,-10} {1,-12} {2,-25} {3,-8} {4,-18} {5,-10} {6, 10}", "Claim ID", "Claim Type", "Claim Description", "Claim Amount", "Date of Incident", "Date of Claim", "Is Valid");
			foreach (Claim claims in listOfContent)
			{
				Console.WriteLine("{0,-10} {1,-12} {2,-25} {3,-8} {4,-18} {5,-10} {6, 10}", claims.ClaimID, claims.TypeOfClaim, claims.Description, claims.ClaimAmount, claims.DateOfIncident, claims.DateOfClaim, claims.IsValid);
			}

			PressAnyKeyToContinue();


		}



		//view existing claim by ID
		private void TakeCareOfClaim()
		{
			DisplayAllClaims();

			//prompt user to give me an ID
			Console.WriteLine("\n" +
			"Enter ID number of the claim you would like to take care of");

			int inputId = GetUserInputAsInt();


			//get the user's input
			int inputUserId = inputId;

			//find content by the id
			Claim content = _claimRepo.GetClaimByID(inputId);

			// display said content if it isnt null
			if (content != null)
			{
				Console.WriteLine($"Claim ID: {content.ClaimID}\n" +
				$"Claim Type: { content.TypeOfClaim}\n" +
				$"Description: {content.Description}\n" +
				$"Amount: {content.ClaimAmount}\n" +
				$"Date of Incident: {content.DateOfIncident}\n" +
				$"Date of Claim: {content.DateOfClaim}\n" +
				$"Is Valid: {content.IsValid}");
			}
			else
			{ //if not valid id number, then we return to main menu
				Console.WriteLine("No Claims with that ID, going back to main menu now");
				PressAnyKeyToContinue();
				Menu();
			}



			//next we ask the user if they would like to take care of the claim
			Console.WriteLine("\n" +
			"Do you want to take care of this claim now (y/n)?");

			string userResponse = Console.ReadLine().ToLower();     //we take in the user's input and converts it to lowercase
			if (userResponse == "y")  //if yes
			{
				_claimRepo.RemoveClaimFromList(content.ClaimID);   //we use our CRUD remove method to "take care of" and remove claim by ID number
				Console.Clear();
				Console.WriteLine("This claim has now been taken care of.\n" +      //output confirmation
				"The claim has now been removed from the system.");
				PressAnyKeyToContinue();
				Menu();   //returns to main menu
			}
			else
			{ //if user input was not y, then we return to the main menu
				Console.WriteLine("Ok going back to main menu now");
				PressAnyKeyToContinue();
				Menu();
			}


		}

		//}

		////update existing content

		//private void UpdateExistingContent()
		//{
		// //display all content
		// DisplayAllClaims();

		// //ask for the title of the content to update
		// Console.WriteLine("Enter the title of the content you'd like to update");

		// //get that title
		// string oldTitle = Console.ReadLine();

		// //we will build a new object
		// Claim newContent = new Claim(); //we declare it first so that we can then use the property of object for user input

		// //title
		// Console.WriteLine("Enter the title for the content");
		// newContent.ClaimID = Console.ReadLine();

		// //description
		// Console.WriteLine("Enter the description for the content");
		// newContent.Description = Console.ReadLine();

		// //maturity rating
		// Console.WriteLine("Enter the maturity rating for the content: (G, PG, PG-13, etc");
		// newContent.ClaimAmount = Console.ReadLine();

		// //star rating
		// Console.WriteLine("Enter the star rating for the content: (5.8, 10, 1.5, etc)");
		// string starsAsString = Console.ReadLine();
		// newContent.DateOfIncident = double.Parse(starsAsString);

		// //is familly friendly\
		// Console.WriteLine("Is this content family friendly?  (y/n)");
		// string familyFriendlyString = Console.ReadLine().ToLower();

		// if (familyFriendlyString == "y")
		// {
		// newContent.IsValid = true;
		// }
		// else
		// {
		// newContent.IsValid = false;
		// }

		// //genretype
		// Console.WriteLine("Enter the genre number: \n" +
		// "1. Horror\n " +
		// "2. RomCom\n " +
		// "3. SciFi\n " +
		// "4. Documentary\n " +
		// "5. Drama\n " +
		// "6. Action");

		// string genreAsString = Console.ReadLine();
		// int genreAsInt = int.Parse(genreAsString);
		// newContent.TypeOfClaim = (ClaimType)genreAsInt;    //casting integer into enum by the enum's numerical value

		// //verify the update worked
		// //repo update method returns a bool if it was updated, if old title was not found (null) then returns false
		// bool wasUpdated =_claimRepo.UpdateExistingClaim(oldTitle, newContent);

		// if (wasUpdated)
		// {
		// Console.WriteLine("Content successfully updated!");
		// }
		// else
		// {
		// Console.WriteLine("Could not update content/content to be replaced could not be found.");
		// }

		//}

		//delete existing content
		private void DeleteExistingClaim()
		{
			DisplayAllClaims();

			//get the id we want to delete
			Console.WriteLine("\nEnter the ID of the claim you would like to remove.");

			string input = Console.ReadLine();
			int idInput = int.Parse(input);

			//call the delete method
			//calls repository method of content repo and deletes, if it was deleted it returns true, if not then false, so we make it a boolean
			bool wasDeleted = _claimRepo.RemoveClaimFromList(idInput);

			//if the content was deleted, say so
			if (wasDeleted)
			{
				Console.WriteLine("The content was successfully deleted");
			}
			else
			{
				Console.WriteLine("Content could not be deleted");
			}

			//otherwise state it could not be deleted
		}


		//Seed Method  --> will seed the content list so that we can have some stuff on the list, not an empty one
		private void SeedContentList()
		{
			Claim seed1 = new Claim(1735, "Car was in collision", 2250, DateTime.Parse("05/15/2021"), DateTime.Parse("05/21/2021"), true, ClaimType.Car);
			Claim seed2 = new Claim(1738, "Hurricane tore roof off home's room", 28500, DateTime.Parse("05/18/2021"), DateTime.Parse("06/25/2021"), false, ClaimType.Home);
			Claim seed3 = new Claim(1832, "Home was broken into", 3500, DateTime.Parse("05/19/2021"), DateTime.Parse("05/26/2021"), true, ClaimType.Theft);

			_claimRepo.AddClaimToList(seed1);
			_claimRepo.AddClaimToList(seed2);
			_claimRepo.AddClaimToList(seed3);
		}

		//helper methods

		public int NumberOfDaysBetweeenIncidentAndClaim(DateTime incident, DateTime claim) //returns the int number of days between the incident and claim
		{
			DateTime incidentDate = incident; //incident datetime
			DateTime claimDate = claim;  //claim datetime
			int totalDays = (claimDate.Date - incidentDate.Date).Days;  //the difference between the two dates expressed as an int
			return totalDays;   //return the int
		}

		public bool IsValid(int numDays)  //receives the number of days after incident that claim was made
		{
			if (numDays <= 30)    //if it was 30 days or less
			{
				return true;
			}
			else   //if it was over 30 days
			{
				return false;
			}
		}


		public void DisplayClaims(List<Claim> _allClaims)
		{
			Console.WriteLine("{0,-13} {1,-14} {2,-20} {3,-16} {4,-20} {5,-17} {6, -15}", "Claim ID", "Claim Type", "Claim Description", "Claim Amount", "Date of Incident", "Date of Claim", "Is Valid");
			foreach (Claim claims in _allClaims)
			{
				Console.WriteLine("{0,-13} {1,-14} {2,-20} {3,-16} {4,-20} {5,-17} {6, -15}", claims.ClaimID, claims.TypeOfClaim, claims.Description, claims.ClaimAmount, claims.DateOfIncident, claims.DateOfClaim, claims.IsValid);
			}
		}



		//private void DevelopmentTeamMenu()
		//{
		// bool continueDevTeamMenu = true;

		// while (continueDevTeamMenu)
		// {
		// Console.Clear();
		// Console.WriteLine("DEVELOPMENT TEAM MENU\n" +
		// "1. Add a development team\n" +
		// "2. View all development teams\n" +
		// "3. View a specific development team and it's developers\n" +
		// "4. Update a development team name\n" +
		// "5. Add multiple developers to a development team\n" +
		// "6. Remove developers from a development team\n" +
		// "7. Go back to main menu.\n");

		// string userChoice = Console.ReadLine();
		// Console.Clear();
		// switch (userChoice)
		// {
		// //Add a dev team
		// case "1":
		// DevelopmentTeam devTeamToAdd = new DevelopmentTeam();
		// DevTeamRepo.AddObjectToRepository(devTeamToAdd);
		// devTeamToAdd.AddDevelopers(new List<Developer>());


		// Console.WriteLine("Please provide an ID for the development team:");
		// devTeamToAdd.ID = GetUserInputAsInt();

		// Console.WriteLine("What would you like to name this development team?");
		// devTeamToAdd.Name = Console.ReadLine();

		// Console.WriteLine("Would you like to add any developers to the dev team? Y/N");
		// string userReply = Console.ReadLine().ToUpper();
		// if (userReply == "Y")
		// {
		// AddMultipleDevelopersToTeam(devTeamToAdd.ID);
		// }
		// Console.WriteLine("Your Dev team has been added.");

		// PressAnyKeyToContinue();
		// break;
		// case "2":
		// PrintDevelopmentTeamsInRepo();

		// PressAnyKeyToContinue();
		// break;
		// case "3":
		// Console.WriteLine("Please select a development team of which you would like to see the developers.");
		// PrintDevelopmentTeamsInRepo();

		// int userSelection = GetUserInputAsInt();

		// if (userSelection == 0 || !DevRepo.RepositoryContainsObject(userSelection))
		// {
		// Console.WriteLine("Please try again. Entry does not contain a valid dev team number.");
		// }
		// else
		// {
		// Console.Clear();
		// DevelopmentTeam userSelectedDevTeam = DevTeamRepo.GetDevelopmentTeamById(userSelection);
		// List<Developer> devTeamDevList = userSelectedDevTeam.DevTeam;
		// if (devTeamDevList.Count > 0)
		// {
		// Console.WriteLine($"ID: {userSelectedDevTeam.ID}, Name: {userSelectedDevTeam.Name}\n" +
		// $"Dev Team Members:");
		// foreach (Developer dev in devTeamDevList)
		// {
		// Console.WriteLine($"ID: {dev.ID}, Name: {dev.Name}, Has Pluralsight Access: {dev.HasPluralsightAccess}");
		// }
		// }
		// else
		// {
		// Console.WriteLine($"Sorry, but {userSelectedDevTeam.Name} has no developers assigned.");
		// }
		// }

		// PressAnyKeyToContinue();
		// break;
		// case "4":
		// Console.WriteLine("Please select a development team of which you would like to update the name.");
		// PrintDevelopmentTeamsInRepo();

		// userSelection = GetUserInputAsInt();
		// DevelopmentTeam devTeamToUpdateName = DevTeamRepo.GetDevelopmentTeamById(userSelection);
		// if (userSelection == 0 || !DevRepo.RepositoryContainsObject(userSelection))
		// {
		// Console.WriteLine("Please try again. Entry does not contain a valid dev team number.");
		// }
		// else
		// {
		// Console.WriteLine("What would you like the new name of the team to be?");
		// string devTeamsNewName = Console.ReadLine();
		// devTeamToUpdateName.Name = devTeamsNewName;
		// }

		// break;
		// case "5":
		// Console.WriteLine("Please select a development team of which you would like to add developers");
		// PrintDevelopmentTeamsInRepo();

		// userSelection = GetUserInputAsInt();
		// DevelopmentTeam devTeamToUpdateDevs = DevTeamRepo.GetDevelopmentTeamById(userSelection);
		// AddMultipleDevelopersToTeam(devTeamToUpdateDevs.ID);

		// Console.WriteLine("All selected developers have been added.");
		// PressAnyKeyToContinue();

		// break;
		// case "6":
		// Console.WriteLine("Please select a development team of which you would like to remove developers");
		// PrintDevelopmentTeamsInRepo();

		// userSelection = GetUserInputAsInt();
		// DevelopmentTeam devTeamToRemoveDevs = DevTeamRepo.GetDevelopmentTeamById(userSelection);
		// List<Developer> devsOnDevTeam = devTeamToRemoveDevs.DevTeam;

		// Console.WriteLine("Please select the ID of the developer that you would like to remove.");
		// foreach (Developer dev in devsOnDevTeam)
		// {
		// Console.WriteLine($"ID: {dev.ID}, Name: {dev.Name}");
		// }
		// userSelection = GetUserInputAsInt();
		// Developer userChosenDeveloper = (Developer)DevRepo.GetBusinessObjectsById(userSelection);
		// if (devsOnDevTeam.Contains(userChosenDeveloper))
		// {
		// devTeamToRemoveDevs.RemoveDeveloperById(userSelection);
		// DevsOnTeam.Remove(userSelection);
		// Console.WriteLine("Developer has been removed.");
		// }
		// else
		// {
		// Console.WriteLine("Your developer ID selection did not match a developer in that dev team.");
		// }

		// PressAnyKeyToContinue();
		// break;
		// case "7":
		// continueDevTeamMenu = false;
		// break;
		// default:
		// Console.WriteLine("Option invalid. Returning to the development team menu.");
		// PressAnyKeyToContinue();
		// break;
		// }
		// }
		//}

		//Helper Methods
		private void PressAnyKeyToContinue()
		{
			Console.WriteLine("\n" +
			"Press any key to continue ...");
			Console.ReadKey();
		}

		public int GetUserInputAsInt()
		{
			int returnValue = 0;
			bool keepGoing = true;
			while (keepGoing)
			{
				string userInput = Console.ReadLine();
				try
				{
					returnValue = int.Parse(userInput);   //try if the user input can be parsed into an integer
				}
				catch
				{
					returnValue = 0;   //if it cannot, then we catch it by keeping returnValue at 0
				}

				if (returnValue == 0)  //if it stayed at 0 then we need to remind the user to input a valid integer

				{
					Console.Clear();
					Console.WriteLine("Please provide a valid integer for the response.");
				}
				else
				//else stop
				{
					keepGoing = false;
				}
			}
			return returnValue;  //returns the int value
		}

		//class
	}
}//namespace