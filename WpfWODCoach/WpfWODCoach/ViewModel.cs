using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Windows.Controls;
using System.Windows;

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
        public static void AddWodToAthlete(int WodId, int athleteId, DateTime dateTime, string movement, int reps, int rounds, string comments)
        {
            using (var ctx = new WODCoachEntities())
            {
                // if value sent to property wodId is 0 it means its new WOD
                if (WodId == 0) // insert
                {
                    var wod = new Wod();
                    wod.movementName = movement;
                    wod.date = dateTime;
                    wod.idAthlete = athleteId;
                    wod.repsCount = reps;
                    wod.roundCount = rounds;
                    wod.comment = comments;
                    wod.done = false;
                    ctx.Wod.Add(wod);
                }
                else
                {
                    Wod wod = ctx.Wod.First(i => i.idWod == WodId);
                    //var wod = ctx.Wod.Where(x => x.idWod == WodId) as Wod;

                    wod.movementName = movement;
                    wod.date = dateTime;
                    wod.idAthlete = athleteId;
                    wod.repsCount = reps;
                    wod.roundCount = rounds;
                    wod.comment = comments;
                    
                }
                ctx.SaveChanges();
            }
        }
        // Saves the state of wod, done (true) or not done (false).
        public static void SaveDoneWod(int WodId, bool state)
        {
            using (var ctx = new WODCoachEntities())
            {
                Wod wod = ctx.Wod.First(i => i.idWod == WodId);
                if (state == false) wod.done = null;
                else wod.done = state;
                ctx.SaveChanges();
            }
        }








    }
}
