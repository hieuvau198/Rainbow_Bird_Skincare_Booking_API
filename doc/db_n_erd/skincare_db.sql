-- Create Database
CREATE DATABASE SkincareDb
GO

USE SkincareDb
GO

-- Create User table
CREATE TABLE [User] (
    user_id INT IDENTITY(1,1) PRIMARY KEY,
    username NVARCHAR(50) NOT NULL UNIQUE,
    password NVARCHAR(255) NOT NULL,
    email NVARCHAR(100) NOT NULL UNIQUE,
    phone NVARCHAR(20),
    full_name NVARCHAR(100) NOT NULL,
    role NVARCHAR(20) CHECK (role IN ('customer', 'therapist', 'staff', 'manager')),
    created_at DATETIME DEFAULT GETDATE(),
    last_login_at DATETIME
)

-- Create Customer table
CREATE TABLE Customer (
    customer_id INT IDENTITY(1,1) PRIMARY KEY,
    user_id INT UNIQUE NOT NULL,
    preferences NVARCHAR(MAX),
    medical_history NVARCHAR(MAX),
    last_visit_at DATETIME,
    FOREIGN KEY (user_id) REFERENCES [User](user_id)
)

-- Create Therapist table
CREATE TABLE Therapist (
    therapist_id INT IDENTITY(1,1) PRIMARY KEY,
    user_id INT UNIQUE NOT NULL,
    is_available BIT DEFAULT 1,
    schedule NVARCHAR(MAX),
    rating DECIMAL(3,2),
    FOREIGN KEY (user_id) REFERENCES [User](user_id)
)

-- Create TherapistProfile table
CREATE TABLE TherapistProfile (
    profile_id INT IDENTITY(1,1) PRIMARY KEY,
    therapist_id INT UNIQUE NOT NULL,
    bio NVARCHAR(MAX),
    personal_statement NVARCHAR(MAX),
    profile_image NVARCHAR(255),
    education NVARCHAR(MAX),
    certifications NVARCHAR(MAX),
    specialties NVARCHAR(MAX),
    languages NVARCHAR(MAX),
    years_experience INT,
    accepts_new_clients BIT DEFAULT 1,
    created_at DATETIME DEFAULT GETDATE(),
    updated_at DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (therapist_id) REFERENCES Therapist(therapist_id)
)

-- Create Staff table
CREATE TABLE Staff (
    staff_id INT IDENTITY(1,1) PRIMARY KEY,
    user_id INT UNIQUE NOT NULL,
    department NVARCHAR(50),
    position NVARCHAR(50),
    hire_date DATETIME,
    FOREIGN KEY (user_id) REFERENCES [User](user_id)
)

-- Create Manager table
CREATE TABLE Manager (
    manager_id INT IDENTITY(1,1) PRIMARY KEY,
    user_id INT UNIQUE NOT NULL,
    department NVARCHAR(50),
    responsibilities NVARCHAR(MAX),
    hire_date DATETIME,
    FOREIGN KEY (user_id) REFERENCES [User](user_id)
)

-- Create Quiz table
CREATE TABLE Quiz (
    quiz_id INT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(100) NOT NULL,
    description NVARCHAR(MAX),
    category NVARCHAR(50),
    total_points INT,
    is_active BIT DEFAULT 1,
    created_at DATETIME DEFAULT GETDATE()
)

-- Create Question table
CREATE TABLE Question (
    question_id INT IDENTITY(1,1) PRIMARY KEY,
    quiz_id INT NOT NULL,
    content NVARCHAR(MAX) NOT NULL,
    points INT DEFAULT 0,
    is_multiple_choice BIT DEFAULT 0,
    display_order INT,
    is_active BIT DEFAULT 1,
    created_at DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (quiz_id) REFERENCES Quiz(quiz_id)
)

-- Create Answer table
CREATE TABLE Answer (
    answer_id INT IDENTITY(1,1) PRIMARY KEY,
    question_id INT NOT NULL,
    content NVARCHAR(MAX) NOT NULL,
    points INT DEFAULT 0,
    is_active BIT DEFAULT 1,
    created_at DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (question_id) REFERENCES Question(question_id)
)

-- Create CustomerQuiz table
CREATE TABLE CustomerQuiz (
    customer_quiz_id INT IDENTITY(1,1) PRIMARY KEY,
    customer_id INT NOT NULL,
    quiz_id INT NOT NULL,
    total_score INT,
    status NVARCHAR(20),
    started_at DATETIME DEFAULT GETDATE(),
    completed_at DATETIME,
    FOREIGN KEY (customer_id) REFERENCES Customer(customer_id),
    FOREIGN KEY (quiz_id) REFERENCES Quiz(quiz_id)
)

-- Create CustomerAnswer table
CREATE TABLE CustomerAnswer (
    customer_answer_id INT IDENTITY(1,1) PRIMARY KEY,
    customer_quiz_id INT NOT NULL,
    question_id INT NOT NULL,
    answer_id INT NOT NULL,
    points_earned INT DEFAULT 0,
    answered_at DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (customer_quiz_id) REFERENCES CustomerQuiz(customer_quiz_id),
    FOREIGN KEY (question_id) REFERENCES Question(question_id),
    FOREIGN KEY (answer_id) REFERENCES Answer(answer_id)
)

