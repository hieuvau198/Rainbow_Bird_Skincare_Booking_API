using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CancelPolicy",
                columns: table => new
                {
                    policy_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    max_cancellations = table.Column<int>(type: "int", nullable: true),
                    waiting_time_minutes = table.Column<int>(type: "int", nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CancelPo__47DA3F03A0859F16", x => x.policy_id);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    payment_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    total_amount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    currency = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true, defaultValue: "USD"),
                    payment_method = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    payment_date = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Payment__ED1FC9EAA349FC37", x => x.payment_id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentPolicy",
                columns: table => new
                {
                    policy_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    currency = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true, defaultValue: "USD"),
                    payment_window_hours = table.Column<int>(type: "int", nullable: true),
                    tax_percentage = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PaymentP__47DA3F0323B2C361", x => x.policy_id);
                });

            migrationBuilder.CreateTable(
                name: "Quiz",
                columns: table => new
                {
                    quiz_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    total_points = table.Column<int>(type: "int", nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Quiz__2D7053ECA184AAA8", x => x.quiz_id);
                });

            migrationBuilder.CreateTable(
                name: "Service",
                columns: table => new
                {
                    service_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    service_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    currency = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true, defaultValue: "USD"),
                    duration_minutes = table.Column<int>(type: "int", nullable: false),
                    location = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Service__3E0DB8AFBACF949C", x => x.service_id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    full_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    role = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    last_login_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__User__B9BE370FBC7F1B8F", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "WorkingDay",
                columns: table => new
                {
                    working_day_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    day_name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    start_time = table.Column<TimeOnly>(type: "time", nullable: false),
                    end_time = table.Column<TimeOnly>(type: "time", nullable: false),
                    slot_duration_minutes = table.Column<int>(type: "int", nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__WorkingD__FE446ADF3074D242", x => x.working_day_id);
                });

            migrationBuilder.CreateTable(
                name: "Question",
                columns: table => new
                {
                    question_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    quiz_id = table.Column<int>(type: "int", nullable: false),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    points = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    is_multiple_choice = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    display_order = table.Column<int>(type: "int", nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Question__2EC2154994824B43", x => x.question_id);
                    table.ForeignKey(
                        name: "FK__Question__quiz_i__5BE2A6F2",
                        column: x => x.quiz_id,
                        principalTable: "Quiz",
                        principalColumn: "quiz_id");
                });

            migrationBuilder.CreateTable(
                name: "QuizRecommendation",
                columns: table => new
                {
                    recommendation_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    quiz_id = table.Column<int>(type: "int", nullable: false),
                    service_id = table.Column<int>(type: "int", nullable: false),
                    min_score = table.Column<int>(type: "int", nullable: true),
                    max_score = table.Column<int>(type: "int", nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__QuizReco__BCB11F4FB1377181", x => x.recommendation_id);
                    table.ForeignKey(
                        name: "FK__QuizRecom__quiz___76969D2E",
                        column: x => x.quiz_id,
                        principalTable: "Quiz",
                        principalColumn: "quiz_id");
                    table.ForeignKey(
                        name: "FK__QuizRecom__servi__778AC167",
                        column: x => x.service_id,
                        principalTable: "Service",
                        principalColumn: "service_id");
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    customer_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    preferences = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    medical_history = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    last_visit_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Customer__CD65CB85E44E2287", x => x.customer_id);
                    table.ForeignKey(
                        name: "FK__Customer__user_i__3E52440B",
                        column: x => x.user_id,
                        principalTable: "User",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "Manager",
                columns: table => new
                {
                    manager_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    department = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    responsibilities = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    hire_date = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Manager__5A6073FC39E06974", x => x.manager_id);
                    table.ForeignKey(
                        name: "FK__Manager__user_id__5165187F",
                        column: x => x.user_id,
                        principalTable: "User",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "Staff",
                columns: table => new
                {
                    staff_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    department = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    position = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    hire_date = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Staff__1963DD9CF72FBC06", x => x.staff_id);
                    table.ForeignKey(
                        name: "FK__Staff__user_id__4D94879B",
                        column: x => x.user_id,
                        principalTable: "User",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "Therapist",
                columns: table => new
                {
                    therapist_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    is_available = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    schedule = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    rating = table.Column<decimal>(type: "decimal(3,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Therapis__16DFDBD188680417", x => x.therapist_id);
                    table.ForeignKey(
                        name: "FK__Therapist__user___4316F928",
                        column: x => x.user_id,
                        principalTable: "User",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "TimeSlot",
                columns: table => new
                {
                    slot_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    working_day_id = table.Column<int>(type: "int", nullable: false),
                    start_time = table.Column<TimeOnly>(type: "time", nullable: false),
                    end_time = table.Column<TimeOnly>(type: "time", nullable: false),
                    slot_number = table.Column<int>(type: "int", nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TimeSlot__971A01BB3891BB3F", x => x.slot_id);
                    table.ForeignKey(
                        name: "FK__TimeSlot__workin__00200768",
                        column: x => x.working_day_id,
                        principalTable: "WorkingDay",
                        principalColumn: "working_day_id");
                });

            migrationBuilder.CreateTable(
                name: "Answer",
                columns: table => new
                {
                    answer_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    question_id = table.Column<int>(type: "int", nullable: false),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    points = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    is_active = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Answer__33724318250B0AF4", x => x.answer_id);
                    table.ForeignKey(
                        name: "FK__Answer__question__619B8048",
                        column: x => x.question_id,
                        principalTable: "Question",
                        principalColumn: "question_id");
                });

            migrationBuilder.CreateTable(
                name: "CustomerQuiz",
                columns: table => new
                {
                    customer_quiz_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    customer_id = table.Column<int>(type: "int", nullable: false),
                    quiz_id = table.Column<int>(type: "int", nullable: false),
                    total_score = table.Column<int>(type: "int", nullable: true),
                    status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    started_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    completed_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Customer__A70E104FCC2E841A", x => x.customer_quiz_id);
                    table.ForeignKey(
                        name: "FK__CustomerQ__custo__656C112C",
                        column: x => x.customer_id,
                        principalTable: "Customer",
                        principalColumn: "customer_id");
                    table.ForeignKey(
                        name: "FK__CustomerQ__quiz___66603565",
                        column: x => x.quiz_id,
                        principalTable: "Quiz",
                        principalColumn: "quiz_id");
                });

            migrationBuilder.CreateTable(
                name: "TherapistProfile",
                columns: table => new
                {
                    profile_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    therapist_id = table.Column<int>(type: "int", nullable: false),
                    bio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    personal_statement = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    profile_image = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    education = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    certifications = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    specialties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    languages = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    years_experience = table.Column<int>(type: "int", nullable: true),
                    accepts_new_clients = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Therapis__AEBB701F4784F495", x => x.profile_id);
                    table.ForeignKey(
                        name: "FK__Therapist__thera__49C3F6B7",
                        column: x => x.therapist_id,
                        principalTable: "Therapist",
                        principalColumn: "therapist_id");
                });

            migrationBuilder.CreateTable(
                name: "Booking",
                columns: table => new
                {
                    booking_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    customer_id = table.Column<int>(type: "int", nullable: false),
                    therapist_id = table.Column<int>(type: "int", nullable: false),
                    service_id = table.Column<int>(type: "int", nullable: false),
                    slot_id = table.Column<int>(type: "int", nullable: false),
                    payment_id = table.Column<int>(type: "int", nullable: true),
                    booking_date = table.Column<DateOnly>(type: "date", nullable: false),
                    status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Booking__5DE3A5B1E7F536B6", x => x.booking_id);
                    table.ForeignKey(
                        name: "FK__Booking__custome__0D7A0286",
                        column: x => x.customer_id,
                        principalTable: "Customer",
                        principalColumn: "customer_id");
                    table.ForeignKey(
                        name: "FK__Booking__payment__114A936A",
                        column: x => x.payment_id,
                        principalTable: "Payment",
                        principalColumn: "payment_id");
                    table.ForeignKey(
                        name: "FK__Booking__service__0F624AF8",
                        column: x => x.service_id,
                        principalTable: "Service",
                        principalColumn: "service_id");
                    table.ForeignKey(
                        name: "FK__Booking__slot_id__10566F31",
                        column: x => x.slot_id,
                        principalTable: "TimeSlot",
                        principalColumn: "slot_id");
                    table.ForeignKey(
                        name: "FK__Booking__therapi__0E6E26BF",
                        column: x => x.therapist_id,
                        principalTable: "Therapist",
                        principalColumn: "therapist_id");
                });

            migrationBuilder.CreateTable(
                name: "TherapistAvailability",
                columns: table => new
                {
                    availability_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    therapist_id = table.Column<int>(type: "int", nullable: false),
                    slot_id = table.Column<int>(type: "int", nullable: false),
                    working_date = table.Column<DateOnly>(type: "date", nullable: false),
                    status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, defaultValue: "available"),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Therapis__86E3A801B78752C3", x => x.availability_id);
                    table.ForeignKey(
                        name: "FK__Therapist__slot___05D8E0BE",
                        column: x => x.slot_id,
                        principalTable: "TimeSlot",
                        principalColumn: "slot_id");
                    table.ForeignKey(
                        name: "FK__Therapist__thera__04E4BC85",
                        column: x => x.therapist_id,
                        principalTable: "Therapist",
                        principalColumn: "therapist_id");
                });

            migrationBuilder.CreateTable(
                name: "CustomerAnswer",
                columns: table => new
                {
                    customer_answer_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    customer_quiz_id = table.Column<int>(type: "int", nullable: false),
                    question_id = table.Column<int>(type: "int", nullable: false),
                    answer_id = table.Column<int>(type: "int", nullable: false),
                    points_earned = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    answered_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Customer__F5422C52BDDEAA8C", x => x.customer_answer_id);
                    table.ForeignKey(
                        name: "FK__CustomerA__answe__6D0D32F4",
                        column: x => x.answer_id,
                        principalTable: "Answer",
                        principalColumn: "answer_id");
                    table.ForeignKey(
                        name: "FK__CustomerA__custo__6B24EA82",
                        column: x => x.customer_quiz_id,
                        principalTable: "CustomerQuiz",
                        principalColumn: "customer_quiz_id");
                    table.ForeignKey(
                        name: "FK__CustomerA__quest__6C190EBB",
                        column: x => x.question_id,
                        principalTable: "Question",
                        principalColumn: "question_id");
                });

            migrationBuilder.CreateTable(
                name: "CancelBooking",
                columns: table => new
                {
                    cancellation_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    booking_id = table.Column<int>(type: "int", nullable: false),
                    cancelled_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    is_refunded = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CancelBo__4ED4366DBC764A69", x => x.cancellation_id);
                    table.ForeignKey(
                        name: "FK__CancelBoo__booki__1CBC4616",
                        column: x => x.booking_id,
                        principalTable: "Booking",
                        principalColumn: "booking_id");
                });

            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    review_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    booking_id = table.Column<int>(type: "int", nullable: false),
                    rating = table.Column<int>(type: "int", nullable: true),
                    feedback = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Review__60883D9004E8B032", x => x.review_id);
                    table.ForeignKey(
                        name: "FK__Review__booking___17036CC0",
                        column: x => x.booking_id,
                        principalTable: "Booking",
                        principalColumn: "booking_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answer_question_id",
                table: "Answer",
                column: "question_id");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_Customer",
                table: "Booking",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_Date",
                table: "Booking",
                column: "booking_date");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_payment_id",
                table: "Booking",
                column: "payment_id");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_service_id",
                table: "Booking",
                column: "service_id");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_slot_id",
                table: "Booking",
                column: "slot_id");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_Therapist",
                table: "Booking",
                column: "therapist_id");

            migrationBuilder.CreateIndex(
                name: "UQ__CancelBo__5DE3A5B08EDFD0DB",
                table: "CancelBooking",
                column: "booking_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Customer__B9BE370E4F7BA8E3",
                table: "Customer",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAnswer_answer_id",
                table: "CustomerAnswer",
                column: "answer_id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAnswer_customer_quiz_id",
                table: "CustomerAnswer",
                column: "customer_quiz_id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAnswer_question_id",
                table: "CustomerAnswer",
                column: "question_id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerQuiz_Customer",
                table: "CustomerQuiz",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerQuiz_quiz_id",
                table: "CustomerQuiz",
                column: "quiz_id");

            migrationBuilder.CreateIndex(
                name: "UQ__Manager__B9BE370E2630FC25",
                table: "Manager",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Question_quiz_id",
                table: "Question",
                column: "quiz_id");

            migrationBuilder.CreateIndex(
                name: "IX_QuizRecommendation_quiz_id",
                table: "QuizRecommendation",
                column: "quiz_id");

            migrationBuilder.CreateIndex(
                name: "IX_QuizRecommendation_service_id",
                table: "QuizRecommendation",
                column: "service_id");

            migrationBuilder.CreateIndex(
                name: "UQ__Review__5DE3A5B01C922905",
                table: "Review",
                column: "booking_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Staff__B9BE370E56FBC23C",
                table: "Staff",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Therapis__B9BE370EA60BC22D",
                table: "Therapist",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TherapistAvailability_Date",
                table: "TherapistAvailability",
                column: "working_date");

            migrationBuilder.CreateIndex(
                name: "IX_TherapistAvailability_slot_id",
                table: "TherapistAvailability",
                column: "slot_id");

            migrationBuilder.CreateIndex(
                name: "IX_TherapistAvailability_therapist_id",
                table: "TherapistAvailability",
                column: "therapist_id");

            migrationBuilder.CreateIndex(
                name: "UQ__Therapis__16DFDBD0353BAE08",
                table: "TherapistProfile",
                column: "therapist_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TimeSlot_working_day_id",
                table: "TimeSlot",
                column: "working_day_id");

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "email");

            migrationBuilder.CreateIndex(
                name: "IX_User_Username",
                table: "User",
                column: "username");

            migrationBuilder.CreateIndex(
                name: "UQ__User__AB6E6164323A2DBF",
                table: "User",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__User__F3DBC572C29C5FE1",
                table: "User",
                column: "username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CancelBooking");

            migrationBuilder.DropTable(
                name: "CancelPolicy");

            migrationBuilder.DropTable(
                name: "CustomerAnswer");

            migrationBuilder.DropTable(
                name: "Manager");

            migrationBuilder.DropTable(
                name: "PaymentPolicy");

            migrationBuilder.DropTable(
                name: "QuizRecommendation");

            migrationBuilder.DropTable(
                name: "Review");

            migrationBuilder.DropTable(
                name: "Staff");

            migrationBuilder.DropTable(
                name: "TherapistAvailability");

            migrationBuilder.DropTable(
                name: "TherapistProfile");

            migrationBuilder.DropTable(
                name: "Answer");

            migrationBuilder.DropTable(
                name: "CustomerQuiz");

            migrationBuilder.DropTable(
                name: "Booking");

            migrationBuilder.DropTable(
                name: "Question");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "Service");

            migrationBuilder.DropTable(
                name: "TimeSlot");

            migrationBuilder.DropTable(
                name: "Therapist");

            migrationBuilder.DropTable(
                name: "Quiz");

            migrationBuilder.DropTable(
                name: "WorkingDay");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
