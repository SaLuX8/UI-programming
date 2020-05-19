using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Windows.Controls;
using System.Windows;
using System.Runtime.InteropServices.ComTypes;
using WpfWODCoach.Model;


namespace WpfWODCoach.Viewmodel
{
    public class ViewModel
    {
        // ---------------------------------------------------------
        // NO REF (future feature): Loads athletes based on coachId
        // ---------------------------------------------------------
        public static List<Athlete> LoadAthletesByCoach(int coachId)
        {
            WODCoachEntities ctx = new WODCoachEntities();
            ctx.Athlete.Load();
            var athletes = ctx.Athlete.Where(x => x.Coach_idCoach == coachId).ToList();
            return athletes;
        }

        // ---------------------------------------------------------
        // NO REF (future feature) : Loads list of Coaches
        // ---------------------------------------------------------
        public static List<Coach> LoadCoaches()
        {
            WODCoachEntities ctx = new WODCoachEntities();
            ctx.Coach.Load();
            return ctx.Coach.ToList();
        }

        // ---------------------------------------------------------
        // NO REF (future feature) : Loads list of all Rates
        // ---------------------------------------------------------
        public static List<Rate> LoadRating()
        {
            WODCoachEntities ctx = new WODCoachEntities();
            ctx.Rate.Load();
            return ctx.Rate.ToList();
        }

        // ---------------------------------------------------------
        // ATHLETEMAIN: Saves the state of wod (movement), done (true) or not done (false).
        // ---------------------------------------------------------
        public static void SaveDoneWod(Wod selectedWod)
        {
            using (var ctx = new WODCoachEntities())
            {
                Wod wod = ctx.Wod.First(i => i.idWod == selectedWod.idWod);
                wod.done = selectedWod.done;
                ctx.SaveChanges();
            }
        }

        // ---------------------------------------------------------
        // ATHLETEMAIN: Adds new or updates Rate for wod (movement)
        // ---------------------------------------------------------
        public static void SaveRating(int number, int wodId, int athleteId, float rating, string comment)
        {
            using (var ctx = new WODCoachEntities())
            {
                if (number == 0)
                {
                    Rate rate = new Rate();
                    rate.athlete_id = athleteId;
                    rate.wod_id = wodId;
                    rate.rating = rating;
                    rate.comment = comment;
                    ctx.Rate.Add(rate);
                    ctx.SaveChanges();
                }
                else
                {
                    Rate rate = ctx.Rate.First(i => i.wod_id == wodId);
                    rate.rating = rating;
                    rate.comment = comment;
                    ctx.SaveChanges();
                }
            }
        }

        // ---------------------------------------------------------
        // ATHLETEMAIN / ADMIN / COACHMAIN : Loads all athletes
        // ---------------------------------------------------------
        public static List<Athlete> LoadAthletes()
        {
            WODCoachEntities ctx = new WODCoachEntities();
            ctx.Athlete.Load();
            return ctx.Athlete.ToList();
        }

