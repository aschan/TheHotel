namespace OnlineBookings
{
    public interface IGuest
    {
        string FirstName { get; set; }
        
        string LastName { get; set; }
        
        string Title { get; set; }
    }
}
