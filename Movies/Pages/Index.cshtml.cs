using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Movies.Pages
{
    public class IndexModel : PageModel
    {

        /// <summary>
        /// The movies to display on the index page 
        /// </summary>
        public IEnumerable<Movie> Movies { get; protected set; }

        /// <summary>
        /// The current search terms 
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public string SearchTerms { get; set; }

        /// <summary>
        /// The filtered MPAA Ratings
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public string[] MPAARatings { get; set; }

        /// <summary>
        /// The filtered genres
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public string[] Genres { get; set; }

        /// <summary>
        /// The minimum IMDB Rating
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public double? IMDBMin { get; set; }

        /// <summary>
        /// The maximum IMDB Rating
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public double? IMDBMax { get; set; }

        /// <summary>
        /// The minimum RT Rating
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public double? RTMin { get; set; }

        /// <summary>
        /// The maximum RT Rating
        /// </summary>
        [BindProperty (SupportsGet = true)]
        public double? RTMax { get; set; }

        /// <summary>
        /// Gets the search results for display on the page
        /// </summary>
        public void OnGet(double? IMDBMin, double? IMDBMax, double? RTMin, double? RTMax)
        {
            //this.IMDBMin = IMDBMin;
            //this.IMDBMax = IMDBMax;
            //this.RTMax = RTMax;
            //this.RTMin = RTMin;
            //SearchTerms = Request.Query["SearchTerms"];
            //MPAARatings = Request.Query["MPAARatings"];
            //Genres = Request.Query["Genres"];
            //Movies = MovieDatabase.Search(SearchTerms);
            //Movies = MovieDatabase.FilterByMPAARating(Movies, MPAARatings);
            //Movies = MovieDatabase.FilterByGenres(Movies, Genres);
            //Movies = MovieDatabase.FilterByIMDBRating(Movies, IMDBMin, IMDBMax);
            //Movies = MovieDatabase.FilterByRTRating(Movies, RTMin, RTMax);

            Movies = MovieDatabase.All;
            if (SearchTerms != null) {
                Movies.Where(movie => movie.Title != null && movie.Title.Contains(SearchTerms, StringComparison.CurrentCultureIgnoreCase));
                //Movies = from movie in Movies
                //         where movie.Title.Contains(SearchTerms, StringComparison.OrdinalIgnoreCase)
                //         select movie;
            }

            if (MPAARatings != null && MPAARatings.Length != 0)
            {
                Movies = Movies.Where(movie => movie.MPAARating != null && MPAARatings.Contains(movie.MPAARating));
            }
            if (Genres != null && Genres.Length != 0)
            {
                Movies = Movies.Where(movie => movie.MajorGenre != null && Genres.Contains(movie.MajorGenre));
            }                       
            if (IMDBMin == null && IMDBMax != null)
            {
                Movies = Movies.Where(movie => movie.IMDBRating <= IMDBMax);
            }
            if (IMDBMax == null && IMDBMin != null)
            {
                Movies = Movies.Where(movie => movie.IMDBRating >= IMDBMin);
            }
            if (IMDBMax != null && IMDBMin != null)
            {

                Movies = Movies.Where(movie => movie.IMDBRating >= IMDBMin && movie.IMDBRating <= IMDBMax);
            }
            if (RTMin == null && RTMin != null)
            {
                Movies = Movies.Where(movie => movie.RottenTomatoesRating <= RTMax);
            }

            if (RTMax == null && RTMin != null)
            {
                Movies = Movies.Where(movie => movie.RottenTomatoesRating >= RTMin);
            }
            if (RTMax != null && RTMin != null)
            {
                Movies = Movies.Where(movie => movie.RottenTomatoesRating >= RTMin && movie.RottenTomatoesRating <= RTMax);
            }
        }

        public void OnPost()
        {
            Movies = MovieDatabase.Search(SearchTerms);
            Movies = MovieDatabase.FilterByMPAARating(Movies, MPAARatings);
            Movies = MovieDatabase.FilterByGenres(Movies, Genres);
            Movies = MovieDatabase.FilterByIMDBRating(Movies, IMDBMin, IMDBMax);
            Movies = MovieDatabase.FilterByRTRating(Movies, RTMin, RTMax);
        }
    }
}
