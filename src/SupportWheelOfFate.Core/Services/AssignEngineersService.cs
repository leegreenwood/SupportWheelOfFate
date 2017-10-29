using System;
using System.Collections.Generic;
using System.Linq;

namespace SupportWheelOfFate.Core.Services
{
    public static class AssignEngineersService
    {
        /// <summary>
        /// Gets the assigned engineers for the given day
        /// </summary>
        /// <param name="engineers">A List of all available Engineers</param>
        /// <param name="supportDate">The date required for support</param>
        /// <returns>A List of the assigned engineers</returns>
        /// <remarks>Handles business rules for max shifts per day, and non-consecutive days.</remarks>
        public static List<Engineer> GetAssignedEngineers(List<Engineer> engineers, DateTime supportDate)
        {
            // Correctly parse the Engineers List
            var assignedEngineers = engineers.Where(e => e.DateLastShift == supportDate.ToString("yyyy-MM-dd")).ToList();

            if (assignedEngineers.Count == 2)
            {
                // There are 2 engineers already assigned, return them
                return assignedEngineers;
            }
            else 
            {
                // Assign some engineers for this specified day

                // Create a shortlist where the engineers didn't have a shift the previous day
                var shortlist = engineers.Where(e => supportDate.Date - Convert.ToDateTime(e.DateLastShift).Date != TimeSpan.FromDays(1)).ToList();

                var rnd = new Random();

                // Select and remove the first Engineer
                var firstEngineer = shortlist[rnd.Next(shortlist.Count)];
                shortlist.Remove(firstEngineer);

                // Assign the first Engineer
                firstEngineer.DateLastShift = supportDate.Date.ToString("yyyy-MM-dd");
                firstEngineer.TimeLastShift = "AM";
                firstEngineer.UpdateEngineer = true;

                // Select and remove the second Engineer
                var secondEngineer = shortlist[rnd.Next(shortlist.Count)];
                shortlist.Remove(secondEngineer);

                // Assign the second Engineer
                secondEngineer.DateLastShift = supportDate.Date.ToString("yyyy-MM-dd");
                secondEngineer.TimeLastShift = "PM";
                secondEngineer.UpdateEngineer = true;
                
                return new List<Engineer>
                {
                    firstEngineer,
                    secondEngineer
                };                
            }            
        }
    }
}
