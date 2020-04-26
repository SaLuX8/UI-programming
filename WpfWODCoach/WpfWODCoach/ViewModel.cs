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



    }
}
