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
       
        public string SearchTerms { get; set; }

        /// <summary>
        /// The filtered MPAA Ratings
        /// </summary>
        
        public string[] MPAARatings { get; set; }

        /// <summary>
        /// The filtered genres
        /// </summary>
        
        public string[] Genres { get; set; }

        /// <summary>
        /// The minimum IMDB Rating
        /// </summary>
        
        public double? IMDBMin { get; set; }

        /// <summary>
        /// The maximum IMDB Rating
        /// </summary>
        
        public double? IMDBMax { get; set; }

        /// <summary>
        /// The minimum RT Rating
        /// </summary>
        
        public double? RTMin { get; set; }

        /// <summary>
        /// The maximum RT Rating
        /// </summary>
        
        public double? RTMax { get; set; }

        /// <summary>
        /// Gets the search results for display on the page
        /// </summary>
        public void OnGet(string SearchTerms, string[] MPAARatings, string[] Genres, double IMDBMin, double IMDBMax, double RTMax, double RTMin)
        {
            // Nullable conversion workaround
            this.IMDBMin = IMDBMin;
            this.IMDBMax = IMDBMax;
            this.RTMin = RTMin;
            this.RTMax = RTMax;
            Movies = MovieDatabase.Search(SearchTerms);
            Movies = MovieDatabase.FilterByMPAARating(Movies, MPAARatings);
            Movies = MovieDatabase.FilterByGenres(Movies, Genres);
            Movies = MovieDatabase.FilterByIMDBRating(Movies, IMDBMin, IMDBMax);
            Movies = MovieDatabase.FilterByRTRating(Movies, RTMin, RTMax);
        }
    }
}
