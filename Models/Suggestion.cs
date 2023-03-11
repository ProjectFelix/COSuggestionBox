using System;
using System.Collections.Generic;

namespace COSuggestionBox.Models;

public partial class Suggestion
{
    public long Id { get; set; }

    public string Comment { get; set; } = null!;

    public int UserId { get; set; }

    public DateTime DateCreated { get; set; }

    public int Status { get; set; }

    public DateTime? DateUpdated { get; set; }

    public DateTime? DateFinished { get; set; }
}
