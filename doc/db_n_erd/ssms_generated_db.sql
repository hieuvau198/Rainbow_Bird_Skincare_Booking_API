/****** Object:  Database [prestinedb]    Script Date: 3/7/2025 9:22:53 PM ******/
CREATE DATABASE [prestinedb]  (EDITION = 'Basic', SERVICE_OBJECTIVE = 'Basic', MAXSIZE = 500 MB) WITH CATALOG_COLLATION = SQL_Latin1_General_CP1_CI_AS, LEDGER = OFF;
GO
ALTER DATABASE [prestinedb] SET COMPATIBILITY_LEVEL = 160
GO
ALTER DATABASE [prestinedb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [prestinedb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [prestinedb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [prestinedb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [prestinedb] SET ARITHABORT OFF 
GO
ALTER DATABASE [prestinedb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [prestinedb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [prestinedb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [prestinedb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [prestinedb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [prestinedb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [prestinedb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [prestinedb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [prestinedb] SET ALLOW_SNAPSHOT_ISOLATION ON 
GO
ALTER DATABASE [prestinedb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [prestinedb] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [prestinedb] SET  MULTI_USER 
GO
ALTER DATABASE [prestinedb] SET ENCRYPTION ON
GO
ALTER DATABASE [prestinedb] SET QUERY_STORE = ON
GO
ALTER DATABASE [prestinedb] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 7), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 10, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
/*** The scripts of database scoped configurations in Azure should be executed inside the target database connection. ***/
GO
-- ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 8;
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 3/7/2025 9:22:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Answer]    Script Date: 3/7/2025 9:22:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Answer](
	[answer_id] [int] IDENTITY(1,1) NOT NULL,
	[question_id] [int] NOT NULL,
	[content] [nvarchar](max) NOT NULL,
	[points] [int] NULL,
	[is_active] [bit] NULL,
	[created_at] [datetime] NULL,
 CONSTRAINT [PK__Answer__33724318250B0AF4] PRIMARY KEY CLUSTERED 
(
	[answer_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Blog]    Script Date: 3/7/2025 9:22:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Blog](
	[blog_id] [int] IDENTITY(1,1) NOT NULL,
	[title] [nvarchar](255) NOT NULL,
	[content] [nvarchar](max) NOT NULL,
	[image_url] [nvarchar](500) NULL,
	[created_at] [datetime] NOT NULL,
	[updated_at] [datetime] NULL,
	[author_id] [int] NOT NULL,
 CONSTRAINT [PK_Blog] PRIMARY KEY CLUSTERED 
(
	[blog_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Booking]    Script Date: 3/7/2025 9:22:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Booking](
	[booking_id] [int] IDENTITY(1,1) NOT NULL,
	[customer_id] [int] NOT NULL,
	[therapist_id] [int] NOT NULL,
	[service_id] [int] NOT NULL,
	[slot_id] [int] NOT NULL,
	[payment_id] [int] NULL,
	[booking_date] [date] NOT NULL,
	[status] [nvarchar](20) NULL,
	[notes] [nvarchar](max) NULL,
	[created_at] [datetime] NULL,
 CONSTRAINT [PK__Booking__5DE3A5B1E7F536B6] PRIMARY KEY CLUSTERED 
(
	[booking_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CancelBooking]    Script Date: 3/7/2025 9:22:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CancelBooking](
	[cancellation_id] [int] IDENTITY(1,1) NOT NULL,
	[booking_id] [int] NOT NULL,
	[cancelled_at] [datetime] NULL,
	[reason] [nvarchar](max) NULL,
	[is_refunded] [bit] NULL,
 CONSTRAINT [PK__CancelBo__4ED4366DBC764A69] PRIMARY KEY CLUSTERED 
(
	[cancellation_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CancelPolicy]    Script Date: 3/7/2025 9:22:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CancelPolicy](
	[policy_id] [int] IDENTITY(1,1) NOT NULL,
	[max_cancellations] [int] NULL,
	[waiting_time_minutes] [int] NULL,
	[is_active] [bit] NULL,
	[created_at] [datetime] NULL,
 CONSTRAINT [PK__CancelPo__47DA3F03A0859F16] PRIMARY KEY CLUSTERED 
(
	[policy_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 3/7/2025 9:22:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[customer_id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NOT NULL,
	[preferences] [nvarchar](max) NULL,
	[medical_history] [nvarchar](max) NULL,
	[last_visit_at] [datetime] NULL,
 CONSTRAINT [PK__Customer__CD65CB85E44E2287] PRIMARY KEY CLUSTERED 
(
	[customer_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomerAnswer]    Script Date: 3/7/2025 9:22:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerAnswer](
	[customer_answer_id] [int] IDENTITY(1,1) NOT NULL,
	[customer_quiz_id] [int] NOT NULL,
	[question_id] [int] NOT NULL,
	[answer_id] [int] NOT NULL,
	[points_earned] [int] NULL,
	[answered_at] [datetime] NULL,
 CONSTRAINT [PK__Customer__F5422C52BDDEAA8C] PRIMARY KEY CLUSTERED 
(
	[customer_answer_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomerFeedback]    Script Date: 3/7/2025 9:22:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerFeedback](
	[customer_feedback_id] [int] IDENTITY(1,1) NOT NULL,
	[booking_id] [int] NOT NULL,
	[submitted_at] [datetime] NULL,
 CONSTRAINT [PK_Customer_Feedbacks] PRIMARY KEY CLUSTERED 
(
	[customer_feedback_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomerFeedbackAnswer]    Script Date: 3/7/2025 9:22:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerFeedbackAnswer](
	[response_id] [int] IDENTITY(1,1) NOT NULL,
	[customer_feedback_id] [int] NOT NULL,
	[answer_text] [nvarchar](max) NULL,
	[selected_answer_option_id] [int] NULL,
	[created_at] [datetime] NULL,
 CONSTRAINT [PK_Customer_Feedback_Answers] PRIMARY KEY CLUSTERED 
(
	[response_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomerQuiz]    Script Date: 3/7/2025 9:22:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerQuiz](
	[customer_quiz_id] [int] IDENTITY(1,1) NOT NULL,
	[customer_id] [int] NOT NULL,
	[quiz_id] [int] NOT NULL,
	[total_score] [int] NULL,
	[status] [nvarchar](20) NULL,
	[started_at] [datetime] NULL,
	[completed_at] [datetime] NULL,
 CONSTRAINT [PK__Customer__A70E104FCC2E841A] PRIMARY KEY CLUSTERED 
(
	[customer_quiz_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomerRating]    Script Date: 3/7/2025 9:22:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerRating](
	[rating_id] [int] IDENTITY(1,1) NOT NULL,
	[booking_id] [int] NOT NULL,
	[rating_value] [int] NOT NULL,
	[experience_image_url] [nvarchar](500) NULL,
	[comment] [nvarchar](max) NULL,
	[created_at] [datetime] NULL,
 CONSTRAINT [PK_Customer_Ratings] PRIMARY KEY CLUSTERED 
(
	[rating_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FeedbackAnswer]    Script Date: 3/7/2025 9:22:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FeedbackAnswer](
	[answer_option_id] [int] IDENTITY(1,1) NOT NULL,
	[feedback_question_id] [int] NOT NULL,
	[answer_text] [nvarchar](max) NOT NULL,
	[created_at] [datetime] NULL,
	[updated_at] [datetime] NULL,
 CONSTRAINT [PK_Feedback_Answers] PRIMARY KEY CLUSTERED 
(
	[answer_option_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FeedbackQuestion]    Script Date: 3/7/2025 9:22:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FeedbackQuestion](
	[question_id] [int] IDENTITY(1,1) NOT NULL,
	[question_text] [nvarchar](max) NOT NULL,
	[created_at] [datetime] NULL,
	[updated_at] [datetime] NULL,
 CONSTRAINT [PK_Feedback_Questions] PRIMARY KEY CLUSTERED 
(
	[question_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Manager]    Script Date: 3/7/2025 9:22:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Manager](
	[manager_id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NOT NULL,
	[department] [nvarchar](50) NULL,
	[responsibilities] [nvarchar](max) NULL,
	[hire_date] [datetime] NULL,
 CONSTRAINT [PK__Manager__5A6073FC39E06974] PRIMARY KEY CLUSTERED 
(
	[manager_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[News]    Script Date: 3/7/2025 9:22:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[News](
	[news_id] [int] IDENTITY(1,1) NOT NULL,
	[title] [nvarchar](255) NOT NULL,
	[content] [nvarchar](max) NOT NULL,
	[image_url] [nvarchar](500) NULL,
	[published_at] [datetime] NOT NULL,
	[is_published] [bit] NOT NULL,
	[publisher_id] [int] NOT NULL,
 CONSTRAINT [PK_News] PRIMARY KEY CLUSTERED 
(
	[news_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payment]    Script Date: 3/7/2025 9:22:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payment](
	[payment_id] [int] IDENTITY(1,1) NOT NULL,
	[total_amount] [decimal](10, 2) NOT NULL,
	[currency] [nvarchar](3) NULL,
	[payment_method] [nvarchar](50) NULL,
	[status] [nvarchar](20) NULL,
	[payment_date] [datetime] NULL,
 CONSTRAINT [PK__Payment__ED1FC9EAA349FC37] PRIMARY KEY CLUSTERED 
(
	[payment_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PaymentPolicy]    Script Date: 3/7/2025 9:22:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentPolicy](
	[policy_id] [int] IDENTITY(1,1) NOT NULL,
	[currency] [nvarchar](3) NULL,
	[payment_window_hours] [int] NULL,
	[tax_percentage] [decimal](5, 2) NULL,
	[is_active] [bit] NULL,
	[created_at] [datetime] NULL,
 CONSTRAINT [PK__PaymentP__47DA3F0323B2C361] PRIMARY KEY CLUSTERED 
(
	[policy_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Question]    Script Date: 3/7/2025 9:22:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Question](
	[question_id] [int] IDENTITY(1,1) NOT NULL,
	[quiz_id] [int] NOT NULL,
	[content] [nvarchar](max) NOT NULL,
	[points] [int] NULL,
	[is_multiple_choice] [bit] NULL,
	[display_order] [int] NULL,
	[is_active] [bit] NULL,
	[created_at] [datetime] NULL,
 CONSTRAINT [PK__Question__2EC2154994824B43] PRIMARY KEY CLUSTERED 
(
	[question_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Quiz]    Script Date: 3/7/2025 9:22:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Quiz](
	[quiz_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NOT NULL,
	[description] [nvarchar](max) NULL,
	[category] [nvarchar](50) NULL,
	[total_points] [int] NULL,
	[is_active] [bit] NULL,
	[created_at] [datetime] NULL,
 CONSTRAINT [PK__Quiz__2D7053ECA184AAA8] PRIMARY KEY CLUSTERED 
(
	[quiz_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuizRecommendation]    Script Date: 3/7/2025 9:22:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuizRecommendation](
	[recommendation_id] [int] IDENTITY(1,1) NOT NULL,
	[quiz_id] [int] NOT NULL,
	[service_id] [int] NOT NULL,
	[min_score] [int] NULL,
	[max_score] [int] NULL,
	[is_active] [bit] NULL,
	[created_at] [datetime] NULL,
 CONSTRAINT [PK__QuizReco__BCB11F4FB1377181] PRIMARY KEY CLUSTERED 
(
	[recommendation_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Review]    Script Date: 3/7/2025 9:22:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Review](
	[review_id] [int] IDENTITY(1,1) NOT NULL,
	[booking_id] [int] NOT NULL,
	[rating] [int] NULL,
	[feedback] [nvarchar](max) NULL,
	[created_at] [datetime] NULL,
 CONSTRAINT [PK__Review__60883D9004E8B032] PRIMARY KEY CLUSTERED 
(
	[review_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Service]    Script Date: 3/7/2025 9:22:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Service](
	[service_id] [int] IDENTITY(1,1) NOT NULL,
	[service_name] [nvarchar](100) NOT NULL,
	[description] [nvarchar](max) NULL,
	[price] [decimal](10, 2) NOT NULL,
	[currency] [nvarchar](3) NULL,
	[duration_minutes] [int] NOT NULL,
	[location] [nvarchar](100) NULL,
	[is_active] [bit] NULL,
	[created_at] [datetime] NULL,
	[service_image] [nvarchar](max) NULL,
	[rating_count] [int] NULL,
	[rating] [decimal](18, 0) NULL,
 CONSTRAINT [PK__Service__3E0DB8AFBACF949C] PRIMARY KEY CLUSTERED 
(
	[service_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Staff]    Script Date: 3/7/2025 9:22:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Staff](
	[staff_id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NOT NULL,
	[department] [nvarchar](50) NULL,
	[position] [nvarchar](50) NULL,
	[hire_date] [datetime] NULL,
 CONSTRAINT [PK__Staff__1963DD9CF72FBC06] PRIMARY KEY CLUSTERED 
(
	[staff_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Therapist]    Script Date: 3/7/2025 9:22:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Therapist](
	[therapist_id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NOT NULL,
	[is_available] [bit] NULL,
	[schedule] [nvarchar](max) NULL,
	[rating] [decimal](3, 2) NULL,
	[rating_count] [int] NULL,
 CONSTRAINT [PK__Therapis__16DFDBD188680417] PRIMARY KEY CLUSTERED 
(
	[therapist_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TherapistAvailability]    Script Date: 3/7/2025 9:22:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TherapistAvailability](
	[availability_id] [int] IDENTITY(1,1) NOT NULL,
	[therapist_id] [int] NOT NULL,
	[slot_id] [int] NOT NULL,
	[working_date] [date] NOT NULL,
	[status] [nvarchar](20) NULL,
	[created_at] [datetime] NULL,
 CONSTRAINT [PK__Therapis__86E3A801B78752C3] PRIMARY KEY CLUSTERED 
(
	[availability_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TherapistProfile]    Script Date: 3/7/2025 9:22:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TherapistProfile](
	[profile_id] [int] IDENTITY(1,1) NOT NULL,
	[therapist_id] [int] NOT NULL,
	[bio] [nvarchar](max) NULL,
	[personal_statement] [nvarchar](max) NULL,
	[profile_image] [nvarchar](max) NULL,
	[education] [nvarchar](max) NULL,
	[certifications] [nvarchar](max) NULL,
	[specialties] [nvarchar](max) NULL,
	[languages] [nvarchar](max) NULL,
	[years_experience] [int] NULL,
	[accepts_new_clients] [bit] NULL,
	[created_at] [datetime] NULL,
	[updated_at] [datetime] NULL,
 CONSTRAINT [PK__Therapis__AEBB701F4784F495] PRIMARY KEY CLUSTERED 
(
	[profile_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TimeSlot]    Script Date: 3/7/2025 9:22:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TimeSlot](
	[slot_id] [int] IDENTITY(1,1) NOT NULL,
	[working_day_id] [int] NOT NULL,
	[start_time] [time](7) NOT NULL,
	[end_time] [time](7) NOT NULL,
	[slot_number] [int] NOT NULL,
	[is_active] [bit] NULL,
	[created_at] [datetime] NULL,
 CONSTRAINT [PK__TimeSlot__971A01BB3891BB3F] PRIMARY KEY CLUSTERED 
(
	[slot_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 3/7/2025 9:22:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[user_id] [int] IDENTITY(1,1) NOT NULL,
	[username] [nvarchar](50) NOT NULL,
	[password] [nvarchar](255) NOT NULL,
	[email] [nvarchar](100) NOT NULL,
	[phone] [nvarchar](20) NULL,
	[full_name] [nvarchar](100) NOT NULL,
	[role] [int] NULL,
	[created_at] [datetime] NULL,
	[last_login_at] [datetime] NULL,
	[RefreshToken] [nvarchar](max) NULL,
	[RefreshTokenExpiryTime] [datetime2](7) NULL,
 CONSTRAINT [PK__User__B9BE370FBC7F1B8F] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WorkingDay]    Script Date: 3/7/2025 9:22:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkingDay](
	[working_day_id] [int] IDENTITY(1,1) NOT NULL,
	[day_name] [nvarchar](20) NOT NULL,
	[start_time] [time](7) NOT NULL,
	[end_time] [time](7) NOT NULL,
	[slot_duration_minutes] [int] NOT NULL,
	[is_active] [bit] NULL,
	[created_at] [datetime] NULL,
 CONSTRAINT [PK__WorkingD__FE446ADF3074D242] PRIMARY KEY CLUSTERED 
(
	[working_day_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Index [IX_Answer_question_id]    Script Date: 3/7/2025 9:22:53 PM ******/
CREATE NONCLUSTERED INDEX [IX_Answer_question_id] ON [dbo].[Answer]
(
	[question_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Booking_Customer]    Script Date: 3/7/2025 9:22:53 PM ******/
CREATE NONCLUSTERED INDEX [IX_Booking_Customer] ON [dbo].[Booking]
(
	[customer_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Booking_Date]    Script Date: 3/7/2025 9:22:53 PM ******/
CREATE NONCLUSTERED INDEX [IX_Booking_Date] ON [dbo].[Booking]
(
	[booking_date] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Booking_payment_id]    Script Date: 3/7/2025 9:22:53 PM ******/
CREATE NONCLUSTERED INDEX [IX_Booking_payment_id] ON [dbo].[Booking]
(
	[payment_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Booking_service_id]    Script Date: 3/7/2025 9:22:53 PM ******/
CREATE NONCLUSTERED INDEX [IX_Booking_service_id] ON [dbo].[Booking]
(
	[service_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Booking_slot_id]    Script Date: 3/7/2025 9:22:53 PM ******/
CREATE NONCLUSTERED INDEX [IX_Booking_slot_id] ON [dbo].[Booking]
(
	[slot_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Booking_Therapist]    Script Date: 3/7/2025 9:22:53 PM ******/
CREATE NONCLUSTERED INDEX [IX_Booking_Therapist] ON [dbo].[Booking]
(
	[therapist_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [UQ__CancelBo__5DE3A5B08EDFD0DB]    Script Date: 3/7/2025 9:22:53 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ__CancelBo__5DE3A5B08EDFD0DB] ON [dbo].[CancelBooking]
(
	[booking_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [UQ__Customer__B9BE370E4F7BA8E3]    Script Date: 3/7/2025 9:22:53 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ__Customer__B9BE370E4F7BA8E3] ON [dbo].[Customer]
(
	[user_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_CustomerAnswer_answer_id]    Script Date: 3/7/2025 9:22:53 PM ******/
CREATE NONCLUSTERED INDEX [IX_CustomerAnswer_answer_id] ON [dbo].[CustomerAnswer]
(
	[answer_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_CustomerAnswer_customer_quiz_id]    Script Date: 3/7/2025 9:22:53 PM ******/
CREATE NONCLUSTERED INDEX [IX_CustomerAnswer_customer_quiz_id] ON [dbo].[CustomerAnswer]
(
	[customer_quiz_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_CustomerAnswer_question_id]    Script Date: 3/7/2025 9:22:53 PM ******/
CREATE NONCLUSTERED INDEX [IX_CustomerAnswer_question_id] ON [dbo].[CustomerAnswer]
(
	[question_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_CustomerQuiz_Customer]    Script Date: 3/7/2025 9:22:53 PM ******/
CREATE NONCLUSTERED INDEX [IX_CustomerQuiz_Customer] ON [dbo].[CustomerQuiz]
(
	[customer_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_CustomerQuiz_quiz_id]    Script Date: 3/7/2025 9:22:53 PM ******/
CREATE NONCLUSTERED INDEX [IX_CustomerQuiz_quiz_id] ON [dbo].[CustomerQuiz]
(
	[quiz_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [UQ__Manager__B9BE370E2630FC25]    Script Date: 3/7/2025 9:22:53 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ__Manager__B9BE370E2630FC25] ON [dbo].[Manager]
(
	[user_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Question_quiz_id]    Script Date: 3/7/2025 9:22:53 PM ******/
CREATE NONCLUSTERED INDEX [IX_Question_quiz_id] ON [dbo].[Question]
(
	[quiz_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_QuizRecommendation_quiz_id]    Script Date: 3/7/2025 9:22:53 PM ******/
CREATE NONCLUSTERED INDEX [IX_QuizRecommendation_quiz_id] ON [dbo].[QuizRecommendation]
(
	[quiz_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_QuizRecommendation_service_id]    Script Date: 3/7/2025 9:22:53 PM ******/
CREATE NONCLUSTERED INDEX [IX_QuizRecommendation_service_id] ON [dbo].[QuizRecommendation]
(
	[service_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [UQ__Review__5DE3A5B01C922905]    Script Date: 3/7/2025 9:22:53 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ__Review__5DE3A5B01C922905] ON [dbo].[Review]
(
	[booking_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [UQ__Staff__B9BE370E56FBC23C]    Script Date: 3/7/2025 9:22:53 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ__Staff__B9BE370E56FBC23C] ON [dbo].[Staff]
(
	[user_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [UQ__Therapis__B9BE370EA60BC22D]    Script Date: 3/7/2025 9:22:53 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ__Therapis__B9BE370EA60BC22D] ON [dbo].[Therapist]
(
	[user_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_TherapistAvailability_Date]    Script Date: 3/7/2025 9:22:53 PM ******/
CREATE NONCLUSTERED INDEX [IX_TherapistAvailability_Date] ON [dbo].[TherapistAvailability]
(
	[working_date] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_TherapistAvailability_slot_id]    Script Date: 3/7/2025 9:22:53 PM ******/
CREATE NONCLUSTERED INDEX [IX_TherapistAvailability_slot_id] ON [dbo].[TherapistAvailability]
(
	[slot_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_TherapistAvailability_therapist_id]    Script Date: 3/7/2025 9:22:53 PM ******/
CREATE NONCLUSTERED INDEX [IX_TherapistAvailability_therapist_id] ON [dbo].[TherapistAvailability]
(
	[therapist_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [UQ__Therapis__16DFDBD0353BAE08]    Script Date: 3/7/2025 9:22:53 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ__Therapis__16DFDBD0353BAE08] ON [dbo].[TherapistProfile]
(
	[therapist_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_TimeSlot_working_day_id]    Script Date: 3/7/2025 9:22:53 PM ******/
CREATE NONCLUSTERED INDEX [IX_TimeSlot_working_day_id] ON [dbo].[TimeSlot]
(
	[working_day_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_User_Email]    Script Date: 3/7/2025 9:22:53 PM ******/
CREATE NONCLUSTERED INDEX [IX_User_Email] ON [dbo].[User]
(
	[email] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_User_Username]    Script Date: 3/7/2025 9:22:53 PM ******/
CREATE NONCLUSTERED INDEX [IX_User_Username] ON [dbo].[User]
(
	[username] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__User__AB6E6164323A2DBF]    Script Date: 3/7/2025 9:22:53 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ__User__AB6E6164323A2DBF] ON [dbo].[User]
(
	[email] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__User__F3DBC572C29C5FE1]    Script Date: 3/7/2025 9:22:53 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ__User__F3DBC572C29C5FE1] ON [dbo].[User]
(
	[username] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Answer] ADD  DEFAULT ((0)) FOR [points]
GO
ALTER TABLE [dbo].[Answer] ADD  DEFAULT (CONVERT([bit],(1))) FOR [is_active]
GO
ALTER TABLE [dbo].[Answer] ADD  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[Blog] ADD  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[Booking] ADD  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[CancelBooking] ADD  DEFAULT (getdate()) FOR [cancelled_at]
GO
ALTER TABLE [dbo].[CancelBooking] ADD  DEFAULT (CONVERT([bit],(0))) FOR [is_refunded]
GO
ALTER TABLE [dbo].[CancelPolicy] ADD  DEFAULT (CONVERT([bit],(1))) FOR [is_active]
GO
ALTER TABLE [dbo].[CancelPolicy] ADD  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[CustomerAnswer] ADD  DEFAULT ((0)) FOR [points_earned]
GO
ALTER TABLE [dbo].[CustomerAnswer] ADD  DEFAULT (getdate()) FOR [answered_at]
GO
ALTER TABLE [dbo].[CustomerFeedback] ADD  DEFAULT (getdate()) FOR [submitted_at]
GO
ALTER TABLE [dbo].[CustomerFeedbackAnswer] ADD  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[CustomerQuiz] ADD  DEFAULT (getdate()) FOR [started_at]
GO
ALTER TABLE [dbo].[CustomerRating] ADD  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[FeedbackAnswer] ADD  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[FeedbackAnswer] ADD  DEFAULT (getdate()) FOR [updated_at]
GO
ALTER TABLE [dbo].[FeedbackQuestion] ADD  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[FeedbackQuestion] ADD  DEFAULT (getdate()) FOR [updated_at]
GO
ALTER TABLE [dbo].[News] ADD  DEFAULT (getdate()) FOR [published_at]
GO
ALTER TABLE [dbo].[News] ADD  DEFAULT ((0)) FOR [is_published]
GO
ALTER TABLE [dbo].[Payment] ADD  DEFAULT (N'USD') FOR [currency]
GO
ALTER TABLE [dbo].[Payment] ADD  DEFAULT (getdate()) FOR [payment_date]
GO
ALTER TABLE [dbo].[PaymentPolicy] ADD  DEFAULT (N'USD') FOR [currency]
GO
ALTER TABLE [dbo].[PaymentPolicy] ADD  DEFAULT (CONVERT([bit],(1))) FOR [is_active]
GO
ALTER TABLE [dbo].[PaymentPolicy] ADD  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[Question] ADD  DEFAULT ((0)) FOR [points]
GO
ALTER TABLE [dbo].[Question] ADD  DEFAULT (CONVERT([bit],(0))) FOR [is_multiple_choice]
GO
ALTER TABLE [dbo].[Question] ADD  DEFAULT (CONVERT([bit],(1))) FOR [is_active]
GO
ALTER TABLE [dbo].[Question] ADD  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[Quiz] ADD  DEFAULT (CONVERT([bit],(1))) FOR [is_active]
GO
ALTER TABLE [dbo].[Quiz] ADD  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[QuizRecommendation] ADD  DEFAULT (CONVERT([bit],(1))) FOR [is_active]
GO
ALTER TABLE [dbo].[QuizRecommendation] ADD  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[Review] ADD  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[Service] ADD  DEFAULT (N'USD') FOR [currency]
GO
ALTER TABLE [dbo].[Service] ADD  DEFAULT (CONVERT([bit],(1))) FOR [is_active]
GO
ALTER TABLE [dbo].[Service] ADD  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[Service] ADD  DEFAULT ((0)) FOR [rating_count]
GO
ALTER TABLE [dbo].[Service] ADD  DEFAULT ((0)) FOR [rating]
GO
ALTER TABLE [dbo].[Therapist] ADD  DEFAULT (CONVERT([bit],(1))) FOR [is_available]
GO
ALTER TABLE [dbo].[Therapist] ADD  DEFAULT ((0)) FOR [rating_count]
GO
ALTER TABLE [dbo].[TherapistAvailability] ADD  DEFAULT (N'available') FOR [status]
GO
ALTER TABLE [dbo].[TherapistAvailability] ADD  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[TherapistProfile] ADD  DEFAULT (CONVERT([bit],(1))) FOR [accepts_new_clients]
GO
ALTER TABLE [dbo].[TherapistProfile] ADD  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[TherapistProfile] ADD  DEFAULT (getdate()) FOR [updated_at]
GO
ALTER TABLE [dbo].[TimeSlot] ADD  DEFAULT (CONVERT([bit],(1))) FOR [is_active]
GO
ALTER TABLE [dbo].[TimeSlot] ADD  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[User] ADD  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[WorkingDay] ADD  DEFAULT (CONVERT([bit],(1))) FOR [is_active]
GO
ALTER TABLE [dbo].[WorkingDay] ADD  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[Answer]  WITH CHECK ADD  CONSTRAINT [FK__Answer__question__619B8048] FOREIGN KEY([question_id])
REFERENCES [dbo].[Question] ([question_id])
GO
ALTER TABLE [dbo].[Answer] CHECK CONSTRAINT [FK__Answer__question__619B8048]
GO
ALTER TABLE [dbo].[Blog]  WITH CHECK ADD  CONSTRAINT [FK_Blog_Author] FOREIGN KEY([author_id])
REFERENCES [dbo].[User] ([user_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Blog] CHECK CONSTRAINT [FK_Blog_Author]
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD  CONSTRAINT [FK__Booking__custome__0D7A0286] FOREIGN KEY([customer_id])
REFERENCES [dbo].[Customer] ([customer_id])
GO
ALTER TABLE [dbo].[Booking] CHECK CONSTRAINT [FK__Booking__custome__0D7A0286]
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD  CONSTRAINT [FK__Booking__payment__114A936A] FOREIGN KEY([payment_id])
REFERENCES [dbo].[Payment] ([payment_id])
GO
ALTER TABLE [dbo].[Booking] CHECK CONSTRAINT [FK__Booking__payment__114A936A]
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD  CONSTRAINT [FK__Booking__service__0F624AF8] FOREIGN KEY([service_id])
REFERENCES [dbo].[Service] ([service_id])
GO
ALTER TABLE [dbo].[Booking] CHECK CONSTRAINT [FK__Booking__service__0F624AF8]
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD  CONSTRAINT [FK__Booking__slot_id__10566F31] FOREIGN KEY([slot_id])
REFERENCES [dbo].[TimeSlot] ([slot_id])
GO
ALTER TABLE [dbo].[Booking] CHECK CONSTRAINT [FK__Booking__slot_id__10566F31]
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD  CONSTRAINT [FK__Booking__therapi__0E6E26BF] FOREIGN KEY([therapist_id])
REFERENCES [dbo].[Therapist] ([therapist_id])
GO
ALTER TABLE [dbo].[Booking] CHECK CONSTRAINT [FK__Booking__therapi__0E6E26BF]
GO
ALTER TABLE [dbo].[CancelBooking]  WITH CHECK ADD  CONSTRAINT [FK__CancelBoo__booki__1CBC4616] FOREIGN KEY([booking_id])
REFERENCES [dbo].[Booking] ([booking_id])
GO
ALTER TABLE [dbo].[CancelBooking] CHECK CONSTRAINT [FK__CancelBoo__booki__1CBC4616]
GO
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK__Customer__user_i__3E52440B] FOREIGN KEY([user_id])
REFERENCES [dbo].[User] ([user_id])
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK__Customer__user_i__3E52440B]
GO
ALTER TABLE [dbo].[CustomerAnswer]  WITH CHECK ADD  CONSTRAINT [FK__CustomerA__answe__6D0D32F4] FOREIGN KEY([answer_id])
REFERENCES [dbo].[Answer] ([answer_id])
GO
ALTER TABLE [dbo].[CustomerAnswer] CHECK CONSTRAINT [FK__CustomerA__answe__6D0D32F4]
GO
ALTER TABLE [dbo].[CustomerAnswer]  WITH CHECK ADD  CONSTRAINT [FK__CustomerA__custo__6B24EA82] FOREIGN KEY([customer_quiz_id])
REFERENCES [dbo].[CustomerQuiz] ([customer_quiz_id])
GO
ALTER TABLE [dbo].[CustomerAnswer] CHECK CONSTRAINT [FK__CustomerA__custo__6B24EA82]
GO
ALTER TABLE [dbo].[CustomerAnswer]  WITH CHECK ADD  CONSTRAINT [FK__CustomerA__quest__6C190EBB] FOREIGN KEY([question_id])
REFERENCES [dbo].[Question] ([question_id])
GO
ALTER TABLE [dbo].[CustomerAnswer] CHECK CONSTRAINT [FK__CustomerA__quest__6C190EBB]
GO
ALTER TABLE [dbo].[CustomerFeedback]  WITH CHECK ADD  CONSTRAINT [FK_Customer_Feedbacks_Booking] FOREIGN KEY([booking_id])
REFERENCES [dbo].[Booking] ([booking_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CustomerFeedback] CHECK CONSTRAINT [FK_Customer_Feedbacks_Booking]
GO
ALTER TABLE [dbo].[CustomerFeedbackAnswer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_Feedback_Answers_Customer_Feedbacks] FOREIGN KEY([customer_feedback_id])
REFERENCES [dbo].[CustomerFeedback] ([customer_feedback_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CustomerFeedbackAnswer] CHECK CONSTRAINT [FK_Customer_Feedback_Answers_Customer_Feedbacks]
GO
ALTER TABLE [dbo].[CustomerQuiz]  WITH CHECK ADD  CONSTRAINT [FK__CustomerQ__custo__656C112C] FOREIGN KEY([customer_id])
REFERENCES [dbo].[Customer] ([customer_id])
GO
ALTER TABLE [dbo].[CustomerQuiz] CHECK CONSTRAINT [FK__CustomerQ__custo__656C112C]
GO
ALTER TABLE [dbo].[CustomerQuiz]  WITH CHECK ADD  CONSTRAINT [FK__CustomerQ__quiz___66603565] FOREIGN KEY([quiz_id])
REFERENCES [dbo].[Quiz] ([quiz_id])
GO
ALTER TABLE [dbo].[CustomerQuiz] CHECK CONSTRAINT [FK__CustomerQ__quiz___66603565]
GO
ALTER TABLE [dbo].[CustomerRating]  WITH CHECK ADD  CONSTRAINT [FK_Customer_Ratings_Booking] FOREIGN KEY([booking_id])
REFERENCES [dbo].[Booking] ([booking_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CustomerRating] CHECK CONSTRAINT [FK_Customer_Ratings_Booking]
GO
ALTER TABLE [dbo].[FeedbackAnswer]  WITH CHECK ADD  CONSTRAINT [FK_Feedback_Answers_Feedback_Questions] FOREIGN KEY([feedback_question_id])
REFERENCES [dbo].[FeedbackQuestion] ([question_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[FeedbackAnswer] CHECK CONSTRAINT [FK_Feedback_Answers_Feedback_Questions]
GO
ALTER TABLE [dbo].[Manager]  WITH CHECK ADD  CONSTRAINT [FK__Manager__user_id__5165187F] FOREIGN KEY([user_id])
REFERENCES [dbo].[User] ([user_id])
GO
ALTER TABLE [dbo].[Manager] CHECK CONSTRAINT [FK__Manager__user_id__5165187F]
GO
ALTER TABLE [dbo].[News]  WITH CHECK ADD  CONSTRAINT [FK_News_Publisher] FOREIGN KEY([publisher_id])
REFERENCES [dbo].[User] ([user_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[News] CHECK CONSTRAINT [FK_News_Publisher]
GO
ALTER TABLE [dbo].[Question]  WITH CHECK ADD  CONSTRAINT [FK__Question__quiz_i__5BE2A6F2] FOREIGN KEY([quiz_id])
REFERENCES [dbo].[Quiz] ([quiz_id])
GO
ALTER TABLE [dbo].[Question] CHECK CONSTRAINT [FK__Question__quiz_i__5BE2A6F2]
GO
ALTER TABLE [dbo].[QuizRecommendation]  WITH CHECK ADD  CONSTRAINT [FK__QuizRecom__quiz___76969D2E] FOREIGN KEY([quiz_id])
REFERENCES [dbo].[Quiz] ([quiz_id])
GO
ALTER TABLE [dbo].[QuizRecommendation] CHECK CONSTRAINT [FK__QuizRecom__quiz___76969D2E]
GO
ALTER TABLE [dbo].[QuizRecommendation]  WITH CHECK ADD  CONSTRAINT [FK__QuizRecom__servi__778AC167] FOREIGN KEY([service_id])
REFERENCES [dbo].[Service] ([service_id])
GO
ALTER TABLE [dbo].[QuizRecommendation] CHECK CONSTRAINT [FK__QuizRecom__servi__778AC167]
GO
ALTER TABLE [dbo].[Review]  WITH CHECK ADD  CONSTRAINT [FK__Review__booking___17036CC0] FOREIGN KEY([booking_id])
REFERENCES [dbo].[Booking] ([booking_id])
GO
ALTER TABLE [dbo].[Review] CHECK CONSTRAINT [FK__Review__booking___17036CC0]
GO
ALTER TABLE [dbo].[Staff]  WITH CHECK ADD  CONSTRAINT [FK__Staff__user_id__4D94879B] FOREIGN KEY([user_id])
REFERENCES [dbo].[User] ([user_id])
GO
ALTER TABLE [dbo].[Staff] CHECK CONSTRAINT [FK__Staff__user_id__4D94879B]
GO
ALTER TABLE [dbo].[Therapist]  WITH CHECK ADD  CONSTRAINT [FK__Therapist__user___4316F928] FOREIGN KEY([user_id])
REFERENCES [dbo].[User] ([user_id])
GO
ALTER TABLE [dbo].[Therapist] CHECK CONSTRAINT [FK__Therapist__user___4316F928]
GO
ALTER TABLE [dbo].[TherapistAvailability]  WITH CHECK ADD  CONSTRAINT [FK__Therapist__slot___05D8E0BE] FOREIGN KEY([slot_id])
REFERENCES [dbo].[TimeSlot] ([slot_id])
GO
ALTER TABLE [dbo].[TherapistAvailability] CHECK CONSTRAINT [FK__Therapist__slot___05D8E0BE]
GO
ALTER TABLE [dbo].[TherapistAvailability]  WITH CHECK ADD  CONSTRAINT [FK__Therapist__thera__04E4BC85] FOREIGN KEY([therapist_id])
REFERENCES [dbo].[Therapist] ([therapist_id])
GO
ALTER TABLE [dbo].[TherapistAvailability] CHECK CONSTRAINT [FK__Therapist__thera__04E4BC85]
GO
ALTER TABLE [dbo].[TherapistProfile]  WITH CHECK ADD  CONSTRAINT [FK__Therapist__thera__49C3F6B7] FOREIGN KEY([therapist_id])
REFERENCES [dbo].[Therapist] ([therapist_id])
GO
ALTER TABLE [dbo].[TherapistProfile] CHECK CONSTRAINT [FK__Therapist__thera__49C3F6B7]
GO
ALTER TABLE [dbo].[TimeSlot]  WITH CHECK ADD  CONSTRAINT [FK__TimeSlot__workin__00200768] FOREIGN KEY([working_day_id])
REFERENCES [dbo].[WorkingDay] ([working_day_id])
GO
ALTER TABLE [dbo].[TimeSlot] CHECK CONSTRAINT [FK__TimeSlot__workin__00200768]
GO
ALTER TABLE [dbo].[CustomerRating]  WITH CHECK ADD CHECK  (([rating_value]>=(1) AND [rating_value]<=(5)))
GO
ALTER DATABASE [prestinedb] SET  READ_WRITE 
GO
