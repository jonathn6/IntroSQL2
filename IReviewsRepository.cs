using System;
using System.Collections.Generic;
using System.Text;

namespace IntroSQL
{
    public interface IReviewsRepository
    {
        public IEnumerable<Reviews> GetAllReviews();
    }
}
