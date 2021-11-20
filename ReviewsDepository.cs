using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace IntroSQL
{
    public class ReviewsDepository : IReviewsRepository
    {
        private readonly IDbConnection _connection;

        //Constructor
        public ReviewsDepository(IDbConnection connection)
        {
            _connection = connection;
        }
    
        public IEnumerable<Reviews> GetAllReviews()
        {
            return _connection.Query<Reviews>("SELECT * FROM reviews;");
        }
    }
}
