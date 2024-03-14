using System;
using System.Collections.Generic;

namespace blocv1.Models;

public partial class Post
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Body { get; set; } = null!;

    public string? Thumbnail { get; set; }

    public DateTime DateTime { get; set; }

    public int? CategoryId { get; set; }

    public int AuthorId { get; set; }

    public bool? IsFeatured { get; set; }
}
