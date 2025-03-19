using Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Answer> Answers { get; }
        IGenericRepository<Blog> Blogs { get; }
        IGenericRepository<BlogComment> BlogComments { get; }
        IGenericRepository<BlogHashtag> BlogHashtags { get; }
        IGenericRepository<Booking> Bookings { get; }
        IGenericRepository<CancelBooking> CancelBookings { get; }
        IGenericRepository<CancelPolicy> CancelPolicies { get; }
        IGenericRepository<Customer> Customers { get; }
        IGenericRepository<CustomerAnswer> CustomerAnswers { get; }
        IGenericRepository<CustomerFeedback> CustomerFeedbacks { get; }
        IGenericRepository<CustomerFeedbackAnswer> CustomerFeedbackAnswers { get; }
        IGenericRepository<CustomerQuiz> CustomerQuizzes { get; }
        IGenericRepository<CustomerRating> CustomerRatings { get; }
        IGenericRepository<FeedbackAnswer> FeedbackAnswers { get; }
        IGenericRepository<FeedbackQuestion> FeedbackQuestions { get; }
        IGenericRepository<Hashtag> Hashtags { get; }
        IGenericRepository<Manager> Managers { get; }
        IGenericRepository<News> News { get; }
        IGenericRepository<NewsHashtag> NewsHashtags { get; }
        IGenericRepository<Payment> Payments { get; }
        IGenericRepository<PaymentPolicy> PaymentPolicies { get; }
        IGenericRepository<Question> Questions { get; }
        IGenericRepository<Quiz> Quizzes { get; }
        IGenericRepository<QuizRecommendation> QuizRecommendations { get; }
        IGenericRepository<Review> Reviews { get; }
        IGenericRepository<Service> Services { get; }
        IGenericRepository<ServiceCategory> ServiceCategories { get; }
        IGenericRepository<Staff> Staff { get; }
        IGenericRepository<Therapist> Therapists { get; }
        IGenericRepository<TherapistAvailability> TherapistAvailabilities { get; }
        IGenericRepository<TherapistProfile> TherapistProfiles { get; }
        IGenericRepository<TimeSlot> TimeSlots { get; }
        IGenericRepository<Transaction> Transactions { get; }
        IGenericRepository<User> Users { get; }
        IGenericRepository<WorkingDay> WorkingDays { get; }

        Task<int> SaveChangesAsync();
    }
}
