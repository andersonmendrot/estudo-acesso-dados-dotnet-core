using System;
using System.Linq;
using ConsoleTables;
using MovieApp.Entities;
using MovieApp.Extensions;
using MovieApp.Models;

namespace MovieApp
{
    public static class Module1Helper
    {
        internal static void SelectList()
        {
            MoviesContext moviesContext = MoviesContext.Instance;
            
            var actors = moviesContext.Actors.Select(a => a.Copy<Actor, ActorModel>());
            Console.WriteLine(actors.Count());
            ConsoleTable.From(actors).Write();

            var films = moviesContext.Films.Select(f => f.Copy<Film, FilmModel>());
            ConsoleTable.From(films).Write();

        }

        internal static void SelectById()
        {
            var moviesContext = MoviesContext.Instance;

            Console.WriteLine("Enter an Actor ID: ");
            int actorId = int.Parse(Console.ReadLine());
            var actor = moviesContext.Actors.SingleOrDefault(a => a.ActorId == actorId);

            if(actor == null){
                Console.WriteLine($"Actor with ID {actorId} not found");
            }
            else {
                Console.WriteLine($"ID: {actor.ActorId}\nName: {actor.FirstName} {actor.LastName}");
            }

            /////////////

            Console.WriteLine("Enter an Film ID: ");
            int filmId = int.Parse(Console.ReadLine());

            var film = moviesContext.Films.SingleOrDefault(f => f.FilmId == filmId);

            if(film == null){
                Console.WriteLine($"Film with ID: {filmId} not found");
            }

            else{
                Console.WriteLine($"ID: {film.FilmId}\nName: {film.Title}");
            }
        }
            
        internal static void CreateItem()
        {
            Console.WriteLine("Add an Actor");
            Console.WriteLine("Enter a first name: ");
            var firstName = Console.ReadLine();
            Console.WriteLine("Enter a last name: ");
            var lastName = Console.ReadLine();

            var moviesContext = MoviesContext.Instance;

            var actor = new Actor {FirstName = firstName, LastName = lastName};

            moviesContext.Actors.Add(actor);
            moviesContext.SaveChanges();

            //Func delegate
            //Func<Actor, bool> isActorId = a => a.ActorId == actor.ActorId;
            //converted to 
            //System.Linq.Expressions.Expression<Func<Actor, bool>> isActorIdExp = a => a.ActorId == actor.ActorId;

            var actors = moviesContext.Actors
            .Where(a => a.ActorId == actor.ActorId)
            .Select(a => a.Copy<Actor, ActorModel>());

            ConsoleTable.From(actors).Write();
        }

        internal static void UpdateItem()
        {
            var moviesContext = MoviesContext.Instance;

            Console.WriteLine("Update an actor");
            Console.WriteLine("Enter an Actor ID: ");
            var actorId = int.Parse(Console.ReadLine());

            var actor = moviesContext.Actors.SingleOrDefault(a => a.ActorId == actorId);

            if(actor == null)
            {
                Console.WriteLine($"Actor with id {actorId} not found");
            }
            else
            {
                ActorModel[] actorArray = new ActorModel[]{actor.Copy<Actor, ActorModel>()};
                ConsoleTable.From(actorArray).Write();

                Console.WriteLine("Enter the first name: ");
                var firstName = Console.ReadLine().Trim();

                Console.WriteLine("Enter the last name: ");
                var lastName = Console.ReadLine().Trim();

                actor.FirstName = firstName;
                actor.LastName = lastName;

                moviesContext.SaveChanges();
                
                //IQueryable deriva de IEnumerable
                //IQueryable é útil quando você esta consultando uma coleção que foi carregada usando LINQ ou Entity Framework e você quer aplicar um filtro nesta coleção. (ou seja, a usamos para dados que não estão em memória)
                //IEnumerable é mais usada com dados que já estão em memória, como vetores, listas, etc
                IQueryable<ActorModel> actors = moviesContext.Actors
                                        .Where(a => a.ActorId == actor.ActorId)
                                        .Select(a => a.Copy<Actor, ActorModel>());

                ConsoleTable.From(actors).Write();
            }
        }
        
        internal static void DeleteItem()
        {
            var moviesContext = MoviesContext.Instance;

            Console.WriteLine("Enter an actor ID: ");
            var actorId = int.Parse(Console.ReadLine());

            var actor = moviesContext.Actors.SingleOrDefault(a => a.ActorId == actorId);

            if(actor == null){
                Console.WriteLine($"Actor with id {actorId} not found");
            }

            else{
                Console.WriteLine("Existing actors");
                WriteActors();

                moviesContext.Actors.Remove(actor);
                moviesContext.SaveChanges();

                Console.WriteLine("With actor removed");
                WriteActors();
            }
        }

        private static void WriteActors()
        {
            var actors = MoviesContext.Instance.Actors
                            .Select(a => a.Copy<Actor, ActorModel>());
            ConsoleTable.From(actors).Write();
        }
    }
}



