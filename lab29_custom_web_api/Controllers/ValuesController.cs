using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using lab29_custom_web_api.Models;

namespace lab29_custom_web_api.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

        public List<Movie> GetTitles()
        {
            FilmDBEntities2 db = new FilmDBEntities2();
            List<Movie> Titles = db.Movies.ToList();
            return Titles;
        }

        public List<Movie> GetMoviesByCategory(string Category)
        {
            FilmDBEntities2 db = new FilmDBEntities2();
            List<Movie> CatList = (from p in db.Movies
                                  where p.Category.Contains(Category)
                                  select p).ToList();
            return CatList;
        }

        public Movie GetRandomMovie()
        {
            FilmDBEntities2 db = new FilmDBEntities2();
            List<Movie> Titles = db.Movies.ToList();
            var rand = new Random();
            var movie = Titles.ElementAt(rand.Next(Titles.Count()));

            return movie;
        }

        public Movie GetRandomMovieFromCat(string category)
        {
            FilmDBEntities2 db = new FilmDBEntities2();
            List<Movie> MovieList = (from p in db.Movies
                                  where p.Category.Contains(category)
                                  select p).ToList();
            var rand = new Random();
            var movie = MovieList.ElementAt(rand.Next(MovieList.Count()));

            return movie;
        }

        public List<Movie> GetRandomMovieListByQuantity(int quantity)
        {
            FilmDBEntities2 db = new FilmDBEntities2(); // db created
            List<Movie> Titles = db.Movies.ToList();
            List<Movie> RandoMovieList = new List<Movie>(quantity); // new list created, based on quantity
            var rand = new Random();
            for (int i = 0; i < quantity; i++)
            {
                Movie newmovie = Titles.ElementAt(rand.Next(Titles.Count()));
                if (RandoMovieList.Contains(newmovie))
                {
                    i--;
                }
                else
                {
                    RandoMovieList.Add(newmovie);
                }
            }
            return RandoMovieList;
        }

        public List<string> GetCategoryList()
        {
            FilmDBEntities2 db = new FilmDBEntities2();
            List<string> CatList = (from m in db.Movies
                                   where m.Category != null
                                   select m.Category).Distinct().ToList();
            return CatList;
        }

        public Movie GetMovieInfo(string title)
        {
            FilmDBEntities2 db = new FilmDBEntities2();


            Movie movie = (from m in db.Movies
                              where m.Title == title
                              select m).Single();
            return movie;
        }

        public Movie GetMovieInfoByKeyword(string title)
        {
            FilmDBEntities2 db = new FilmDBEntities2();
            Movie movie = null;
            try
            {
                movie = (from m in db.Movies
                               where m.Title.Contains(title)
                               select m).Single();

                
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: '{0}'", e);
            }
            return movie;
        }
    }
}
