-- ========================================
-- Feedback System Tables
-- ========================================

-- Feedback Questions Table
CREATE TABLE [dbo].[FeedbackQuestion](
    [question_id] INT IDENTITY(1,1) NOT NULL,
    [question_text] NVARCHAR(MAX) NOT NULL,
    [created_at] DATETIME DEFAULT GETDATE(),
    [updated_at] DATETIME DEFAULT GETDATE(),
    CONSTRAINT [PK_Feedback_Questions] PRIMARY KEY CLUSTERED ([question_id] ASC)
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];
GO

-- Feedback Answers Table
CREATE TABLE [dbo].[FeedbackAnswer](
    [answer_option_id] INT IDENTITY(1,1) NOT NULL,
    [feedback_question_id] INT NOT NULL,
    [answer_text] NVARCHAR(MAX) NOT NULL,
    [created_at] DATETIME DEFAULT GETDATE(),
    [updated_at] DATETIME DEFAULT GETDATE(),
    CONSTRAINT [PK_Feedback_Answers] PRIMARY KEY CLUSTERED ([answer_option_id] ASC),
    CONSTRAINT [FK_Feedback_Answers_Feedback_Questions] FOREIGN KEY([feedback_question_id])
        REFERENCES [dbo].[FeedbackQuestion] ([question_id]) ON DELETE CASCADE
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];
GO

-- ========================================
-- Customer Feedback Tables
-- ========================================

-- Customer Feedback Table
CREATE TABLE [dbo].[CustomerFeedback](
    [customer_feedback_id] INT IDENTITY(1,1) NOT NULL,
    [booking_id] INT NOT NULL,
    [submitted_at] DATETIME DEFAULT GETDATE(),
    CONSTRAINT [PK_Customer_Feedbacks] PRIMARY KEY CLUSTERED ([customer_feedback_id] ASC),
    CONSTRAINT [FK_Customer_Feedbacks_Booking] FOREIGN KEY([booking_id])
        REFERENCES [dbo].[Booking] ([booking_id]) ON DELETE CASCADE
) ON [PRIMARY];
GO

-- Customer Feedback Answers Table
CREATE TABLE [dbo].[CustomerFeedbackAnswer](
    [response_id] INT IDENTITY(1,1) NOT NULL,
    [customer_feedback_id] INT NOT NULL,
    [answer_text] NVARCHAR(MAX) NULL,  -- Open-ended responses
    [selected_answer_option_id] INT NULL,  -- If selecting from predefined options
    [created_at] DATETIME DEFAULT GETDATE(),
    CONSTRAINT [PK_Customer_Feedback_Answers] PRIMARY KEY CLUSTERED ([response_id] ASC),
    CONSTRAINT [FK_Customer_Feedback_Answers_Customer_Feedbacks] FOREIGN KEY([customer_feedback_id])
        REFERENCES [dbo].[CustomerFeedback] ([customer_feedback_id]) ON DELETE CASCADE
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];
GO

-- ========================================
-- Customer Rating Table
-- ========================================

-- Customer Ratings Table
CREATE TABLE [dbo].[CustomerRating](
    [rating_id] INT IDENTITY(1,1) NOT NULL,
    [booking_id] INT NOT NULL,
    [rating_value] INT NOT NULL CHECK ([rating_value] BETWEEN 1 AND 5),
    [experience_image_url] NVARCHAR(500) NULL,
    [comment] NVARCHAR(MAX) NULL,
    [created_at] DATETIME DEFAULT GETDATE(),
    CONSTRAINT [PK_Customer_Ratings] PRIMARY KEY CLUSTERED ([rating_id] ASC),
    CONSTRAINT [FK_Customer_Ratings_Booking] FOREIGN KEY([booking_id])
        REFERENCES [dbo].[Booking] ([booking_id]) ON DELETE CASCADE
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];
GO
