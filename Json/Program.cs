using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Json
{
    class Program
    {
        static void Main(string[] args)
        {
            Movie movie = new Movie
            {
                MovieId = 2,
                Name = "Pulp Fiction",
                Author = "Quentin Tarantino",
                Ratings = new List<int> { 10, 9, 10, 8 },
                Commnets = new List<Comment>
                {
                    new Comment
                    {
                        UserId = 1,
                        Message = "Cool"
                    },
                    new Comment
                    {
                        UserId =2 ,
                        Message = "Super",
                    }
                }
            };

            string json = JsonConvert.SerializeObject(movie);

            Console.WriteLine(json);
        }
    }

    class Comment
    {
        public int UserId { get; set; }
        public string Message { get; set; }
    }

    class Movie
    {
        public int MovieId { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public List<int> Ratings { get; set; }
        public List<Comment> Commnets { get; set; }
    }
}
