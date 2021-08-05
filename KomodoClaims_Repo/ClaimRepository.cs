using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomodoClaims_Repo
{
	public class ClaimRepository
	{
		public readonly List<Claim> _listOfClaims = new List<Claim>();   //field made up of the claim objects, not repos
																		 //field usables in all crud methods, they can all use the same list in all methods, persisting object
																		 //the methods need to be used outside, public
		//create
		public void AddClaimToList(Claim content) //add claim to list, building entryways into our class
		{
			_listOfClaims.Add(content);  //anything with an underscore and camelcase is a field
		}

		//read
		public List<Claim> GetClaimsList()   //returns whole list
		{
			return _listOfClaims;
		}

		//update
		public bool UpdateExistingClaim(int originalID, Claim newContent) //object in second param, same object new values
																		  //this replaces one object in the list for another
		{
			//find the old object
			Claim oldContent = GetClaimByID(originalID);

			//update content with new content,, is  possible we may get a null ID if it doesnt exist
			if (oldContent != null)
			{
				oldContent.ClaimID = newContent.ClaimID;
				oldContent.Description = newContent.Description;
				oldContent.ClaimAmount = newContent.ClaimAmount;
				oldContent.DateOfIncident = newContent.DateOfIncident;
				oldContent.IsValid = newContent.IsValid;
				oldContent.TypeOfClaim = newContent.TypeOfClaim;
				return true;
			}
			else
			{
				return false;
			}
		}

		//delete
		public bool RemoveClaimFromList(int id)
		{
			Claim content = GetClaimByID(id);
			if (content == null)   //check this first, if content does't exist
			{
				return false;
			}
			int initialCount = _listOfClaims.Count;  //count is a property of the list that will get us the number of elements in a list
			_listOfClaims.Remove(content); //remove it

			//check if it was removed
			if (initialCount > _listOfClaims.Count)  //if the initial count is greater than the current count, then we did remove is
			{
				return true;
			}
			else
			{
				return false;  //if content was not removed
			}
		}

		//helper method
		public Claim GetClaimByID(int id)   //returns an object of the list
		{
			foreach (Claim content in _listOfClaims)  //for each object in list
			{
				if (content.ClaimID == id)     //helper methods which get with an id
				{
					return content;  //if equals the id of object requested, then return
				}
			}
			return null; //if we find it we return, if not return null
		}
	}
}