namespace Material.BLL.Exceptions;

public class UserNotFoundException(string message) : CustomException(message);