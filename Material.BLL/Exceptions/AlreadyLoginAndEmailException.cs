namespace Material.BLL.Exceptions;

public class AlreadyLoginAndEmailException(string message) : CustomException(message);