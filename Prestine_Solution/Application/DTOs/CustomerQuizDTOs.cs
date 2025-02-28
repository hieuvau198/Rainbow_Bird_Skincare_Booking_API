﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CustomerQuizDto
    {
        public int CustomerQuizId { get; set; }
        public int CustomerId { get; set; }
        public int QuizId { get; set; }
        public int? TotalScore { get; set; }
        public string Status { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public CustomerDto Customer { get; set; } = null!;
        public QuizDto Quiz { get; set; } = null!;
    }

    public class CreateCustomerQuizDto
    {
        public int CustomerId { get; set; }
        public int QuizId { get; set; }
    }

    public class UpdateCustomerQuizDto
    {
        public int? TotalScore { get; set; }
        public string Status { get; set; }
    }
}
