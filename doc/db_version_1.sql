-- Users table
CREATE TABLE users (
    user_id INT PRIMARY KEY AUTO_INCREMENT,
    username VARCHAR(50) NOT NULL UNIQUE,
    password VARCHAR(255) NOT NULL,
    email VARCHAR(100) NOT NULL UNIQUE,
    phone VARCHAR(20) NOT NULL,
    full_name VARCHAR(100) NOT NULL,
    role VARCHAR(20) NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    last_login_at TIMESTAMP
);

-- Services table
CREATE TABLE services (
    service_id INT PRIMARY KEY AUTO_INCREMENT,
    service_name VARCHAR(100) NOT NULL,
    description TEXT,
    price DECIMAL(10,2) NOT NULL,
    currency VARCHAR(3) NOT NULL,
    duration_minutes INT NOT NULL,
    location VARCHAR(100) NOT NULL,
    is_active BOOLEAN DEFAULT true,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Questions table
CREATE TABLE questions (
    question_id INT PRIMARY KEY AUTO_INCREMENT,
    description VARCHAR(200) NOT NULL,
    is_active BOOLEAN DEFAULT true,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Answers table
CREATE TABLE answers (
    answer_id INT PRIMARY KEY AUTO_INCREMENT,
    question_id INT NOT NULL,
    description VARCHAR(200) NOT NULL,
    is_active BOOLEAN DEFAULT true,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (question_id) REFERENCES questions(question_id)
);

-- Answer Recommendations table
CREATE TABLE answer_recommendations (
    recommendation_id INT PRIMARY KEY AUTO_INCREMENT,
    answer_id INT NOT NULL,
    service_id INT NOT NULL,
    is_active BOOLEAN DEFAULT true,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (answer_id) REFERENCES answers(answer_id),
    FOREIGN KEY (service_id) REFERENCES services(service_id)
);

-- Customers table
CREATE TABLE customers (
    customer_id INT PRIMARY KEY AUTO_INCREMENT,
    user_id INT NOT NULL UNIQUE,
    preferences TEXT,
    medical_history TEXT,
    last_visit_at TIMESTAMP,
    FOREIGN KEY (user_id) REFERENCES users(user_id)
);

-- Therapists table
CREATE TABLE therapists (
    therapist_id INT PRIMARY KEY AUTO_INCREMENT,
    user_id INT NOT NULL UNIQUE,
    specialization VARCHAR(100),
    bio TEXT,
    is_available BOOLEAN DEFAULT true,
    schedule TEXT,
    rating DECIMAL(3,2),
    FOREIGN KEY (user_id) REFERENCES users(user_id)
);

-- Bookings table
CREATE TABLE bookings (
    booking_id INT PRIMARY KEY AUTO_INCREMENT,
    customer_id INT NOT NULL,
    therapist_id INT NOT NULL,
    service_id INT NOT NULL,
    booking_date DATE NOT NULL,
    start_time TIMESTAMP NOT NULL,
    end_time TIMESTAMP NOT NULL,
    status VARCHAR(20) NOT NULL,
    notes TEXT,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (customer_id) REFERENCES customers(customer_id),
    FOREIGN KEY (therapist_id) REFERENCES therapists(therapist_id),
    FOREIGN KEY (service_id) REFERENCES services(service_id)
);

-- Payments table
CREATE TABLE payments (
    payment_id INT PRIMARY KEY AUTO_INCREMENT,
    booking_id INT NOT NULL UNIQUE,
    amount DECIMAL(10,2) NOT NULL,
    currency VARCHAR(3) NOT NULL,
    payment_method VARCHAR(50) NOT NULL,
    status VARCHAR(20) NOT NULL,
    payment_date TIMESTAMP NOT NULL,
    transaction_id VARCHAR(100) UNIQUE,
    FOREIGN KEY (booking_id) REFERENCES bookings(booking_id)
);

-- Reviews table
CREATE TABLE reviews (
    review_id INT PRIMARY KEY AUTO_INCREMENT,
    booking_id INT NOT NULL UNIQUE,
    rating INT NOT NULL,
    feedback TEXT,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (booking_id) REFERENCES bookings(booking_id)
);

-- Cancel Bookings table
CREATE TABLE cancel_bookings (
    cancellation_id INT PRIMARY KEY AUTO_INCREMENT,
    booking_id INT NOT NULL UNIQUE,
    cancelled_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    reason TEXT,
    is_refunded BOOLEAN DEFAULT false,
    FOREIGN KEY (booking_id) REFERENCES bookings(booking_id)
);

-- Cancel Policy table
CREATE TABLE cancel_policies (
    policy_id INT PRIMARY KEY AUTO_INCREMENT,
    max_cancellations INT NOT NULL,
    waiting_time_minutes INT NOT NULL,
    is_active BOOLEAN DEFAULT true,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Payment Policy table
CREATE TABLE payment_policies (
    policy_id INT PRIMARY KEY AUTO_INCREMENT,
    minimum_payment_amount DECIMAL(10,2) NOT NULL,
    currency VARCHAR(3) NOT NULL,
    payment_window_hours INT NOT NULL,
    requires_deposit BOOLEAN DEFAULT false,
    deposit_percentage DECIMAL(5,2),
    is_active BOOLEAN DEFAULT true,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Staff table
CREATE TABLE staff (
    staff_id INT PRIMARY KEY AUTO_INCREMENT,
    user_id INT NOT NULL UNIQUE,
    department VARCHAR(50) NOT NULL,
    position VARCHAR(50) NOT NULL,
    hire_date DATE NOT NULL,
    FOREIGN KEY (user_id) REFERENCES users(user_id)
);

-- Managers table
CREATE TABLE managers (
    manager_id INT PRIMARY KEY AUTO_INCREMENT,
    user_id INT NOT NULL UNIQUE,
    department VARCHAR(50) NOT NULL,
    responsibilities TEXT,
    hire_date DATE NOT NULL,
    FOREIGN KEY (user_id) REFERENCES users(user_id)
);

-- Reports table
CREATE TABLE reports (
    report_id INT PRIMARY KEY AUTO_INCREMENT,
    staff_id INT NOT NULL,
    manager_id INT NOT NULL,
    report_type VARCHAR(50) NOT NULL,
    content TEXT NOT NULL,
    status VARCHAR(20) NOT NULL,
    report_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (staff_id) REFERENCES staff(staff_id),
    FOREIGN KEY (manager_id) REFERENCES managers(manager_id)
);

-- Add constraints to ensure mandatory answers for questions
ALTER TABLE answers
ADD CONSTRAINT question_must_have_answer 
CHECK (question_id IN (SELECT question_id FROM answers GROUP BY question_id HAVING COUNT(*) >= 1));

-- Add constraints to ensure mandatory recommendations for answers
ALTER TABLE answer_recommendations
ADD CONSTRAINT answer_must_have_recommendation
CHECK (answer_id IN (SELECT answer_id FROM answer_recommendations GROUP BY answer_id HAVING COUNT(*) >= 1));

-- Add constraints to ensure mandatory payment for booking
ALTER TABLE payments
ADD CONSTRAINT booking_must_have_payment
CHECK (booking_id IN (SELECT booking_id FROM payments));
