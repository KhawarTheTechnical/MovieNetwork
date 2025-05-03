namespace MovieNetwork.Models
{
    public class Recommendation
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public List<int> RecommendedContent { get; set; } = new List<int>();

        public List<int> FilterRecommendationsByGenre(string genre, List<Movie> movies, List<TVShow> tvShows)
        {
            var filteredRecommendations = new List<int>();

            foreach (var movie in movies)
            {
                if (movie.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase) && RecommendedContent.Contains(movie.Id))
                {
                    filteredRecommendations.Add(movie.Id);
                }
            }

            foreach (var tvShow in tvShows)
            {
                if (tvShow.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase) && RecommendedContent.Contains(tvShow.Id))
                {
                    filteredRecommendations.Add(tvShow.Id);
                }
            }

            return filteredRecommendations;
        }
    }
}