namespace canuckacs.Models;

public class Tag
{
    public long Id { get; set; } // The Id property functions as the unique key in a relational database.
    public long UserId  { get; set; }
    public long TagParentId { get; set; }
    public string? Name { get; set; }
} // end class Tag