-- Create Service table
CREATE TABLE Service (
    service_id INT IDENTITY(1,1) PRIMARY KEY,
    service_name NVARCHAR(100) NOT NULL,
    description NVARCHAR(MAX),
    price DECIMAL(10,2) NOT NULL,
    currency NVARCHAR(3) DEFAULT 'USD',
    duration_minutes INT NOT NULL,
    location NVARCHAR(100),
    is_active BIT DEFAULT 1,
    created_at DATETIME DEFAULT GETDATE()
)

-- Create QuizRecommendation table
CREATE TABLE QuizRecommendation (
    recommendation_id INT IDENTITY(1,1) PRIMARY KEY,
    quiz_id INT NOT NULL,
    service_id INT NOT NULL,
    min_score INT,
    max_score INT,
    is_active BIT DEFAULT 1,
    created_at DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (quiz_id) REFERENCES Quiz(quiz_id),
    FOREIGN KEY (service_id) REFERENCES Service(service_id)
)

-- Create WorkingDay table
CREATE TABLE WorkingDay (
    working_day_id INT IDENTITY(1,1) PRIMARY KEY,
    day_name NVARCHAR(20) NOT NULL,
    start_time TIME NOT NULL,
    end_time TIME NOT NULL,
    slot_duration_minutes INT NOT NULL,
    is_active BIT DEFAULT 1,
    created_at DATETIME DEFAULT GETDATE()
)

-- Create TimeSlot table
CREATE TABLE TimeSlot (
    slot_id INT IDENTITY(1,1) PRIMARY KEY,
    working_day_id INT NOT NULL,
    start_time TIME NOT NULL,
    end_time TIME NOT NULL,
    slot_number INT NOT NULL,
    is_active BIT DEFAULT 1,
    created_at DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (working_day_id) REFERENCES WorkingDay(working_day_id)
)

-- Create TherapistAvailability table
CREATE TABLE TherapistAvailability (
    availability_id INT IDENTITY(1,1) PRIMARY KEY,
    therapist_id INT NOT NULL,
    slot_id INT NOT NULL,
    working_date DATE NOT NULL,
    status NVARCHAR(20) DEFAULT 'available',
    created_at DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (therapist_id) REFERENCES Therapist(therapist_id),
    FOREIGN KEY (slot_id) REFERENCES TimeSlot(slot_id)
)

-- Create Payment table
CREATE TABLE Payment (
    payment_id INT IDENTITY(1,1) PRIMARY KEY,
    total_amount DECIMAL(10,2) NOT NULL,
    currency NVARCHAR(3) DEFAULT 'USD',
    payment_method NVARCHAR(50),
    status NVARCHAR(20),
    payment_date DATETIME DEFAULT GETDATE()
)

-- Create Booking table
CREATE TABLE Booking (
    booking_id INT IDENTITY(1,1) PRIMARY KEY,
    customer_id INT NOT NULL,
    therapist_id INT NOT NULL,
    service_id INT NOT NULL,
    slot_id INT NOT NULL,
    payment_id INT,
    booking_date DATE NOT NULL,
    status NVARCHAR(20),
    notes NVARCHAR(MAX),
    created_at DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (customer_id) REFERENCES Customer(customer_id),
    FOREIGN KEY (therapist_id) REFERENCES Therapist(therapist_id),
    FOREIGN KEY (service_id) REFERENCES Service(service_id),
    FOREIGN KEY (slot_id) REFERENCES TimeSlot(slot_id),
    FOREIGN KEY (payment_id) REFERENCES Payment(payment_id)
)

-- Create Review table
CREATE TABLE Review (
    review_id INT IDENTITY(1,1) PRIMARY KEY,
    booking_id INT UNIQUE NOT NULL,
    rating INT CHECK (rating BETWEEN 1 AND 5),
    feedback NVARCHAR(MAX),
    created_at DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (booking_id) REFERENCES Booking(booking_id)
)

-- Create CancelBooking table
CREATE TABLE CancelBooking (
    cancellation_id INT IDENTITY(1,1) PRIMARY KEY,
    booking_id INT UNIQUE NOT NULL,
    cancelled_at DATETIME DEFAULT GETDATE(),
    reason NVARCHAR(MAX),
    is_refunded BIT DEFAULT 0,
    FOREIGN KEY (booking_id) REFERENCES Booking(booking_id)
)

-- Create CancelPolicy table
CREATE TABLE CancelPolicy (
    policy_id INT IDENTITY(1,1) PRIMARY KEY,
    max_cancellations INT,
    waiting_time_minutes INT,
    is_active BIT DEFAULT 1,
    created_at DATETIME DEFAULT GETDATE()
)

-- Create PaymentPolicy table
CREATE TABLE PaymentPolicy (
    policy_id INT IDENTITY(1,1) PRIMARY KEY,
    currency NVARCHAR(3) DEFAULT 'USD',
    payment_window_hours INT,
    tax_percentage DECIMAL(5,2),
    is_active BIT DEFAULT 1,
    created_at DATETIME DEFAULT GETDATE()
)

-- Create indexes for better performance
CREATE INDEX IX_User_Email ON [User](email)
CREATE INDEX IX_User_Username ON [User](username)
CREATE INDEX IX_Booking_Date ON Booking(booking_date)
CREATE INDEX IX_TherapistAvailability_Date ON TherapistAvailability(working_date)
CREATE INDEX IX_CustomerQuiz_Customer ON CustomerQuiz(customer_id)
CREATE INDEX IX_Booking_Customer ON Booking(customer_id)
CREATE INDEX IX_Booking_Therapist ON Booking(therapist_id)
