using System;
using System.Collections.Generic;

namespace Domain.Entities;
public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public string FullName { get; set; } = null!;

    public int? Role { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? LastLoginAt { get; set; }

    public string? RefreshToken { get; set; }

    public DateTime? RefreshTokenExpiryTime { get; set; }

    public virtual ICollection<Blog> Blogs { get; set; } = new List<Blog>();

    public virtual Customer? Customer { get; set; }

    public virtual Manager? Manager { get; set; }

    public virtual ICollection<News> News { get; set; } = new List<News>();

    public virtual Staff? Staff { get; set; }

    public virtual Therapist? Therapist { get; set; }
}
