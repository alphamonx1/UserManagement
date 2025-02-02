namespace UserService.Application.DTOs
{
    public class UserInformationDTO
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public string FullName { get; set; }

    }
}
