namespace Material.BLL.Exceptions;

public class WrongLoginOrPasswordException(string message) : CustomException(message);