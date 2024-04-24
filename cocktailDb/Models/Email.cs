namespace cocktailDb.Models;

public class Email
{
    [Key]
    public int Id { get; set; }
    public string EmailAdress { get; set; }
    public int EmailsSent { get; set; }
}