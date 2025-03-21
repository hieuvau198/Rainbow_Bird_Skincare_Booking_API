﻿using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Service
{
    public int ServiceId { get; set; }

    public string ServiceName { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public string? Currency { get; set; }

    public int DurationMinutes { get; set; }

    public string? Location { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? ServiceImage { get; set; }

    public int? RatingCount { get; set; }

    public decimal? Rating { get; set; }

    public decimal? AverageReview { get; set; }

    public string? ShortDescription { get; set; }

    public int BookingNumber { get; set; }

    public int? CategoryId { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ServiceCategory? Category { get; set; }

    public virtual ICollection<QuizRecommendation> QuizRecommendations { get; set; } = new List<QuizRecommendation>();
}
