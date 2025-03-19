using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PrestinedbContext _context;

        public IGenericRepository<Answer> Answers { get; }
        public IGenericRepository<Blog> Blogs { get; }
        public IGenericRepository<BlogComment> BlogComments { get; }
        public IGenericRepository<BlogHashtag> BlogHashtags { get; }
        public IGenericRepository<Booking> Bookings { get; }
        public IGenericRepository<CancelBooking> CancelBookings { get; }
        public IGenericRepository<CancelPolicy> CancelPolicies { get; }
        public IGenericRepository<Customer> Customers { get; }
        public IGenericRepository<CustomerAnswer> CustomerAnswers { get; }
        public IGenericRepository<CustomerFeedback> CustomerFeedbacks { get; }
        public IGenericRepository<CustomerFeedbackAnswer> CustomerFeedbackAnswers { get; }
        public IGenericRepository<CustomerQuiz> CustomerQuizzes { get; }
        public IGenericRepository<CustomerRating> CustomerRatings { get; }
        public IGenericRepository<FeedbackAnswer> FeedbackAnswers { get; }
        public IGenericRepository<FeedbackQuestion> FeedbackQuestions { get; }
        public IGenericRepository<Hashtag> Hashtags { get; }
        public IGenericRepository<Manager> Managers { get; }
        public IGenericRepository<News> News { get; }
        public IGenericRepository<NewsHashtag> NewsHashtags { get; }
        public IGenericRepository<Payment> Payments { get; }
        public IGenericRepository<PaymentPolicy> PaymentPolicies { get; }
        public IGenericRepository<Question> Questions { get; }
        public IGenericRepository<Quiz> Quizzes { get; }
        public IGenericRepository<QuizRecommendation> QuizRecommendations { get; }
        public IGenericRepository<Review> Reviews { get; }
        public IGenericRepository<Service> Services { get; }
        public IGenericRepository<ServiceCategory> ServiceCategories { get; }
        public IGenericRepository<Staff> Staff { get; }
        public IGenericRepository<Therapist> Therapists { get; }
        public IGenericRepository<TherapistAvailability> TherapistAvailabilities { get; }
        public IGenericRepository<TherapistProfile> TherapistProfiles { get; }
        public IGenericRepository<TimeSlot> TimeSlots { get; }
        public IGenericRepository<Transaction> Transactions { get; }
        public IGenericRepository<User> Users { get; }
        public IGenericRepository<WorkingDay> WorkingDays { get; }

        public UnitOfWork(PrestinedbContext context)
        {
            _context = context;

            Answers = new GenericRepository<Answer>(context);
            Blogs = new GenericRepository<Blog>(context);
            BlogComments = new GenericRepository<BlogComment>(context);
            BlogHashtags = new GenericRepository<BlogHashtag>(context);
            Bookings = new GenericRepository<Booking>(context);
            CancelBookings = new GenericRepository<CancelBooking>(context);
            CancelPolicies = new GenericRepository<CancelPolicy>(context);
            Customers = new GenericRepository<Customer>(context);
            CustomerAnswers = new GenericRepository<CustomerAnswer>(context);
            CustomerFeedbacks = new GenericRepository<CustomerFeedback>(context);
            CustomerFeedbackAnswers = new GenericRepository<CustomerFeedbackAnswer>(context);
            CustomerQuizzes = new GenericRepository<CustomerQuiz>(context);
            CustomerRatings = new GenericRepository<CustomerRating>(context);
            FeedbackAnswers = new GenericRepository<FeedbackAnswer>(context);
            FeedbackQuestions = new GenericRepository<FeedbackQuestion>(context);
            Hashtags = new GenericRepository<Hashtag>(context);
            Managers = new GenericRepository<Manager>(context);
            News = new GenericRepository<News>(context);
            NewsHashtags = new GenericRepository<NewsHashtag>(context);
            Payments = new GenericRepository<Payment>(context);
            PaymentPolicies = new GenericRepository<PaymentPolicy>(context);
            Questions = new GenericRepository<Question>(context);
            Quizzes = new GenericRepository<Quiz>(context);
            QuizRecommendations = new GenericRepository<QuizRecommendation>(context);
            Reviews = new GenericRepository<Review>(context);
            Services = new GenericRepository<Service>(context);
            ServiceCategories = new GenericRepository<ServiceCategory>(context);
            Staff = new GenericRepository<Staff>(context);
            Therapists = new GenericRepository<Therapist>(context);
            TherapistAvailabilities = new GenericRepository<TherapistAvailability>(context);
            TherapistProfiles = new GenericRepository<TherapistProfile>(context);
            TimeSlots = new GenericRepository<TimeSlot>(context);
            Transactions = new GenericRepository<Transaction>(context);
            Users = new GenericRepository<User>(context);
            WorkingDays = new GenericRepository<WorkingDay>(context);
        }

        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();
    }
}
