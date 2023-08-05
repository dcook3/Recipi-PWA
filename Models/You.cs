using Recipi_PWA.Models;
using System;
using System.Collections.Generic;

namespace Recipi_PWA.Models;

public class You : IUserProfile
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string? ProfilePicture { get; set; }

    public string? Biography { get; set; }

    public UserStats userStats { get; set; }

    public List<string> YourRelationships { get; set; } = new();

    public string Email { get; set; } = null!;

    public byte Verified { get; set; }

    public DateTime RegisteredDatetime { get; set; }
}
