using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KomodoClaims_Repo;

namespace KomodoClaims_Repo
{
	public enum ClaimType   
	{
		Car = 1,  //because it defaults at 0
		Home,
		Theft
	}
	//POCO -- Plaing old Csharp object
	//Simple object that holds data
	public class Claim
	{
		//properties
		public int ClaimID { get; set; }
		public string Description { get; set; }
		public double ClaimAmount { get; set; }
		public DateTime DateOfIncident { get; set; }
		public DateTime DateOfClaim { get; set; }
		public bool IsValid { get; set; }
		public ClaimType TypeOfClaim { get; set; }

		//constructors
		public Claim() { }

		//cant go out can only go in,, scope
		public Claim(int claimID, string description, double claimAmount, DateTime dateOfIncident, DateTime dateOfClaim, bool isValid, ClaimType typeOfClaim)
		{
			ClaimID = claimID;
			Description = description;
			ClaimAmount = claimAmount;
			DateOfIncident = dateOfIncident;
			DateOfClaim = dateOfClaim;
			IsValid = isValid;
			TypeOfClaim = typeOfClaim;
		}
	}
}
