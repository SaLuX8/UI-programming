using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Windows.Controls;

namespace WpfWODCoach
{
    public class ViewModel
    {
       

        // public static DataGrid dataGrid; // tsekkaa tämä 8:53 "www.youtube.com/watch?v=VGRvi4-1VhA"
        public static List<Athlete> LoadAthletes()
        {
            WODCoachEntities ctx = new WODCoachEntities();
            ctx.Athlete.Load();
            return ctx.Athlete.ToList();
        }

        //Loads athletes based on coachId
        public static List<Athlete> LoadAthletesByCoach(int coachId)
        {
            WODCoachEntities ctx = new WODCoachEntities();
            ctx.Athlete.Load();
            var athletes = ctx.Athlete.Where(x => x.Coach_idCoach == coachId).ToList();
            return athletes;
        }

        public static List<Wod> LoadWodsByAthlete(int athleteId, DateTime dateTime)
        {
            WODCoachEntities ctx = new WODCoachEntities();
            ctx.Wod.Load();
            var wod = ctx.Wod.ToList(); 
            if (dateTime == null)
            {
                wod = ctx.Wod.Where(x => x.idAthlete == athleteId).ToList();
            }
            else
            {
                wod = ctx.Wod.Where(x => (x.idAthlete == athleteId & x.date==dateTime)).ToList();
            }
            return wod;
        }

        public static List<Coach> LoadCoaches()
        {
            WODCoachEntities ctx = new WODCoachEntities();
            ctx.Coach.Load();
            return ctx.Coach.ToList();
        }

        public static List<Wod> LoadWods()
        {
            WODCoachEntities ctx = new WODCoachEntities();
            ctx.Wod.Load();
            return ctx.Wod.ToList();
        }

        // Method adds WOD to athlete to certain date
        public static void AddWodToAthlete(int athleteId, DateTime dateTime, string movement, int reps, int rounds, string comments)
        {
            WODCoachEntities ctx = new WODCoachEntities();
            var wod = new Wod() { movementName = movement, date = dateTime, idAthlete = athleteId, repsCount = reps, roundCount = rounds, comment= comments};
            ctx.Wod.Add(wod);
            ctx.SaveChanges();
            

        }



    }
}
