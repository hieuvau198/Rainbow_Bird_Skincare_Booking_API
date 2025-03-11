using System;
using System.Collections.Generic;
using BCrypt.Net;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public partial class SkincareDbContext : DbContext
{
    public SkincareDbContext()
    {
    }

    public SkincareDbContext(DbContextOptions<SkincareDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Answer> Answers { get; set; }

    public virtual DbSet<Blog> Blogs { get; set; }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<CancelBooking> CancelBookings { get; set; }

    public virtual DbSet<CancelPolicy> CancelPolicies { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<CustomerAnswer> CustomerAnswers { get; set; }

    public virtual DbSet<CustomerFeedback> CustomerFeedbacks { get; set; }

    public virtual DbSet<CustomerFeedbackAnswer> CustomerFeedbackAnswers { get; set; }

    public virtual DbSet<CustomerQuiz> CustomerQuizzes { get; set; }

    public virtual DbSet<CustomerRating> CustomerRatings { get; set; }

    public virtual DbSet<FeedbackAnswer> FeedbackAnswers { get; set; }

    public virtual DbSet<FeedbackQuestion> FeedbackQuestions { get; set; }

    public virtual DbSet<Manager> Managers { get; set; }

    public virtual DbSet<News> News { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<PaymentPolicy> PaymentPolicies { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<Quiz> Quizzes { get; set; }

    public virtual DbSet<QuizRecommendation> QuizRecommendations { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<ServiceCategory> ServiceCategories { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    public virtual DbSet<Therapist> Therapists { get; set; }

    public virtual DbSet<TherapistAvailability> TherapistAvailabilities { get; set; }

    public virtual DbSet<TherapistProfile> TherapistProfiles { get; set; }

    public virtual DbSet<TimeSlot> TimeSlots { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<WorkingDay> WorkingDays { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Answer>(entity =>
        {
            entity.HasKey(e => e.AnswerId).HasName("PK__Answer__33724318250B0AF4");

            entity.ToTable("Answer");

            entity.HasIndex(e => e.QuestionId, "IX_Answer_question_id");

            entity.Property(e => e.AnswerId).HasColumnName("answer_id");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.Points)
                .HasDefaultValue(0)
                .HasColumnName("points");
            entity.Property(e => e.QuestionId).HasColumnName("question_id");

            entity.HasOne(d => d.Question).WithMany(p => p.Answers)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Answer__question__619B8048");
        });

        modelBuilder.Entity<Blog>(entity =>
        {
            entity.ToTable("Blog");

            entity.Property(e => e.BlogId).HasColumnName("blog_id");
            entity.Property(e => e.AuthorId).HasColumnName("author_id");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(500)
                .HasColumnName("image_url");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Author).WithMany(p => p.Blogs)
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("FK_Blog_Author");
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__Booking__5DE3A5B1E7F536B6");

            entity.ToTable("Booking");

            entity.HasIndex(e => e.CustomerId, "IX_Booking_Customer");

            entity.HasIndex(e => e.BookingDate, "IX_Booking_Date");

            entity.HasIndex(e => e.TherapistId, "IX_Booking_Therapist");

            entity.HasIndex(e => e.PaymentId, "IX_Booking_payment_id");

            entity.HasIndex(e => e.ServiceId, "IX_Booking_service_id");

            entity.HasIndex(e => e.SlotId, "IX_Booking_slot_id");

            entity.Property(e => e.BookingId).HasColumnName("booking_id");
            entity.Property(e => e.BookingDate).HasColumnName("booking_date");
            entity.Property(e => e.BookingFee)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("booking_fee");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Currency)
                .HasMaxLength(10)
                .HasDefaultValue("VND")
                .HasColumnName("currency");
            entity.Property(e => e.CustomerEmail)
                .HasMaxLength(255)
                .HasColumnName("customer_email");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.CustomerName)
                .HasMaxLength(255)
                .HasColumnName("customer_name");
            entity.Property(e => e.CustomerNote).HasColumnName("customer_note");
            entity.Property(e => e.CustomerPhone)
                .HasMaxLength(20)
                .HasColumnName("customer_phone");
            entity.Property(e => e.DurationMinutes)
                .HasDefaultValue(60)
                .HasColumnName("duration_minutes");
            entity.Property(e => e.IsFeedback).HasColumnName("is_feedback");
            entity.Property(e => e.IsRated).HasColumnName("is_rated");
            entity.Property(e => e.Location)
                .HasMaxLength(255)
                .HasDefaultValue("Prestine Care Center, District 9")
                .HasColumnName("location");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.PaymentAmount)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("payment_amount");
            entity.Property(e => e.PaymentId).HasColumnName("payment_id");
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(50)
                .HasDefaultValue("Pending")
                .HasColumnName("payment_status");
            entity.Property(e => e.ServiceId).HasColumnName("service_id");
            entity.Property(e => e.ServiceName)
                .HasMaxLength(255)
                .HasDefaultValue("")
                .HasColumnName("service_name");
            entity.Property(e => e.ServicePrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("service_price");
            entity.Property(e => e.SlotId).HasColumnName("slot_id");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.TherapistId).HasColumnName("therapist_id");
            entity.Property(e => e.TherapistName)
                .HasMaxLength(255)
                .HasDefaultValue("Not Assigned Yet")
                .HasColumnName("therapist_name");

            entity.HasOne(d => d.Customer).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Booking__custome__0D7A0286");

            entity.HasOne(d => d.Payment).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.PaymentId)
                .HasConstraintName("FK__Booking__payment__114A936A");

            entity.HasOne(d => d.Service).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Booking__service__0F624AF8");

            entity.HasOne(d => d.Slot).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.SlotId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Booking__slot_id__10566F31");
        });

        modelBuilder.Entity<CancelBooking>(entity =>
        {
            entity.HasKey(e => e.CancellationId).HasName("PK__CancelBo__4ED4366DBC764A69");

            entity.ToTable("CancelBooking");

            entity.HasIndex(e => e.BookingId, "UQ__CancelBo__5DE3A5B08EDFD0DB").IsUnique();

            entity.Property(e => e.CancellationId).HasColumnName("cancellation_id");
            entity.Property(e => e.BookingId).HasColumnName("booking_id");
            entity.Property(e => e.CancelledAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("cancelled_at");
            entity.Property(e => e.IsRefunded)
                .HasDefaultValue(false)
                .HasColumnName("is_refunded");
            entity.Property(e => e.Reason).HasColumnName("reason");

            entity.HasOne(d => d.Booking).WithOne(p => p.CancelBooking)
                .HasForeignKey<CancelBooking>(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CancelBoo__booki__1CBC4616");
        });

        modelBuilder.Entity<CancelPolicy>(entity =>
        {
            entity.HasKey(e => e.PolicyId).HasName("PK__CancelPo__47DA3F03A0859F16");

            entity.ToTable("CancelPolicy");

            entity.Property(e => e.PolicyId).HasColumnName("policy_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.MaxCancellations).HasColumnName("max_cancellations");
            entity.Property(e => e.WaitingTimeMinutes).HasColumnName("waiting_time_minutes");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__CD65CB85E44E2287");

            entity.ToTable("Customer");

            entity.HasIndex(e => e.UserId, "UQ__Customer__B9BE370E4F7BA8E3").IsUnique();

            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.LastVisitAt)
                .HasColumnType("datetime")
                .HasColumnName("last_visit_at");
            entity.Property(e => e.MedicalHistory).HasColumnName("medical_history");
            entity.Property(e => e.Preferences).HasColumnName("preferences");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithOne(p => p.Customer)
                .HasForeignKey<Customer>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Customer__user_i__3E52440B");
        });

        modelBuilder.Entity<CustomerAnswer>(entity =>
        {
            entity.HasKey(e => e.CustomerAnswerId).HasName("PK__Customer__F5422C52BDDEAA8C");

            entity.ToTable("CustomerAnswer");

            entity.HasIndex(e => e.AnswerId, "IX_CustomerAnswer_answer_id");

            entity.HasIndex(e => e.CustomerQuizId, "IX_CustomerAnswer_customer_quiz_id");

            entity.HasIndex(e => e.QuestionId, "IX_CustomerAnswer_question_id");

            entity.Property(e => e.CustomerAnswerId).HasColumnName("customer_answer_id");
            entity.Property(e => e.AnswerId).HasColumnName("answer_id");
            entity.Property(e => e.AnsweredAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("answered_at");
            entity.Property(e => e.CustomerQuizId).HasColumnName("customer_quiz_id");
            entity.Property(e => e.PointsEarned)
                .HasDefaultValue(0)
                .HasColumnName("points_earned");
            entity.Property(e => e.QuestionId).HasColumnName("question_id");

            entity.HasOne(d => d.Answer).WithMany(p => p.CustomerAnswers)
                .HasForeignKey(d => d.AnswerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CustomerA__answe__6D0D32F4");

            entity.HasOne(d => d.CustomerQuiz).WithMany(p => p.CustomerAnswers)
                .HasForeignKey(d => d.CustomerQuizId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CustomerA__custo__6B24EA82");

            entity.HasOne(d => d.Question).WithMany(p => p.CustomerAnswers)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CustomerA__quest__6C190EBB");
        });

        modelBuilder.Entity<CustomerFeedback>(entity =>
        {
            entity.HasKey(e => e.CustomerFeedbackId).HasName("PK_Customer_Feedbacks");

            entity.ToTable("CustomerFeedback");

            entity.Property(e => e.CustomerFeedbackId).HasColumnName("customer_feedback_id");
            entity.Property(e => e.BookingId).HasColumnName("booking_id");
            entity.Property(e => e.SubmittedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("submitted_at");

            entity.HasOne(d => d.Booking).WithMany(p => p.CustomerFeedbacks)
                .HasForeignKey(d => d.BookingId)
                .HasConstraintName("FK_Customer_Feedbacks_Booking");
        });

        modelBuilder.Entity<CustomerFeedbackAnswer>(entity =>
        {
            entity.HasKey(e => e.ResponseId).HasName("PK_Customer_Feedback_Answers");

            entity.ToTable("CustomerFeedbackAnswer");

            entity.Property(e => e.ResponseId).HasColumnName("response_id");
            entity.Property(e => e.AnswerText).HasColumnName("answer_text");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CustomerFeedbackId).HasColumnName("customer_feedback_id");
            entity.Property(e => e.SelectedAnswerOptionId).HasColumnName("selected_answer_option_id");

            entity.HasOne(d => d.CustomerFeedback).WithMany(p => p.CustomerFeedbackAnswers)
                .HasForeignKey(d => d.CustomerFeedbackId)
                .HasConstraintName("FK_Customer_Feedback_Answers_Customer_Feedbacks");
        });

        modelBuilder.Entity<CustomerQuiz>(entity =>
        {
            entity.HasKey(e => e.CustomerQuizId).HasName("PK__Customer__A70E104FCC2E841A");

            entity.ToTable("CustomerQuiz");

            entity.HasIndex(e => e.CustomerId, "IX_CustomerQuiz_Customer");

            entity.HasIndex(e => e.QuizId, "IX_CustomerQuiz_quiz_id");

            entity.Property(e => e.CustomerQuizId).HasColumnName("customer_quiz_id");
            entity.Property(e => e.CompletedAt)
                .HasColumnType("datetime")
                .HasColumnName("completed_at");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.QuizId).HasColumnName("quiz_id");
            entity.Property(e => e.StartedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("started_at");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasColumnName("status");
            entity.Property(e => e.TotalScore).HasColumnName("total_score");

            entity.HasOne(d => d.Customer).WithMany(p => p.CustomerQuizzes)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CustomerQ__custo__656C112C");

            entity.HasOne(d => d.Quiz).WithMany(p => p.CustomerQuizzes)
                .HasForeignKey(d => d.QuizId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CustomerQ__quiz___66603565");
        });

        modelBuilder.Entity<CustomerRating>(entity =>
        {
            entity.HasKey(e => e.RatingId).HasName("PK_Customer_Ratings");

            entity.ToTable("CustomerRating");

            entity.Property(e => e.RatingId).HasColumnName("rating_id");
            entity.Property(e => e.BookingId).HasColumnName("booking_id");
            entity.Property(e => e.Comment).HasColumnName("comment");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.ExperienceImageUrl)
                .HasMaxLength(500)
                .HasColumnName("experience_image_url");
            entity.Property(e => e.RatingValue).HasColumnName("rating_value");

            entity.HasOne(d => d.Booking).WithMany(p => p.CustomerRatings)
                .HasForeignKey(d => d.BookingId)
                .HasConstraintName("FK_Customer_Ratings_Booking");
        });

        modelBuilder.Entity<FeedbackAnswer>(entity =>
        {
            entity.HasKey(e => e.AnswerOptionId).HasName("PK_Feedback_Answers");

            entity.ToTable("FeedbackAnswer");

            entity.Property(e => e.AnswerOptionId).HasColumnName("answer_option_id");
            entity.Property(e => e.AnswerText).HasColumnName("answer_text");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.FeedbackQuestionId).HasColumnName("feedback_question_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.FeedbackQuestion).WithMany(p => p.FeedbackAnswers)
                .HasForeignKey(d => d.FeedbackQuestionId)
                .HasConstraintName("FK_Feedback_Answers_Feedback_Questions");
        });

        modelBuilder.Entity<FeedbackQuestion>(entity =>
        {
            entity.HasKey(e => e.QuestionId).HasName("PK_Feedback_Questions");

            entity.ToTable("FeedbackQuestion");

            entity.Property(e => e.QuestionId).HasColumnName("question_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.QuestionText).HasColumnName("question_text");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Manager>(entity =>
        {
            entity.HasKey(e => e.ManagerId).HasName("PK__Manager__5A6073FC39E06974");

            entity.ToTable("Manager");

            entity.HasIndex(e => e.UserId, "UQ__Manager__B9BE370E2630FC25").IsUnique();

            entity.Property(e => e.ManagerId).HasColumnName("manager_id");
            entity.Property(e => e.Department)
                .HasMaxLength(50)
                .HasColumnName("department");
            entity.Property(e => e.HireDate)
                .HasColumnType("datetime")
                .HasColumnName("hire_date");
            entity.Property(e => e.Responsibilities).HasColumnName("responsibilities");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithOne(p => p.Manager)
                .HasForeignKey<Manager>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Manager__user_id__5165187F");
        });

        modelBuilder.Entity<News>(entity =>
        {
            entity.Property(e => e.NewsId).HasColumnName("news_id");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(500)
                .HasColumnName("image_url");
            entity.Property(e => e.IsPublished).HasColumnName("is_published");
            entity.Property(e => e.PublishedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("published_at");
            entity.Property(e => e.PublisherId).HasColumnName("publisher_id");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");

            entity.HasOne(d => d.Publisher).WithMany(p => p.News)
                .HasForeignKey(d => d.PublisherId)
                .HasConstraintName("FK_News_Publisher");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payment__ED1FC9EAA349FC37");

            entity.ToTable("Payment");

            entity.Property(e => e.PaymentId).HasColumnName("payment_id");
            entity.Property(e => e.Currency)
                .HasMaxLength(3)
                .HasDefaultValue("USD")
                .HasColumnName("currency");
            entity.Property(e => e.PaymentDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("payment_date");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(50)
                .HasColumnName("payment_method");
            entity.Property(e => e.Receiver)
                .HasMaxLength(255)
                .HasColumnName("receiver");
            entity.Property(e => e.Sender)
                .HasMaxLength(255)
                .HasColumnName("sender");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasColumnName("status");
            entity.Property(e => e.Tax)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("tax");
            entity.Property(e => e.TotalAmount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total_amount");
        });

        modelBuilder.Entity<PaymentPolicy>(entity =>
        {
            entity.HasKey(e => e.PolicyId).HasName("PK__PaymentP__47DA3F0323B2C361");

            entity.ToTable("PaymentPolicy");

            entity.Property(e => e.PolicyId).HasColumnName("policy_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Currency)
                .HasMaxLength(3)
                .HasDefaultValue("USD")
                .HasColumnName("currency");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.PaymentWindowHours).HasColumnName("payment_window_hours");
            entity.Property(e => e.TaxPercentage)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("tax_percentage");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.QuestionId).HasName("PK__Question__2EC2154994824B43");

            entity.ToTable("Question");

            entity.HasIndex(e => e.QuizId, "IX_Question_quiz_id");

            entity.Property(e => e.QuestionId).HasColumnName("question_id");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.DisplayOrder).HasColumnName("display_order");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.IsMultipleChoice)
                .HasDefaultValue(false)
                .HasColumnName("is_multiple_choice");
            entity.Property(e => e.Points)
                .HasDefaultValue(0)
                .HasColumnName("points");
            entity.Property(e => e.QuizId).HasColumnName("quiz_id");

            entity.HasOne(d => d.Quiz).WithMany(p => p.Questions)
                .HasForeignKey(d => d.QuizId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Question__quiz_i__5BE2A6F2");
        });

        modelBuilder.Entity<Quiz>(entity =>
        {
            entity.HasKey(e => e.QuizId).HasName("PK__Quiz__2D7053ECA184AAA8");

            entity.ToTable("Quiz");

            entity.Property(e => e.QuizId).HasColumnName("quiz_id");
            entity.Property(e => e.Category)
                .HasMaxLength(50)
                .HasColumnName("category");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.TotalPoints).HasColumnName("total_points");
        });

        modelBuilder.Entity<QuizRecommendation>(entity =>
        {
            entity.HasKey(e => e.RecommendationId).HasName("PK__QuizReco__BCB11F4FB1377181");

            entity.ToTable("QuizRecommendation");

            entity.HasIndex(e => e.QuizId, "IX_QuizRecommendation_quiz_id");

            entity.HasIndex(e => e.ServiceId, "IX_QuizRecommendation_service_id");

            entity.Property(e => e.RecommendationId).HasColumnName("recommendation_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.MaxScore).HasColumnName("max_score");
            entity.Property(e => e.MinScore).HasColumnName("min_score");
            entity.Property(e => e.QuizId).HasColumnName("quiz_id");
            entity.Property(e => e.ServiceId).HasColumnName("service_id");

            entity.HasOne(d => d.Quiz).WithMany(p => p.QuizRecommendations)
                .HasForeignKey(d => d.QuizId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__QuizRecom__quiz___76969D2E");

            entity.HasOne(d => d.Service).WithMany(p => p.QuizRecommendations)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__QuizRecom__servi__778AC167");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__Review__60883D9004E8B032");

            entity.ToTable("Review");

            entity.HasIndex(e => e.BookingId, "UQ__Review__5DE3A5B01C922905").IsUnique();

            entity.Property(e => e.ReviewId).HasColumnName("review_id");
            entity.Property(e => e.BookingId).HasColumnName("booking_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Feedback).HasColumnName("feedback");
            entity.Property(e => e.Rating).HasColumnName("rating");

            entity.HasOne(d => d.Booking).WithOne(p => p.Review)
                .HasForeignKey<Review>(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Review__booking___17036CC0");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.ServiceId).HasName("PK__Service__3E0DB8AFBACF949C");

            entity.ToTable("Service");

            entity.Property(e => e.ServiceId).HasColumnName("service_id");
            entity.Property(e => e.AverageReview)
                .HasColumnType("decimal(3, 2)")
                .HasColumnName("average_review");
            entity.Property(e => e.BookingNumber).HasColumnName("booking_number");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Currency)
                .HasMaxLength(3)
                .HasDefaultValue("USD")
                .HasColumnName("currency");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.DurationMinutes).HasColumnName("duration_minutes");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.Location)
                .HasMaxLength(100)
                .HasColumnName("location");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.Rating)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("rating");
            entity.Property(e => e.RatingCount)
                .HasDefaultValue(0)
                .HasColumnName("rating_count");
            entity.Property(e => e.ServiceImage).HasColumnName("service_image");
            entity.Property(e => e.ServiceName)
                .HasMaxLength(100)
                .HasColumnName("service_name");
            entity.Property(e => e.ShortDescription)
                .HasMaxLength(255)
                .HasColumnName("short_description");

            entity.HasOne(d => d.Category).WithMany(p => p.Services)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Service_ServiceCategory");
        });

        modelBuilder.Entity<ServiceCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId);

            entity.ToTable("ServiceCategory");

            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(100)
                .HasColumnName("category_name");
            entity.Property(e => e.Description).HasColumnName("description");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.StaffId).HasName("PK__Staff__1963DD9CF72FBC06");

            entity.HasIndex(e => e.UserId, "UQ__Staff__B9BE370E56FBC23C").IsUnique();

            entity.Property(e => e.StaffId).HasColumnName("staff_id");
            entity.Property(e => e.Department)
                .HasMaxLength(50)
                .HasColumnName("department");
            entity.Property(e => e.HireDate)
                .HasColumnType("datetime")
                .HasColumnName("hire_date");
            entity.Property(e => e.Position)
                .HasMaxLength(50)
                .HasColumnName("position");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithOne(p => p.Staff)
                .HasForeignKey<Staff>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Staff__user_id__4D94879B");
        });

        modelBuilder.Entity<Therapist>(entity =>
        {
            entity.HasKey(e => e.TherapistId).HasName("PK__Therapis__16DFDBD188680417");

            entity.ToTable("Therapist");

            entity.HasIndex(e => e.UserId, "UQ__Therapis__B9BE370EA60BC22D").IsUnique();

            entity.Property(e => e.TherapistId).HasColumnName("therapist_id");
            entity.Property(e => e.IsAvailable)
                .HasDefaultValue(true)
                .HasColumnName("is_available");
            entity.Property(e => e.Rating)
                .HasColumnType("decimal(3, 2)")
                .HasColumnName("rating");
            entity.Property(e => e.RatingCount)
                .HasDefaultValue(0)
                .HasColumnName("rating_count");
            entity.Property(e => e.Schedule).HasColumnName("schedule");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithOne(p => p.Therapist)
                .HasForeignKey<Therapist>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Therapist__user___4316F928");
        });

        modelBuilder.Entity<TherapistAvailability>(entity =>
        {
            entity.HasKey(e => e.AvailabilityId).HasName("PK__Therapis__86E3A801B78752C3");

            entity.ToTable("TherapistAvailability");

            entity.HasIndex(e => e.WorkingDate, "IX_TherapistAvailability_Date");

            entity.HasIndex(e => e.SlotId, "IX_TherapistAvailability_slot_id");

            entity.HasIndex(e => e.TherapistId, "IX_TherapistAvailability_therapist_id");

            entity.Property(e => e.AvailabilityId).HasColumnName("availability_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.SlotId).HasColumnName("slot_id");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("available")
                .HasColumnName("status");
            entity.Property(e => e.TherapistId).HasColumnName("therapist_id");
            entity.Property(e => e.WorkingDate).HasColumnName("working_date");

            entity.HasOne(d => d.Slot).WithMany(p => p.TherapistAvailabilities)
                .HasForeignKey(d => d.SlotId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Therapist__slot___05D8E0BE");

            entity.HasOne(d => d.Therapist).WithMany(p => p.TherapistAvailabilities)
                .HasForeignKey(d => d.TherapistId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Therapist__thera__04E4BC85");
        });

        modelBuilder.Entity<TherapistProfile>(entity =>
        {
            entity.HasKey(e => e.ProfileId).HasName("PK__Therapis__AEBB701F4784F495");

            entity.ToTable("TherapistProfile");

            entity.HasIndex(e => e.TherapistId, "UQ__Therapis__16DFDBD0353BAE08").IsUnique();

            entity.Property(e => e.ProfileId).HasColumnName("profile_id");
            entity.Property(e => e.AcceptsNewClients)
                .HasDefaultValue(true)
                .HasColumnName("accepts_new_clients");
            entity.Property(e => e.Bio).HasColumnName("bio");
            entity.Property(e => e.Certifications).HasColumnName("certifications");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Education).HasColumnName("education");
            entity.Property(e => e.Languages).HasColumnName("languages");
            entity.Property(e => e.PersonalStatement).HasColumnName("personal_statement");
            entity.Property(e => e.ProfileImage).HasColumnName("profile_image");
            entity.Property(e => e.Specialties).HasColumnName("specialties");
            entity.Property(e => e.TherapistId).HasColumnName("therapist_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.YearsExperience).HasColumnName("years_experience");

            entity.HasOne(d => d.Therapist).WithOne(p => p.TherapistProfile)
                .HasForeignKey<TherapistProfile>(d => d.TherapistId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Therapist__thera__49C3F6B7");
        });

        modelBuilder.Entity<TimeSlot>(entity =>
        {
            entity.HasKey(e => e.SlotId).HasName("PK__TimeSlot__971A01BB3891BB3F");

            entity.ToTable("TimeSlot");

            entity.HasIndex(e => e.WorkingDayId, "IX_TimeSlot_working_day_id");

            entity.Property(e => e.SlotId).HasColumnName("slot_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.EndTime).HasColumnName("end_time");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.SlotNumber).HasColumnName("slot_number");
            entity.Property(e => e.StartTime).HasColumnName("start_time");
            entity.Property(e => e.WorkingDayId).HasColumnName("working_day_id");

            entity.HasOne(d => d.WorkingDay).WithMany(p => p.TimeSlots)
                .HasForeignKey(d => d.WorkingDayId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TimeSlot__workin__00200768");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__B9BE370FBC7F1B8F");

            entity.ToTable("User");

            entity.HasIndex(e => e.Email, "IX_User_Email");

            entity.HasIndex(e => e.Username, "IX_User_Username");

            entity.HasIndex(e => e.Email, "UQ__User__AB6E6164323A2DBF").IsUnique();

            entity.HasIndex(e => e.Username, "UQ__User__F3DBC572C29C5FE1").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .HasColumnName("full_name");
            entity.Property(e => e.LastLoginAt)
                .HasColumnType("datetime")
                .HasColumnName("last_login_at");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            entity.Property(e => e.Role).HasColumnName("role");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
        });

        modelBuilder.Entity<WorkingDay>(entity =>
        {
            entity.HasKey(e => e.WorkingDayId).HasName("PK__WorkingD__FE446ADF3074D242");

            entity.ToTable("WorkingDay");

            entity.Property(e => e.WorkingDayId).HasColumnName("working_day_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.DayName)
                .HasMaxLength(20)
                .HasColumnName("day_name");
            entity.Property(e => e.EndTime).HasColumnName("end_time");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.SlotDurationMinutes).HasColumnName("slot_duration_minutes");
            entity.Property(e => e.StartTime).HasColumnName("start_time");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