        // ---------------------------------------------------------
        // ATHLETEMAIN / COACHMAIN : Returns list of movements (Wods) from certain athlete and date 
        // ---------------------------------------------------------
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
                wod = ctx.Wod.Where(x => (x.idAthlete == athleteId & x.date == dateTime)).ToList();
            }
            return wod;
        }

        // ---------------------------------------------------------
        // ATHLETEMAIN / COACHMAIN / MOVEMENTMAIN : Loads list of movements (Wods)
        // ---------------------------------------------------------
        public static List<Wod> LoadWods()
        {
            WODCoachEntities ctx = new WODCoachEntities();
            ctx.Wod.Load();
            return ctx.Wod.ToList();
        }

        // ---------------------------------------------------------
        // ADMIN: Add or update athlete information 
        // ---------------------------------------------------------
        public static void SaveAthlete(int number, Athlete selectedAthlete)
        {
            using (var ctx = new WODCoachEntities())
            {
                if (number == 0)
                {
                    Athlete athlete1 = new Athlete();
                    athlete1.fullname = selectedAthlete.fullname;
                    athlete1.telNumber = selectedAthlete.telNumber;
                    athlete1.Coach_idCoach = selectedAthlete.Coach_idCoach;
                    ctx.Athlete.Add(athlete1);
                    ctx.SaveChanges();
                }
                else
                {
                    Athlete athlete1 = ctx.Athlete.First(i => i.idAthlete == number);
                    athlete1.fullname = selectedAthlete.fullname;
                    athlete1.telNumber = selectedAthlete.telNumber;
                    athlete1.Coach_idCoach = selectedAthlete.Coach_idCoach;
                    ctx.SaveChanges();
                }
            }
        }

        // ---------------------------------------------------------
        // ADMIN: DELETE Athlete
        // ---------------------------------------------------------
        public static void DeleteAthlete(Athlete selectedAthlete)
        {
            using (var ctx = new WODCoachEntities())
            {
                Athlete athlete1 = ctx.Athlete.First(i => i.idAthlete == selectedAthlete.idAthlete);
                ctx.Athlete.Remove(athlete1);
                ctx.SaveChanges();
            }
        }

        // ---------------------------------------------------------
        // COACHMAIN: Removes connection from between a wod (movement) and athlete. Nothing is deleted. Used in CoachMain.
        // ---------------------------------------------------------
        public static void RemoveWodFromAthlete(int wodId, int athlete_id)
        {
            using (var ctx = new WODCoachEntities())
            {
                Wod wod = ctx.Wod.First(i => i.idWod == wodId);
                wod.idAthlete = null;
                wod.repsCount = 0;
                wod.roundCount = 0;
                ctx.SaveChanges();
            }
        }

        // ---------------------------------------------------------
        // COACHMAIN: Method adds WOD to athlete to certain date
        // ---------------------------------------------------------
        public static void AddWodToAthlete(int WodId, int athleteId, DateTime dateTime, Wod movement, int reps, int rounds, string comment, int level)
        {
            using (var ctx = new WODCoachEntities())
            {
                // if value sent in property wodId is 0 it means nothing was selected from datagrid
                if (WodId == 0) // insert
                {
                    Wod wod;
                    if(ctx.Wod.First(i => i.idWod == movement.idWod)!=null)     // Check if movement (wod) was selected from combobox
                    {
                        wod = ctx.Wod.First(i => i.idWod == movement.idWod);    // if yes, it is NOT new movement (wod)
                        wod.movementName = movement.movementName;
                        wod.date = dateTime;
                        wod.idAthlete = athleteId;
                        wod.repsCount = reps;
                        wod.roundCount = rounds;
                        wod.comment = comment;
                        wod.level = level;
                        wod.done = false;
                    }
                    else                                                        // if wod from combobox was not selected, it is a NEW movement (wod)
                    {
                        wod = new Wod();
                        wod.movementName = movement.movementName;
                        wod.date = dateTime;
                        wod.idAthlete = athleteId;
                        wod.repsCount = reps;
                        wod.roundCount = rounds;
                        wod.comment = comment;
                        wod.level = level;
                        wod.done = false;
                        ctx.Wod.Add(wod);
                    }
                }
                else                // if value sent with wodId is not 0 then search for Wod with Id
                {
                    Wod wod = ctx.Wod.First(i => i.idWod == WodId);
                    wod.movementName = movement.movementName;
                    wod.date = dateTime;
                    wod.idAthlete = athleteId;
                    wod.repsCount = reps;
                    wod.roundCount = rounds;
                    wod.comment = comment;
                    wod.level = level;
                }
                ctx.SaveChanges();
            }
        }

        // ---------------------------------------------------------
        // MOVEMENTMAIN: Add or update Wods (movements)
        // ---------------------------------------------------------
        public static void SaveWod(int number, Wod selectedWod)
        {
            using (var ctx = new WODCoachEntities())
            {
                if (number == 0)
                {
                    Wod wod = new Wod();
                    wod.movementName = selectedWod.movementName;
                    wod.level = selectedWod.level;
                    wod.done = false;
                    ctx.Wod.Add(wod);
                    ctx.SaveChanges();
                }
                else
                {
                    Wod wod = ctx.Wod.First(i => i.idWod == number);
                    wod.movementName = selectedWod.movementName;
                    wod.level = selectedWod.level;
                    wod.done = selectedWod.done;
                    ctx.SaveChanges();
                }
            }
        }

        // ---------------------------------------------------------
        // MOVEMENTMAIN: Delete Wod (Movement) identified by wodId
        // ---------------------------------------------------------
        public static void DeleteWod(int wodId)
        {
            using (var ctx = new WODCoachEntities())
            {
                Wod wod = ctx.Wod.First(i => i.idWod == wodId);
                if (wod.Rate.Count > 0)
                {
                    Rate rate = ctx.Rate.First(i => i.wod_id == wodId);
                    ctx.Rate.Remove(rate);
                }
                ctx.Wod.Remove(wod);
                ctx.SaveChanges();
            }
        }
    }
}
